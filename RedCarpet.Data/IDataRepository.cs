﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedCarpet.Data
{
	public interface IDataRepository
	{
		Task<T> Find<T>(string id) where T : class;
		void Update<T>(T entity) where T : class;
	}
}