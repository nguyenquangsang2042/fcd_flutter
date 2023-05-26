using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Crypto.Generators;


using Org.BouncyCastle.OpenSsl;
using System.IO;
using System.Security.Cryptography;

using Newtonsoft.Json.Converters;

namespace VNASchedule.Class
{
    public class Crypter
    {
        private RsaKeyParameters rsaKeyParameters;

        public Crypter(string key, bool isPublicKey = true)
        {

            if (isPublicKey)
            {
                var keyInfoData = Convert.FromBase64String(key);
                rsaKeyParameters = PublicKeyFactory.CreateKey(keyInfoData) as RsaKeyParameters;
            }
            else
            {
                rsaKeyParameters = (RsaKeyParameters)readPrivateKey(key);
            }
        }

        public string Encrypt(object obj)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new TickDateTimeConverter() }
            };

            var serialized = JsonConvert.SerializeObject(obj, settings);
            var payloadBytes = Encoding.UTF8.GetBytes(serialized);

            var cipher = GetAsymmetricBlockCipher(true);
            var encrypted = Process(cipher, payloadBytes);

            var encoded = Convert.ToBase64String(encrypted);
            return encoded;
        }

        public string EncryptStr(string obj)
        {
            var payloadBytes = Encoding.UTF8.GetBytes(obj);

            var cipher = GetAsymmetricBlockCipher(true);
            var encrypted = Process(cipher, payloadBytes);

            var encoded = Convert.ToBase64String(encrypted);
            return encoded;
        }

        public T Decrypt<T>(string encryptedText)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new TickDateTimeConverter() }
            };
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            var cipher = GetAsymmetricBlockCipher(false);
            var decrypted = Process(cipher, cipherTextBytes);
            var decoded = Encoding.UTF8.GetString(decrypted);
            return JsonConvert.DeserializeObject<T>(decoded, settings);
        }

        public string Decrypt(string encryptedText)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new TickDateTimeConverter() }
            };


            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

            var cipher = GetAsymmetricBlockCipher(false);
            var decrypted = Process(cipher, cipherTextBytes);

            var decoded = Encoding.UTF8.GetString(decrypted);
            return decoded;
        }

        public string TryDecrypt(string encryptedText)
        {
            string retValue = "";
            try
            {
                retValue = Decrypt(encryptedText);
            }
            catch { }
            return retValue;
        }
        public string TestDecrypt(string privateKey, string strData)
        {
            string retValue = "";

            AsymmetricKeyParameter key = readPrivateKey(privateKey);
            rsaKeyParameters = (RsaKeyParameters)key;
            var cipher = GetAsymmetricBlockCipher(false);
            var data = Convert.FromBase64String(strData);
            byte[] decipheredBytes = Process(cipher, data);
            retValue = Encoding.UTF8.GetString(decipheredBytes);
            return retValue;
        }

        private IAsymmetricBlockCipher GetAsymmetricBlockCipher(bool forEncryption)
        {
            var cipher = new Pkcs1Encoding(new RsaEngine());
            cipher.Init(forEncryption, rsaKeyParameters);
            return cipher;
        }

        private byte[] Process(IAsymmetricBlockCipher cipher, byte[] payloadBytes)
        {
            int length = payloadBytes.Length;
            int blockSize = cipher.GetInputBlockSize();

            var plainTextBytes = new List<byte>();
            for (int chunkPosition = 0; chunkPosition < length; chunkPosition += blockSize)
            {
                //int chunkSize = Math.Min(blockSize, length - chunkPosition);
                //plainTextBytes.AddRange(cipher.ProcessBlock(
                //    payloadBytes, chunkPosition, chunkSize
                //));

                int chunkSize = Math.Min(blockSize, length - chunkPosition);
                byte[] bockGet = cipher.ProcessBlock(payloadBytes, chunkPosition, chunkSize);
                plainTextBytes.AddRange(bockGet);

            }

            return plainTextBytes.ToArray();
        }





        public static KeyValuePair<string, string> getKeyPair()
        {
            RsaKeyPairGenerator rsaKeyPairGnr = new RsaKeyPairGenerator();
            rsaKeyPairGnr.Init(new Org.BouncyCastle.Crypto.KeyGenerationParameters(new SecureRandom(), 512));
            Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair keyPair = rsaKeyPairGnr.GenerateKeyPair();


            RSAParameters rsaPrivaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)keyPair.Private);
            RSACryptoServiceProvider cspPrivateKey = new RSACryptoServiceProvider();
            cspPrivateKey.ImportParameters(rsaPrivaParams);
            string strPrivateKey = ExportPrivateKey(cspPrivateKey);


            RSAParameters rsaPublicParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)keyPair.Public);
            RSACryptoServiceProvider cspPublicKey = new RSACryptoServiceProvider();
            cspPublicKey.ImportParameters(rsaPublicParams);
            string strPublicKey = ExportPublicKey(cspPublicKey);


            //return new KeyValuePair<string, string>(keyPair.Private.ToString(), keyPair.Public.ToString());
            return new KeyValuePair<string, string>(strPrivateKey, strPublicKey);
        }


        public static RSACryptoServiceProvider ImportPrivateKey(string pem)
        {
            PemReader pr = new PemReader(new StringReader(pem));
            AsymmetricCipherKeyPair KeyPair = (AsymmetricCipherKeyPair)pr.ReadObject();
            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)KeyPair.Private);

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();// cspParams);
            csp.ImportParameters(rsaParams);
            return csp;
        }

        public static AsymmetricKeyParameter readPrivateKey(string pemData)
        {

            PemReader pr = new PemReader(new StringReader(pemData));
            AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pr.ReadObject();

            return keyPair.Private;
        }

        public static string ExportPrivateKey(RSACryptoServiceProvider csp)
        {

            //string retValue = "";
            //if (csp.PublicOnly) throw new ArgumentException("CSP does not contain a private key", "csp");
            //var parameters = csp.ExportParameters(true);
            //using (var stream = new MemoryStream())
            //{
            //    var writer = new BinaryWriter(stream);
            //    writer.Write((byte)0x30); // SEQUENCE
            //    using (var innerStream = new MemoryStream())
            //    {
            //        var innerWriter = new BinaryWriter(innerStream);
            //        EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
            //        EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
            //        EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
            //        EncodeIntegerBigEndian(innerWriter, parameters.D);
            //        EncodeIntegerBigEndian(innerWriter, parameters.P);
            //        EncodeIntegerBigEndian(innerWriter, parameters.Q);
            //        EncodeIntegerBigEndian(innerWriter, parameters.DP);
            //        EncodeIntegerBigEndian(innerWriter, parameters.DQ);
            //        EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);
            //        var length = (int)innerStream.Length;
            //        EncodeLength(writer, length);
            //        writer.Write(innerStream.GetBuffer(), 0, length);
            //    }

            //    retValue = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);


            //}

            //return retValue;

            StringWriter outputStream = new StringWriter();
            if (csp.PublicOnly) throw new ArgumentException("CSP does not contain a private key", "csp");
            var parameters = csp.ExportParameters(true);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    EncodeIntegerBigEndian(innerWriter, new byte[] { 0x00 }); // Version
                    EncodeIntegerBigEndian(innerWriter, parameters.Modulus);
                    EncodeIntegerBigEndian(innerWriter, parameters.Exponent);
                    EncodeIntegerBigEndian(innerWriter, parameters.D);
                    EncodeIntegerBigEndian(innerWriter, parameters.P);
                    EncodeIntegerBigEndian(innerWriter, parameters.Q);
                    EncodeIntegerBigEndian(innerWriter, parameters.DP);
                    EncodeIntegerBigEndian(innerWriter, parameters.DQ);
                    EncodeIntegerBigEndian(innerWriter, parameters.InverseQ);
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                // WriteLine terminates with \r\n, we want only \n
                outputStream.Write("-----BEGIN RSA PRIVATE KEY-----\n");
                // Output as Base64 with lines chopped at 64 characters
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.Write(base64, i, Math.Min(64, base64.Length - i));
                    outputStream.Write("\n");
                }
                outputStream.Write("-----END RSA PRIVATE KEY-----");
            }

            return outputStream.ToString();
        }


        public static string ExportPublicKey(RSACryptoServiceProvider csp)
        {
            //string retValue = "";
            //var parameters = csp.ExportParameters(false);
            //using (var stream = new MemoryStream())
            //{
            //    var writer = new BinaryWriter(stream);
            //    writer.Write((byte)0x30); // SEQUENCE
            //    using (var innerStream = new MemoryStream())
            //    {
            //        var innerWriter = new BinaryWriter(innerStream);
            //        innerWriter.Write((byte)0x30); // SEQUENCE
            //        EncodeLength(innerWriter, 13);
            //        innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
            //        var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
            //        EncodeLength(innerWriter, rsaEncryptionOid.Length);
            //        innerWriter.Write(rsaEncryptionOid);
            //        innerWriter.Write((byte)0x05); // NULL
            //        EncodeLength(innerWriter, 0);
            //        innerWriter.Write((byte)0x03); // BIT STRING
            //        using (var bitStringStream = new MemoryStream())
            //        {
            //            var bitStringWriter = new BinaryWriter(bitStringStream);
            //            bitStringWriter.Write((byte)0x00); // # of unused bits
            //            bitStringWriter.Write((byte)0x30); // SEQUENCE
            //            using (var paramsStream = new MemoryStream())
            //            {
            //                var paramsWriter = new BinaryWriter(paramsStream);
            //                EncodeIntegerBigEndian(paramsWriter, parameters.Modulus); // Modulus
            //                EncodeIntegerBigEndian(paramsWriter, parameters.Exponent); // Exponent
            //                var paramsLength = (int)paramsStream.Length;
            //                EncodeLength(bitStringWriter, paramsLength);
            //                bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
            //            }
            //            var bitStringLength = (int)bitStringStream.Length;
            //            EncodeLength(innerWriter, bitStringLength);
            //            innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
            //        }
            //        var length = (int)innerStream.Length;
            //        EncodeLength(writer, length);
            //        writer.Write(innerStream.GetBuffer(), 0, length);
            //    }

            //    retValue = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length);

            //}

            //return retValue;

            StringWriter outputStream = new StringWriter();
            var parameters = csp.ExportParameters(false);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    innerWriter.Write((byte)0x30); // SEQUENCE
                    EncodeLength(innerWriter, 13);
                    innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
                    var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
                    EncodeLength(innerWriter, rsaEncryptionOid.Length);
                    innerWriter.Write(rsaEncryptionOid);
                    innerWriter.Write((byte)0x05); // NULL
                    EncodeLength(innerWriter, 0);
                    innerWriter.Write((byte)0x03); // BIT STRING
                    using (var bitStringStream = new MemoryStream())
                    {
                        var bitStringWriter = new BinaryWriter(bitStringStream);
                        bitStringWriter.Write((byte)0x00); // # of unused bits
                        bitStringWriter.Write((byte)0x30); // SEQUENCE
                        using (var paramsStream = new MemoryStream())
                        {
                            var paramsWriter = new BinaryWriter(paramsStream);
                            EncodeIntegerBigEndian(paramsWriter, parameters.Modulus); // Modulus
                            EncodeIntegerBigEndian(paramsWriter, parameters.Exponent); // Exponent
                            var paramsLength = (int)paramsStream.Length;
                            EncodeLength(bitStringWriter, paramsLength);
                            bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
                        }
                        var bitStringLength = (int)bitStringStream.Length;
                        EncodeLength(innerWriter, bitStringLength);
                        innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
                    }
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                // WriteLine terminates with \r\n, we want only \n
                //outputStream.Write("-----BEGIN PUBLIC KEY-----\n");
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.Write(base64, i, Math.Min(64, base64.Length - i));
                    //outputStream.Write("\n");
                }
                //outputStream.Write("-----END PUBLIC KEY-----");
            }

            return outputStream.ToString();
        }



        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }


        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }

    }

    public class TickDateTimeConverter : DateTimeConverterBase
    {
        private static DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long tick = (long)reader.Value;

            return unixEpoch.AddMilliseconds(tick).ToLocalTime();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return base.CanConvert(objectType);
        }
    }
}
