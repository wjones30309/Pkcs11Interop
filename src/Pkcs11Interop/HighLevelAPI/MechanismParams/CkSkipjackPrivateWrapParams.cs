/*
 *  Pkcs11Interop - Managed .NET wrapper for unmanaged PKCS#11 libraries
 *  Copyright (c) 2012-2015 JWC s.r.o. <http://www.jwc.sk>
 *  Author: Jaroslav Imrich <jimrich@jimrich.sk>
 *
 *  Licensing for open source projects:
 *  Pkcs11Interop is available under the terms of the GNU Affero General 
 *  Public License version 3 as published by the Free Software Foundation.
 *  Please see <http://www.gnu.org/licenses/agpl-3.0.html> for more details.
 *
 *  Licensing for other types of projects:
 *  Pkcs11Interop is available under the terms of flexible commercial license.
 *  Please contact JWC s.r.o. at <info@pkcs11interop.net> for more details.
 */

using System;
using Net.Pkcs11Interop.Common;

namespace Net.Pkcs11Interop.HighLevelAPI.MechanismParams
{
    /// <summary>
    /// Parameters for the CKM_SKIPJACK_PRIVATE_WRAP mechanism
    /// </summary>
    public class CkSkipjackPrivateWrapParams : IMechanismParams, IDisposable
    {
        /// <summary>
        /// Flag indicating whether instance has been disposed
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Platform specific CkSkipjackPrivateWrapParams
        /// </summary>
        private HighLevelAPI4.MechanismParams.CkSkipjackPrivateWrapParams _params4 = null;

        /// <summary>
        /// Platform specific CkSkipjackPrivateWrapParams
        /// </summary>
        private HighLevelAPI8.MechanismParams.CkSkipjackPrivateWrapParams _params8 = null;
        
        /// <summary>
        /// Initializes a new instance of the CkSkipjackPrivateWrapParams class.
        /// </summary>
        /// <param name='password'>User-supplied password</param>
        /// <param name='publicData'>Other party's key exchange public key value</param>
        /// <param name='randomA'>Ra data</param>
        /// <param name='primeP'>Prime, p, value</param>
        /// <param name='baseG'>Base, g, value</param>
        /// <param name='subprimeQ'>Subprime, q, value</param>
        public CkSkipjackPrivateWrapParams(byte[] password, byte[] publicData, byte[] randomA, byte[] primeP, byte[] baseG, byte[] subprimeQ)
        {
            if (UnmanagedLong.Size == 4)
                _params4 = new HighLevelAPI4.MechanismParams.CkSkipjackPrivateWrapParams(password, publicData, randomA, primeP, baseG, subprimeQ);
            else
                _params8 = new HighLevelAPI8.MechanismParams.CkSkipjackPrivateWrapParams(password, publicData, randomA, primeP, baseG, subprimeQ);
        }
        
        #region IMechanismParams

        /// <summary>
        /// Returns managed object that can be marshaled to an unmanaged block of memory
        /// </summary>
        /// <returns>A managed object holding the data to be marshaled. This object must be an instance of a formatted class.</returns>
        public object ToMarshalableStructure()
        {
            if (this._disposed)
                throw new ObjectDisposedException(this.GetType().FullName);

            if (UnmanagedLong.Size == 4)
                return _params4.ToMarshalableStructure();
            else
                return _params8.ToMarshalableStructure();
        }
        
        #endregion
        
        #region IDisposable
        
        /// <summary>
        /// Disposes object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// Disposes object
        /// </summary>
        /// <param name="disposing">Flag indicating whether managed resources should be disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Dispose managed objects
                    if (_params4 != null)
                    {
                        _params4.Dispose();
                        _params4 = null;
                    }

                    if (_params8 != null)
                    {
                        _params8.Dispose();
                        _params8 = null;
                    }
                }
                
                // Dispose unmanaged objects

                _disposed = true;
            }
        }
        
        /// <summary>
        /// Class destructor that disposes object if caller forgot to do so
        /// </summary>
        ~CkSkipjackPrivateWrapParams()
        {
            Dispose(false);
        }
        
        #endregion
    }
}
