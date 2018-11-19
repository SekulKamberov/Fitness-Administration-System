namespace FAS.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum MembershipType
    {
        Home = 0,
        Country = 1,
        World = 2,
        Silver = 3,
        Gold = 4,
        SteinmetzPink = 5,
        DeBeers = 6,
        [Display(Name = "Koh I Noor")]
        KohINoor = 7
    }
}
