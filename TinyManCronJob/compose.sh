ver=1.0.2
echo "docker build -t \"scholtz2/cron-tm:$ver-stable\" -f Dockerfile .."
docker build -t "scholtz2/cron-tm:$ver-stable" -f Dockerfile .. --no-cache
docker push "scholtz2/cron-tm:$ver-stable"
echo "Image: scholtz2/cron-tm:$ver-stable"