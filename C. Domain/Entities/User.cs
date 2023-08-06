using C._Domain.Enum;

namespace C._Domain.Entities
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string SystemName { get; set; }
        public int TableNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime DateOfDismissal { get; set; }
        public bool IsOnlyPlagins { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsWorker { get; set; }
        public bool IsGuest { get; set; }
        public bool IsTransactionsCounterpartyCard { get; set; }
        public string Division { get; set; }
        public string InitialAdjustmentTM { get; set; }
        public string Respondent { get; set; }
        public string Note { get; set; }
        public Gender Gender { get; set; }
        public UserType UserType { get; set; }
        public int CardId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
