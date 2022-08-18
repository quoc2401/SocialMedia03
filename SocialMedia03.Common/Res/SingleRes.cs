using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.Common.Res
{
    public class SingleRes : BaseRes
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>  
        public SingleRes() : base() { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="message">Message</param>
        public SingleRes(string message) : base(message) { }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="titleError">Title error</param>
        public SingleRes(string message, string titleError) : base(message, titleError) { }

        /// <summary>
        /// Set data
        /// </summary>
        /// <param name="code">Success code</param>
        /// <param name="data">Data</param>
        public void SetData(string code, object data)
        {
            Code = code;
            Data = data;
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Data
        /// </summary>
        public object Data { get; set; }

        #endregion
    }
}
