using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedCarpet.Data
{
	public class SampleClass
	{
		[Key]
		public int MyProperty { get; set; }
		public int MyProperty2 { get; set; }

	}
}
