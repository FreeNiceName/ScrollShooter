using System;
using System.Reflection;
using System.ComponentModel;

public class Binding
{
    private object _source;
    private object _target;
    private PropertyInfo _sourceProperty;
    private PropertyInfo _targetProperty;
    private Type _sourceType;
    private Type _targetType;

    public Binding(object source, object target, string sourcePropertyName, string targetPropertyName)
    {
        _source = source;
        _target = target;

        _sourceType = _source.GetType();
        _sourceProperty = _sourceType.GetProperty(sourcePropertyName);

        _targetType = target.GetType();
        _targetProperty = _targetType.GetProperty(targetPropertyName);

        var eventInfo = _sourceType.GetEvent("PropertyChanged");
        PropertyChangedEventHandler del = (s, args) => Reset();
        eventInfo.AddEventHandler(source, del);

        Reset();
    }

    public void Reset()
    {
        var value = _sourceType.InvokeMember(_sourceProperty.Name,
                    BindingFlags.GetField | BindingFlags.GetProperty,
                    null, _source, null);

        _targetProperty.SetValue(_target, value);
    }
}
