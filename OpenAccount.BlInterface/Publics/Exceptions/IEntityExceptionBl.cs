﻿using OpenAccount.BlInterface.Infrastructure;
using OpenAccount.Entities.Publics.Exceptions;

namespace OpenAccount.BlInterface.Publics.Exceptions
{
	public interface IEntityExceptionBl : IBaseLogic<EntityException, Guid>
	{
	}
}