using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAF.Providers
{
    public static class MySqlHelpers
    {
        /// <summary>
        /// The guid converter.
        /// </summary>
        /// <param name="gd">
        /// The gd.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/>.
        /// </returns>
        public static Guid GuidConverter(Guid gd)
        {
            var barrIn = gd.ToByteArray();
            var barrOut = new byte[16];

            barrOut[0] = barrIn[3];
            barrOut[1] = barrIn[2];
            barrOut[2] = barrIn[1];
            barrOut[3] = barrIn[0];
            barrOut[4] = barrIn[5];
            barrOut[5] = barrIn[4];

            barrOut[6] = barrIn[7];
            barrOut[7] = barrIn[6];

            for (var i = 8; i < 16; i++)
            {
                barrOut[i] = barrIn[i];
            }

            return new Guid(barrOut);
        }
    }
}
