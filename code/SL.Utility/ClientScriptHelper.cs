using System;
using System.Text;
using System.Web.UI;

namespace SL.Utility
{
    /// <summary>
    /// �ͻ��˽ű�
    /// </summary>
    public class ClientScriptHelper
    {
        Page _page = null;
        public ClientScriptHelper(Page page)
        {
            this._page = page;
        }

        #region [ RegisterClientScriptBlock ]

        /// <summary>
        /// ʹ�ü��ͽű��ı��� Page ����ע��ͻ��˽ű�
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="script">�ű�</param>
        public void RegisterClientScriptBlock(string key, string script)
        {
            this.RegisterClientScriptBlock(typeof(ClientScriptHelper), key, script, false);
        }

        /// <summary>
        /// ʹ�ü��ͽű��ı��� Page ����ע��ͻ��˽ű�
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="script">�ű�</param>
        /// <param name="addScriptTags">ָʾ�Ƿ���ӽű���ǵĲ���ֵ</param>
        public void RegisterClientScriptBlock(string key, string script, bool addScriptTags)
        {
            this.RegisterClientScriptBlock(typeof(ClientScriptHelper), key, script, addScriptTags);
        }

        /// <summary>
        /// ʹ�����͡����ͽű��ı��� Page ����ע��ͻ��˽ű�
        /// </summary>
        /// <param name="type">����</param>
        /// <param name="key">��</param>
        /// <param name="script">�ű�</param>
        public void RegisterClientScriptBlock(Type type, string key, string script)
        {
            this.RegisterClientScriptBlock(type, key, script, false);
        }

        /// <summary>
        /// ʹ�����͡����ͽű��ı��� Page ����ע��ͻ��˽ű�
        /// </summary>
        /// <param name="type">����</param>
        /// <param name="key">��</param>
        /// <param name="script">�ű�</param>
        /// <param name="addScriptTags">ָʾ�Ƿ���ӽű���ǵĲ���ֵ</param>
        public void RegisterClientScriptBlock(Type type, string key, string script, bool addScriptTags)
        {
            if (_page.ClientScript.IsClientScriptBlockRegistered(type, key) == false)
            {
                _page.ClientScript.RegisterClientScriptBlock(type, key, script, addScriptTags);
            }
        }

        #endregion

        #region [ AlertMessage ]

        /// <summary>
        ///�ڿͻ�����ʾһ����Ϣ
        /// </summary>
        /// <param name="message">��ʾ����Ϣ</param>
        public void AlertMessage(string message)
        {
            this.RegisterClientScriptBlock("AlertMessage", "<script language='javascript'>alert(\"" + message.Replace("'", "��") + "\");</script>");
        }

        public void AlertMessage()
        {
            AlertMessage("����ɹ���");
        }
        /// <summary>
        /// �ڿͻ�����ʾһ����ʾ��Ϣ
        /// </summary>
        /// <param name="message"></param>
        public void Alert(string message)
        {
            _page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + message + "');</script>");
        }
        /// <summary>
        /// �ڿͻ�����ʾһ����Ϣ������ʾ���������� 
        /// </summary>
        /// <param name="field">����������</param>
        /// <param name="message">��Ϣ</param>
        //public void AlertMessage(string field, string message)
        //{
        //    this.RegisterClientScriptBlock("AlertMessage_" + field, "<script language='javascript'>alert(\"" + message.Replace("'", "��") + "\");displayErrField('" + field + "');</script>");
        //}

        #endregion

        /// <summary>
        /// �ٿͻ�����ʾһ����Ϣ��Ȼ��ת��һ��ҳ��
        /// </summary>
        /// <param name="message">��ʾ����Ϣ</param>
        /// <param name="url">ת��ҳ��·��</param>
        public void AlertRedirect(string message, string url)
        {
            this.RegisterClientScriptBlock("AlertRedirect", "<script language='javascript'>alert(\"" + message.Replace("'", "��") + "\");window.navigate('" + url + "');</script>");
        }

        /// <summary>
        /// ת��ָ����ҳ��
        /// </summary>
        /// <param name="url">ת��ҳ��·��</param>
        public void Redirect(string url)
        {
            this.RegisterClientScriptBlock("Redirect", "<script language='javascript'>window.navigate('" + url + "');</script>");
        }

        /// <summary>
        /// �ٿͻ�����ʾһ����Ϣ��Ȼ��ر�ҳ��
        /// </summary>
        /// <param name="message"></param>
        public void AlertClose(string message)
        {
            this.RegisterClientScriptBlock("AlertClose", "<script language='javascript'>alert(\"" + message.Replace("'", "��") + "\");window.close();</script>");
        }

        /// <summary>
        /// �ڿͻ�����ʾһ����Ϣ��Ȼ�󷵻���һ��ҳ��
        /// </summary>
        /// <param name="message"></param>
        public void AlertReflashFrame(string message, string frame)
        {
            AlertReflashFrame(message, frame, false);
        }

        /// <summary>
        /// �ڿͻ�����ʾһ����Ϣ��Ȼ�󷵻���һ��ҳ��
        /// </summary>
        /// <param name="message"></param>
        public void AlertReflashFrame(string message, string frame, bool isOpener)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language='javascript'>");
            sb.Append("alert('" + message.Replace("'", "��") + "');");
            if (isOpener)
            {
                sb.Append("window.opener.parent.frames['" + frame + "'].location.reload();");
                sb.Append("if(typeof(window.opener) == 'undefined') { window.location.href=window.location.href;} else { window.opener.location.reload();}");
                sb.Append("window.close();");
            }
            else
            {
                sb.Append("window.parent.frames['" + frame + "'].location.reload();");
            }
            sb.Append("</script>");

            this.RegisterClientScriptBlock("AlertReflashFrame_" + frame, sb.ToString());
        }

        /// <summary>
        ///  �ڿͻ�����ʾһ����Ϣ��Ȼ���ҳ��ˢ��һ��
        /// </summary>
        /// <param name="message"></param>
        public void AlertReLoad(string message)
        {
            this.RegisterClientScriptBlock("AlertReLoad", "<script language='javascript'>alert('"
                + message.Replace("'", "��") + "');window.location.href=window.location.href;</script>");
        }

        /// <summary>
        /// ˢ�´����ҳ��ĸ�ҳ��
        /// </summary>
        /// <param name="message"></param>
        public void AlertReflashOpener(string message)
        {
            this.RegisterClientScriptBlock("AlertReflashOpener", "<script language='javascript'>alert('"
                + message.Replace("'", "��") + "');if(typeof(window.opener) == 'undefined') { window.location.href=window.location.href;} else { window.opener.location.reload();window.close();}</script>");
        }

        public void AlertBack(string message)
        {
            this.RegisterClientScriptBlock("AlertBack", "<script language='javascript'>alert('" + message.Replace("'", "��") + "');history.back();</script>");
        }

        public void ReflashFrame(string strFrames)
        {
            this.RegisterClientScriptBlock("ReflashFrame", "<script language='javascript'>" + strFrames + ".location.reload();</script>");
        }

        public void AlertConfirm(string message)
        {
            this.RegisterClientScriptBlock("AlertConfirm", "<script language='javascript'> if(confirm('" + message + "')) return true; else return false;</script>");
        }

        /// <summary>
        /// ʹҳ��������Ԫ�ش��ڽ���״̬
        /// </summary>
        public void DisableAllElement()
        {
            this.RegisterClientScriptBlock("DisableAllElement", "<script language='javascript'>DisableAllElement();</script>");
        }

        public void DisableAllInputElement()
        {
            this.RegisterClientScriptBlock("DisableAllInputElement", "<script language='javascript'>DisableAllInputElement();</script>");
        }

        public void HiddenClientElement(string name)
        {
            this.RegisterClientScriptBlock("HiddenClientElement_" + name, "<script language='javascript'>HiddenByName('" + name + "');</script>");
        }

        public void DisableClientElement(string name)
        {
            this.RegisterClientScriptBlock("DisableClientElement_" + name, "<script language='javascript'>Form1." + name + ".disabled=true ;</script>");
        }

        //public void ShowDetailPanel(string partpath )
        //{
        //    string path = ConfigurationManager.AppSettings["basePath"];
        //    string fullPath = string.Format("{0}/{1}", path,partpath);
        //    string javascript = "window.open('" + fullPath + "','', 'captionbar=no,toolbar=no,scrollbars=no,width=920,height=800', '')";
        //    _page.ClientScript.RegisterClientScriptBlock(_page.GetType(),_page.GetType().FullName,javascript,true);       

        //}

        /// <summary>
        /// �رմ���
        /// </summary>
        public void CloseWindow()
        {
            this.RegisterClientScriptBlock("CloseWindow", "<script language='javascript'>window.close();</script>");
        }

        public string PageTitle
        {
            set
            {
                //string strSystemName = System.Configuration.ConfigurationManager.AppSettings["SystemName"];
                //this.RegisterClientScriptBlock("pageTitle", "<script language='javascript'>if(window.parent!=null)window.parent.document.title=\"" + strSystemName + "--��ǰ�û�-" + value + "\";</script>");
            }
        }
    }
}
