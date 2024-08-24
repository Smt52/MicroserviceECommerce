﻿using CommonOperations.Exceptions;

namespace Products.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product",Id)
        {
        }
    }
}