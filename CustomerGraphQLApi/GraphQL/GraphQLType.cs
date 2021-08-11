using CustomerGraphQLApi.Models;
using HotChocolate.Types;

namespace CustomerGraphQLApi.GraphQL
{
    public class CustomerType : ObjectType<Customer>
    {

    }
    public class CustomerInputType : InputObjectType<AddCustomer>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddCustomer> descriptor)
        {
            descriptor.Name("AddCustomer");
            descriptor.Field(p => p.Email).Type<StringType>().Name("Email");
            descriptor.Field(p => p.Name).Type<StringType>().Name("Name");
            descriptor.Field(p => p.Code).Type<IntType>().Name("Code");
            descriptor.Field(p => p.Status).Type<EnumType<Status>>().Name("Status");
            descriptor.Field(p => p.IsBlocked).Type<BooleanType>().Name("IsBlocked");
        }
    }
    public class CustomerDeleteByIdType : InputObjectType<DeleteCustomerById> 
    {
        protected override void Configure(IInputObjectTypeDescriptor<DeleteCustomerById> descriptor)
        {
            descriptor.Name("DeleteCustomerById");
            descriptor.Field(p => p.Id).Type<LongType>().Name("Id");            
        }
    }
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor
                .Field(p => p.Create(default))
                .Type<CustomerType>()
                .Name("create")
                .Argument("input", a => a.Type<CustomerInputType>());
            descriptor
                .Field(p => p.DeleteById(default))
                .Type<BooleanType>()
                .Name("deletedbyid")
                .Argument("input", a => a.Type<CustomerDeleteByIdType>());
        }
    }
}
