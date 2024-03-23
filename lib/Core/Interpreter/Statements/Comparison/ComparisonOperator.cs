namespace AInterpreter.Core.Interpreter.Statements.Comparison
{
    /* ! This enum was created by Chat GPT ! */
  
    public enum ComparisonOperator
    {
        [StringValue("<")]
        LessThan,
        [StringValue(">")]
        GreaterThan,
        [StringValue("<=")]
        LessThanOrEqual,
        [StringValue(">=")]
        greaterThanOrEqual,
        [StringValue("==")]
        Equal,
        [StringValue("!=")]
        NotEqual
    }
}