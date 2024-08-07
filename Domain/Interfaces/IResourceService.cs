﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface IResourceService
	{
		Task<List<ItemOption>> GetOptions(string text, CancellationToken cancellationToken);
	}
}
