using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace Mane.CFDI
{
    public class XmlObject :  IFormattable, INotifyPropertyChanged
    {
        protected XmlNamespaceManager nsmgr;
        protected XmlNode node;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyChange([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        protected void SetAttributes()
        {
            if (node == null)
            {
                construct();
                return;
            }
            try
            {
                var props = GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.CanWrite)
                    {
                        prop.SetValue(this, ConvertirATipo(prop.PropertyType, GetAttribute(prop.Name)));
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetAttribute(string attributeName)
        {
            if (node == null) return "";
            try
            {
                return node.Attributes[attributeName].Value;
            }
            catch (Exception)
            {

            }
            return "";
        }
        public XmlNode GetNode(string nodeName)
        {
            if (node == null) return null;
            try
            {
                foreach (XmlNode item in node.ChildNodes)
                {
                    if (item.Name == nodeName) return item;
                }
            }
            catch (Exception)
            {


            }
            return null;
        }
        protected virtual void construct()
        {

        }
        public virtual dynamic ConvertirATipo(Type t, string value)
        { 
                switch (t.Name)
                {
                    case "String": return string.IsNullOrEmpty(value) ? "" : value.ToString();
                    case "DateTime": return string.IsNullOrEmpty(value) ? DateTime.MinValue : DateTime.Parse(value.ToString());
                    case "Double": return string.IsNullOrEmpty(value) ? 0 : double.Parse(value.ToString());
                    case "Int32": return string.IsNullOrEmpty(value) ? 0 : int.Parse(value.ToString());
                    default: return null;
                }           
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return GetType().Name;
        }
    }
}
