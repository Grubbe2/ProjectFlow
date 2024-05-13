using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = CharlotteSchou_Booking.Model.Attribute;

namespace CharlotteSchou_Booking.Viewmodels
{
    public class AttributeVM
    {
        private Attribute attribute;
        private string _characteristics;
        private string _service;
        private int _attributeId;

        public string Characteristics { get => _characteristics; set => _characteristics = value; }
        public string Service { get => _service; set => _service = value; }
        public int AttributeId { get => _attributeId; set => _attributeId = value; }

        public AttributeVM(Attribute attribute)
        {
            this.attribute = attribute;
            _characteristics = attribute.Characteristics;
            _service = attribute.Service;
            _attributeId = attribute.AttributeId;
        }
    }
}
