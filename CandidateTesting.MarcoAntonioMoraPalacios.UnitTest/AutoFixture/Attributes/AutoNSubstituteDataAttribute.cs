using AutoFixture.AutoNSubstitute;
using AutoFixture;
using AutoFixture.Xunit2;

namespace CandidateTesting.MarcoAntonioMoraPalacios.UnitTest.AutoFixture.Attributes
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
       : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        { }
    }
}
