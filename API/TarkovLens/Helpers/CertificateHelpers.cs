using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TarkovLens.Helpers
{
    public static class CertificateHelpers
    {
        /// <summary>
        /// Creates a certificate to be used with RavenDb.
        /// </summary>
        /// <param name="crt">The contents of the .crt file that RavenDb provides.</param>
        /// <param name="key">The contents of the .key file that RavenDb provides.</param>
        /// <returns></returns>
        public static X509Certificate2 CreateRavenCertificate(string crt, string key)
        {
            byte[] certificateBytes = Convert.FromBase64String(crt);
            var certificate = new X509Certificate2(certificateBytes);
            byte[] privateKey = Convert.FromBase64String(key);

            using var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);
            certificate = certificate.CopyWithPrivateKey(rsa);
            certificate = new X509Certificate2(certificate.Export(X509ContentType.Pkcs12));

            return certificate;
        }
    }
}
