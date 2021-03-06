/* 
 * for KMD HTTP API
 *
 * API for KMD (Key Management Daemon)
 *
 * OpenAPI spec version: 0.0.1
 * Contact: contact@algorand.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = Algorand.Client.SwaggerDateConverter;

namespace Algorand.Kmd.Model
{
    /// <summary>
    /// APIV1WalletHandle includes the wallet the handle corresponds to and the number of number of seconds to expiration
    /// </summary>
    [DataContract]
        public partial class APIV1WalletHandle :  IEquatable<APIV1WalletHandle>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APIV1WalletHandle" /> class.
        /// </summary>
        /// <param name="expiresSeconds">expiresSeconds.</param>
        /// <param name="wallet">wallet.</param>
        public APIV1WalletHandle(long? expiresSeconds = default(long?), APIV1Wallet wallet = default(APIV1Wallet))
        {
            this.ExpiresSeconds = expiresSeconds;
            this.Wallet = wallet;
        }
        
        /// <summary>
        /// Gets or Sets ExpiresSeconds
        /// </summary>
        [DataMember(Name="expires_seconds", EmitDefaultValue=false)]
        public long? ExpiresSeconds { get; set; }

        /// <summary>
        /// Gets or Sets Wallet
        /// </summary>
        [DataMember(Name="wallet", EmitDefaultValue=false)]
        public APIV1Wallet Wallet { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class APIV1WalletHandle {\n");
            sb.Append("  ExpiresSeconds: ").Append(ExpiresSeconds).Append("\n");
            sb.Append("  Wallet: ").Append(Wallet).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as APIV1WalletHandle);
        }

        /// <summary>
        /// Returns true if APIV1WalletHandle instances are equal
        /// </summary>
        /// <param name="input">Instance of APIV1WalletHandle to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(APIV1WalletHandle input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ExpiresSeconds == input.ExpiresSeconds ||
                    (this.ExpiresSeconds != null &&
                    this.ExpiresSeconds.Equals(input.ExpiresSeconds))
                ) && 
                (
                    this.Wallet == input.Wallet ||
                    (this.Wallet != null &&
                    this.Wallet.Equals(input.Wallet))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.ExpiresSeconds != null)
                    hashCode = hashCode * 59 + this.ExpiresSeconds.GetHashCode();
                if (this.Wallet != null)
                    hashCode = hashCode * 59 + this.Wallet.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
