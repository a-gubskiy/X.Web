using System.Web.UI;

namespace X.Web
{
    public abstract class XMasterPage : MasterPage
    {
        /// <summary>
        /// Current X Framework Page
        /// </summary>
        public XMasterPage XMaster
        {
            get
            {
                if (Master is XMasterPage)
                {
                    return Master as XMasterPage;
                }

                return null;
            }
        }

        /// <summary>
        /// Current X Framework Page
        /// </summary>
        public XPage XPage
        {
            get
            {
                if (base.Page is XPage)
                {
                    return base.Page as XPage;
                }

                return null;
            }
        }

        protected XRequest XRequest
        {
            get
            {
                return XPage != null ? XPage.XRequest : null;
            }
        }

        public virtual string PageTitle { get; set; }
    }
}
