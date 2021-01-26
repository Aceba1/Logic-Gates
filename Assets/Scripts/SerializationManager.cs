using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

public class SerializationManager : MonoBehaviour
{
    public static IEnumerable<Type> FindByDescendant<T>() where T : Attribute
    {
        return from assembly in AppDomain.CurrentDomain.GetAssemblies()
               from type in assembly.GetTypes()
               where type.IsSubclassOf(typeof(T))
               select type;
    }

    static void Foo()
    {
        //JsonUtility.FromJson<>("");
        //JsonUtility.ToJson(, true);
    }
}

[Serializable]
public abstract class Serializer
{

}