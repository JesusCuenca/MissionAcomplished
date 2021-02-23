
public class ValidatorFactory
{
    public static BaseValidator make(MissionDefinition definition) {
        return new AllSumValidator(definition.type, definition.arguments);
    }
}
