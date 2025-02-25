#include "pch-cpp.hpp"





template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1, typename T2>
struct VirtualFuncInvoker2
{
	typedef R (*Func)(void*, T1, T2, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};

struct List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80;
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
struct List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C;
struct List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8;
struct List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12;
struct RngItemTable_1_tED7B627B543F283774C60E36C3BFDF359B0958F4;
struct RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69;
struct RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct IRngRandomU5BU5D_t3C08F34BFE70F0C59C1650A0D50B27B017AE76B3;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct RngItemU5BU5D_t8FC2999336906AA99865B928F3CB4F94113E2C25;
struct RngItem_IndividuallyU5BU5D_t07F6C072BF5CAEABC02ECC90DB45D71097F780E2;
struct RngItem_WeightU5BU5D_tAED034F63361ED658B3CD5EF238E11EAEE912780;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct AllIndividuallyRandom_tFE6B6BA299F413DBC2E5C67AB790765CA0196399;
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263;
struct CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B;
struct Exception_t;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IRngItem_t26B4B585832A434853DD55DCB81A5E253B6B9521;
struct IRngRandom_t31B0315449676429B032761C6915795F7EA4701C;
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
struct LayeredRandom_t5E4331D90E7FCE57AE01DEBA0A50386F2E5E0D19;
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71;
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
struct Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8;
struct RngDrop_tCE1D3F2889352159223B75428201140D2D2FB169;
struct RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399;
struct RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64;
struct RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F;
struct RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348;
struct Rng_Example_t4009C6A5109A88144DF9B8C826E94A49A78E494E;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct UnitySourceGeneratedAssemblyMonoScriptTypes_v1_tA4B973DA3A7B9CF771AFAC147250CE26EE7E19FA;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct WeightedRandom_tAA23F7C4FABBEEDD80A480A52950981A0087D087;

IL2CPP_EXTERN_C RuntimeClass* ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IRngItem_t26B4B585832A434853DD55DCB81A5E253B6B9521_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_t3B666390B7C59FEA0120805E8D0FF61275434A74____364C5923BB45923A0B1713166678F0E9AD024A39AF89B0CCFB02EDAC83BBEE81_FieldInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_t3B666390B7C59FEA0120805E8D0FF61275434A74____945166DA71A2486A400AF81202ADF5D6A0BA60C9BB23AD236F0B42CAC6FC3559_FieldInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral254511C12007A18F9311F47EA8BAF6CAEFD53C67;
IL2CPP_EXTERN_C String_t* _stringLiteral85F7391ABDBA5999E3E0583FF27BD6D7A96DA970;
IL2CPP_EXTERN_C String_t* _stringLiteral975DF47637BB4A94C821F9D23A2A24093F7BCF6C;
IL2CPP_EXTERN_C String_t* _stringLiteralA804421FFE0500D1BA6B5ABC0E71D3DABEC9EAE8;
IL2CPP_EXTERN_C String_t* _stringLiteralE37295ACACA3F08816EEA507DDB54F778A46DE52;
IL2CPP_EXTERN_C const RuntimeMethod* AllIndividuallyRandom_GetRandomDrop_m28640601F33334CCC89B2938BD4C28B7099B6FB5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AllIndividuallyRandom_RandomDrop_m1E003ABCF547B940529439E5343F5F34C3D1685E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_Dispose_m840C913BB45303F569EA68230E7ADC889394CB68_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_Dispose_mA8BCFB7BDAEC426E827218FBAFE14EA339621CAB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_m573337375B14071EF77F7E63B5AAD662053279D3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_m886F52C409835533936118CD98A25AFA15164357_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_m2AE5925E24796D1A0B302F9309C5BE541C973594_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_mCC0A01905537053EEA18C3A51919C043749C77DD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* LayeredRandom_AddLayer_mD96F5CE9042740A932FCF44290553D832E1E312C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* LayeredRandom_GetRandomDrop_m8EABA5A2AA2248858EED5BBA36FFF35752D951B5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* LayeredRandom_RandomDrop_m5370AA1CF48AC776BA48B883CEDB963F27650B2B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_m274E12C4434A0E6893C7E51DE916B997F25DE7D8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_m4723DDC6BB675D0B90840C3A394BBB854B5CE8DA_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Clear_m038BAB2EE3A8E697C1CEE286DE13011D9CA39DC2_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_GetEnumerator_m68935667C8AB0C6F4E2FF7E8C0857B8B0BCC8EA1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_GetEnumerator_mF330E028FF966DD513137B78EA74EED6043EFB44_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m14B9A3ED375DA0902DC827C8F7D1F1ED771F85AF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m62C5D5B222E339A5FFAD7FEE0A182423C401CFAC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_m27D57BC61E3F97E2849F97D1FA3815FF18F60F5A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_m93614A840363DCCC8B8E0111DAF51B3B13661A0D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_mD746AF7C7721939B982B34CE7B5C086AECAA8484_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* RngItemTable_1_CalculateWeight_mB9795FA83E990FFD09B8DC1BD228E9B35F72EA64_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* RngItemTable_1__ctor_m2FB34A0C332AF7236DE875AFF1EF98D8BE7EEC22_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* RngItemTable_1__ctor_m7BE0124C0A9F10DA211B17BDD3B2F22A7E7EBF9B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* WeightedRandom_GetRandomDrop_mCD86FEAC037C7081FE4FA5E459484C0B3D61CDC5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* WeightedRandom_RandomDrop_mF31A3D6D133677CB2208D84DF5483A5CA091611B_RuntimeMethod_var;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t24C7577B2066BD7BFCCFBCCC2B13E73EA1C01536 
{
};
struct List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80  : public RuntimeObject
{
	IRngRandomU5BU5D_t3C08F34BFE70F0C59C1650A0D50B27B017AE76B3* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D  : public RuntimeObject
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C  : public RuntimeObject
{
	RngItemU5BU5D_t8FC2999336906AA99865B928F3CB4F94113E2C25* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8  : public RuntimeObject
{
	RngItem_IndividuallyU5BU5D_t07F6C072BF5CAEABC02ECC90DB45D71097F780E2* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12  : public RuntimeObject
{
	RngItem_WeightU5BU5D_tAED034F63361ED658B3CD5EF238E11EAEE912780* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69  : public RuntimeObject
{
	String_t* ___TableName;
	int32_t ___TotalWeight;
	List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8* ___items;
};
struct RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766  : public RuntimeObject
{
	String_t* ___TableName;
	int32_t ___TotalWeight;
	List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12* ___items;
};
struct U3CPrivateImplementationDetailsU3E_t3B666390B7C59FEA0120805E8D0FF61275434A74  : public RuntimeObject
{
};
struct IRngRandom_t31B0315449676429B032761C6915795F7EA4701C  : public RuntimeObject
{
};
struct Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8  : public RuntimeObject
{
	int32_t ____inext;
	int32_t ____inextp;
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____seedArray;
};
struct RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399  : public RuntimeObject
{
	RuntimeObject* ___U3CObjU3Ek__BackingField;
	int32_t ___Amount;
	Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___Data;
};
struct RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348  : public RuntimeObject
{
	List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* ___U3CItemsU3Ek__BackingField;
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct UnitySourceGeneratedAssemblyMonoScriptTypes_v1_tA4B973DA3A7B9CF771AFAC147250CE26EE7E19FA  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8 
{
	List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* ____list;
	int32_t ____index;
	int32_t ____version;
	IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* ____current;
};
struct Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A 
{
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ____list;
	int32_t ____index;
	int32_t ____version;
	RuntimeObject* ____current;
};
struct Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4 
{
	List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8* ____list;
	int32_t ____index;
	int32_t ____version;
	RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* ____current;
};
struct Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4 
{
	List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12* ____list;
	int32_t ____index;
	int32_t ____version;
	RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* ____current;
};
struct AllIndividuallyRandom_tFE6B6BA299F413DBC2E5C67AB790765CA0196399  : public IRngRandom_t31B0315449676429B032761C6915795F7EA4701C
{
	RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69* ___itemTable;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	uint8_t ___m_value;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct LayeredRandom_t5E4331D90E7FCE57AE01DEBA0A50386F2E5E0D19  : public IRngRandom_t31B0315449676429B032761C6915795F7EA4701C
{
	List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* ___ItemTables;
};
struct RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64  : public RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399
{
	int32_t ___U3CWeightU3Ek__BackingField;
};
struct RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F  : public RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399
{
	int32_t ___U3CWeightU3Ek__BackingField;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct WeightedRandom_tAA23F7C4FABBEEDD80A480A52950981A0087D087  : public IRngRandom_t31B0315449676429B032761C6915795F7EA4701C
{
	RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* ___itemTable;
};
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D244_t60CE0B7A801967EAFF5836F6C3B92CE728D9E356 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D244_t60CE0B7A801967EAFF5836F6C3B92CE728D9E356__padding[244];
	};
};
#pragma pack(pop, tp)
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D372_tD6B26D6D098E298AD1513D6F49EC1CC6482190E5 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D372_tD6B26D6D098E298AD1513D6F49EC1CC6482190E5__padding[372];
	};
};
#pragma pack(pop, tp)
struct MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C 
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___FilePathsData;
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	bool ___IsEditorOnly;
};
struct MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshaled_pinvoke
{
	Il2CppSafeArray* ___FilePathsData;
	Il2CppSafeArray* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	int32_t ___IsEditorOnly;
};
struct MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshaled_com
{
	Il2CppSafeArray* ___FilePathsData;
	Il2CppSafeArray* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	int32_t ___IsEditorOnly;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct RandomEnum_tD83FF95F4E94496C94A240EC2158AE5417CA4944 
{
	int32_t ___value__;
};
struct RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 
{
	intptr_t ___value;
};
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct RngDrop_tCE1D3F2889352159223B75428201140D2D2FB169  : public RuntimeObject
{
	int32_t ___RandomType;
	IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* ___items;
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	String_t* ____paramName;
};
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
	CancellationTokenSource_tAAE1E0033BCFC233801F8CB4CED5C852B350CB7B* ___m_CancellationTokenSource;
};
struct Rng_Example_t4009C6A5109A88144DF9B8C826E94A49A78E494E  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	RngDrop_tCE1D3F2889352159223B75428201140D2D2FB169* ___RngDrop;
};
struct List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80_StaticFields
{
	IRngRandomU5BU5D_t3C08F34BFE70F0C59C1650A0D50B27B017AE76B3* ___s_emptyArray;
};
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_StaticFields
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___s_emptyArray;
};
struct List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C_StaticFields
{
	RngItemU5BU5D_t8FC2999336906AA99865B928F3CB4F94113E2C25* ___s_emptyArray;
};
struct List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8_StaticFields
{
	RngItem_IndividuallyU5BU5D_t07F6C072BF5CAEABC02ECC90DB45D71097F780E2* ___s_emptyArray;
};
struct List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12_StaticFields
{
	RngItem_WeightU5BU5D_tAED034F63361ED658B3CD5EF238E11EAEE912780* ___s_emptyArray;
};
struct U3CPrivateImplementationDetailsU3E_t3B666390B7C59FEA0120805E8D0FF61275434A74_StaticFields
{
	__StaticArrayInitTypeSizeU3D244_t60CE0B7A801967EAFF5836F6C3B92CE728D9E356 ___364C5923BB45923A0B1713166678F0E9AD024A39AF89B0CCFB02EDAC83BBEE81;
	__StaticArrayInitTypeSizeU3D372_tD6B26D6D098E298AD1513D6F49EC1CC6482190E5 ___945166DA71A2486A400AF81202ADF5D6A0BA60C9BB23AD236F0B42CAC6FC3559;
};
struct Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_StaticFields
{
	Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8* ___s_globalRandom;
};
struct Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_ThreadStaticFields
{
	Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8* ___t_threadRandom;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct Exception_t_StaticFields
{
	RuntimeObject* ___s_EDILock;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngItemTable_1__ctor_m59159D7BA2AB61100B01F7490B6EFF5F1B9865A5_gshared (RngItemTable_1_tED7B627B543F283774C60E36C3BFDF359B0958F4* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t RngItemTable_1_CalculateWeight_m358A8A42BA3068159DC21D3C673E8B66AB1CF478_gshared (RngItemTable_1_tED7B627B543F283774C60E36C3BFDF359B0958F4* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m79A9BF770BEF9C06BE40D5401E55E375F2726CC4_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B (RuntimeArray* ___0_array, RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 ___1_fldHandle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
inline int32_t List_1_get_Count_m27D57BC61E3F97E2849F97D1FA3815FF18F60F5A_inline (List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8*, const RuntimeMethod*))List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngResult__ctor_mBD046D2101593735B6539386B727D4CF2FF74C4A (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, const RuntimeMethod* method) ;
inline Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4 List_1_GetEnumerator_m68935667C8AB0C6F4E2FF7E8C0857B8B0BCC8EA1 (List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4 (*) (List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8*, const RuntimeMethod*))List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared)(__this, method);
}
inline void Enumerator_Dispose_m840C913BB45303F569EA68230E7ADC889394CB68 (Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4*, const RuntimeMethod*))Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared)(__this, method);
}
inline RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* Enumerator_get_Current_m2AE5925E24796D1A0B302F9309C5BE541C973594_inline (Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4* __this, const RuntimeMethod* method)
{
	return ((  RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* (*) (Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4*, const RuntimeMethod*))Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Random__ctor_m151183BD4F021499A98B9DE8502DAD4B12DD16AC (Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngResult_Add_m88793C00B649731FA7AB99828DC367ADD0132665 (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* ___0_item, const RuntimeMethod* method) ;
inline bool Enumerator_MoveNext_m886F52C409835533936118CD98A25AFA15164357 (Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4*, const RuntimeMethod*))Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared)(__this, method);
}
inline void RngItemTable_1__ctor_m7BE0124C0A9F10DA211B17BDD3B2F22A7E7EBF9B (RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69* __this, const RuntimeMethod* method)
{
	((  void (*) (RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69*, const RuntimeMethod*))RngItemTable_1__ctor_m59159D7BA2AB61100B01F7490B6EFF5F1B9865A5_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IRngRandom__ctor_m5FEE0413CC0F18388AE62312F8D65BC2CF1329E8 (IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* __this, const RuntimeMethod* method) ;
inline Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8 List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD (List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8 (*) (List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80*, const RuntimeMethod*))List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared)(__this, method);
}
inline void Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766 (Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8*, const RuntimeMethod*))Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared)(__this, method);
}
inline IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_inline (Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8* __this, const RuntimeMethod* method)
{
	return ((  IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* (*) (Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8*, const RuntimeMethod*))Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline)(__this, method);
}
inline bool Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC (Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8*, const RuntimeMethod*))Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465 (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* __this, String_t* ___0_message, const RuntimeMethod* method) ;
inline void List_1_Add_m274E12C4434A0E6893C7E51DE916B997F25DE7D8_inline (List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* __this, IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80*, IRngRandom_t31B0315449676429B032761C6915795F7EA4701C*, const RuntimeMethod*))List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline)(__this, ___0_item, method);
}
inline int32_t List_1_get_Count_mD746AF7C7721939B982B34CE7B5C086AECAA8484_inline (List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80*, const RuntimeMethod*))List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline)(__this, method);
}
inline void List_1__ctor_m62C5D5B222E339A5FFAD7FEE0A182423C401CFAC (List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* RngItem_get_Obj_mE0FF47C5419BD898FC0286BE7B4E72F1A1ACDB4C_inline (RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void RngItem_set_Obj_mFE20246916C4E8277E53D4E9D7CEC82FA0D06028_inline (RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngItem__ctor_m717233576D979AB5582EC7A43BD64AAE87C747C0 (RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* __this, const RuntimeMethod* method) ;
inline void List_1__ctor_m14B9A3ED375DA0902DC827C8F7D1F1ED771F85AF (List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void RngResult_set_Items_m9FCD04F1B5AA474384EC7B3625E7350C2DF8FC97_inline (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* RngResult_get_Items_m0C9AA21C5559B9563D6C0EBC5E891696E11F088F_inline (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, const RuntimeMethod* method) ;
inline void List_1_Add_m4723DDC6BB675D0B90840C3A394BBB854B5CE8DA_inline (List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* __this, RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C*, RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399*, const RuntimeMethod*))List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline)(__this, ___0_item, method);
}
inline void List_1_Clear_m038BAB2EE3A8E697C1CEE286DE13011D9CA39DC2_inline (List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C*, const RuntimeMethod*))List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline)(__this, method);
}
inline int32_t RngItemTable_1_CalculateWeight_mB9795FA83E990FFD09B8DC1BD228E9B35F72EA64 (RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766*, const RuntimeMethod*))RngItemTable_1_CalculateWeight_m358A8A42BA3068159DC21D3C673E8B66AB1CF478_gshared)(__this, method);
}
inline int32_t List_1_get_Count_m93614A840363DCCC8B8E0111DAF51B3B13661A0D_inline (List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12*, const RuntimeMethod*))List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline)(__this, method);
}
inline Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4 List_1_GetEnumerator_mF330E028FF966DD513137B78EA74EED6043EFB44 (List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4 (*) (List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12*, const RuntimeMethod*))List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared)(__this, method);
}
inline void Enumerator_Dispose_mA8BCFB7BDAEC426E827218FBAFE14EA339621CAB (Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4*, const RuntimeMethod*))Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared)(__this, method);
}
inline RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* Enumerator_get_Current_mCC0A01905537053EEA18C3A51919C043749C77DD_inline (Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4* __this, const RuntimeMethod* method)
{
	return ((  RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* (*) (Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4*, const RuntimeMethod*))Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline)(__this, method);
}
inline bool Enumerator_MoveNext_m573337375B14071EF77F7E63B5AAD662053279D3 (Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4*, const RuntimeMethod*))Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F (Exception_t* __this, String_t* ___0_message, const RuntimeMethod* method) ;
inline void RngItemTable_1__ctor_m2FB34A0C332AF7236DE875AFF1EF98D8BE7EEC22 (RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* __this, const RuntimeMethod* method)
{
	((  void (*) (RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766*, const RuntimeMethod*))RngItemTable_1__ctor_m59159D7BA2AB61100B01F7490B6EFF5F1B9865A5_gshared)(__this, method);
}
inline void List_1_AddWithResize_m79A9BF770BEF9C06BE40D5401E55E375F2726CC4 (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, RuntimeObject*, const RuntimeMethod*))List_1_AddWithResize_m79A9BF770BEF9C06BE40D5401E55E375F2726CC4_gshared)(__this, ___0_item, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB (RuntimeArray* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Rng_Example__ctor_m65C327ACFEF32EA507F13416647A53F967D6B4D5 (Rng_Example_t4009C6A5109A88144DF9B8C826E94A49A78E494E* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C UnitySourceGeneratedAssemblyMonoScriptTypes_v1_Get_mFFECF9494B671F0A0AD522002AB5EE9FC873021E (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_t3B666390B7C59FEA0120805E8D0FF61275434A74____364C5923BB45923A0B1713166678F0E9AD024A39AF89B0CCFB02EDAC83BBEE81_FieldInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_t3B666390B7C59FEA0120805E8D0FF61275434A74____945166DA71A2486A400AF81202ADF5D6A0BA60C9BB23AD236F0B42CAC6FC3559_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		il2cpp_codegen_initobj((&V_0), sizeof(MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)244));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_1 = L_0;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_2 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_t3B666390B7C59FEA0120805E8D0FF61275434A74____364C5923BB45923A0B1713166678F0E9AD024A39AF89B0CCFB02EDAC83BBEE81_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_1, L_2, NULL);
		(&V_0)->___FilePathsData = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___FilePathsData), (void*)L_1);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_3 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)372));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_4 = L_3;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_5 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_t3B666390B7C59FEA0120805E8D0FF61275434A74____945166DA71A2486A400AF81202ADF5D6A0BA60C9BB23AD236F0B42CAC6FC3559_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_4, L_5, NULL);
		(&V_0)->___TypesData = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___TypesData), (void*)L_4);
		(&V_0)->___TotalFiles = 5;
		(&V_0)->___TotalTypes = ((int32_t)13);
		(&V_0)->___IsEditorOnly = (bool)0;
		MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C L_6 = V_0;
		return L_6;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnitySourceGeneratedAssemblyMonoScriptTypes_v1__ctor_m2EB8DD0AF974C83994762EC3E0D56D9340956F8F (UnitySourceGeneratedAssemblyMonoScriptTypes_v1_tA4B973DA3A7B9CF771AFAC147250CE26EE7E19FA* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshal_pinvoke(const MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C& unmarshaled, MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshaled_pinvoke& marshaled)
{
	marshaled.___FilePathsData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___FilePathsData);
	marshaled.___TypesData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___TypesData);
	marshaled.___TotalTypes = unmarshaled.___TotalTypes;
	marshaled.___TotalFiles = unmarshaled.___TotalFiles;
	marshaled.___IsEditorOnly = static_cast<int32_t>(unmarshaled.___IsEditorOnly);
}
IL2CPP_EXTERN_C void MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshal_pinvoke_back(const MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshaled_pinvoke& marshaled, MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C& unmarshaled)
{
	unmarshaled.___FilePathsData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___FilePathsData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData));
	unmarshaled.___TypesData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___TypesData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData));
	int32_t unmarshaledTotalTypes_temp_2 = 0;
	unmarshaledTotalTypes_temp_2 = marshaled.___TotalTypes;
	unmarshaled.___TotalTypes = unmarshaledTotalTypes_temp_2;
	int32_t unmarshaledTotalFiles_temp_3 = 0;
	unmarshaledTotalFiles_temp_3 = marshaled.___TotalFiles;
	unmarshaled.___TotalFiles = unmarshaledTotalFiles_temp_3;
	bool unmarshaledIsEditorOnly_temp_4 = false;
	unmarshaledIsEditorOnly_temp_4 = static_cast<bool>(marshaled.___IsEditorOnly);
	unmarshaled.___IsEditorOnly = unmarshaledIsEditorOnly_temp_4;
}
IL2CPP_EXTERN_C void MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshal_pinvoke_cleanup(MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshaled_pinvoke& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___FilePathsData);
	marshaled.___FilePathsData = NULL;
	il2cpp_codegen_com_destroy_safe_array(marshaled.___TypesData);
	marshaled.___TypesData = NULL;
}
IL2CPP_EXTERN_C void MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshal_com(const MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C& unmarshaled, MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshaled_com& marshaled)
{
	marshaled.___FilePathsData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___FilePathsData);
	marshaled.___TypesData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___TypesData);
	marshaled.___TotalTypes = unmarshaled.___TotalTypes;
	marshaled.___TotalFiles = unmarshaled.___TotalFiles;
	marshaled.___IsEditorOnly = static_cast<int32_t>(unmarshaled.___IsEditorOnly);
}
IL2CPP_EXTERN_C void MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshal_com_back(const MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshaled_com& marshaled, MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C& unmarshaled)
{
	unmarshaled.___FilePathsData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___FilePathsData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___FilePathsData));
	unmarshaled.___TypesData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___TypesData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, il2cpp_defaults.byte_class, marshaled.___TypesData));
	int32_t unmarshaledTotalTypes_temp_2 = 0;
	unmarshaledTotalTypes_temp_2 = marshaled.___TotalTypes;
	unmarshaled.___TotalTypes = unmarshaledTotalTypes_temp_2;
	int32_t unmarshaledTotalFiles_temp_3 = 0;
	unmarshaledTotalFiles_temp_3 = marshaled.___TotalFiles;
	unmarshaled.___TotalFiles = unmarshaledTotalFiles_temp_3;
	bool unmarshaledIsEditorOnly_temp_4 = false;
	unmarshaledIsEditorOnly_temp_4 = static_cast<bool>(marshaled.___IsEditorOnly);
	unmarshaled.___IsEditorOnly = unmarshaledIsEditorOnly_temp_4;
}
IL2CPP_EXTERN_C void MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshal_com_cleanup(MonoScriptData_tB5B0FBE1F791F406BEA5CC20C8437F0608447E1C_marshaled_com& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___FilePathsData);
	marshaled.___FilePathsData = NULL;
	il2cpp_codegen_com_destroy_safe_array(marshaled.___TypesData);
	marshaled.___TypesData = NULL;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AllIndividuallyRandom_CalculateWeight_mFF96096BCD06EBE2A09864D62EF4801FF73C67A1 (AllIndividuallyRandom_tFE6B6BA299F413DBC2E5C67AB790765CA0196399* __this, const RuntimeMethod* method) 
{
	{
		return 0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* AllIndividuallyRandom_GetRandomDrop_m28640601F33334CCC89B2938BD4C28B7099B6FB5 (AllIndividuallyRandom_tFE6B6BA299F413DBC2E5C67AB790765CA0196399* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m840C913BB45303F569EA68230E7ADC889394CB68_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m886F52C409835533936118CD98A25AFA15164357_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_m2AE5925E24796D1A0B302F9309C5BE541C973594_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m68935667C8AB0C6F4E2FF7E8C0857B8B0BCC8EA1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_m27D57BC61E3F97E2849F97D1FA3815FF18F60F5A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* V_0 = NULL;
	Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4 V_1;
	memset((&V_1), 0, sizeof(V_1));
	RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* V_2 = NULL;
	{
		RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69* L_0 = __this->___itemTable;
		NullCheck(L_0);
		List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8* L_1 = L_0->___items;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = List_1_get_Count_m27D57BC61E3F97E2849F97D1FA3815FF18F60F5A_inline(L_1, List_1_get_Count_m27D57BC61E3F97E2849F97D1FA3815FF18F60F5A_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_001d;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral254511C12007A18F9311F47EA8BAF6CAEFD53C67)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AllIndividuallyRandom_GetRandomDrop_m28640601F33334CCC89B2938BD4C28B7099B6FB5_RuntimeMethod_var)));
	}

IL_001d:
	{
		RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_4 = (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348*)il2cpp_codegen_object_new(RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348_il2cpp_TypeInfo_var);
		RngResult__ctor_mBD046D2101593735B6539386B727D4CF2FF74C4A(L_4, NULL);
		V_0 = L_4;
		RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69* L_5 = __this->___itemTable;
		NullCheck(L_5);
		List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8* L_6 = L_5->___items;
		NullCheck(L_6);
		Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4 L_7;
		L_7 = List_1_GetEnumerator_m68935667C8AB0C6F4E2FF7E8C0857B8B0BCC8EA1(L_6, List_1_GetEnumerator_m68935667C8AB0C6F4E2FF7E8C0857B8B0BCC8EA1_RuntimeMethod_var);
		V_1 = L_7;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0068:
			{
				Enumerator_Dispose_m840C913BB45303F569EA68230E7ADC889394CB68((&V_1), Enumerator_Dispose_m840C913BB45303F569EA68230E7ADC889394CB68_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_005d_1;
			}

IL_0036_1:
			{
				RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* L_8;
				L_8 = Enumerator_get_Current_m2AE5925E24796D1A0B302F9309C5BE541C973594_inline((&V_1), Enumerator_get_Current_m2AE5925E24796D1A0B302F9309C5BE541C973594_RuntimeMethod_var);
				V_2 = L_8;
				Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8* L_9 = (Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8*)il2cpp_codegen_object_new(Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_il2cpp_TypeInfo_var);
				Random__ctor_m151183BD4F021499A98B9DE8502DAD4B12DD16AC(L_9, NULL);
				NullCheck(L_9);
				int32_t L_10;
				L_10 = VirtualFuncInvoker2< int32_t, int32_t, int32_t >::Invoke(6, L_9, 0, ((int32_t)10000));
				RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* L_11 = V_2;
				NullCheck(L_11);
				int32_t L_12;
				L_12 = VirtualFuncInvoker0< int32_t >::Invoke(4, L_11);
				if ((((int32_t)L_10) >= ((int32_t)L_12)))
				{
					goto IL_005d_1;
				}
			}
			{
				RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_13 = V_0;
				RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* L_14 = V_2;
				NullCheck(L_13);
				RngResult_Add_m88793C00B649731FA7AB99828DC367ADD0132665(L_13, L_14, NULL);
			}

IL_005d_1:
			{
				bool L_15;
				L_15 = Enumerator_MoveNext_m886F52C409835533936118CD98A25AFA15164357((&V_1), Enumerator_MoveNext_m886F52C409835533936118CD98A25AFA15164357_RuntimeMethod_var);
				if (L_15)
				{
					goto IL_0036_1;
				}
			}
			{
				goto IL_0076;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0076:
	{
		RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_16 = V_0;
		return L_16;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AllIndividuallyRandom_RandomDrop_m1E003ABCF547B940529439E5343F5F34C3D1685E (AllIndividuallyRandom_tFE6B6BA299F413DBC2E5C67AB790765CA0196399* __this, RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* ___0_r, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m840C913BB45303F569EA68230E7ADC889394CB68_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m886F52C409835533936118CD98A25AFA15164357_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_m2AE5925E24796D1A0B302F9309C5BE541C973594_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m68935667C8AB0C6F4E2FF7E8C0857B8B0BCC8EA1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_m27D57BC61E3F97E2849F97D1FA3815FF18F60F5A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* V_1 = NULL;
	{
		RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69* L_0 = __this->___itemTable;
		NullCheck(L_0);
		List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8* L_1 = L_0->___items;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = List_1_get_Count_m27D57BC61E3F97E2849F97D1FA3815FF18F60F5A_inline(L_1, List_1_get_Count_m27D57BC61E3F97E2849F97D1FA3815FF18F60F5A_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_001d;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral254511C12007A18F9311F47EA8BAF6CAEFD53C67)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AllIndividuallyRandom_RandomDrop_m1E003ABCF547B940529439E5343F5F34C3D1685E_RuntimeMethod_var)));
	}

IL_001d:
	{
		RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69* L_4 = __this->___itemTable;
		NullCheck(L_4);
		List_1_t3CB5B125F97CF50D8ECC3AC38F4C5F8058F42FA8* L_5 = L_4->___items;
		NullCheck(L_5);
		Enumerator_t2391AB64D6B34B6C2BF57496B3C3198FF92518B4 L_6;
		L_6 = List_1_GetEnumerator_m68935667C8AB0C6F4E2FF7E8C0857B8B0BCC8EA1(L_5, List_1_GetEnumerator_m68935667C8AB0C6F4E2FF7E8C0857B8B0BCC8EA1_RuntimeMethod_var);
		V_0 = L_6;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0062:
			{
				Enumerator_Dispose_m840C913BB45303F569EA68230E7ADC889394CB68((&V_0), Enumerator_Dispose_m840C913BB45303F569EA68230E7ADC889394CB68_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_0057_1;
			}

IL_0030_1:
			{
				RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* L_7;
				L_7 = Enumerator_get_Current_m2AE5925E24796D1A0B302F9309C5BE541C973594_inline((&V_0), Enumerator_get_Current_m2AE5925E24796D1A0B302F9309C5BE541C973594_RuntimeMethod_var);
				V_1 = L_7;
				Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8* L_8 = (Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8*)il2cpp_codegen_object_new(Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_il2cpp_TypeInfo_var);
				Random__ctor_m151183BD4F021499A98B9DE8502DAD4B12DD16AC(L_8, NULL);
				NullCheck(L_8);
				int32_t L_9;
				L_9 = VirtualFuncInvoker2< int32_t, int32_t, int32_t >::Invoke(6, L_8, 0, ((int32_t)10000));
				RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* L_10 = V_1;
				NullCheck(L_10);
				int32_t L_11;
				L_11 = VirtualFuncInvoker0< int32_t >::Invoke(4, L_10);
				if ((((int32_t)L_9) >= ((int32_t)L_11)))
				{
					goto IL_0057_1;
				}
			}
			{
				RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_12 = ___0_r;
				RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* L_13 = V_1;
				NullCheck(L_12);
				RngResult_Add_m88793C00B649731FA7AB99828DC367ADD0132665(L_12, L_13, NULL);
			}

IL_0057_1:
			{
				bool L_14;
				L_14 = Enumerator_MoveNext_m886F52C409835533936118CD98A25AFA15164357((&V_0), Enumerator_MoveNext_m886F52C409835533936118CD98A25AFA15164357_RuntimeMethod_var);
				if (L_14)
				{
					goto IL_0030_1;
				}
			}
			{
				goto IL_0070;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0070:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AllIndividuallyRandom__ctor_m80A6626546947074A81D6FE65E9203BF10A7C594 (AllIndividuallyRandom_tFE6B6BA299F413DBC2E5C67AB790765CA0196399* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RngItemTable_1__ctor_m7BE0124C0A9F10DA211B17BDD3B2F22A7E7EBF9B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69* L_0 = (RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69*)il2cpp_codegen_object_new(RngItemTable_1_tC3AB5F427F22A0EFC3DFD8686B9F629D30D2CA69_il2cpp_TypeInfo_var);
		RngItemTable_1__ctor_m7BE0124C0A9F10DA211B17BDD3B2F22A7E7EBF9B(L_0, RngItemTable_1__ctor_m7BE0124C0A9F10DA211B17BDD3B2F22A7E7EBF9B_RuntimeMethod_var);
		__this->___itemTable = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___itemTable), (void*)L_0);
		IRngRandom__ctor_m5FEE0413CC0F18388AE62312F8D65BC2CF1329E8(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t LayeredRandom_CalculateWeight_m3EF976EA9D59B74CAF343F23820DBA09364C04DF (LayeredRandom_t5E4331D90E7FCE57AE01DEBA0A50386F2E5E0D19* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8 V_1;
	memset((&V_1), 0, sizeof(V_1));
	IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* V_2 = NULL;
	{
		V_0 = 0;
		List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* L_0 = __this->___ItemTables;
		NullCheck(L_0);
		Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8 L_1;
		L_1 = List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD(L_0, List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD_RuntimeMethod_var);
		V_1 = L_1;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0032:
			{
				Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766((&V_1), Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_0027_1;
			}

IL_0010_1:
			{
				IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_2;
				L_2 = Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_inline((&V_1), Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_RuntimeMethod_var);
				V_2 = L_2;
				IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_3 = V_2;
				if (!L_3)
				{
					goto IL_0027_1;
				}
			}
			{
				IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_4 = V_2;
				if (!L_4)
				{
					goto IL_0027_1;
				}
			}
			{
				int32_t L_5 = V_0;
				IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_6 = V_2;
				NullCheck(L_6);
				int32_t L_7;
				L_7 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_6);
				V_0 = ((int32_t)il2cpp_codegen_add(L_5, L_7));
			}

IL_0027_1:
			{
				bool L_8;
				L_8 = Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC((&V_1), Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC_RuntimeMethod_var);
				if (L_8)
				{
					goto IL_0010_1;
				}
			}
			{
				goto IL_0040;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0040:
	{
		int32_t L_9 = V_0;
		return L_9;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LayeredRandom_AddLayer_mD96F5CE9042740A932FCF44290553D832E1E312C (LayeredRandom_t5E4331D90E7FCE57AE01DEBA0A50386F2E5E0D19* __this, IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* ___0_dropTable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m274E12C4434A0E6893C7E51DE916B997F25DE7D8_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_0 = ___0_dropTable;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_1 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral975DF47637BB4A94C821F9D23A2A24093F7BCF6C)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&LayeredRandom_AddLayer_mD96F5CE9042740A932FCF44290553D832E1E312C_RuntimeMethod_var)));
	}

IL_000e:
	{
		List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* L_2 = __this->___ItemTables;
		IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_3 = ___0_dropTable;
		NullCheck(L_2);
		List_1_Add_m274E12C4434A0E6893C7E51DE916B997F25DE7D8_inline(L_2, L_3, List_1_Add_m274E12C4434A0E6893C7E51DE916B997F25DE7D8_RuntimeMethod_var);
		IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_4 = ___0_dropTable;
		NullCheck(L_4);
		int32_t L_5;
		L_5 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_4);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* LayeredRandom_GetRandomDrop_m8EABA5A2AA2248858EED5BBA36FFF35752D951B5 (LayeredRandom_t5E4331D90E7FCE57AE01DEBA0A50386F2E5E0D19* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mD746AF7C7721939B982B34CE7B5C086AECAA8484_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* V_0 = NULL;
	Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* L_0 = __this->___ItemTables;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = List_1_get_Count_mD746AF7C7721939B982B34CE7B5C086AECAA8484_inline(L_0, List_1_get_Count_mD746AF7C7721939B982B34CE7B5C086AECAA8484_RuntimeMethod_var);
		if (L_1)
		{
			goto IL_0018;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_2 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE37295ACACA3F08816EEA507DDB54F778A46DE52)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&LayeredRandom_GetRandomDrop_m8EABA5A2AA2248858EED5BBA36FFF35752D951B5_RuntimeMethod_var)));
	}

IL_0018:
	{
		RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_3 = (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348*)il2cpp_codegen_object_new(RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348_il2cpp_TypeInfo_var);
		RngResult__ctor_mBD046D2101593735B6539386B727D4CF2FF74C4A(L_3, NULL);
		V_0 = L_3;
		List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* L_4 = __this->___ItemTables;
		NullCheck(L_4);
		Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8 L_5;
		L_5 = List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD(L_4, List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD_RuntimeMethod_var);
		V_1 = L_5;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0044:
			{
				Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766((&V_1), Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_0039_1;
			}

IL_002c_1:
			{
				IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_6;
				L_6 = Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_inline((&V_1), Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_RuntimeMethod_var);
				RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_7 = V_0;
				NullCheck(L_6);
				VirtualActionInvoker1< RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* >::Invoke(7, L_6, L_7);
			}

IL_0039_1:
			{
				bool L_8;
				L_8 = Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC((&V_1), Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC_RuntimeMethod_var);
				if (L_8)
				{
					goto IL_002c_1;
				}
			}
			{
				goto IL_0052;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0052:
	{
		RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_9 = V_0;
		return L_9;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LayeredRandom_RandomDrop_m5370AA1CF48AC776BA48B883CEDB963F27650B2B (LayeredRandom_t5E4331D90E7FCE57AE01DEBA0A50386F2E5E0D19* __this, RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* ___0_r, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mD746AF7C7721939B982B34CE7B5C086AECAA8484_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* L_0 = __this->___ItemTables;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = List_1_get_Count_mD746AF7C7721939B982B34CE7B5C086AECAA8484_inline(L_0, List_1_get_Count_mD746AF7C7721939B982B34CE7B5C086AECAA8484_RuntimeMethod_var);
		if (L_1)
		{
			goto IL_0018;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_2 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_2, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE37295ACACA3F08816EEA507DDB54F778A46DE52)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&LayeredRandom_RandomDrop_m5370AA1CF48AC776BA48B883CEDB963F27650B2B_RuntimeMethod_var)));
	}

IL_0018:
	{
		List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* L_3 = __this->___ItemTables;
		NullCheck(L_3);
		Enumerator_t6AFEBBFCD325039C535187CE06BE19BCC50D60B8 L_4;
		L_4 = List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD(L_3, List_1_GetEnumerator_m35ADC9BFC83E9C38DE4F89BD065782AE2B7BE8FD_RuntimeMethod_var);
		V_0 = L_4;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_003e:
			{
				Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766((&V_0), Enumerator_Dispose_m3C784B60DC9A866E62DC22990A9800A06E0A5766_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_0033_1;
			}

IL_0026_1:
			{
				IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_5;
				L_5 = Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_inline((&V_0), Enumerator_get_Current_mEFC7FED3FD331538C066A6414821736259810DD5_RuntimeMethod_var);
				RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_6 = ___0_r;
				NullCheck(L_5);
				VirtualActionInvoker1< RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* >::Invoke(7, L_5, L_6);
			}

IL_0033_1:
			{
				bool L_7;
				L_7 = Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC((&V_0), Enumerator_MoveNext_m0585C4592ED4C45F8EDC56F15516E2E124E25CFC_RuntimeMethod_var);
				if (L_7)
				{
					goto IL_0026_1;
				}
			}
			{
				goto IL_004c;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_004c:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LayeredRandom__ctor_mD4AA39C0270D0B96906CC0F55766ED3A760D3A7B (LayeredRandom_t5E4331D90E7FCE57AE01DEBA0A50386F2E5E0D19* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m62C5D5B222E339A5FFAD7FEE0A182423C401CFAC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80* L_0 = (List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80*)il2cpp_codegen_object_new(List_1_tEB28B9361EA2C920FD687571E52C57C7B7AFDF80_il2cpp_TypeInfo_var);
		List_1__ctor_m62C5D5B222E339A5FFAD7FEE0A182423C401CFAC(L_0, List_1__ctor_m62C5D5B222E339A5FFAD7FEE0A182423C401CFAC_RuntimeMethod_var);
		__this->___ItemTables = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___ItemTables), (void*)L_0);
		IRngRandom__ctor_m5FEE0413CC0F18388AE62312F8D65BC2CF1329E8(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* RngDrop_GetRandomDrop_mE4AFD1398F493BE25D0F74CC7BA7C7B7A8C183AB (RngDrop_tCE1D3F2889352159223B75428201140D2D2FB169* __this, const RuntimeMethod* method) 
{
	{
		IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* L_0 = __this->___items;
		NullCheck(L_0);
		RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_1;
		L_1 = VirtualFuncInvoker0< RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* >::Invoke(6, L_0);
		return L_1;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngDrop__ctor_mEB17D50576CD135B8BB70B9D238A9E9A47D0F49C (RngDrop_tCE1D3F2889352159223B75428201140D2D2FB169* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* RngItem_get_Obj_mE0FF47C5419BD898FC0286BE7B4E72F1A1ACDB4C (RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CObjU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngItem_set_Obj_mFE20246916C4E8277E53D4E9D7CEC82FA0D06028 (RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = ___0_value;
		__this->___U3CObjU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CObjU3Ek__BackingField), (void*)L_0);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* RngItem_GetObj_mC5F51B2A94D10965A4D52C6859FB682CAD140116 (RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IRngItem_t26B4B585832A434853DD55DCB81A5E253B6B9521_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA804421FFE0500D1BA6B5ABC0E71D3DABEC9EAE8);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_0 = __this->___Data;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_1)
		{
			goto IL_0010;
		}
	}
	{
		return (RuntimeObject*)NULL;
	}

IL_0010:
	{
		RuntimeObject* L_2;
		L_2 = RngItem_get_Obj_mE0FF47C5419BD898FC0286BE7B4E72F1A1ACDB4C_inline(__this, NULL);
		if (L_2)
		{
			goto IL_0038;
		}
	}
	{
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_3 = __this->___Data;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, IRngItem_t26B4B585832A434853DD55DCB81A5E253B6B9521_il2cpp_TypeInfo_var));
		RuntimeObject* L_4 = V_0;
		if (L_4)
		{
			goto IL_0031;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(_stringLiteralA804421FFE0500D1BA6B5ABC0E71D3DABEC9EAE8, NULL);
	}

IL_0031:
	{
		RuntimeObject* L_5 = V_0;
		RngItem_set_Obj_mFE20246916C4E8277E53D4E9D7CEC82FA0D06028_inline(__this, L_5, NULL);
	}

IL_0038:
	{
		RuntimeObject* L_6;
		L_6 = RngItem_get_Obj_mE0FF47C5419BD898FC0286BE7B4E72F1A1ACDB4C_inline(__this, NULL);
		return L_6;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngItem__ctor_m717233576D979AB5582EC7A43BD64AAE87C747C0 (RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t RngItem_Weight_get_Weight_mE7B5834EB71D6A6E177FC6E631464D586F551805 (RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CWeightU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngItem_Weight_set_Weight_m7BFAA22EC81A5503EEA071E2D7EEC096BF4A8B06 (RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = ___0_value;
		__this->___U3CWeightU3Ek__BackingField = L_0;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngItem_Weight__ctor_m2A4ADD82202D4FB0DCBB47490EA166B89CB14674 (RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* __this, const RuntimeMethod* method) 
{
	{
		RngItem__ctor_m717233576D979AB5582EC7A43BD64AAE87C747C0(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t RngItem_Individually_get_Weight_m0B639229A0ECA10AACBB40F5AFF963C3380A6006 (RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CWeightU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngItem_Individually_set_Weight_m804AD4E6E6190345D161AB6598F91D3CF7A78A6E (RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = ___0_value;
		__this->___U3CWeightU3Ek__BackingField = L_0;
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngItem_Individually__ctor_mB49CC3AB155645D5040F70619AFB1244216178E3 (RngItem_Individually_t9C13D5671626C6C6CBEE060174255247935E0E64* __this, const RuntimeMethod* method) 
{
	{
		RngItem__ctor_m717233576D979AB5582EC7A43BD64AAE87C747C0(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* RngResult_get_Items_m0C9AA21C5559B9563D6C0EBC5E891696E11F088F (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, const RuntimeMethod* method) 
{
	{
		List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* L_0 = __this->___U3CItemsU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngResult_set_Items_m9FCD04F1B5AA474384EC7B3625E7350C2DF8FC97 (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* ___0_value, const RuntimeMethod* method) 
{
	{
		List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* L_0 = ___0_value;
		__this->___U3CItemsU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CItemsU3Ek__BackingField), (void*)L_0);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngResult__ctor_mBD046D2101593735B6539386B727D4CF2FF74C4A (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m14B9A3ED375DA0902DC827C8F7D1F1ED771F85AF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* L_0 = (List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C*)il2cpp_codegen_object_new(List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C_il2cpp_TypeInfo_var);
		List_1__ctor_m14B9A3ED375DA0902DC827C8F7D1F1ED771F85AF(L_0, List_1__ctor_m14B9A3ED375DA0902DC827C8F7D1F1ED771F85AF_RuntimeMethod_var);
		RngResult_set_Items_m9FCD04F1B5AA474384EC7B3625E7350C2DF8FC97_inline(__this, L_0, NULL);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngResult_Add_m88793C00B649731FA7AB99828DC367ADD0132665 (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* ___0_item, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m4723DDC6BB675D0B90840C3A394BBB854B5CE8DA_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* L_0;
		L_0 = RngResult_get_Items_m0C9AA21C5559B9563D6C0EBC5E891696E11F088F_inline(__this, NULL);
		RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* L_1 = ___0_item;
		NullCheck(L_0);
		List_1_Add_m4723DDC6BB675D0B90840C3A394BBB854B5CE8DA_inline(L_0, L_1, List_1_Add_m4723DDC6BB675D0B90840C3A394BBB854B5CE8DA_RuntimeMethod_var);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RngResult_Dispose_m465F2CB1C9B225B64F27B9BEAD59B35E331AE524 (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Clear_m038BAB2EE3A8E697C1CEE286DE13011D9CA39DC2_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* L_0;
		L_0 = RngResult_get_Items_m0C9AA21C5559B9563D6C0EBC5E891696E11F088F_inline(__this, NULL);
		NullCheck(L_0);
		List_1_Clear_m038BAB2EE3A8E697C1CEE286DE13011D9CA39DC2_inline(L_0, List_1_Clear_m038BAB2EE3A8E697C1CEE286DE13011D9CA39DC2_RuntimeMethod_var);
		RngResult_set_Items_m9FCD04F1B5AA474384EC7B3625E7350C2DF8FC97_inline(__this, (List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C*)NULL, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IRngRandom__ctor_m5FEE0413CC0F18388AE62312F8D65BC2CF1329E8 (IRngRandom_t31B0315449676429B032761C6915795F7EA4701C* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t WeightedRandom_CalculateWeight_m0048F46984FDF218E8031DB8F7576C251762AC57 (WeightedRandom_tAA23F7C4FABBEEDD80A480A52950981A0087D087* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RngItemTable_1_CalculateWeight_mB9795FA83E990FFD09B8DC1BD228E9B35F72EA64_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* L_0 = __this->___itemTable;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = RngItemTable_1_CalculateWeight_mB9795FA83E990FFD09B8DC1BD228E9B35F72EA64(L_0, RngItemTable_1_CalculateWeight_mB9795FA83E990FFD09B8DC1BD228E9B35F72EA64_RuntimeMethod_var);
		return L_1;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* WeightedRandom_GetRandomDrop_mCD86FEAC037C7081FE4FA5E459484C0B3D61CDC5 (WeightedRandom_tAA23F7C4FABBEEDD80A480A52950981A0087D087* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_mA8BCFB7BDAEC426E827218FBAFE14EA339621CAB_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m573337375B14071EF77F7E63B5AAD662053279D3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCC0A01905537053EEA18C3A51919C043749C77DD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m4723DDC6BB675D0B90840C3A394BBB854B5CE8DA_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_mF330E028FF966DD513137B78EA74EED6043EFB44_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_m93614A840363DCCC8B8E0111DAF51B3B13661A0D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4 V_2;
	memset((&V_2), 0, sizeof(V_2));
	RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* V_3 = NULL;
	RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* V_4 = NULL;
	{
		RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* L_0 = __this->___itemTable;
		NullCheck(L_0);
		List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12* L_1 = L_0->___items;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = List_1_get_Count_m93614A840363DCCC8B8E0111DAF51B3B13661A0D_inline(L_1, List_1_get_Count_m93614A840363DCCC8B8E0111DAF51B3B13661A0D_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_001d;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral254511C12007A18F9311F47EA8BAF6CAEFD53C67)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&WeightedRandom_GetRandomDrop_mCD86FEAC037C7081FE4FA5E459484C0B3D61CDC5_RuntimeMethod_var)));
	}

IL_001d:
	{
		Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8* L_4 = (Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8*)il2cpp_codegen_object_new(Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_il2cpp_TypeInfo_var);
		Random__ctor_m151183BD4F021499A98B9DE8502DAD4B12DD16AC(L_4, NULL);
		RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* L_5 = __this->___itemTable;
		NullCheck(L_5);
		int32_t L_6 = L_5->___TotalWeight;
		NullCheck(L_4);
		int32_t L_7;
		L_7 = VirtualFuncInvoker2< int32_t, int32_t, int32_t >::Invoke(6, L_4, 0, L_6);
		V_0 = L_7;
		V_1 = 0;
		RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* L_8 = __this->___itemTable;
		NullCheck(L_8);
		List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12* L_9 = L_8->___items;
		NullCheck(L_9);
		Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4 L_10;
		L_10 = List_1_GetEnumerator_mF330E028FF966DD513137B78EA74EED6043EFB44(L_9, List_1_GetEnumerator_mF330E028FF966DD513137B78EA74EED6043EFB44_RuntimeMethod_var);
		V_2 = L_10;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_007e:
			{
				Enumerator_Dispose_mA8BCFB7BDAEC426E827218FBAFE14EA339621CAB((&V_2), Enumerator_Dispose_mA8BCFB7BDAEC426E827218FBAFE14EA339621CAB_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_0073_1;
			}

IL_0049_1:
			{
				RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* L_11;
				L_11 = Enumerator_get_Current_mCC0A01905537053EEA18C3A51919C043749C77DD_inline((&V_2), Enumerator_get_Current_mCC0A01905537053EEA18C3A51919C043749C77DD_RuntimeMethod_var);
				V_3 = L_11;
				int32_t L_12 = V_1;
				RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* L_13 = V_3;
				NullCheck(L_13);
				int32_t L_14;
				L_14 = VirtualFuncInvoker0< int32_t >::Invoke(4, L_13);
				V_1 = ((int32_t)il2cpp_codegen_add(L_12, L_14));
				int32_t L_15 = V_0;
				int32_t L_16 = V_1;
				if ((((int32_t)L_15) >= ((int32_t)L_16)))
				{
					goto IL_0073_1;
				}
			}
			{
				RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_17 = (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348*)il2cpp_codegen_object_new(RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348_il2cpp_TypeInfo_var);
				RngResult__ctor_mBD046D2101593735B6539386B727D4CF2FF74C4A(L_17, NULL);
				RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_18 = L_17;
				NullCheck(L_18);
				List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* L_19;
				L_19 = RngResult_get_Items_m0C9AA21C5559B9563D6C0EBC5E891696E11F088F_inline(L_18, NULL);
				RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* L_20 = V_3;
				NullCheck(L_19);
				List_1_Add_m4723DDC6BB675D0B90840C3A394BBB854B5CE8DA_inline(L_19, L_20, List_1_Add_m4723DDC6BB675D0B90840C3A394BBB854B5CE8DA_RuntimeMethod_var);
				V_4 = L_18;
				goto IL_0097;
			}

IL_0073_1:
			{
				bool L_21;
				L_21 = Enumerator_MoveNext_m573337375B14071EF77F7E63B5AAD662053279D3((&V_2), Enumerator_MoveNext_m573337375B14071EF77F7E63B5AAD662053279D3_RuntimeMethod_var);
				if (L_21)
				{
					goto IL_0049_1;
				}
			}
			{
				goto IL_008c;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_008c:
	{
		Exception_t* L_22 = (Exception_t*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)));
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_22, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral85F7391ABDBA5999E3E0583FF27BD6D7A96DA970)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_22, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&WeightedRandom_GetRandomDrop_mCD86FEAC037C7081FE4FA5E459484C0B3D61CDC5_RuntimeMethod_var)));
	}

IL_0097:
	{
		RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_23 = V_4;
		return L_23;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WeightedRandom_RandomDrop_mF31A3D6D133677CB2208D84DF5483A5CA091611B (WeightedRandom_tAA23F7C4FABBEEDD80A480A52950981A0087D087* __this, RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* ___0_result, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_mA8BCFB7BDAEC426E827218FBAFE14EA339621CAB_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m573337375B14071EF77F7E63B5AAD662053279D3_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_mCC0A01905537053EEA18C3A51919C043749C77DD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_mF330E028FF966DD513137B78EA74EED6043EFB44_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_m93614A840363DCCC8B8E0111DAF51B3B13661A0D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4 V_2;
	memset((&V_2), 0, sizeof(V_2));
	RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* V_3 = NULL;
	{
		RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* L_0 = __this->___itemTable;
		NullCheck(L_0);
		List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12* L_1 = L_0->___items;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = List_1_get_Count_m93614A840363DCCC8B8E0111DAF51B3B13661A0D_inline(L_1, List_1_get_Count_m93614A840363DCCC8B8E0111DAF51B3B13661A0D_RuntimeMethod_var);
		if (L_2)
		{
			goto IL_001d;
		}
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_3 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_3, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral254511C12007A18F9311F47EA8BAF6CAEFD53C67)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&WeightedRandom_RandomDrop_mF31A3D6D133677CB2208D84DF5483A5CA091611B_RuntimeMethod_var)));
	}

IL_001d:
	{
		Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8* L_4 = (Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8*)il2cpp_codegen_object_new(Random_t79716069EDE67D1D7734F60AE402D0CA3FB6B4C8_il2cpp_TypeInfo_var);
		Random__ctor_m151183BD4F021499A98B9DE8502DAD4B12DD16AC(L_4, NULL);
		RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* L_5 = __this->___itemTable;
		NullCheck(L_5);
		int32_t L_6 = L_5->___TotalWeight;
		NullCheck(L_4);
		int32_t L_7;
		L_7 = VirtualFuncInvoker2< int32_t, int32_t, int32_t >::Invoke(6, L_4, 0, L_6);
		V_0 = L_7;
		V_1 = 0;
		RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* L_8 = __this->___itemTable;
		NullCheck(L_8);
		List_1_t322A66105C3B0109505CD72A22E15CFBFC864A12* L_9 = L_8->___items;
		NullCheck(L_9);
		Enumerator_tA52F9AABBBB8C171D8CC30E304FD95B0E3442FE4 L_10;
		L_10 = List_1_GetEnumerator_mF330E028FF966DD513137B78EA74EED6043EFB44(L_9, List_1_GetEnumerator_mF330E028FF966DD513137B78EA74EED6043EFB44_RuntimeMethod_var);
		V_2 = L_10;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0072:
			{
				Enumerator_Dispose_mA8BCFB7BDAEC426E827218FBAFE14EA339621CAB((&V_2), Enumerator_Dispose_mA8BCFB7BDAEC426E827218FBAFE14EA339621CAB_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_0067_1;
			}

IL_0049_1:
			{
				RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* L_11;
				L_11 = Enumerator_get_Current_mCC0A01905537053EEA18C3A51919C043749C77DD_inline((&V_2), Enumerator_get_Current_mCC0A01905537053EEA18C3A51919C043749C77DD_RuntimeMethod_var);
				V_3 = L_11;
				int32_t L_12 = V_1;
				RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* L_13 = V_3;
				NullCheck(L_13);
				int32_t L_14;
				L_14 = VirtualFuncInvoker0< int32_t >::Invoke(4, L_13);
				V_1 = ((int32_t)il2cpp_codegen_add(L_12, L_14));
				int32_t L_15 = V_0;
				int32_t L_16 = V_1;
				if ((((int32_t)L_15) >= ((int32_t)L_16)))
				{
					goto IL_0067_1;
				}
			}
			{
				RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* L_17 = ___0_result;
				RngItem_Weight_t5E7D0BFFF5521C986DEB85258838CF90F103C72F* L_18 = V_3;
				NullCheck(L_17);
				RngResult_Add_m88793C00B649731FA7AB99828DC367ADD0132665(L_17, L_18, NULL);
				goto IL_008b;
			}

IL_0067_1:
			{
				bool L_19;
				L_19 = Enumerator_MoveNext_m573337375B14071EF77F7E63B5AAD662053279D3((&V_2), Enumerator_MoveNext_m573337375B14071EF77F7E63B5AAD662053279D3_RuntimeMethod_var);
				if (L_19)
				{
					goto IL_0049_1;
				}
			}
			{
				goto IL_0080;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0080:
	{
		Exception_t* L_20 = (Exception_t*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)));
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_20, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral85F7391ABDBA5999E3E0583FF27BD6D7A96DA970)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_20, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&WeightedRandom_RandomDrop_mF31A3D6D133677CB2208D84DF5483A5CA091611B_RuntimeMethod_var)));
	}

IL_008b:
	{
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void WeightedRandom__ctor_m71FEF462E0DB8626CE7FAA6C66B9F452DF186822 (WeightedRandom_tAA23F7C4FABBEEDD80A480A52950981A0087D087* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RngItemTable_1__ctor_m2FB34A0C332AF7236DE875AFF1EF98D8BE7EEC22_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766* L_0 = (RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766*)il2cpp_codegen_object_new(RngItemTable_1_t3C69DD751BC1004718393DA1D0362239A9119766_il2cpp_TypeInfo_var);
		RngItemTable_1__ctor_m2FB34A0C332AF7236DE875AFF1EF98D8BE7EEC22(L_0, RngItemTable_1__ctor_m2FB34A0C332AF7236DE875AFF1EF98D8BE7EEC22_RuntimeMethod_var);
		__this->___itemTable = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___itemTable), (void*)L_0);
		IRngRandom__ctor_m5FEE0413CC0F18388AE62312F8D65BC2CF1329E8(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* RngItem_get_Obj_mE0FF47C5419BD898FC0286BE7B4E72F1A1ACDB4C_inline (RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___U3CObjU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void RngItem_set_Obj_mFE20246916C4E8277E53D4E9D7CEC82FA0D06028_inline (RngItem_t998A96C1AFF56B1E188F887DC9C7B7E1C83C0399* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = ___0_value;
		__this->___U3CObjU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CObjU3Ek__BackingField), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void RngResult_set_Items_m9FCD04F1B5AA474384EC7B3625E7350C2DF8FC97_inline (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* ___0_value, const RuntimeMethod* method) 
{
	{
		List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* L_0 = ___0_value;
		__this->___U3CItemsU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CItemsU3Ek__BackingField), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* RngResult_get_Items_m0C9AA21C5559B9563D6C0EBC5E891696E11F088F_inline (RngResult_t20D49D1E2561AE54384CF60C50096241BF20A348* __this, const RuntimeMethod* method) 
{
	{
		List_1_tED72132AC05D6C3954D988CA9F7D6AC174E8E28C* L_0 = __this->___U3CItemsU3Ek__BackingField;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____size;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->____current;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = V_0;
		NullCheck(L_4);
		if ((!(((uint32_t)L_3) < ((uint32_t)((int32_t)(((RuntimeArray*)L_4)->max_length))))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_5 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_5, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = V_0;
		int32_t L_7 = V_1;
		RuntimeObject* L_8 = ___0_item;
		NullCheck(L_6);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(L_7), (RuntimeObject*)L_8);
		return;
	}

IL_0034:
	{
		RuntimeObject* L_9 = ___0_item;
		List_1_AddWithResize_m79A9BF770BEF9C06BE40D5401E55E375F2726CC4(__this, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 14));
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
	}
	{
		int32_t L_1 = __this->____size;
		V_0 = L_1;
		__this->____size = 0;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) <= ((int32_t)0)))
		{
			goto IL_003c;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = __this->____items;
		int32_t L_4 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_3, 0, L_4, NULL);
		return;
	}

IL_003c:
	{
		return;
	}
}
