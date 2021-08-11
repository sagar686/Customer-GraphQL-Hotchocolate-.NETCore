using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;
using HotChocolate.Types;

namespace CustomerGraphQLApi.Models
{
    public enum Status
    {
        Active,
        Inactive
    }

    public class Customer
    {        
        [Key]
        [GraphQLType(typeof(NonNullType<IdType>))]        
        public long Id { get; set; }        
        [MaxLength(128)]
        public string Email { get; set; }        
        [MaxLength(128)]
        public string Name { get; set; }        
        public int? Code { get; set; }        
        public Status Status { get; set; }        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }        
        public bool IsBlocked { get; set; }
    }
    public class AddCustomer
    {
        [MaxLength(128)]
        public string Email { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        public int? Code { get; set; }
        public Status Status { get; set; }
        public bool IsBlocked { get; set; }
    }
    public class DeleteCustomerById
    {
        public long Id { get; set; }
        
    }
}
