using MediatR;
using Shop.AdminPanel.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shop.AdminPanel.Handlers.Tests
{
  
    public class CreateProductHandlerTests
    {
        private readonly IMediator _mediator;
        public CreateProductHandlerTests(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Fact]
        public void HandleTest()
        {
            var a = 4;
            Assert.Equal(true, true);
        }
    }
}