// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Bicep.Core.Diagnostics;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Bicep.Core.UnitTests.Assertions
{
    public static class DiagnosticExtensions 
    {
        public static DiagnosticAssertions Should(this Diagnostic instance)
        {
            return new DiagnosticAssertions(instance); 
        }
    }

    public class DiagnosticAssertions : ReferenceTypeAssertions<Diagnostic, DiagnosticAssertions>
    {
        public DiagnosticAssertions(Diagnostic instance)
        {
            Subject = instance;
        }

        protected override string Identifier => "diagnostic";

        public AndConstraint<DiagnosticAssertions> HaveCodeAndSeverity(string code, DiagnosticLevel level, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given<string>(() => Subject.Code)
                .ForCondition(x => x == code)
                .FailWith("Expected code to be '{0}' but it was '{1}'", _ => code, x => x)
                .Then
                .Given<DiagnosticLevel?>(_ => Subject.Level)
                .ForCondition(x => x == level)
                .FailWith("Expected level to be '{0}' but it was '{1}'", _ => level, x => x);

            return new AndConstraint<DiagnosticAssertions>(this);
        }

        public AndConstraint<DiagnosticAssertions> HaveMessage(string message, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given<string?>(() => Subject.Message)
                .ForCondition(x => x == message)
                .FailWith("Expected message to be '{0}' but it was '{1}'", _ => message, x => x);

            return new AndConstraint<DiagnosticAssertions>(this);
        }
    }
}
