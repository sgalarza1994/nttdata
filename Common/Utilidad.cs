using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NttDataApi.Common
{
    public static class Utilidad
    {
        public static string CrearCuenta(int cuentaId)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + cuentaId;
        }
        public static string EstadoVisualizar(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return "";
            if (estado.Equals("C"))
            {
                return "ACTIVO";
            }
            else if (estado.Equals("E"))
                return "ELIMINADO";
            else
                return "BLOQUEADO";
        }
        public static int ObtenerEdad(string fecha)
        {
            if (string.IsNullOrWhiteSpace(fecha))
                return 0;
            DateTime fechaNacimiento = Convert.ToDateTime(fecha);

            return DateTime.Today.AddTicks(-fechaNacimiento.Ticks).Year - 1;
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public static bool ConfirmationPassword(string password,
           byte[] salt, string passwordHash)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            if (hashed == passwordHash)
            {
                return true;
            }
            return false;

        }

        public static Tuple<byte[], string> Generatehashed(string password)
        {
            var salt = GenerateSalt();
            Tuple<byte[], string> response;
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(

                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            response = new Tuple<byte[], string>(salt, hashed);
            return response;

        }

        public static string GenerateToken(string key, string issuer, string audience, List<Claim> claims)
        {
            //creamos el header
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _header = new JwtHeader(_signingCredentials);

            //creara el payload
            var _payLoad = new JwtPayload(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(30));

            var _token = new JwtSecurityToken(
                _header,
                _payLoad);
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }
    }
}
