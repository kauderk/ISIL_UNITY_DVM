using System;
using System.Runtime.Serialization;

[Serializable]
public enum SceneID
{
    Lobby,
    HUB,
    Enviroment,
    Gameplay
}
[DataContract(Name = "CarCondition")]
public enum CarConditionWithDifferentNames
{
    [EnumMember(Value = "New")]
    BrandNew,
    [EnumMember(Value = "Used")]
    PreviouslyOwned,
    [EnumMember]
    Rental
}
[DataContract]
[System.Flags]
public enum CarFeatures
{
    None = 0,
    [EnumMember]
    AirConditioner = 1,
    [EnumMember]
    AutomaticTransmission = 2,
    [EnumMember]
    PowerDoors = 4,
    AlloyWheels = 8,
    DeluxePackage = AirConditioner | AutomaticTransmission | PowerDoors | AlloyWheels,
    [EnumMember]
    CDPlayer = 16,
    [EnumMember]
    TapePlayer = 32,
    MusicPackage = CDPlayer | TapePlayer,
    [EnumMember]
    Everything = DeluxePackage | MusicPackage
}