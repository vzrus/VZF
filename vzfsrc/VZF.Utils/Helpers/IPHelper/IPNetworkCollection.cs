#region copyright
/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 *
 * http://www.yetanotherforum.net/
 *
 * This file can contain some changes in 2014-2016 by Vladimir Zakharov(vzrus)
 * for VZF forum
 *
 * http://www.code.coolhobby.ru/
 * 
 * File IPNetworkCollection.cs created  on 21.11.2015 in  2:13 AM.
 * Last changed on 5.21.2016 in 12:59 PM.
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
#endregion

using System.Collections.Generic;
using System.Collections;
using System.Numerics;

namespace System.Net {
    public class IPNetworkCollection : IEnumerable<IPNetwork>, IEnumerator<IPNetwork> {

        private BigInteger _enumerator;
        private byte _cidrSubnet;
        private IPNetwork _ipnetwork;

        private byte _cidr {
            get { return this._ipnetwork.Cidr; }
        }
        private BigInteger _broadcast {
            get { return IPNetwork.ToBigInteger(this._ipnetwork.Broadcast); }
        }
        private BigInteger _lastUsable {
            get { return IPNetwork.ToBigInteger(this._ipnetwork.LastUsable); }
        }
        private BigInteger _network {
            get { return IPNetwork.ToBigInteger(this._ipnetwork.Network); }
        }

        internal IPNetworkCollection(IPNetwork ipnetwork, byte cidrSubnet) {

            int maxCidr = ipnetwork.AddressFamily == Sockets.AddressFamily.InterNetwork ? 32 : 128;
            if (cidrSubnet > maxCidr) {
                throw new ArgumentOutOfRangeException("cidrSubnet");
            }

            if (cidrSubnet < ipnetwork.Cidr) {
                throw new ArgumentException("cidr");
            }

            this._cidrSubnet = cidrSubnet;
            this._ipnetwork = ipnetwork;
            this._enumerator = -1;
        }

        #region Count, Array, Enumerator

        public BigInteger Count
        {
            get
            {
                BigInteger count = BigInteger.Pow(2, this._cidrSubnet - this._cidr);
                return count; 
            }
        }

        public IPNetwork this[BigInteger i] {
            get
            {
                if (i >= this.Count)
                {
                    throw new ArgumentOutOfRangeException("i");
                }

                BigInteger last = this._ipnetwork.AddressFamily == Sockets.AddressFamily.InterNetworkV6
                    ? this._lastUsable : this._broadcast;
                BigInteger increment = (last - this._network) / this.Count;
                BigInteger uintNetwork = this._network + ((increment + 1) * i);
                IPNetwork ipn = new IPNetwork(uintNetwork, this._ipnetwork.AddressFamily, this._cidrSubnet);
                return ipn;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator<IPNetwork> IEnumerable<IPNetwork>.GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #region IEnumerator<IPNetwork> Members

        public IPNetwork Current
        {
            get { return this[this._enumerator]; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // nothing to dispose
            return;
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public bool MoveNext()
        {
            this._enumerator++;
            if (this._enumerator >= this.Count)
            {
                return false;
            }
            return true;

        }

        public void Reset()
        {
            this._enumerator = -1;
        }

        #endregion

        #endregion

    }
}
