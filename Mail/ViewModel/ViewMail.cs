using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.ViewModel
{
    /// <summary>
    /// Модель входящих данных
    /// </summary>
    public class ViewMail
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipient { get; set; }
    }
}
