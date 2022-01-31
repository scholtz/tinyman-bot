// See https://aka.ms/new-console-template for more information

using Tinyman.V1.Model;
using TinyManCronJob;

Console.WriteLine($"");
var configData = File.ReadAllText("appsettings.json");
var config = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(configData);
if (config == null || string.IsNullOrEmpty(config.ApiHost))
{
    Console.Error.WriteLine("Invalid configuration");
    throw new Exception("Fatal.. please make sure you have correct appsettings");
}
var myAccount = new Algorand.Account(config.MyAccountMnemonic);
var rand = new Random();
var client = new Tinyman.V1.TinymanMainnetClient(config.ApiHost, config.ApiKey);

var assets = new Dictionary<ulong, Asset>();
var assetSell = config.BaseAsset ?? 452399768;
if (!assets.ContainsKey(assetSell))
{
    assets[assetSell] = await client.FetchAssetAsync(assetSell);
}

if (!config.OtherAssets.Any())
{
    config.OtherAssets.Add(0);
    config.OtherAssets.Add(31566704);
}

foreach (var item in config.OtherAssets)
{
    assets[item] = await client.FetchAssetAsync(item);
}

var sellTinyManAsset = assets[assetSell];



while (true)
{
    try
    {
        var sleep = rand.Next(config.DelayFrom, config.DelayTo);
        Console.WriteLine($"Waiting {sleep} seconds {DateTimeOffset.Now.AddSeconds(sleep).ToString("R")}");
        await Task.Delay(sleep * 1000);
        var pools = new Dictionary<ulong, Pool>();
        var quotes = new Dictionary<ulong, SwapQuote>();
        var actions = new Dictionary<ulong, Tinyman.V1.Action.Swap>();

        int toSellVolume = 0;
        if (config.ToSell.HasValue) toSellVolume = config.ToSell.Value;
        if (config.ToTradeQFrom.HasValue && config.ToTradeQTo.HasValue)
        {
            toSellVolume = rand.Next(config.ToTradeQFrom.Value, config.ToTradeQTo.Value);
        }
        if (toSellVolume <= 0) throw new Exception("Invalid sell volume");
        ulong multiplier = config.ToTradeQMultiplier ?? 1000000;
        ulong toSellFullAmount = multiplier * Convert.ToUInt64(toSellVolume);
        foreach (var toBuy in config.OtherAssets)
        {
            pools[toBuy] = await client.FetchPoolAsync(assets[assetSell], assets[toBuy]);
            if (config.TradeType == 1)
            {
                quotes[toBuy] = pools[toBuy].CalculateFixedOutputSwapQuote(new Tinyman.V1.Model.AssetAmount(assets[assetSell], toSellFullAmount), 0.05);
            }
            else
            {
                var quote = pools[toBuy].CalculateFixedOutputSwapQuote(new Tinyman.V1.Model.AssetAmount(assets[assetSell], toSellFullAmount), 0.05);
                quotes[toBuy] = pools[toBuy].CalculateFixedInputSwapQuote(new Tinyman.V1.Model.AssetAmount(assets[toBuy], quote.AmountIn.Amount), 0.05);
            }
            actions[toBuy] = Tinyman.V1.Action.Swap.FromQuote(quotes[toBuy]);
        }

        var tasks = new List<Task<Algorand.V2.Algod.Model.PostTransactionsResponse?>>();
        foreach (var toBuy in config.OtherAssets)
        {
            tasks.Add(Task.Run(() =>
            {
                try
                {
                    return client.SwapAsync(myAccount, actions[toBuy], true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }));
        }
        var continuation = Task.WhenAll(tasks.ToArray());
        try
        {
            continuation.Wait();
        }
        catch (AggregateException ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
        if (continuation.Status == TaskStatus.RanToCompletion)
        {
            foreach (var result in continuation.Result)
            {
                Console.WriteLine($"TxId: {result?.TxId}");
            }
        }
        else
        {
            Console.Error.WriteLine($"continuation.Status == {continuation.Status}");
        }
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.Message);
        await Task.Delay(1000000);
    }
    await Task.Delay(config.DelayAfter * 1000);
}