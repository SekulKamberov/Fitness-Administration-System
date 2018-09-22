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

        //[Display(Name = "Town")]
        //Home = 0,

        //[Display(Name = "Country")]
        //Country = 1,

        //[Display(Name = "World")]
        //World = 2,

        //[Display(Name = "Silver")]
        //Silver = 3,

        //[Display(Name = "Gold")]
        //Gold = 4,

        //[Display(Name = "Steinmetz Pink")]
        //SteinmetzPink = 5,

        //[Display(Name = "De Beers")]
        //DeBeers = 6,

        //[Display(Name = "Koh I Noor")]
        //KohINoor = 7
    }
}
