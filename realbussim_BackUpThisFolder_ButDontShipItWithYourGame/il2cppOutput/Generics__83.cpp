#include "pch-cpp.hpp"





struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*,const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj,invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1Invoker;
template <typename T1>
struct InterfaceActionInvoker1Invoker<T1*>
{
	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1* p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		void* params[1] = { p1 };
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, params, params[0]);
	}
};
template <typename T1, typename T2>
struct InterfaceActionInvoker2
{
	typedef void (*Action)(void*,T1,T2,const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj,p1,p2,invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*,const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj,invokeData.method);
	}
};
template <typename T1>
struct InvokerActionInvoker1;
template <typename T1>
struct InvokerActionInvoker1<T1*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1)
	{
		void* params[1] = { p1 };
		method->invoker_method(methodPtr, method, obj, params, params[0]);
	}
};
template <typename T1, typename T2>
struct InvokerActionInvoker2;
template <typename T1, typename T2>
struct InvokerActionInvoker2<T1, T2*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1 p1, T2* p2)
	{
		void* params[2] = { &p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, params[1]);
	}
};
template <typename R, typename T1>
struct InvokerFuncInvoker1;
template <typename R, typename T1>
struct InvokerFuncInvoker1<R, T1*>
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1)
	{
		R ret;
		void* params[1] = { p1 };
		method->invoker_method(methodPtr, method, obj, params, &ret);
		return ret;
	}
};
template <typename R, typename T1, typename T2, typename T3, typename T4>
struct InvokerFuncInvoker4;
template <typename R, typename T1, typename T2, typename T3, typename T4>
struct InvokerFuncInvoker4<R, T1, T2, T3*, T4*>
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1 p1, T2 p2, T3* p3, T4* p4)
	{
		R ret;
		void* params[4] = { &p1, &p2, p3, p4 };
		method->invoker_method(methodPtr, method, obj, params, &ret);
		return ret;
	}
};
template <typename R, typename T1, typename T2, typename T3, typename T4>
struct InvokerFuncInvoker4<R, T1*, T2*, T3, T4>
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2, T3 p3, T4 p4)
	{
		R ret;
		void* params[4] = { p1, p2, &p3, &p4 };
		method->invoker_method(methodPtr, method, obj, params, &ret);
		return ret;
	}
};
template <typename R, typename T1, typename T2, typename T3, typename T4, typename T5>
struct InvokerFuncInvoker5;
template <typename R, typename T1, typename T2, typename T3, typename T4, typename T5>
struct InvokerFuncInvoker5<R, T1*, T2, T3, T4*, T5*>
{
	static inline R Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2 p2, T3 p3, T4* p4, T5* p5)
	{
		R ret;
		void* params[5] = { p1, &p2, &p3, p4, p5 };
		method->invoker_method(methodPtr, method, obj, params, &ret);
		return ret;
	}
};

struct Action_1_tC8CC18F6848AAE74469E78A12EC004873F4332A7;
struct Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505;
struct Action_1_t923A20D1D4F6B55B2ED5AE21B90F1A0CE0450D99;
struct Action_1_t5FD82308A537DD6AD12A51AF0790FA13C5BE2D65;
struct Action_1_t6E017BD45EFAD0010771D0174C3A5653E776128E;
struct Action_1_t9BCEE3E1405DA67DBE5A4E9F0882507B63BCEC2F;
struct Action_1_t7D3408AF7B9CB908929DA7A8A01F6231B5EE5413;
struct Action_1_t24D24AE7BDA27ABE5E9058347421CD45845804B3;
struct Action_1_t43B3A21E3139B3E2E49698F940F979F37AD1E63E;
struct Comparison_1_t55FCE173570442A2D451AE8BFC15B22D41D55DC9;
struct Comparison_1_tC9FBFF747FA0455CD35BD7D097691DE20DDCC2B5;
struct Comparison_1_tE94B5C4AE59DE10B7A54AD7C3291462A05D5CD49;
struct Comparison_1_t0F283DBF0EC45EE18464332F2713E3E393EED6C3;
struct Comparison_1_t3C004EF64D01B2F9DC8BB66F28EE82E69221F131;
struct Comparison_1_tEB713FAFEA92D337C218C257736265B7A7EDF135;
struct Comparison_1_tD83994F0AC2F81C51937890F172D6969927BB246;
struct Comparison_1_t53C7968EAD02BE23D913BD2C7F7F58BBBDD8D096;
struct Comparison_1_tC8BBA462D5337723AC34773EEE343018753C7FF3;
struct ICollection_1_t191C893BDB2C098042ED421B3FEB9C45162A4D0B;
struct ICollection_1_t58D5B5A564BF08CD3C8EE106100AC0D7B8C4508B;
struct ICollection_1_tB388ED908E2D241F56264CA971F25D9ADC1ADEC3;
struct ICollection_1_t7E71F1CA7971B264996E675DD23421D9B7165B6A;
struct ICollection_1_tF5B970262711DD81554F22B7853BC212B91004A5;
struct ICollection_1_tBDE3930F273F618DCA72B40DC565E50ECB42ED8A;
struct ICollection_1_t7062615CB0B105587427949AB42915A7568EDBF0;
struct ICollection_1_tA62A86C840971AC7E1018DED53CE9ECDE4B25035;
struct ICollection_1_t17FF839BF7E6E58666DFD1EF4BA5F4D216E5C16E;
struct IComparer_1_t12FDD9882EC7ECD970F65BAE66029647C62E55C5;
struct IComparer_1_t3B0F39D062C0830CA6D3DD5C771FEF9D2E6F5DEF;
struct IComparer_1_tCA720E7837F12C38065A4AB632B5FAAD19EC30AA;
struct IComparer_1_t9A1CC2C8D88CCF81FC46F2CAF5CF3E1D9B82EFF9;
struct IComparer_1_t0B4FFBB77C1E8EB4AE468F02F10EFA6E6CBDC7E7;
struct IComparer_1_tF938F2B5DA76A37749CB99F5C6FBA00E209C3B7B;
struct IComparer_1_t7FC52FE45B0AD22E081CA2681C0C6F8554DA9D00;
struct IComparer_1_t65DE0390A9D7CDEE5D1FA583A8AAC417A8B00F3F;
struct IComparer_1_t5C8ADB6DF803417A1731826FA07541C9E165AB71;
struct IEnumerable_1_tDDB768CC2EC358D9530029011A515F5583CA0015;
struct IEnumerable_1_t6D5FFB1B4A97F30426C5D394135AD0D2FAEAE680;
struct IEnumerable_1_t29E7244AE33B71FA0981E50D5BC73B7938F35C66;
struct IEnumerable_1_t9C73E2DA2A24734B7BEE3997930B156A82172C56;
struct IEnumerable_1_t7F8FE45EEF4D9F99F029D4D62B8190C000927977;
struct IEnumerable_1_t978278F7B7CC1DC77F2F0E9F9368D5F432B8520D;
struct IEnumerable_1_tB7D145B175A3E61E3FB743ADFAC1B649B3D887F9;
struct IEnumerable_1_tEA49AFF11AE5182E575D731B33B5BBEE9EB916BD;
struct IEnumerable_1_t616D8494D191E02CD416B710E4C06389401F060C;
struct IEnumerator_1_tFD92D349677E89FA94B8AEC66F76C0041590FEDB;
struct IEnumerator_1_tB41B292FAFD525FC5C54D7E469F0BA1A193CA8B1;
struct IEnumerator_1_t75CB2681E18F7F2791528FA2CA60361FDB5DA08D;
struct IEnumerator_1_tF74C952F97E24A4AFD21162637D82942513CB35B;
struct IEnumerator_1_t70473736C2E860779B5778F060AD52DB508F1783;
struct IEnumerator_1_t6E08816C74C9EE60AA92FC8327FE3102C9FD491B;
struct IEnumerator_1_tBA60D05392C0A7C85CB6BA1567984A5737545CA5;
struct IEnumerator_1_tD347B69D3F9B752863168A9BA3E7E7A42DBAEC81;
struct IEnumerator_1_tFA909B65895E40AAB1C8DD427FCA2D3EE869603F;
struct IList_1_t82892C93D8E03E68BB11DCD20470A61C1188C7B5;
struct IList_1_t7DC7A22F42EEFA1E8D796516A9F22282DF4EB5FA;
struct IList_1_t8AC59FFD0F90EC2DAD4C3FA39B2A5851F9D2987A;
struct IList_1_t4D7D7DAB02DB631C876CF3E68462F0FD9F9A8985;
struct IList_1_t723F6D5137B11A931DA716028E7AB780AB396C40;
struct IList_1_t2A2061D17084BF886A6254DD2794AC58D2F5461F;
struct IList_1_tACFCA9E565A19C3136A49A3D192A849510C81D87;
struct IList_1_t166E7698F41C5D0654B5D8B28A6701DCEEBCB91F;
struct IList_1_t7B3BE05FF22559A6D63B8878D1E20E1F73864551;
struct List_1_t41896DF347A129D13205BBFADE03DFB21E05E459;
struct List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB;
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E;
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A;
struct List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255;
struct List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75;
struct List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6;
struct List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2;
struct List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50;
struct List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A;
struct Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5;
struct Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88;
struct Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107;
struct Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2;
struct Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716;
struct Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA;
struct Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1;
struct Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C;
struct Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0;
struct ReadOnlyCollection_1_t803A81BE343B15275BCD30A219ACAC97F0B9A1C8;
struct ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263;
struct ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8;
struct ReadOnlyCollection_1_tA097AD399F1ED95CBDEFC0663790A10C4A43DFB8;
struct ReadOnlyCollection_1_t57A9EA1D0B6B2A5908DD58EA8C84F29F53D509A1;
struct ReadOnlyCollection_1_t1D742B1E1C22078FEF5E998EDD21A4405B13B076;
struct ReadOnlyCollection_1_tEFFE9324903FBAB1A9A712578488B71F4CBC3BF5;
struct ReadOnlyCollection_1_t0072B9B1BE9DDF9C8BF0ABE59E7EEBD11BFAA293;
struct ReadOnlyCollection_1_t00EFCE24C7E2AF195B108C1FC1E1BE4F7007EC1E;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7;
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
struct __Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297;
struct __Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43;
struct float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251;
struct WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80;
struct AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560;
struct OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88;
struct Awaitable_t690337FEC1C411606E233EA36A41337B931C23CB;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct MethodInfo_t;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct String_t;
struct Type_t;
struct UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RuntimeObject_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7;
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
struct __Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297;
struct __Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43;
struct float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251;
struct WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80;
struct AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560;
struct OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB  : public RuntimeObject
{
	XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E  : public RuntimeObject
{
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A  : public RuntimeObject
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255  : public RuntimeObject
{
	__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75  : public RuntimeObject
{
	__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6  : public RuntimeObject
{
	float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2  : public RuntimeObject
{
	WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50  : public RuntimeObject
{
	AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A  : public RuntimeObject
{
	OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t803A81BE343B15275BCD30A219ACAC97F0B9A1C8  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_tA097AD399F1ED95CBDEFC0663790A10C4A43DFB8  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t57A9EA1D0B6B2A5908DD58EA8C84F29F53D509A1  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t1D742B1E1C22078FEF5E998EDD21A4405B13B076  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_tEFFE9324903FBAB1A9A712578488B71F4CBC3BF5  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t0072B9B1BE9DDF9C8BF0ABE59E7EEBD11BFAA293  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t00EFCE24C7E2AF195B108C1FC1E1BE4F7007EC1E  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct MemberInfo_t  : public RuntimeObject
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
struct Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9 
{
	List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* ____list;
	int32_t ____index;
	int32_t ____version;
	Il2CppSharedGenericObject* ____current;
};
typedef Il2CppFullySharedGenericStruct Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF;
struct Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6 
{
	List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* ____list;
	int32_t ____index;
	int32_t ____version;
	int32_t ____current;
};
struct Enumerator_t474D057FE73061C816292BC377C411C94495375A 
{
	List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* ____list;
	int32_t ____index;
	int32_t ____version;
	uint32_t ____current;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
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
struct float3_t7600B73F092B37F484B12910A5269F30C778D31E 
{
	float ___x;
	float ___y;
	float ___z;
};
struct AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 
{
	Awaitable_t690337FEC1C411606E233EA36A41337B931C23CB* ___U3CAwaitableU3Ek__BackingField;
	int32_t ___U3CFrameIndexU3Ek__BackingField;
};
struct AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_marshaled_pinvoke
{
	Awaitable_t690337FEC1C411606E233EA36A41337B931C23CB* ___U3CAwaitableU3Ek__BackingField;
	int32_t ___U3CFrameIndexU3Ek__BackingField;
};
struct AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_marshaled_com
{
	Awaitable_t690337FEC1C411606E233EA36A41337B931C23CB* ___U3CAwaitableU3Ek__BackingField;
	int32_t ___U3CFrameIndexU3Ek__BackingField;
};
struct OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 
{
	int32_t ___order;
	UnityAction_t11A1F3B953B365C072A5DCC32677EE1796A962A7* ___callback;
};
struct OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_marshaled_pinvoke
{
	int32_t ___order;
	Il2CppMethodPointer ___callback;
};
struct OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_marshaled_com
{
	int32_t ___order;
	Il2CppMethodPointer ___callback;
};
struct Enumerator_t92A9B1D618B36DF47EC28CBA0FFF9405BE75926E 
{
	List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* ____list;
	int32_t ____index;
	int32_t ____version;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ____current;
};
struct Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5 
{
	List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* ____list;
	int32_t ____index;
	int32_t ____version;
	AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ____current;
};
struct Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251 
{
	List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* ____list;
	int32_t ____index;
	int32_t ____version;
	OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ____current;
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
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
struct ExceptionArgument_t60E7F8D9DE5362CBE9365893983C30302D83B778 
{
	int32_t ___value__;
};
struct ExceptionResource_t609A85E253A4E615583553D91D839E2E79FDFBD9 
{
	int32_t ___value__;
};
struct FeatureType_t794EFAD24C687FA0CDC5A313581A39F5CB61CC41 
{
	int32_t ___value__;
};
struct PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 
{
	intptr_t ___m_Handle;
	uint32_t ___m_Version;
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F 
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Handle;
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 
{
	String_t* ___name;
	List_1_t41896DF347A129D13205BBFADE03DFB21E05E459* ___usageHints;
	int32_t ___featureType;
	uint32_t ___customSize;
};
struct XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_marshaled_pinvoke
{
	char* ___name;
	List_1_t41896DF347A129D13205BBFADE03DFB21E05E459* ___usageHints;
	int32_t ___featureType;
	uint32_t ___customSize;
};
struct XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_marshaled_com
{
	Il2CppChar* ___name;
	List_1_t41896DF347A129D13205BBFADE03DFB21E05E459* ___usageHints;
	int32_t ___featureType;
	uint32_t ___customSize;
};
struct Action_1_tC8CC18F6848AAE74469E78A12EC004873F4332A7  : public MulticastDelegate_t
{
};
struct Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505  : public MulticastDelegate_t
{
};
struct Action_1_t923A20D1D4F6B55B2ED5AE21B90F1A0CE0450D99  : public MulticastDelegate_t
{
};
struct Action_1_t5FD82308A537DD6AD12A51AF0790FA13C5BE2D65  : public MulticastDelegate_t
{
};
struct Action_1_t6E017BD45EFAD0010771D0174C3A5653E776128E  : public MulticastDelegate_t
{
};
struct Action_1_t9BCEE3E1405DA67DBE5A4E9F0882507B63BCEC2F  : public MulticastDelegate_t
{
};
struct Action_1_t24D24AE7BDA27ABE5E9058347421CD45845804B3  : public MulticastDelegate_t
{
};
struct Action_1_t43B3A21E3139B3E2E49698F940F979F37AD1E63E  : public MulticastDelegate_t
{
};
struct Comparison_1_t55FCE173570442A2D451AE8BFC15B22D41D55DC9  : public MulticastDelegate_t
{
};
struct Comparison_1_tC9FBFF747FA0455CD35BD7D097691DE20DDCC2B5  : public MulticastDelegate_t
{
};
struct Comparison_1_tE94B5C4AE59DE10B7A54AD7C3291462A05D5CD49  : public MulticastDelegate_t
{
};
struct Comparison_1_t0F283DBF0EC45EE18464332F2713E3E393EED6C3  : public MulticastDelegate_t
{
};
struct Comparison_1_t3C004EF64D01B2F9DC8BB66F28EE82E69221F131  : public MulticastDelegate_t
{
};
struct Comparison_1_tEB713FAFEA92D337C218C257736265B7A7EDF135  : public MulticastDelegate_t
{
};
struct Comparison_1_t53C7968EAD02BE23D913BD2C7F7F58BBBDD8D096  : public MulticastDelegate_t
{
};
struct Comparison_1_tC8BBA462D5337723AC34773EEE343018753C7FF3  : public MulticastDelegate_t
{
};
struct Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0 
{
	List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* ____list;
	int32_t ____index;
	int32_t ____version;
	XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ____current;
};
struct Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5  : public MulticastDelegate_t
{
};
struct Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88  : public MulticastDelegate_t
{
};
struct Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107  : public MulticastDelegate_t
{
};
struct Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2  : public MulticastDelegate_t
{
};
struct Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716  : public MulticastDelegate_t
{
};
struct Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA  : public MulticastDelegate_t
{
};
struct Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C  : public MulticastDelegate_t
{
};
struct Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0  : public MulticastDelegate_t
{
};
struct ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 
{
	Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F ___mixer;
	Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F ___parentMixer;
	int32_t ___port;
};
struct Action_1_t7D3408AF7B9CB908929DA7A8A01F6231B5EE5413  : public MulticastDelegate_t
{
};
struct Comparison_1_tD83994F0AC2F81C51937890F172D6969927BB246  : public MulticastDelegate_t
{
};
struct Enumerator_t848512BD3FDC8088BBB0EFAC6CE52E9F2EDF4B18 
{
	List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* ____list;
	int32_t ____index;
	int32_t ____version;
	WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ____current;
};
struct Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1  : public MulticastDelegate_t
{
};
struct List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB_StaticFields
{
	XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___s_emptyArray;
};
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields
{
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___s_emptyArray;
};
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___s_emptyArray;
};
struct List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255_StaticFields
{
	__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___s_emptyArray;
};
struct List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75_StaticFields
{
	__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___s_emptyArray;
};
struct List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6_StaticFields
{
	float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___s_emptyArray;
};
struct List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2_StaticFields
{
	WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___s_emptyArray;
};
struct List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50_StaticFields
{
	AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___s_emptyArray;
};
struct List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A_StaticFields
{
	OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___s_emptyArray;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct float3_t7600B73F092B37F484B12910A5269F30C778D31E_StaticFields
{
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___zero;
};
struct Type_t_StaticFields
{
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder;
	Il2CppChar ___Delimiter;
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes;
	RuntimeObject* ___Missing;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7  : public RuntimeArray
{
	ALIGN_FIELD (8) XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 m_Items[1];

	inline XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___name), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___usageHints), (void*)NULL);
		#endif
	}
	inline XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___name), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___usageHints), (void*)NULL);
		#endif
	}
};
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979  : public RuntimeArray
{
	ALIGN_FIELD (8) Il2CppSharedGenericObject* m_Items[1];

	inline Il2CppSharedGenericObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Il2CppSharedGenericObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Il2CppSharedGenericObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline Il2CppSharedGenericObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Il2CppSharedGenericObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Il2CppSharedGenericObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + il2cpp_array_calc_byte_offset(this, index);
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + il2cpp_array_calc_byte_offset(this, index);
	}
};
struct __Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297  : public RuntimeArray
{
	ALIGN_FIELD (8) int32_t m_Items[1];

	inline int32_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline int32_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, int32_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline int32_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline int32_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, int32_t value)
	{
		m_Items[index] = value;
	}
};
struct __Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43  : public RuntimeArray
{
	ALIGN_FIELD (8) uint32_t m_Items[1];

	inline uint32_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint32_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint32_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint32_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint32_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint32_t value)
	{
		m_Items[index] = value;
	}
};
struct float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251  : public RuntimeArray
{
	ALIGN_FIELD (8) float3_t7600B73F092B37F484B12910A5269F30C778D31E m_Items[1];

	inline float3_t7600B73F092B37F484B12910A5269F30C778D31E GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline float3_t7600B73F092B37F484B12910A5269F30C778D31E* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, float3_t7600B73F092B37F484B12910A5269F30C778D31E value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline float3_t7600B73F092B37F484B12910A5269F30C778D31E GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline float3_t7600B73F092B37F484B12910A5269F30C778D31E* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, float3_t7600B73F092B37F484B12910A5269F30C778D31E value)
	{
		m_Items[index] = value;
	}
};
struct WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80  : public RuntimeArray
{
	ALIGN_FIELD (8) WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 m_Items[1];

	inline WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 value)
	{
		m_Items[index] = value;
	}
};
struct AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560  : public RuntimeArray
{
	ALIGN_FIELD (8) AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 m_Items[1];

	inline AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___U3CAwaitableU3Ek__BackingField), (void*)NULL);
	}
	inline AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___U3CAwaitableU3Ek__BackingField), (void*)NULL);
	}
};
struct OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88  : public RuntimeArray
{
	ALIGN_FIELD (8) OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 m_Items[1];

	inline OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___callback), (void*)NULL);
	}
	inline OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___callback), (void*)NULL);
	}
};


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m6C3A37912654122DD8AE143EFEB1C2A4C1015C29 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 List_1_get_Item_m4C81AE785745E7C9A726104CD2F588258FBE5E8F (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mADBF15F169E5DF2762161FB96F64C99569429300 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m6C903F3CAAD0D6DA54369D2224E52DFADB242F60 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m55AFD45DC0ADD64C8749E1B59252B0BF0793CE49 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mB16BDFA4B73F390167601AF70BC65637BF9AE433 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m50157FAAF8FE0562A862140A17AD2B1097B078E7_inline (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m2555E93F7C903532A49529BCC327427FC3B1C6EE (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared (ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8* __this, RuntimeObject* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_m469FFB0730A2E7136D62A2AE6D197A1858EC6CFE (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, int32_t ___1_index, int32_t ___2_length, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m603A1422A5D4C6AC20CFDB1897BD527164CFE059 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m54DA0380F7731F2146DEDD98FC646038D9FC3923 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m72B9D7AAB1A1E284232CC6C4CF613FEA6A227584 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_mA297C73B52DACB79EAF9BABC705E6D7DB8EB7978 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m5593B7194363BCB1B716A4ECA594F6E2CC03F94C (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m14D5EB230C22B6C4A649235CC3E08C5F6561E15B (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m91BF5E52B5443EC259EE465ECFE80677DA340F98_inline (Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m3A09BF3BCAAF3C61D9A2EEF2C3090311C21A62EB (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m94DC2E52655A61CACBDD8FFD47097AAE3E618198 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m14B5F0EFFF227D076151E00CACE917B6512254B8_inline (Action_1_tC8CC18F6848AAE74469E78A12EC004873F4332A7* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m5CBA8C17D9A8E5F5E385A4876B9B1745FA3CB8C0 (Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0* __this, List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m1742E82D5AF5200DFAC69B895C4A24E80D760C4D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mECE416DA9044547B55FF326280E3AFAB15850B37 (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mBECAB59CCD69670908799776F1D6507EF7EA7D87 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m53875FA8AA50D0F755EE525314DB9818741B6E2F (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m15AEA673A2E9F2A482D49F374814A49D0734CEC6 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m3CE641659C3CD3760E5EE96E88FA7006B7D9995D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_m6953DC1CD28104B40EF7B6C79E12EDA04CCC52F3 (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m52E4077FCE9FF6DF21E5DCE7A99DACB700AB2E0D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mACACE6CA017DAF3419A4C86E329FA9CFE4018C03 (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m479DAA4C8994B2FBFA82BCC2220725F25C35DCD9 (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_t55FCE173570442A2D451AE8BFC15B22D41D55DC9* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m44B5C2177E9403DB0E3DC2BC6E590CDA7266BE79_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* List_1_get_Item_m02336594E8F76D7313E2BCB7A3ED5ABD13B4D515_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisIl2CppSharedGenericObject_mE7A1EEC53480BA1ED6C827938ADAF45B127DD054_gshared (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_mCD8F7775F9C4842958A7188EBC0EBF1BE03101EF_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, Il2CppSharedGenericObject* ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_mCC24A812DFC30C3DE2230E3C8EE705871C1DFF81_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_m89719BB37EEDA80E6AD7FAC5D1AAC5A47DDE86B5_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m0CE6E39234ED33F5195AABC8029D06D4756C75C1_gshared_inline (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m7B9D6C4AB69B4EA07C23832C6109C25C21E99804_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_TisIl2CppSharedGenericObject_m6D8A1F3C8EDCAAF226FE8B32CBB649F099D8C16B_gshared (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, int32_t ___1_index, int32_t ___2_length, Il2CppSharedGenericObject* ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mB2BE3DF9182ED9C862AA44566C878D046534474E_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, Il2CppSharedGenericObject* ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m4B13B6BE7AA6327431F1BE406264329892975DE9_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m9E5E0DF2443D88570595AEA0FEF7D7122455EB26_gshared (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m63E643DC9A821297E2EBF9085D93B39BFC2F9DCE_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m4B62593B8671BB60809057D57DC60568557315E6_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mD56F0607D7DAD9622F7DE2C6C8957D9C7847EEC5_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_mDF332C0CEB003C0D86A1E3A194ECFA96D424E34C_gshared_inline (Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* __this, Il2CppSharedGenericObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m701B15E692EF4FFE95A773C8246D55779CB00B2D_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m75AB13CBCA95A0E0722F6CDE89F0CBDAB27322DA_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mDB8F7C7381C08B4DCE4E409D2FA4073BC17F90B5_gshared_inline (Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* __this, Il2CppSharedGenericObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mB0C2B8891214015BE286DC32F260DEBDFAD317E9_gshared (Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9* __this, List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m54D719307910A9DC9CD4DEFA8AE2626F49EB7516_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisIl2CppSharedGenericObject_m0C73DE72012C676D6E4354C82EA6550BBADC3D33_gshared (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, Il2CppSharedGenericObject* ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mC5A8238645AD1D203E75313802D4DC78BDE7857E_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, Il2CppSharedGenericObject* ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m36F68EA689DB109415094085A496D5EA21E2F23C_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m0AC731A08F1400089DD478E2D366804833660796_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m97197478E9535E327B4509C065B30BB0DD6F1F99_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_TisIl2CppSharedGenericObject_mFAAB0AB623769BB8245CDF219832F2FE75427541_gshared (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m15AB808AB5390BF8B6D7A057EDA17A54A80A1A6A_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_TisIl2CppSharedGenericObject_m8C8A5C187776FEA1D5CC7F053F0DC63B32764D61_gshared (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m745CBFE82472049A441AC9522C4B8FA33F5AF970_gshared (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_tC9FBFF747FA0455CD35BD7D097691DE20DDCC2B5* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m5C66A91DC68887BE203985B56EF2F74222E9AF50_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m70DD3FB3C4E302AA0AACC45C39BCEC4C2A9DB230_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_get_Item_mEAF67AD94AB1E060C2D03D7A6820C47270E06B75_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m835718C92D2C5DA2051794F6922D21453EAC493E_gshared (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_mCDBFFAC056B77CBE377B26CAED3E893C131F4EFB_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m3B53F17525936469654379AC0F1F0B19EC3DCCE5_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mB5CE702B99DFF8E1BB13570D69ADA436D814DBBF_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m24BA4DEDED426A5B0C6A4AB3E523C876B0B918A6_gshared_inline (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m99F96BAC4A4EE626A6141612A5C6793BE37E006C_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m159F9F5904FFDBCBDB9ED09247CCE6E536AFEA23_gshared (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, int32_t ___1_index, int32_t ___2_length, int32_t ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mA5BE0B8B14B9B4F0EC39944DA4E226868121C961_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, int32_t ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m22A847FE310BE1BA056D9344E43A4E5BB88759D7_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_mD81F4794E5BA5AD31C7A25CA1B456BA04E37EC1D_gshared (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m61A085CF349BFF87A90BE6FEE413C548F3F9E567_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m7AA13FF7BE1A11F437B16EAE3E1FA50DBFC902AE_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m334C39F1B647D613EE0A8043D6B30A64B63E713C_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m0D65AFF43065FE03318CBDDB2A1E63B51476FB9A_gshared_inline (Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* __this, int32_t ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mC9F71DEB213A8D42EBC3A47798AAF6CD140438CA_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mF8BED79CAE6C97F36825E0CB1B54B4976D95ED56_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mE118EB8C2BFC9D370FE37C05DE4F2CD6CBE9DB54_gshared_inline (Action_1_t5FD82308A537DD6AD12A51AF0790FA13C5BE2D65* __this, int32_t ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m0B844022E6FED4C6DA8CD7C270989D9D34800C69_gshared (Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6* __this, List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_mBEC4BB23DEF62EFA5D66833D82CEB6FABDD96E04_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m9AE979627DAA17E8B9E3EF440A130C1B9C245059_gshared (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, int32_t ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_m8C3391D73E5396E7FD12A5DA1CEDDD0A83EAAB79_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_mBA121E6727BDC54EB17A84709997D6920BBE4D10_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_mAF82A7B6E01FEB6F200C76071F76554EE4B35DB8_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m34A5681B1296F6E4F08C9F0986F4E4F1CF23FBE4_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_mA4406A8C17BBC29625F80F783EA91C3AECF7E3EA_gshared (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m29868C70BFD9DDFB44E31BF13302FB7AB5244913_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_mE004CFE5BA4856AC83A08DEA9F09D3224F3EDD7B_gshared (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_mCDDF2BDFA2ACB2C2254C52BBD32AD774C7C05CDC_gshared (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_t0F283DBF0EC45EE18464332F2713E3E393EED6C3* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m681B8A3C4E9973D4BBAAC17D1EF146A796DF11CB_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR uint32_t List_1_get_Item_m996B509A482EA372164DE730F73C1E11BA2F3EAE_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mB550F72315A962B072051B4E80B0F9D124721798_gshared (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m156A9F0E34470C01D118D6391E192E7D54189C0F_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, uint32_t ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m5D090D0E0EFAAA54D1A7D12657EE18BA7AD0F445_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_m832EAA18FAB897AE9CE65E99ED96EF173701E507_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mBDE95F2D32400D0DABA22511FE053AC8957D8D1E_gshared_inline (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m694C6D98C9BDD66E139B4FBDC95D06372CF2869E_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m30014505CD15EFAF86A6FBE09AF0A55BD9377B5F_gshared (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, int32_t ___1_index, int32_t ___2_length, uint32_t ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m15C2E0B120676758F22A35BED084390D6A444A09_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, uint32_t ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m2FB5B4F89323CDCB893E3BB9C2E256169024B872_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_mED20F2A2B89C0BFF977AA52105F288790EEF484F_gshared (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_mD62DF0807CE1CF205884252DA7D06B9EDACF5512_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mCA75F83CA2F20BC02FFECCA387D4C021A2D83511_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m5DFECA19BA1FF082CEE8E8EE854277501A214F17_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m2603DCEFACCFCB054A6BC33E68531E62F317C8A4_gshared_inline (Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* __this, uint32_t ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mA09105FDEDF74F29015B15E28F5E73BA9C4BCAFE_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mAC723FB10B249538A74DB3B20E203F0EF3764C88_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m124DA039CD5C63AD48841B21065DFC6C03E63930_gshared_inline (Action_1_t6E017BD45EFAD0010771D0174C3A5653E776128E* __this, uint32_t ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m48AA10A08952E197C1709A3F3AE09FB34BE212C4_gshared (Enumerator_t474D057FE73061C816292BC377C411C94495375A* __this, List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m82F585DA43A63F3A8BB73B2523628CB488A11894_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m7CDD62DD0B0CCE0B04CF724866933C0921E81011_gshared (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, uint32_t ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mAEFE99D044558897366103682E2EFFAAECA05AD2_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, uint32_t ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_mFF22444E8F3DC86B69E3BC3E2E1F8D4618AC21A7_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_mDFC275828413520E93EC379BEC3B9B419975DD5C_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mCECB54C98550E945F76D6829C58B380B3440046C_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m78A79F57D3A6C8A3500EA86FBF8AA0FF85D0FFE8_gshared (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m8EBB3D347F09C9774F9A22C94ABD938660496D63_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mC3212D949B6A74EE599F5294E85C6CE6E1B706BF_gshared (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m2BEA0432A46856F824268DAD47F6D6F0BAFB1D27_gshared (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_t3C004EF64D01B2F9DC8BB66F28EE82E69221F131* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mECF88FA26E05A64BB84A50E9AA2C455DC087884D (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float3_t7600B73F092B37F484B12910A5269F30C778D31E List_1_get_Item_mDDBEBB5C8EAEE9D5B5759E5F786052C3CBF2EF66 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_mFF49D2E8E8EC50D00B3105009D68AC629F188E93 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_mC016D5FEB9C6627EE3CE089B5E760F2BED4DB8B2 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m5228B46F845892F0F239CC9D7314DF4BA90F6DDE (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_m2506538AE52D102AA80F184BC1DC5B422EC80DB6 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m641521748E28764A6131ED25D555ABC4FF9793D6_inline (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_mDDB22D94E651C9E4469BFD73D4AE7CA67E3FC3B4 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_mD97B04DABC7BA254E85C92D31872920799C700D4 (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, int32_t ___1_index, int32_t ___2_length, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mC27F404E4D89E84B4DC061AF19AEE8DC4A3BCE18 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m2F716DFC3073A2A7136124B4AB10DBD2D931454C (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m8D42F842C68EA7DFED04A55428F0537587793D9A (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_mA083F28DF5E7AB4AB25A1D0ABEB45C999B38DE50 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mF9D4C2FC4283D46C7FF1285A449409DBB5B41FA9 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m290A0FB66360A5FB10E7DC5DA524278A1E4AFF4C (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_mBED251F2E28E4422575173E72B868C1A24BD5285_inline (Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m1D1E66E62875F1DEDA7A916BDF27434658D63A30 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m9365C3DFAC7F3B17896AD6C63197369902F4934E (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mDD4A1B1BE4B3607CBA93CC29D3C64CB95A6143CB_inline (Action_1_t9BCEE3E1405DA67DBE5A4E9F0882507B63BCEC2F* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mD3CA065165D2EC0A9723D510ACA9142DBE5D1BBA (Enumerator_t92A9B1D618B36DF47EC28CBA0FFF9405BE75926E* __this, List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m69268FF8A42D99EBC95B85073ABCA95F734BDDAF (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_m913059E880BC34EDEE917201B440B2E9CEE1A583 (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mDD9B88B581FB24BC9AF2FACB395FE66CE045509F (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m8B4F3036EA9C5AD476DA0B6BA7E5BC3F4CAAB0C8 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m8FDE8B0AF2E22424391F5B414677395A7FE5D325 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m99D3C8B4AFB4E600AA8550AEBE0ED123B80E72E2 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_m86B3F9BB19458E58DBDEC044CF6D4F6518F6E609 (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m8E0156C9CAA092C3F9810489E410391A52F8ED8A (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_m3C148D0E6D87B13570375D359B48243DC7753C71 (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m28F4596182D9E9F98BEB3727027FFCEDC1D1CE8D (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_tEB713FAFEA92D337C218C257736265B7A7EDF135* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m0B91306C3AE3C541A215663CBC439245F8E3679B (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 List_1_get_Item_m16E2D277594D536BEF0EF8463D9DA3C1322F9263 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m0A8BE23260E6E169A1D247D8EAA0631905874EC8 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m8F84A4BC4BFAE1C6B80CB63095C9093716AE3E87 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m843E590839D8B44E5BF14AB39E42CAE86E2AEC9E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mD12A0B04025BCC2368FD9F8FE2B5CF9DC1AC7432 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m184A228230F65332F56708CBA3F3A88489B94D66_inline (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_mE806EE6B9753E133B6809CDA1E26C6A048B5375C (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_mF87C294E9FC4D8FF1307E2EB9EA87171DA824427 (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, int32_t ___1_index, int32_t ___2_length, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mCE539D0C6EA0432518D2558F38362D0CE53DCBF8 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_mE63B255347079C50A47093D77B4CFAEA05B61455 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m91492C9C383B83819368A542FF0C7E2DB817BF42 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m88F444D9C0089E32B73E8F5067B8A8D7B4A4FE03 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mA50F327494067599F9981040CBA1CD52DC8BF6D7 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mD6516F14C77C6B7F4BE00B8267F9967DC240B19E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_mAB88F92ABF8A77B99B3BC9C74D9C9A83878CBE7F_inline (Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m8190C5AD3B8E00871FF03E7886A9CFF2D1380332 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mE2B352FDE238EB11CC24B96E7554145C2BED755C (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m1AA93F8CEDADD37A0C262AE89FCDFCF1102FC270_inline (Action_1_t7D3408AF7B9CB908929DA7A8A01F6231B5EE5413* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m03C1E572D8D5FAD9E64579B8974701EDD1E8FE6C (Enumerator_t848512BD3FDC8088BBB0EFAC6CE52E9F2EDF4B18* __this, List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m463F6FD2D31F18F6BC2A559E4D51030B8E9A8C11 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m5281F142B5974BB41AA0821D1665BDE631701A7B (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_m938317D1E4B79E29DA685118A4043BE7D595F35E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m65889D090EB27B87304CACE748AFF62A4F2D36E4 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m77C714E4BACAF1AF9D076F35A3E1583738ED4CBF (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mA03FEC4E37DECFB8BAA2B89DC8BB3403F5C0DF39 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m4D835914AAD7FE14C0C90C15BB80EDCFA2979ACA (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mF0494466B503CD1E46368A6B7C34BCF0220E1DAD (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m875985D50B08BA88CDD753C2C19C09F1C71FCBBD (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m47F30B1CCA682496F46C591E3B37DA92C6B9533A (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_tD83994F0AC2F81C51937890F172D6969927BB246* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mB565A7AD8E82EBD46650B3F0E052900CE12DF2C5 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 List_1_get_Item_m4077C3878C73CFD2C37863B24FFABC93CCB538C2 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m668408E30034D9AF162BA065A1B8A9190CCB854C (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m9EE840E6FBE06791D954E7A69D53CE90EFCB863D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_mCA55981D47453F8483BD503CA53B0DABBDB8844D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mC9206E89715229E345A1542F94AC519D11E191D3 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mC35682C02D89984B864B24B3BB4F92B5C2374273_inline (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m830DE1B798DA2F877FBFF052C8841D220839E942 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m6F2E3CD5948C6A2A38F39E5200575F0C57CA126C (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, int32_t ___1_index, int32_t ___2_length, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mBA4F662F4752FE0603FA496605CBBF1672CC8CCA (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_mCB5DDE85A3BF1B0AE48D46EB6E22999A0E4D060D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m9AA36809C4850F3957B1A0B552645A8F328B7158 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m82EC683EFB57714A07D26CB6E17D84FFAE7C82AE (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m3332E89182E608A4BF6BE672CB719B8B0067984C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m26711BD95F0847B161016ECEBA1123F477AD8E11 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m8D40905BAEF5849C9DF55268BB4FD2C8CB0A1A79_inline (Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m6C63371ECE0863BF14A70E4012C6A62D66B5F5D7 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m0452816BC1DA4957681EA738C6BF21B054B01DB1 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m87599A17D9A01B7E75A9868586C2C91F0AB8FC85_inline (Action_1_t24D24AE7BDA27ABE5E9058347421CD45845804B3* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m3B885E0CB5C32229381BEF0AD2CE4682713CA0EC (Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5* __this, List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m963DC069890378B6A3E3359D767115A12793B2D5 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m075DCC6CC6C5E2D552CC01B5A5D93D761529CC5E (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mFF1B561AB8F89B1761C15D1863C00ED3AD7BF324 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m252525485F467F26DB5C91A957EA9F842D005C40 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_mA7E179931948DD88B825B2851F0BB01856B70460 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mBDDBB14EC973F164BA222AEB240F57B8460DAC1C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_mB7050783F04AD0453F190FBF1F3F6A2B6CB59506 (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m1CAB75B85D1547C184E1CA824E4A29E11269376E (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m9D7F79A00D6CDEEE91A7009E9C83C8A9190FCA23 (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m46BFEDFA8C5CE90310AFB562E0DE7A7E83E3BFEF (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_t53C7968EAD02BE23D913BD2C7F7F58BBBDD8D096* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mA6E73354873BEAC5429A80AE43CB210589A7EC7E (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 List_1_get_Item_mB65D799560ADD289EA08E717412A24EFADA7FB57 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m054615FD6A62EE7702B344B2E24BA8423FEF9621 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m7533012D6C4CE083D5CAF670259B97F6D5D506F4 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m8B3D79788ADC2B54C9B12CC9EC1158E6A04CDEBC (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mB6EB012362AA419CAA8BF0C488F1010486273A69 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mB308EE5BFFD89A7B8C251644D76CA547456F3012_inline (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m9AA9E2C5627FB6CD9F3493CA8A99588EDA08125D (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_mD97987A6223479E02D026B1D81268848C313482B (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, int32_t ___1_index, int32_t ___2_length, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m04F6731E2C6708D1889CB5C7F06A85173A079914 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_mFA6A5CD427374237339591ED8A5218B6E7EA7F4C (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_mA2F95E82C59734A475035EF3B9DD4BCE9F8429DB (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m590D7926DA2B45C95850710F87644BCD235F1A70 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mC49015D45A78A39909C4541BDB84DAA369E6A4C1 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m8E32928E07448E4DE50B13A27ABDF776DE8F4DDD (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m182E0ACAA85455151F5750CE87F942DA6E3DD3D6_inline (Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mA5DDCB0CBD1B539DB3213A4B3AE1C72E519C98A8 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m5AE304D5E24C284C548257B8911E3E6ADD047585 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m1D3248EEA732AE8F2AA160F73A481C0102193D15_inline (Action_1_t43B3A21E3139B3E2E49698F940F979F37AD1E63E* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mCAD56839187542C3B25E2C1493BBD62861333E4E (Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251* __this, List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m3F4723F3305F67834CE85E425907F53C7AB30484 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m972A3CE5298A6193E512B34C64201444B051D8E9 (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mF138D66498A5DE06E21A7149C35FA14729F4F5E1 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m30CC1D4CA1984DCAEDBCD2228B538A0503AF854A (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m02DCEAFFE826B31F80767A9D191CB4587D82F7BC (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m5903E8D5CE5B92A29530763E182314BDFA781664 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_mDCB49D07F30FD9D952124968159FDF346234BD04 (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mEBED487B845A66E3298CA30654B7EC17F7481995 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m438D9EDDBF93A09C81E89458C2362F85487923EC (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m4A8362BBA870329F45001F17310B50E583BFF19F (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_tC8BBA462D5337723AC34773EEE343018753C7FF3* ___3_comparer, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F (int32_t ___0_argument, int32_t ___1_resource, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC (int32_t ___0_argument, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m6C3A37912654122DD8AE143EFEB1C2A4C1015C29 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41 (RuntimeArray* ___0_sourceArray, int32_t ___1_sourceIndex, RuntimeArray* ___2_destinationArray, int32_t ___3_destinationIndex, int32_t ___4_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 List_1_get_Item_m4C81AE785745E7C9A726104CD2F588258FBE5E8F (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mADBF15F169E5DF2762161FB96F64C99569429300 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m6C903F3CAAD0D6DA54369D2224E52DFADB242F60 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910 (RuntimeObject* ___0_value, Type_t* ___1_targetType, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m55AFD45DC0ADD64C8749E1B59252B0BF0793CE49 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mB16BDFA4B73F390167601AF70BC65637BF9AE433 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m50157FAAF8FE0562A862140A17AD2B1097B078E7_inline (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
inline int32_t List_1_get_Count_m67096A69BCCCB33B2146561E2259883F084F5B95_inline (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m2555E93F7C903532A49529BCC327427FC3B1C6EE (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
inline void ReadOnlyCollection_1__ctor_mCAD4E9DC796CAE1655D1F09987BFC987E8FD72F1 (ReadOnlyCollection_1_t803A81BE343B15275BCD30A219ACAC97F0B9A1C8* __this, RuntimeObject* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlyCollection_1_t803A81BE343B15275BCD30A219ACAC97F0B9A1C8*, RuntimeObject*, const RuntimeMethod*))ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared)(__this, ___0_list, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA (int32_t ___0_resource, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_m469FFB0730A2E7136D62A2AE6D197A1858EC6CFE (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, int32_t ___1_index, int32_t ___2_length, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m603A1422A5D4C6AC20CFDB1897BD527164CFE059 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB (RuntimeArray* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m54DA0380F7731F2146DEDD98FC646038D9FC3923 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m72B9D7AAB1A1E284232CC6C4CF613FEA6A227584 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_mA297C73B52DACB79EAF9BABC705E6D7DB8EB7978 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
inline void List_1_CopyTo_m0E4DE3BC195FF67210FE47068EE58A77C5074C85 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method)
{
	((  void (*) (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB*, XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*, int32_t, const RuntimeMethod*))List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared)(__this, ___0_array, ___1_arrayIndex, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m5593B7194363BCB1B716A4ECA594F6E2CC03F94C (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m14D5EB230C22B6C4A649235CC3E08C5F6561E15B (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m91BF5E52B5443EC259EE465ECFE80677DA340F98_inline (Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m3A09BF3BCAAF3C61D9A2EEF2C3090311C21A62EB (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m94DC2E52655A61CACBDD8FFD47097AAE3E618198 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m14B5F0EFFF227D076151E00CACE917B6512254B8_inline (Action_1_tC8CC18F6848AAE74469E78A12EC004873F4332A7* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m5CBA8C17D9A8E5F5E385A4876B9B1745FA3CB8C0 (Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0* __this, List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m1742E82D5AF5200DFAC69B895C4A24E80D760C4D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mECE416DA9044547B55FF326280E3AFAB15850B37 (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mBECAB59CCD69670908799776F1D6507EF7EA7D87 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m53875FA8AA50D0F755EE525314DB9818741B6E2F (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m15AEA673A2E9F2A482D49F374814A49D0734CEC6 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m3CE641659C3CD3760E5EE96E88FA7006B7D9995D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_m6953DC1CD28104B40EF7B6C79E12EDA04CCC52F3 (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m52E4077FCE9FF6DF21E5DCE7A99DACB700AB2E0D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mACACE6CA017DAF3419A4C86E329FA9CFE4018C03 (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m479DAA4C8994B2FBFA82BCC2220725F25C35DCD9 (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_t55FCE173570442A2D451AE8BFC15B22D41D55DC9* ___3_comparer, const RuntimeMethod* method) ;
inline void List_1_AddEnumerable_m44B5C2177E9403DB0E3DC2BC6E590CDA7266BE79 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, RuntimeObject*, const RuntimeMethod*))List_1_AddEnumerable_m44B5C2177E9403DB0E3DC2BC6E590CDA7266BE79_gshared)(__this, ___0_enumerable, method);
}
inline Il2CppSharedGenericObject* List_1_get_Item_m02336594E8F76D7313E2BCB7A3ED5ABD13B4D515 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, const RuntimeMethod*))List_1_get_Item_m02336594E8F76D7313E2BCB7A3ED5ABD13B4D515_gshared)(__this, ___0_index, method);
}
inline void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisIl2CppSharedGenericObject_mE7A1EEC53480BA1ED6C827938ADAF45B127DD054 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method)
{
	((  void (*) (RuntimeObject*, int32_t, const RuntimeMethod*))ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisIl2CppSharedGenericObject_mE7A1EEC53480BA1ED6C827938ADAF45B127DD054_gshared)(___0_value, ___1_argName, method);
}
inline void List_1_set_Item_mCD8F7775F9C4842958A7188EBC0EBF1BE03101EF (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, Il2CppSharedGenericObject* ___1_value, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, Il2CppSharedGenericObject*, const RuntimeMethod*))List_1_set_Item_mCD8F7775F9C4842958A7188EBC0EBF1BE03101EF_gshared)(__this, ___0_index, ___1_value, method);
}
inline void List_1_AddWithResize_mCC24A812DFC30C3DE2230E3C8EE705871C1DFF81 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, Il2CppSharedGenericObject*, const RuntimeMethod*))List_1_AddWithResize_mCC24A812DFC30C3DE2230E3C8EE705871C1DFF81_gshared)(__this, ___0_item, method);
}
inline void List_1_EnsureCapacity_m89719BB37EEDA80E6AD7FAC5D1AAC5A47DDE86B5 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_min, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, const RuntimeMethod*))List_1_EnsureCapacity_m89719BB37EEDA80E6AD7FAC5D1AAC5A47DDE86B5_gshared)(__this, ___0_min, method);
}
inline void List_1_Add_m0CE6E39234ED33F5195AABC8029D06D4756C75C1_inline (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, Il2CppSharedGenericObject*, const RuntimeMethod*))List_1_Add_m0CE6E39234ED33F5195AABC8029D06D4756C75C1_gshared_inline)(__this, ___0_item, method);
}
inline int32_t List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
inline void List_1_InsertRange_m7B9D6C4AB69B4EA07C23832C6109C25C21E99804 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, RuntimeObject*, const RuntimeMethod*))List_1_InsertRange_m7B9D6C4AB69B4EA07C23832C6109C25C21E99804_gshared)(__this, ___0_index, ___1_collection, method);
}
inline void ReadOnlyCollection_1__ctor_mC1890FAC00703F47A655C35CBCB613C74A811580 (ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263* __this, RuntimeObject* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263*, RuntimeObject*, const RuntimeMethod*))ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared)(__this, ___0_list, method);
}
inline int32_t Array_BinarySearch_TisIl2CppSharedGenericObject_m6D8A1F3C8EDCAAF226FE8B32CBB649F099D8C16B (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, int32_t ___1_index, int32_t ___2_length, Il2CppSharedGenericObject* ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method)
{
	return ((  int32_t (*) (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*, int32_t, int32_t, Il2CppSharedGenericObject*, RuntimeObject*, const RuntimeMethod*))Array_BinarySearch_TisIl2CppSharedGenericObject_m6D8A1F3C8EDCAAF226FE8B32CBB649F099D8C16B_gshared)(___0_array, ___1_index, ___2_length, ___3_value, ___4_comparer, method);
}
inline int32_t List_1_BinarySearch_mB2BE3DF9182ED9C862AA44566C878D046534474E (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, Il2CppSharedGenericObject* ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, int32_t, Il2CppSharedGenericObject*, RuntimeObject*, const RuntimeMethod*))List_1_BinarySearch_mB2BE3DF9182ED9C862AA44566C878D046534474E_gshared)(__this, ___0_index, ___1_count, ___2_item, ___3_comparer, method);
}
inline int32_t List_1_IndexOf_m4B13B6BE7AA6327431F1BE406264329892975DE9 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, Il2CppSharedGenericObject*, const RuntimeMethod*))List_1_IndexOf_m4B13B6BE7AA6327431F1BE406264329892975DE9_gshared)(__this, ___0_item, method);
}
inline bool List_1_IsCompatibleObject_m9E5E0DF2443D88570595AEA0FEF7D7122455EB26 (RuntimeObject* ___0_value, const RuntimeMethod* method)
{
	return ((  bool (*) (RuntimeObject*, const RuntimeMethod*))List_1_IsCompatibleObject_m9E5E0DF2443D88570595AEA0FEF7D7122455EB26_gshared)(___0_value, method);
}
inline bool List_1_Contains_m63E643DC9A821297E2EBF9085D93B39BFC2F9DCE (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, Il2CppSharedGenericObject*, const RuntimeMethod*))List_1_Contains_m63E643DC9A821297E2EBF9085D93B39BFC2F9DCE_gshared)(__this, ___0_item, method);
}
inline void List_1_CopyTo_m75919E1737F4EC8D7DD329629C11CACA64EF5BE3 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*, int32_t, const RuntimeMethod*))List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared)(__this, ___0_array, ___1_arrayIndex, method);
}
inline void List_1_set_Capacity_m4B62593B8671BB60809057D57DC60568557315E6 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_value, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, const RuntimeMethod*))List_1_set_Capacity_m4B62593B8671BB60809057D57DC60568557315E6_gshared)(__this, ___0_value, method);
}
inline int32_t List_1_FindIndex_mD56F0607D7DAD9622F7DE2C6C8957D9C7847EEC5 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___0_match, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88*, const RuntimeMethod*))List_1_FindIndex_mD56F0607D7DAD9622F7DE2C6C8957D9C7847EEC5_gshared)(__this, ___0_match, method);
}
inline bool Predicate_1_Invoke_mDF332C0CEB003C0D86A1E3A194ECFA96D424E34C_inline (Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* __this, Il2CppSharedGenericObject* ___0_obj, const RuntimeMethod* method)
{
	return ((  bool (*) (Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88*, Il2CppSharedGenericObject*, const RuntimeMethod*))Predicate_1_Invoke_mDF332C0CEB003C0D86A1E3A194ECFA96D424E34C_gshared_inline)(__this, ___0_obj, method);
}
inline int32_t List_1_FindIndex_m701B15E692EF4FFE95A773C8246D55779CB00B2D (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___2_match, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, int32_t, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88*, const RuntimeMethod*))List_1_FindIndex_m701B15E692EF4FFE95A773C8246D55779CB00B2D_gshared)(__this, ___0_startIndex, ___1_count, ___2_match, method);
}
inline int32_t List_1_FindLastIndex_m75AB13CBCA95A0E0722F6CDE89F0CBDAB27322DA (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___2_match, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, int32_t, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88*, const RuntimeMethod*))List_1_FindLastIndex_m75AB13CBCA95A0E0722F6CDE89F0CBDAB27322DA_gshared)(__this, ___0_startIndex, ___1_count, ___2_match, method);
}
inline void Action_1_Invoke_mDB8F7C7381C08B4DCE4E409D2FA4073BC17F90B5_inline (Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* __this, Il2CppSharedGenericObject* ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505*, Il2CppSharedGenericObject*, const RuntimeMethod*))Action_1_Invoke_mDB8F7C7381C08B4DCE4E409D2FA4073BC17F90B5_gshared_inline)(__this, ___0_obj, method);
}
inline void Enumerator__ctor_mB0C2B8891214015BE286DC32F260DEBDFAD317E9 (Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9* __this, List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9*, List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, const RuntimeMethod*))Enumerator__ctor_mB0C2B8891214015BE286DC32F260DEBDFAD317E9_gshared)(__this, ___0_list, method);
}
inline void List_1__ctor_m54D719307910A9DC9CD4DEFA8AE2626F49EB7516 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_capacity, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, const RuntimeMethod*))List_1__ctor_m54D719307910A9DC9CD4DEFA8AE2626F49EB7516_gshared)(__this, ___0_capacity, method);
}
inline int32_t Array_IndexOf_TisIl2CppSharedGenericObject_m0C73DE72012C676D6E4354C82EA6550BBADC3D33 (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, Il2CppSharedGenericObject* ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method)
{
	return ((  int32_t (*) (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*, Il2CppSharedGenericObject*, int32_t, int32_t, const RuntimeMethod*))Array_IndexOf_TisIl2CppSharedGenericObject_m0C73DE72012C676D6E4354C82EA6550BBADC3D33_gshared)(___0_array, ___1_value, ___2_startIndex, ___3_count, method);
}
inline void List_1_Insert_mC5A8238645AD1D203E75313802D4DC78BDE7857E (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, Il2CppSharedGenericObject* ___1_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, Il2CppSharedGenericObject*, const RuntimeMethod*))List_1_Insert_mC5A8238645AD1D203E75313802D4DC78BDE7857E_gshared)(__this, ___0_index, ___1_item, method);
}
inline void List_1_RemoveAt_m36F68EA689DB109415094085A496D5EA21E2F23C (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, const RuntimeMethod*))List_1_RemoveAt_m36F68EA689DB109415094085A496D5EA21E2F23C_gshared)(__this, ___0_index, method);
}
inline bool List_1_Remove_m0AC731A08F1400089DD478E2D366804833660796 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, Il2CppSharedGenericObject*, const RuntimeMethod*))List_1_Remove_m0AC731A08F1400089DD478E2D366804833660796_gshared)(__this, ___0_item, method);
}
inline void List_1_Reverse_m97197478E9535E327B4509C065B30BB0DD6F1F99 (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, int32_t, const RuntimeMethod*))List_1_Reverse_m97197478E9535E327B4509C065B30BB0DD6F1F99_gshared)(__this, ___0_index, ___1_count, method);
}
inline void Array_Reverse_TisIl2CppSharedGenericObject_mFAAB0AB623769BB8245CDF219832F2FE75427541 (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method)
{
	((  void (*) (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*, int32_t, int32_t, const RuntimeMethod*))Array_Reverse_TisIl2CppSharedGenericObject_mFAAB0AB623769BB8245CDF219832F2FE75427541_gshared)(___0_array, ___1_index, ___2_length, method);
}
inline void List_1_Sort_m15AB808AB5390BF8B6D7A057EDA17A54A80A1A6A (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method)
{
	((  void (*) (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))List_1_Sort_m15AB808AB5390BF8B6D7A057EDA17A54A80A1A6A_gshared)(__this, ___0_index, ___1_count, ___2_comparer, method);
}
inline void Array_Sort_TisIl2CppSharedGenericObject_m8C8A5C187776FEA1D5CC7F053F0DC63B32764D61 (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method)
{
	((  void (*) (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))Array_Sort_TisIl2CppSharedGenericObject_m8C8A5C187776FEA1D5CC7F053F0DC63B32764D61_gshared)(___0_array, ___1_index, ___2_length, ___3_comparer, method);
}
inline void ArraySortHelper_1_Sort_m745CBFE82472049A441AC9522C4B8FA33F5AF970 (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_tC9FBFF747FA0455CD35BD7D097691DE20DDCC2B5* ___3_comparer, const RuntimeMethod* method)
{
	((  void (*) (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*, int32_t, int32_t, Comparison_1_tC9FBFF747FA0455CD35BD7D097691DE20DDCC2B5*, const RuntimeMethod*))ArraySortHelper_1_Sort_m745CBFE82472049A441AC9522C4B8FA33F5AF970_gshared)(___0_keys, ___1_index, ___2_length, ___3_comparer, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58 (const RuntimeMethod* method) ;
inline void Enumerator__ctor_m5C66A91DC68887BE203985B56EF2F74222E9AF50 (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*, List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))Enumerator__ctor_m5C66A91DC68887BE203985B56EF2F74222E9AF50_fshared)(__this, ___0_list, method);
}
inline void List_1_AddEnumerable_m70DD3FB3C4E302AA0AACC45C39BCEC4C2A9DB230 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, RuntimeObject*, const RuntimeMethod*))List_1_AddEnumerable_m70DD3FB3C4E302AA0AACC45C39BCEC4C2A9DB230_gshared)(__this, ___0_enumerable, method);
}
inline int32_t List_1_get_Item_mEAF67AD94AB1E060C2D03D7A6820C47270E06B75 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1_get_Item_mEAF67AD94AB1E060C2D03D7A6820C47270E06B75_gshared)(__this, ___0_index, method);
}
inline void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m835718C92D2C5DA2051794F6922D21453EAC493E (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method)
{
	((  void (*) (RuntimeObject*, int32_t, const RuntimeMethod*))ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m835718C92D2C5DA2051794F6922D21453EAC493E_gshared)(___0_value, ___1_argName, method);
}
inline void List_1_set_Item_mCDBFFAC056B77CBE377B26CAED3E893C131F4EFB (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_value, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, int32_t, const RuntimeMethod*))List_1_set_Item_mCDBFFAC056B77CBE377B26CAED3E893C131F4EFB_gshared)(__this, ___0_index, ___1_value, method);
}
inline void List_1_AddWithResize_m3B53F17525936469654379AC0F1F0B19EC3DCCE5 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1_AddWithResize_m3B53F17525936469654379AC0F1F0B19EC3DCCE5_gshared)(__this, ___0_item, method);
}
inline void List_1_EnsureCapacity_mB5CE702B99DFF8E1BB13570D69ADA436D814DBBF (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_min, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1_EnsureCapacity_mB5CE702B99DFF8E1BB13570D69ADA436D814DBBF_gshared)(__this, ___0_min, method);
}
inline void List_1_Add_m24BA4DEDED426A5B0C6A4AB3E523C876B0B918A6_inline (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1_Add_m24BA4DEDED426A5B0C6A4AB3E523C876B0B918A6_gshared_inline)(__this, ___0_item, method);
}
inline int32_t List_1_get_Count_m8719A0E4E1F227424476DE81DF6DF6B58CA1CD71_inline (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
inline void List_1_InsertRange_m99F96BAC4A4EE626A6141612A5C6793BE37E006C (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, RuntimeObject*, const RuntimeMethod*))List_1_InsertRange_m99F96BAC4A4EE626A6141612A5C6793BE37E006C_gshared)(__this, ___0_index, ___1_collection, method);
}
inline void ReadOnlyCollection_1__ctor_m401D0350AA4E4ACE83F75DE82712FF72120418D4 (ReadOnlyCollection_1_tA097AD399F1ED95CBDEFC0663790A10C4A43DFB8* __this, RuntimeObject* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlyCollection_1_tA097AD399F1ED95CBDEFC0663790A10C4A43DFB8*, RuntimeObject*, const RuntimeMethod*))ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared)(__this, ___0_list, method);
}
inline int32_t Array_BinarySearch_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m159F9F5904FFDBCBDB9ED09247CCE6E536AFEA23 (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, int32_t ___1_index, int32_t ___2_length, int32_t ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method)
{
	return ((  int32_t (*) (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*, int32_t, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))Array_BinarySearch_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m159F9F5904FFDBCBDB9ED09247CCE6E536AFEA23_gshared)(___0_array, ___1_index, ___2_length, ___3_value, ___4_comparer, method);
}
inline int32_t List_1_BinarySearch_mA5BE0B8B14B9B4F0EC39944DA4E226868121C961 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, int32_t ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))List_1_BinarySearch_mA5BE0B8B14B9B4F0EC39944DA4E226868121C961_gshared)(__this, ___0_index, ___1_count, ___2_item, ___3_comparer, method);
}
inline int32_t List_1_IndexOf_m22A847FE310BE1BA056D9344E43A4E5BB88759D7 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1_IndexOf_m22A847FE310BE1BA056D9344E43A4E5BB88759D7_gshared)(__this, ___0_item, method);
}
inline bool List_1_IsCompatibleObject_mD81F4794E5BA5AD31C7A25CA1B456BA04E37EC1D (RuntimeObject* ___0_value, const RuntimeMethod* method)
{
	return ((  bool (*) (RuntimeObject*, const RuntimeMethod*))List_1_IsCompatibleObject_mD81F4794E5BA5AD31C7A25CA1B456BA04E37EC1D_gshared)(___0_value, method);
}
inline bool List_1_Contains_m61A085CF349BFF87A90BE6FEE413C548F3F9E567 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1_Contains_m61A085CF349BFF87A90BE6FEE413C548F3F9E567_gshared)(__this, ___0_item, method);
}
inline void List_1_CopyTo_m02DA9C05536AB370B550B399953F68908C934B06 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, __Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, __Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*, int32_t, const RuntimeMethod*))List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared)(__this, ___0_array, ___1_arrayIndex, method);
}
inline void List_1_set_Capacity_m7AA13FF7BE1A11F437B16EAE3E1FA50DBFC902AE (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_value, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1_set_Capacity_m7AA13FF7BE1A11F437B16EAE3E1FA50DBFC902AE_gshared)(__this, ___0_value, method);
}
inline int32_t List_1_FindIndex_m334C39F1B647D613EE0A8043D6B30A64B63E713C (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___0_match, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2*, const RuntimeMethod*))List_1_FindIndex_m334C39F1B647D613EE0A8043D6B30A64B63E713C_gshared)(__this, ___0_match, method);
}
inline bool Predicate_1_Invoke_m0D65AFF43065FE03318CBDDB2A1E63B51476FB9A_inline (Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* __this, int32_t ___0_obj, const RuntimeMethod* method)
{
	return ((  bool (*) (Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2*, int32_t, const RuntimeMethod*))Predicate_1_Invoke_m0D65AFF43065FE03318CBDDB2A1E63B51476FB9A_gshared_inline)(__this, ___0_obj, method);
}
inline int32_t List_1_FindIndex_mC9F71DEB213A8D42EBC3A47798AAF6CD140438CA (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___2_match, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, int32_t, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2*, const RuntimeMethod*))List_1_FindIndex_mC9F71DEB213A8D42EBC3A47798AAF6CD140438CA_gshared)(__this, ___0_startIndex, ___1_count, ___2_match, method);
}
inline int32_t List_1_FindLastIndex_mF8BED79CAE6C97F36825E0CB1B54B4976D95ED56 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___2_match, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, int32_t, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2*, const RuntimeMethod*))List_1_FindLastIndex_mF8BED79CAE6C97F36825E0CB1B54B4976D95ED56_gshared)(__this, ___0_startIndex, ___1_count, ___2_match, method);
}
inline void Action_1_Invoke_mE118EB8C2BFC9D370FE37C05DE4F2CD6CBE9DB54_inline (Action_1_t5FD82308A537DD6AD12A51AF0790FA13C5BE2D65* __this, int32_t ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t5FD82308A537DD6AD12A51AF0790FA13C5BE2D65*, int32_t, const RuntimeMethod*))Action_1_Invoke_mE118EB8C2BFC9D370FE37C05DE4F2CD6CBE9DB54_gshared_inline)(__this, ___0_obj, method);
}
inline void Enumerator__ctor_m0B844022E6FED4C6DA8CD7C270989D9D34800C69 (Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6* __this, List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6*, List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, const RuntimeMethod*))Enumerator__ctor_m0B844022E6FED4C6DA8CD7C270989D9D34800C69_gshared)(__this, ___0_list, method);
}
inline void List_1__ctor_mBEC4BB23DEF62EFA5D66833D82CEB6FABDD96E04 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_capacity, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1__ctor_mBEC4BB23DEF62EFA5D66833D82CEB6FABDD96E04_gshared)(__this, ___0_capacity, method);
}
inline int32_t Array_IndexOf_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m9AE979627DAA17E8B9E3EF440A130C1B9C245059 (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, int32_t ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method)
{
	return ((  int32_t (*) (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*, int32_t, int32_t, int32_t, const RuntimeMethod*))Array_IndexOf_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m9AE979627DAA17E8B9E3EF440A130C1B9C245059_gshared)(___0_array, ___1_value, ___2_startIndex, ___3_count, method);
}
inline void List_1_Insert_m8C3391D73E5396E7FD12A5DA1CEDDD0A83EAAB79 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, int32_t, const RuntimeMethod*))List_1_Insert_m8C3391D73E5396E7FD12A5DA1CEDDD0A83EAAB79_gshared)(__this, ___0_index, ___1_item, method);
}
inline void List_1_RemoveAt_mBA121E6727BDC54EB17A84709997D6920BBE4D10 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1_RemoveAt_mBA121E6727BDC54EB17A84709997D6920BBE4D10_gshared)(__this, ___0_index, method);
}
inline bool List_1_Remove_mAF82A7B6E01FEB6F200C76071F76554EE4B35DB8 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, const RuntimeMethod*))List_1_Remove_mAF82A7B6E01FEB6F200C76071F76554EE4B35DB8_gshared)(__this, ___0_item, method);
}
inline void List_1_Reverse_m34A5681B1296F6E4F08C9F0986F4E4F1CF23FBE4 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, int32_t, const RuntimeMethod*))List_1_Reverse_m34A5681B1296F6E4F08C9F0986F4E4F1CF23FBE4_gshared)(__this, ___0_index, ___1_count, method);
}
inline void Array_Reverse_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_mA4406A8C17BBC29625F80F783EA91C3AECF7E3EA (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method)
{
	((  void (*) (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*, int32_t, int32_t, const RuntimeMethod*))Array_Reverse_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_mA4406A8C17BBC29625F80F783EA91C3AECF7E3EA_gshared)(___0_array, ___1_index, ___2_length, method);
}
inline void List_1_Sort_m29868C70BFD9DDFB44E31BF13302FB7AB5244913 (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method)
{
	((  void (*) (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))List_1_Sort_m29868C70BFD9DDFB44E31BF13302FB7AB5244913_gshared)(__this, ___0_index, ___1_count, ___2_comparer, method);
}
inline void Array_Sort_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_mE004CFE5BA4856AC83A08DEA9F09D3224F3EDD7B (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method)
{
	((  void (*) (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))Array_Sort_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_mE004CFE5BA4856AC83A08DEA9F09D3224F3EDD7B_gshared)(___0_array, ___1_index, ___2_length, ___3_comparer, method);
}
inline void ArraySortHelper_1_Sort_mCDDF2BDFA2ACB2C2254C52BBD32AD774C7C05CDC (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_t0F283DBF0EC45EE18464332F2713E3E393EED6C3* ___3_comparer, const RuntimeMethod* method)
{
	((  void (*) (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*, int32_t, int32_t, Comparison_1_t0F283DBF0EC45EE18464332F2713E3E393EED6C3*, const RuntimeMethod*))ArraySortHelper_1_Sort_mCDDF2BDFA2ACB2C2254C52BBD32AD774C7C05CDC_gshared)(___0_keys, ___1_index, ___2_length, ___3_comparer, method);
}
inline void List_1_AddEnumerable_m681B8A3C4E9973D4BBAAC17D1EF146A796DF11CB (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, RuntimeObject*, const RuntimeMethod*))List_1_AddEnumerable_m681B8A3C4E9973D4BBAAC17D1EF146A796DF11CB_gshared)(__this, ___0_enumerable, method);
}
inline uint32_t List_1_get_Item_m996B509A482EA372164DE730F73C1E11BA2F3EAE (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	return ((  uint32_t (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, const RuntimeMethod*))List_1_get_Item_m996B509A482EA372164DE730F73C1E11BA2F3EAE_gshared)(__this, ___0_index, method);
}
inline void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mB550F72315A962B072051B4E80B0F9D124721798 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method)
{
	((  void (*) (RuntimeObject*, int32_t, const RuntimeMethod*))ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mB550F72315A962B072051B4E80B0F9D124721798_gshared)(___0_value, ___1_argName, method);
}
inline void List_1_set_Item_m156A9F0E34470C01D118D6391E192E7D54189C0F (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, uint32_t ___1_value, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, uint32_t, const RuntimeMethod*))List_1_set_Item_m156A9F0E34470C01D118D6391E192E7D54189C0F_gshared)(__this, ___0_index, ___1_value, method);
}
inline void List_1_AddWithResize_m5D090D0E0EFAAA54D1A7D12657EE18BA7AD0F445 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, uint32_t, const RuntimeMethod*))List_1_AddWithResize_m5D090D0E0EFAAA54D1A7D12657EE18BA7AD0F445_gshared)(__this, ___0_item, method);
}
inline void List_1_EnsureCapacity_m832EAA18FAB897AE9CE65E99ED96EF173701E507 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_min, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, const RuntimeMethod*))List_1_EnsureCapacity_m832EAA18FAB897AE9CE65E99ED96EF173701E507_gshared)(__this, ___0_min, method);
}
inline void List_1_Add_mBDE95F2D32400D0DABA22511FE053AC8957D8D1E_inline (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, uint32_t, const RuntimeMethod*))List_1_Add_mBDE95F2D32400D0DABA22511FE053AC8957D8D1E_gshared_inline)(__this, ___0_item, method);
}
inline int32_t List_1_get_Count_m4754598204C7F7240B07126FE194433F71E3B48E_inline (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
inline void List_1_InsertRange_m694C6D98C9BDD66E139B4FBDC95D06372CF2869E (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, RuntimeObject*, const RuntimeMethod*))List_1_InsertRange_m694C6D98C9BDD66E139B4FBDC95D06372CF2869E_gshared)(__this, ___0_index, ___1_collection, method);
}
inline void ReadOnlyCollection_1__ctor_m0764B150EBDC3AF359794E686D8C2B31B37100C9 (ReadOnlyCollection_1_t57A9EA1D0B6B2A5908DD58EA8C84F29F53D509A1* __this, RuntimeObject* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlyCollection_1_t57A9EA1D0B6B2A5908DD58EA8C84F29F53D509A1*, RuntimeObject*, const RuntimeMethod*))ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared)(__this, ___0_list, method);
}
inline int32_t Array_BinarySearch_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m30014505CD15EFAF86A6FBE09AF0A55BD9377B5F (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, int32_t ___1_index, int32_t ___2_length, uint32_t ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method)
{
	return ((  int32_t (*) (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*, int32_t, int32_t, uint32_t, RuntimeObject*, const RuntimeMethod*))Array_BinarySearch_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m30014505CD15EFAF86A6FBE09AF0A55BD9377B5F_gshared)(___0_array, ___1_index, ___2_length, ___3_value, ___4_comparer, method);
}
inline int32_t List_1_BinarySearch_m15C2E0B120676758F22A35BED084390D6A444A09 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, uint32_t ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, int32_t, uint32_t, RuntimeObject*, const RuntimeMethod*))List_1_BinarySearch_m15C2E0B120676758F22A35BED084390D6A444A09_gshared)(__this, ___0_index, ___1_count, ___2_item, ___3_comparer, method);
}
inline int32_t List_1_IndexOf_m2FB5B4F89323CDCB893E3BB9C2E256169024B872 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, uint32_t, const RuntimeMethod*))List_1_IndexOf_m2FB5B4F89323CDCB893E3BB9C2E256169024B872_gshared)(__this, ___0_item, method);
}
inline bool List_1_IsCompatibleObject_mED20F2A2B89C0BFF977AA52105F288790EEF484F (RuntimeObject* ___0_value, const RuntimeMethod* method)
{
	return ((  bool (*) (RuntimeObject*, const RuntimeMethod*))List_1_IsCompatibleObject_mED20F2A2B89C0BFF977AA52105F288790EEF484F_gshared)(___0_value, method);
}
inline bool List_1_Contains_mD62DF0807CE1CF205884252DA7D06B9EDACF5512 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, uint32_t, const RuntimeMethod*))List_1_Contains_mD62DF0807CE1CF205884252DA7D06B9EDACF5512_gshared)(__this, ___0_item, method);
}
inline void List_1_CopyTo_mF4A32F59001BD461521EF332C6C8876EBA635A43 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, __Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, __Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*, int32_t, const RuntimeMethod*))List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared)(__this, ___0_array, ___1_arrayIndex, method);
}
inline void List_1_set_Capacity_mCA75F83CA2F20BC02FFECCA387D4C021A2D83511 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_value, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, const RuntimeMethod*))List_1_set_Capacity_mCA75F83CA2F20BC02FFECCA387D4C021A2D83511_gshared)(__this, ___0_value, method);
}
inline int32_t List_1_FindIndex_m5DFECA19BA1FF082CEE8E8EE854277501A214F17 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___0_match, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716*, const RuntimeMethod*))List_1_FindIndex_m5DFECA19BA1FF082CEE8E8EE854277501A214F17_gshared)(__this, ___0_match, method);
}
inline bool Predicate_1_Invoke_m2603DCEFACCFCB054A6BC33E68531E62F317C8A4_inline (Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* __this, uint32_t ___0_obj, const RuntimeMethod* method)
{
	return ((  bool (*) (Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716*, uint32_t, const RuntimeMethod*))Predicate_1_Invoke_m2603DCEFACCFCB054A6BC33E68531E62F317C8A4_gshared_inline)(__this, ___0_obj, method);
}
inline int32_t List_1_FindIndex_mA09105FDEDF74F29015B15E28F5E73BA9C4BCAFE (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___2_match, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, int32_t, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716*, const RuntimeMethod*))List_1_FindIndex_mA09105FDEDF74F29015B15E28F5E73BA9C4BCAFE_gshared)(__this, ___0_startIndex, ___1_count, ___2_match, method);
}
inline int32_t List_1_FindLastIndex_mAC723FB10B249538A74DB3B20E203F0EF3764C88 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___2_match, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, int32_t, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716*, const RuntimeMethod*))List_1_FindLastIndex_mAC723FB10B249538A74DB3B20E203F0EF3764C88_gshared)(__this, ___0_startIndex, ___1_count, ___2_match, method);
}
inline void Action_1_Invoke_m124DA039CD5C63AD48841B21065DFC6C03E63930_inline (Action_1_t6E017BD45EFAD0010771D0174C3A5653E776128E* __this, uint32_t ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t6E017BD45EFAD0010771D0174C3A5653E776128E*, uint32_t, const RuntimeMethod*))Action_1_Invoke_m124DA039CD5C63AD48841B21065DFC6C03E63930_gshared_inline)(__this, ___0_obj, method);
}
inline void Enumerator__ctor_m48AA10A08952E197C1709A3F3AE09FB34BE212C4 (Enumerator_t474D057FE73061C816292BC377C411C94495375A* __this, List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t474D057FE73061C816292BC377C411C94495375A*, List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, const RuntimeMethod*))Enumerator__ctor_m48AA10A08952E197C1709A3F3AE09FB34BE212C4_gshared)(__this, ___0_list, method);
}
inline void List_1__ctor_m82F585DA43A63F3A8BB73B2523628CB488A11894 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_capacity, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, const RuntimeMethod*))List_1__ctor_m82F585DA43A63F3A8BB73B2523628CB488A11894_gshared)(__this, ___0_capacity, method);
}
inline int32_t Array_IndexOf_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m7CDD62DD0B0CCE0B04CF724866933C0921E81011 (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, uint32_t ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method)
{
	return ((  int32_t (*) (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*, uint32_t, int32_t, int32_t, const RuntimeMethod*))Array_IndexOf_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m7CDD62DD0B0CCE0B04CF724866933C0921E81011_gshared)(___0_array, ___1_value, ___2_startIndex, ___3_count, method);
}
inline void List_1_Insert_mAEFE99D044558897366103682E2EFFAAECA05AD2 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, uint32_t ___1_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, uint32_t, const RuntimeMethod*))List_1_Insert_mAEFE99D044558897366103682E2EFFAAECA05AD2_gshared)(__this, ___0_index, ___1_item, method);
}
inline void List_1_RemoveAt_mFF22444E8F3DC86B69E3BC3E2E1F8D4618AC21A7 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, const RuntimeMethod*))List_1_RemoveAt_mFF22444E8F3DC86B69E3BC3E2E1F8D4618AC21A7_gshared)(__this, ___0_index, method);
}
inline bool List_1_Remove_mDFC275828413520E93EC379BEC3B9B419975DD5C (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, uint32_t, const RuntimeMethod*))List_1_Remove_mDFC275828413520E93EC379BEC3B9B419975DD5C_gshared)(__this, ___0_item, method);
}
inline void List_1_Reverse_mCECB54C98550E945F76D6829C58B380B3440046C (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, int32_t, const RuntimeMethod*))List_1_Reverse_mCECB54C98550E945F76D6829C58B380B3440046C_gshared)(__this, ___0_index, ___1_count, method);
}
inline void Array_Reverse_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m78A79F57D3A6C8A3500EA86FBF8AA0FF85D0FFE8 (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method)
{
	((  void (*) (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*, int32_t, int32_t, const RuntimeMethod*))Array_Reverse_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m78A79F57D3A6C8A3500EA86FBF8AA0FF85D0FFE8_gshared)(___0_array, ___1_index, ___2_length, method);
}
inline void List_1_Sort_m8EBB3D347F09C9774F9A22C94ABD938660496D63 (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method)
{
	((  void (*) (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))List_1_Sort_m8EBB3D347F09C9774F9A22C94ABD938660496D63_gshared)(__this, ___0_index, ___1_count, ___2_comparer, method);
}
inline void Array_Sort_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mC3212D949B6A74EE599F5294E85C6CE6E1B706BF (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method)
{
	((  void (*) (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))Array_Sort_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mC3212D949B6A74EE599F5294E85C6CE6E1B706BF_gshared)(___0_array, ___1_index, ___2_length, ___3_comparer, method);
}
inline void ArraySortHelper_1_Sort_m2BEA0432A46856F824268DAD47F6D6F0BAFB1D27 (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_t3C004EF64D01B2F9DC8BB66F28EE82E69221F131* ___3_comparer, const RuntimeMethod* method)
{
	((  void (*) (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*, int32_t, int32_t, Comparison_1_t3C004EF64D01B2F9DC8BB66F28EE82E69221F131*, const RuntimeMethod*))ArraySortHelper_1_Sort_m2BEA0432A46856F824268DAD47F6D6F0BAFB1D27_gshared)(___0_keys, ___1_index, ___2_length, ___3_comparer, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mECF88FA26E05A64BB84A50E9AA2C455DC087884D (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float3_t7600B73F092B37F484B12910A5269F30C778D31E List_1_get_Item_mDDBEBB5C8EAEE9D5B5759E5F786052C3CBF2EF66 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_mFF49D2E8E8EC50D00B3105009D68AC629F188E93 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_mC016D5FEB9C6627EE3CE089B5E760F2BED4DB8B2 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m5228B46F845892F0F239CC9D7314DF4BA90F6DDE (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_m2506538AE52D102AA80F184BC1DC5B422EC80DB6 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m641521748E28764A6131ED25D555ABC4FF9793D6_inline (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
inline int32_t List_1_get_Count_m4FFB175F8DAE5AB81144E07755B2AC56551F84E4_inline (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_mDDB22D94E651C9E4469BFD73D4AE7CA67E3FC3B4 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
inline void ReadOnlyCollection_1__ctor_m87B2FF05BB1173F36FEE8A953CB2851E0D63F92B (ReadOnlyCollection_1_t1D742B1E1C22078FEF5E998EDD21A4405B13B076* __this, RuntimeObject* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlyCollection_1_t1D742B1E1C22078FEF5E998EDD21A4405B13B076*, RuntimeObject*, const RuntimeMethod*))ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared)(__this, ___0_list, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_mD97B04DABC7BA254E85C92D31872920799C700D4 (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, int32_t ___1_index, int32_t ___2_length, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mC27F404E4D89E84B4DC061AF19AEE8DC4A3BCE18 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m2F716DFC3073A2A7136124B4AB10DBD2D931454C (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m8D42F842C68EA7DFED04A55428F0537587793D9A (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_mA083F28DF5E7AB4AB25A1D0ABEB45C999B38DE50 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
inline void List_1_CopyTo_m65718278D38F07F4FAE62C2B51BAA26421813DE8 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method)
{
	((  void (*) (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6*, float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*, int32_t, const RuntimeMethod*))List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared)(__this, ___0_array, ___1_arrayIndex, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mF9D4C2FC4283D46C7FF1285A449409DBB5B41FA9 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m290A0FB66360A5FB10E7DC5DA524278A1E4AFF4C (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_mBED251F2E28E4422575173E72B868C1A24BD5285_inline (Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m1D1E66E62875F1DEDA7A916BDF27434658D63A30 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m9365C3DFAC7F3B17896AD6C63197369902F4934E (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mDD4A1B1BE4B3607CBA93CC29D3C64CB95A6143CB_inline (Action_1_t9BCEE3E1405DA67DBE5A4E9F0882507B63BCEC2F* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mD3CA065165D2EC0A9723D510ACA9142DBE5D1BBA (Enumerator_t92A9B1D618B36DF47EC28CBA0FFF9405BE75926E* __this, List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m69268FF8A42D99EBC95B85073ABCA95F734BDDAF (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_m913059E880BC34EDEE917201B440B2E9CEE1A583 (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mDD9B88B581FB24BC9AF2FACB395FE66CE045509F (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m8B4F3036EA9C5AD476DA0B6BA7E5BC3F4CAAB0C8 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m8FDE8B0AF2E22424391F5B414677395A7FE5D325 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m99D3C8B4AFB4E600AA8550AEBE0ED123B80E72E2 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_m86B3F9BB19458E58DBDEC044CF6D4F6518F6E609 (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m8E0156C9CAA092C3F9810489E410391A52F8ED8A (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_m3C148D0E6D87B13570375D359B48243DC7753C71 (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m28F4596182D9E9F98BEB3727027FFCEDC1D1CE8D (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_tEB713FAFEA92D337C218C257736265B7A7EDF135* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m0B91306C3AE3C541A215663CBC439245F8E3679B (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 List_1_get_Item_m16E2D277594D536BEF0EF8463D9DA3C1322F9263 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m0A8BE23260E6E169A1D247D8EAA0631905874EC8 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m8F84A4BC4BFAE1C6B80CB63095C9093716AE3E87 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m843E590839D8B44E5BF14AB39E42CAE86E2AEC9E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mD12A0B04025BCC2368FD9F8FE2B5CF9DC1AC7432 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m184A228230F65332F56708CBA3F3A88489B94D66_inline (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
inline int32_t List_1_get_Count_mAA4BC36A86EF945C37561EF64337148E583C211E_inline (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_mE806EE6B9753E133B6809CDA1E26C6A048B5375C (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
inline void ReadOnlyCollection_1__ctor_m7E446C3BE50D15E107750A0A02FB85536C539DD2 (ReadOnlyCollection_1_tEFFE9324903FBAB1A9A712578488B71F4CBC3BF5* __this, RuntimeObject* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlyCollection_1_tEFFE9324903FBAB1A9A712578488B71F4CBC3BF5*, RuntimeObject*, const RuntimeMethod*))ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared)(__this, ___0_list, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_mF87C294E9FC4D8FF1307E2EB9EA87171DA824427 (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, int32_t ___1_index, int32_t ___2_length, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mCE539D0C6EA0432518D2558F38362D0CE53DCBF8 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_mE63B255347079C50A47093D77B4CFAEA05B61455 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m91492C9C383B83819368A542FF0C7E2DB817BF42 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m88F444D9C0089E32B73E8F5067B8A8D7B4A4FE03 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
inline void List_1_CopyTo_mD9D5C57DBC12217624E77B77D7BEA6C58FA5F94B (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method)
{
	((  void (*) (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2*, WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*, int32_t, const RuntimeMethod*))List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared)(__this, ___0_array, ___1_arrayIndex, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mA50F327494067599F9981040CBA1CD52DC8BF6D7 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mD6516F14C77C6B7F4BE00B8267F9967DC240B19E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_mAB88F92ABF8A77B99B3BC9C74D9C9A83878CBE7F_inline (Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m8190C5AD3B8E00871FF03E7886A9CFF2D1380332 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mE2B352FDE238EB11CC24B96E7554145C2BED755C (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m1AA93F8CEDADD37A0C262AE89FCDFCF1102FC270_inline (Action_1_t7D3408AF7B9CB908929DA7A8A01F6231B5EE5413* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m03C1E572D8D5FAD9E64579B8974701EDD1E8FE6C (Enumerator_t848512BD3FDC8088BBB0EFAC6CE52E9F2EDF4B18* __this, List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m463F6FD2D31F18F6BC2A559E4D51030B8E9A8C11 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m5281F142B5974BB41AA0821D1665BDE631701A7B (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_m938317D1E4B79E29DA685118A4043BE7D595F35E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m65889D090EB27B87304CACE748AFF62A4F2D36E4 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m77C714E4BACAF1AF9D076F35A3E1583738ED4CBF (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mA03FEC4E37DECFB8BAA2B89DC8BB3403F5C0DF39 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m4D835914AAD7FE14C0C90C15BB80EDCFA2979ACA (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mF0494466B503CD1E46368A6B7C34BCF0220E1DAD (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m875985D50B08BA88CDD753C2C19C09F1C71FCBBD (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m47F30B1CCA682496F46C591E3B37DA92C6B9533A (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_tD83994F0AC2F81C51937890F172D6969927BB246* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mB565A7AD8E82EBD46650B3F0E052900CE12DF2C5 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 List_1_get_Item_m4077C3878C73CFD2C37863B24FFABC93CCB538C2 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m668408E30034D9AF162BA065A1B8A9190CCB854C (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m9EE840E6FBE06791D954E7A69D53CE90EFCB863D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_mCA55981D47453F8483BD503CA53B0DABBDB8844D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mC9206E89715229E345A1542F94AC519D11E191D3 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mC35682C02D89984B864B24B3BB4F92B5C2374273_inline (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
inline int32_t List_1_get_Count_m5F8DAD406CF36D0DEEB854A55C356D94762E30CB_inline (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m830DE1B798DA2F877FBFF052C8841D220839E942 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
inline void ReadOnlyCollection_1__ctor_mB3BFC6EBF26339300AE50AEA966E9B13DA92D54F (ReadOnlyCollection_1_t0072B9B1BE9DDF9C8BF0ABE59E7EEBD11BFAA293* __this, RuntimeObject* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlyCollection_1_t0072B9B1BE9DDF9C8BF0ABE59E7EEBD11BFAA293*, RuntimeObject*, const RuntimeMethod*))ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared)(__this, ___0_list, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m6F2E3CD5948C6A2A38F39E5200575F0C57CA126C (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, int32_t ___1_index, int32_t ___2_length, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mBA4F662F4752FE0603FA496605CBBF1672CC8CCA (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_mCB5DDE85A3BF1B0AE48D46EB6E22999A0E4D060D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m9AA36809C4850F3957B1A0B552645A8F328B7158 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m82EC683EFB57714A07D26CB6E17D84FFAE7C82AE (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
inline void List_1_CopyTo_mBB5FC483C17D644A9362EDE7FB61DD1D6233000C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method)
{
	((  void (*) (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50*, AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*, int32_t, const RuntimeMethod*))List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared)(__this, ___0_array, ___1_arrayIndex, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m3332E89182E608A4BF6BE672CB719B8B0067984C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m26711BD95F0847B161016ECEBA1123F477AD8E11 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m8D40905BAEF5849C9DF55268BB4FD2C8CB0A1A79_inline (Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m6C63371ECE0863BF14A70E4012C6A62D66B5F5D7 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m0452816BC1DA4957681EA738C6BF21B054B01DB1 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m87599A17D9A01B7E75A9868586C2C91F0AB8FC85_inline (Action_1_t24D24AE7BDA27ABE5E9058347421CD45845804B3* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_m3B885E0CB5C32229381BEF0AD2CE4682713CA0EC (Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5* __this, List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m963DC069890378B6A3E3359D767115A12793B2D5 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m075DCC6CC6C5E2D552CC01B5A5D93D761529CC5E (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mFF1B561AB8F89B1761C15D1863C00ED3AD7BF324 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m252525485F467F26DB5C91A957EA9F842D005C40 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_mA7E179931948DD88B825B2851F0BB01856B70460 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mBDDBB14EC973F164BA222AEB240F57B8460DAC1C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_mB7050783F04AD0453F190FBF1F3F6A2B6CB59506 (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m1CAB75B85D1547C184E1CA824E4A29E11269376E (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m9D7F79A00D6CDEEE91A7009E9C83C8A9190FCA23 (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m46BFEDFA8C5CE90310AFB562E0DE7A7E83E3BFEF (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_t53C7968EAD02BE23D913BD2C7F7F58BBBDD8D096* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mA6E73354873BEAC5429A80AE43CB210589A7EC7E (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 List_1_get_Item_mB65D799560ADD289EA08E717412A24EFADA7FB57 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m054615FD6A62EE7702B344B2E24BA8423FEF9621 (RuntimeObject* ___0_value, int32_t ___1_argName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m7533012D6C4CE083D5CAF670259B97F6D5D506F4 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m8B3D79788ADC2B54C9B12CC9EC1158E6A04CDEBC (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mB6EB012362AA419CAA8BF0C488F1010486273A69 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_min, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mB308EE5BFFD89A7B8C251644D76CA547456F3012_inline (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
inline int32_t List_1_get_Count_m905EB6FD0B91CEE1CC0165F1EBB8C1309AEE4BEC_inline (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A*, const RuntimeMethod*))List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m9AA9E2C5627FB6CD9F3493CA8A99588EDA08125D (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) ;
inline void ReadOnlyCollection_1__ctor_m5D03D2E5CE6AAFA9914B3F1E1707393948DA34CB (ReadOnlyCollection_1_t00EFCE24C7E2AF195B108C1FC1E1BE4F7007EC1E* __this, RuntimeObject* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlyCollection_1_t00EFCE24C7E2AF195B108C1FC1E1BE4F7007EC1E*, RuntimeObject*, const RuntimeMethod*))ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared)(__this, ___0_list, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_BinarySearch_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_mD97987A6223479E02D026B1D81268848C313482B (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, int32_t ___1_index, int32_t ___2_length, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___3_value, RuntimeObject* ___4_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m04F6731E2C6708D1889CB5C7F06A85173A079914 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_mFA6A5CD427374237339591ED8A5218B6E7EA7F4C (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_mA2F95E82C59734A475035EF3B9DD4BCE9F8429DB (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m590D7926DA2B45C95850710F87644BCD235F1A70 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
inline void List_1_CopyTo_m96F4BF1E6FF691338A44AAB62506964ACAD70715 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method)
{
	((  void (*) (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A*, OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*, int32_t, const RuntimeMethod*))List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared)(__this, ___0_array, ___1_arrayIndex, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mC49015D45A78A39909C4541BDB84DAA369E6A4C1 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m8E32928E07448E4DE50B13A27ABDF776DE8F4DDD (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___0_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m182E0ACAA85455151F5750CE87F942DA6E3DD3D6_inline (Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mA5DDCB0CBD1B539DB3213A4B3AE1C72E519C98A8 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___2_match, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m5AE304D5E24C284C548257B8911E3E6ADD047585 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___2_match, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m1D3248EEA732AE8F2AA160F73A481C0102193D15_inline (Action_1_t43B3A21E3139B3E2E49698F940F979F37AD1E63E* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator__ctor_mCAD56839187542C3B25E2C1493BBD62861333E4E (Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251* __this, List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* ___0_list, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m3F4723F3305F67834CE85E425907F53C7AB30484 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_IndexOf_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m972A3CE5298A6193E512B34C64201444B051D8E9 (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___1_value, int32_t ___2_startIndex, int32_t ___3_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mF138D66498A5DE06E21A7149C35FA14729F4F5E1 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___1_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m30CC1D4CA1984DCAEDBCD2228B538A0503AF854A (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m02DCEAFFE826B31F80767A9D191CB4587D82F7BC (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m5903E8D5CE5B92A29530763E182314BDFA781664 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Reverse_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_mDCB49D07F30FD9D952124968159FDF346234BD04 (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mEBED487B845A66E3298CA30654B7EC17F7481995 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Sort_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m438D9EDDBF93A09C81E89458C2362F85487923EC (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, int32_t ___1_index, int32_t ___2_length, RuntimeObject* ___3_comparer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArraySortHelper_1_Sort_m4A8362BBA870329F45001F17310B50E583BFF19F (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_keys, int32_t ___1_index, int32_t ___2_length, Comparison_1_tC8BBA462D5337723AC34773EEE343018753C7FF3* ___3_comparer, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 11344
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_mB5A115525F03F979B28964F23108C8C5312FC460 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_0 = ((List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_0);
		return;
	}
}
// Method Definition Index: 11345
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m1742E82D5AF5200DFAC69B895C4A24E80D760C4D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_capacity, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___0_capacity;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)12), 4, NULL);
	}

IL_0012:
	{
		int32_t L_1 = ___0_capacity;
		if (L_1)
		{
			goto IL_0021;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_2 = ((List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_2);
		return;
	}

IL_0021:
	{
		int32_t L_3 = ___0_capacity;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_4 = (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)(XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_3);
		__this->____items = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_4);
		return;
	}
}
// Method Definition Index: 11346
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m9313D754412425CDD96F4AB6AF2BE94CFC547467 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_000f:
	{
		RuntimeObject* L_1 = ___0_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0050;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_3);
		V_1 = L_4;
		int32_t L_5 = V_1;
		if (L_5)
		{
			goto IL_002f;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_6 = ((List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_6);
		return;
	}

IL_002f:
	{
		int32_t L_7 = V_1;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_8 = (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)(XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_7);
		__this->____items = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_8);
		RuntimeObject* L_9 = V_0;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_10 = __this->____items;
		NullCheck(L_9);
		InterfaceActionInvoker2< XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_9, L_10, 0);
		int32_t L_11 = V_1;
		__this->____size = L_11;
		return;
	}

IL_0050:
	{
		__this->____size = 0;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_12 = ((List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		RuntimeObject* L_13 = ___0_collection;
		List_1_AddEnumerable_m6C3A37912654122DD8AE143EFEB1C2A4C1015C29(__this, L_13, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
		return;
	}
}
// Method Definition Index: 11347
// Method Definition Index: 11348
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m5593B7194363BCB1B716A4ECA594F6E2CC03F94C (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* V_0 = NULL;
	{
		int32_t L_0 = ___0_value;
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)15), ((int32_t)21), NULL);
	}

IL_0012:
	{
		int32_t L_2 = ___0_value;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_2) == ((int32_t)L_4)))
		{
			goto IL_0058;
		}
	}
	{
		int32_t L_5 = ___0_value;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_004d;
		}
	}
	{
		int32_t L_6 = ___0_value;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_7 = (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)(XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_6);
		V_0 = L_7;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0045;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_9 = __this->____items;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_10 = V_0;
		int32_t L_11 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_9, 0, (RuntimeArray*)L_10, 0, L_11, NULL);
	}

IL_0045:
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_12 = V_0;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		return;
	}

IL_004d:
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_13 = ((List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_13);
	}

IL_0058:
	{
		return;
	}
}
// Method Definition Index: 11349
// Method Definition Index: 11350
// Method Definition Index: 11351
// Method Definition Index: 11352
// Method Definition Index: 11353
// Method Definition Index: 11354
// Method Definition Index: 11355
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 List_1_get_Item_m4C81AE785745E7C9A726104CD2F588258FBE5E8F (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		return L_5;
	}
}
// Method Definition Index: 11356
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m6C903F3CAAD0D6DA54369D2224E52DFADB242F60 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_4 = ___1_value;
		NullCheck(L_2);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(L_3), (XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2)L_4);
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11357
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m72B9D7AAB1A1E284232CC6C4CF613FEA6A227584 (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6))))
		{
			goto IL_001f;
		}
	}
	{
		RuntimeObject* L_1 = ___0_value;
		if (L_1)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2));
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_2 = V_0;
		return false;
	}

IL_001d:
	{
		return (bool)0;
	}

IL_001f:
	{
		return (bool)1;
	}
}
// Method Definition Index: 11358
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IList_get_Item_m43477563FF796F46E62630752F9BE96E325D1539 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_1;
		L_1 = List_1_get_Item_m4C81AE785745E7C9A726104CD2F588258FBE5E8F(__this, L_0, NULL);
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_2 = L_1;
		RuntimeObject* L_3 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), &L_2);
		return L_3;
	}
}
// Method Definition Index: 11359
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_set_Item_mF605D424A31F1FBA0A3372FFDB96685809FE65F2 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_value;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mADBF15F169E5DF2762161FB96F64C99569429300(L_0, ((int32_t)15), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_value;
		List_1_set_Item_m6C903F3CAAD0D6DA54369D2224E52DFADB242F60(__this, L_1, ((*(XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), NULL);
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_value;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11360
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Add_m50157FAAF8FE0562A862140A17AD2B1097B078E7 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_7 = V_0;
		int32_t L_8 = V_1;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2)L_9);
		return;
	}

IL_0034:
	{
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_10 = ___0_item;
		List_1_AddWithResize_m55AFD45DC0ADD64C8749E1B59252B0BF0793CE49(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 11361
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m55AFD45DC0ADD64C8749E1B59252B0BF0793CE49 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		int32_t L_1 = V_0;
		List_1_EnsureCapacity_mB16BDFA4B73F390167601AF70BC65637BF9AE433(__this, ((int32_t)il2cpp_codegen_add(L_1, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_2 = V_0;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_2, 1));
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_3 = __this->____items;
		int32_t L_4 = V_0;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_5 = ___0_item;
		NullCheck(L_3);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(L_4), (XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2)L_5);
		return;
	}
}
// Method Definition Index: 11362
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_Add_mC6B19D56B8AE9140033F1D945B32C63D7787F1E7 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___0_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mADBF15F169E5DF2762161FB96F64C99569429300(L_0, ((int32_t)20), NULL);
	}
	try
	{
		RuntimeObject* L_1 = ___0_item;
		List_1_Add_m50157FAAF8FE0562A862140A17AD2B1097B078E7_inline(__this, ((*(XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2*)UnBox(L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		goto IL_0029;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0016;
		}
		throw e;
	}

CATCH_0016:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_2 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_3 = ___0_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_4 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_5;
		L_5 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_4, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_3, L_5, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0029;
	}

IL_0029:
	{
		int32_t L_6;
		L_6 = List_1_get_Count_m67096A69BCCCB33B2146561E2259883F084F5B95_inline(__this, NULL);
		return ((int32_t)il2cpp_codegen_subtract(L_6, 1));
	}
}
// Method Definition Index: 11363
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m11FEE6342C344D8B7EC9B4149CC6481D71B8131C (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		RuntimeObject* L_1 = ___0_collection;
		List_1_InsertRange_m2555E93F7C903532A49529BCC327427FC3B1C6EE(__this, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return;
	}
}
// Method Definition Index: 11364
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_t803A81BE343B15275BCD30A219ACAC97F0B9A1C8* List_1_AsReadOnly_mACE06FAFE83E7F8666C85F2B1221320C9317229A (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlyCollection_1_t803A81BE343B15275BCD30A219ACAC97F0B9A1C8* L_0 = (ReadOnlyCollection_1_t803A81BE343B15275BCD30A219ACAC97F0B9A1C8*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
		ReadOnlyCollection_1__ctor_mCAD4E9DC796CAE1655D1F09987BFC987E8FD72F1(L_0, __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11365
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m603A1422A5D4C6AC20CFDB1897BD527164CFE059 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		int32_t L_7 = ___1_count;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_8 = ___2_item;
		RuntimeObject* L_9 = ___3_comparer;
		int32_t L_10;
		L_10 = Array_BinarySearch_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_m469FFB0730A2E7136D62A2AE6D197A1858EC6CFE(L_5, L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18));
		return L_10;
	}
}
// Method Definition Index: 11366
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m801D312379BE0AE1182D84AD92CA7B2E586F4C94 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m67096A69BCCCB33B2146561E2259883F084F5B95_inline(__this, NULL);
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_BinarySearch_m603A1422A5D4C6AC20CFDB1897BD527164CFE059(__this, 0, L_0, L_1, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_2;
	}
}
// Method Definition Index: 11367
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m626ED321DA6F622943A33FF1D261635171AFF7EC (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, RuntimeObject* ___1_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m67096A69BCCCB33B2146561E2259883F084F5B95_inline(__this, NULL);
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_1 = ___0_item;
		RuntimeObject* L_2 = ___1_comparer;
		int32_t L_3;
		L_3 = List_1_BinarySearch_m603A1422A5D4C6AC20CFDB1897BD527164CFE059(__this, 0, L_0, L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_3;
	}
}
// Method Definition Index: 11368
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Clear_mEE78EC937E81E2FDA5335F33388A6E97E29F8F31 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
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
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_3 = __this->____items;
		int32_t L_4 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_3, 0, L_4, NULL);
		return;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11369
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_mA297C73B52DACB79EAF9BABC705E6D7DB8EB7978 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_IndexOf_m54DA0380F7731F2146DEDD98FC646038D9FC3923(__this, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return (bool)((((int32_t)((((int32_t)L_2) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_0016:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11370
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_Contains_m0CDE85DB8C5DC9B72599D70A788A493AC0B8A208 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m72B9D7AAB1A1E284232CC6C4CF613FEA6A227584(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Contains_mA297C73B52DACB79EAF9BABC705E6D7DB8EB7978(__this, ((*(XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11371
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_mF8F83E4B8C9C108EF18A8DD2013F6B28D7973601 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_0 = ___0_array;
		List_1_CopyTo_m0E4DE3BC195FF67210FE47068EE58A77C5074C85(__this, L_0, 0, NULL);
		return;
	}
}
// Method Definition Index: 11372
// Method Definition Index: 11373
// Method Definition Index: 11374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mB16BDFA4B73F390167601AF70BC65637BF9AE433 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_min, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t G_B4_0 = 0;
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int32_t L_2 = ___0_min;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003d;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_3 = __this->____items;
		NullCheck(L_3);
		if (!(((RuntimeArray*)L_3)->max_length))
		{
			goto IL_0020;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_4 = __this->____items;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		G_B4_0 = ((int32_t)il2cpp_codegen_multiply(L_5, 2));
		goto IL_0021;
	}

IL_0020:
	{
		G_B4_0 = 4;
	}

IL_0021:
	{
		V_0 = G_B4_0;
		int32_t L_6 = V_0;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)2146435071)))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = ((int32_t)2146435071);
	}

IL_0030:
	{
		int32_t L_7 = V_0;
		int32_t L_8 = ___0_min;
		if ((((int32_t)L_7) >= ((int32_t)L_8)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_9 = ___0_min;
		V_0 = L_9;
	}

IL_0036:
	{
		int32_t L_10 = V_0;
		List_1_set_Capacity_m5593B7194363BCB1B716A4ECA594F6E2CC03F94C(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_003d:
	{
		return;
	}
}
// Method Definition Index: 11375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_mB669AD0B558524366CCCE12802080E2B6C8E29F7 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_0 = ___0_match;
		int32_t L_1;
		L_1 = List_1_FindIndex_m14D5EB230C22B6C4A649235CC3E08C5F6561E15B(__this, L_0, NULL);
		return (bool)((((int32_t)((((int32_t)L_1) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 11376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 List_1_Find_mEA5FFD8AE168068A5E1A7C3F2EE50951DE0D95C4 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0032;
	}

IL_000d:
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_1 = ___0_match;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_2 = __this->____items;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		NullCheck(L_1);
		bool L_6;
		L_6 = Predicate_1_Invoke_m91BF5E52B5443EC259EE465ECFE80677DA340F98_inline(L_1, L_5, NULL);
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_7 = __this->____items;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		return L_10;
	}

IL_002e:
	{
		int32_t L_11 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0032:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____size;
		if ((((int32_t)L_12) < ((int32_t)L_13)))
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_1), sizeof(XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2));
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_14 = V_1;
		return L_14;
	}
}
// Method Definition Index: 11377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m14D5EB230C22B6C4A649235CC3E08C5F6561E15B (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_1 = ___0_match;
		int32_t L_2;
		L_2 = List_1_FindIndex_m3A09BF3BCAAF3C61D9A2EEF2C3090311C21A62EB(__this, 0, L_0, L_1, NULL);
		return L_2;
	}
}
// Method Definition Index: 11378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m3A09BF3BCAAF3C61D9A2EEF2C3090311C21A62EB (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_startIndex;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_000e:
	{
		int32_t L_2 = ___1_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_3) <= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_4, L_5)))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_0022:
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_6 = ___2_match;
		if (L_6)
		{
			goto IL_002b;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_002b:
	{
		int32_t L_7 = ___0_startIndex;
		int32_t L_8 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, L_8));
		int32_t L_9 = ___0_startIndex;
		V_1 = L_9;
		goto IL_004d;
	}

IL_0033:
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_10 = ___2_match;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_11 = __this->____items;
		int32_t L_12 = V_1;
		NullCheck(L_11);
		int32_t L_13 = L_12;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_14 = (L_11)->GetAt(static_cast<il2cpp_array_size_t>(L_13));
		NullCheck(L_10);
		bool L_15;
		L_15 = Predicate_1_Invoke_m91BF5E52B5443EC259EE465ECFE80677DA340F98_inline(L_10, L_14, NULL);
		if (!L_15)
		{
			goto IL_0049;
		}
	}
	{
		int32_t L_16 = V_1;
		return L_16;
	}

IL_0049:
	{
		int32_t L_17 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_004d:
	{
		int32_t L_18 = V_1;
		int32_t L_19 = V_0;
		if ((((int32_t)L_18) < ((int32_t)L_19)))
		{
			goto IL_0033;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m6D3AC86C71F42816D3E21F44F07E449BA78CFD8D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		int32_t L_1 = __this->____size;
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_2 = ___0_match;
		int32_t L_3;
		L_3 = List_1_FindLastIndex_m94DC2E52655A61CACBDD8FFD47097AAE3E618198(__this, ((int32_t)il2cpp_codegen_subtract(L_0, 1)), L_1, L_2, NULL);
		return L_3;
	}
}
// Method Definition Index: 11380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m94DC2E52655A61CACBDD8FFD47097AAE3E618198 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_0 = ___2_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		int32_t L_1 = __this->____size;
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = ___0_startIndex;
		if ((((int32_t)L_2) == ((int32_t)(-1))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
		goto IL_002a;
	}

IL_001c:
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		if ((!(((uint32_t)L_3) >= ((uint32_t)L_4))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_002a:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) < ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_6 = ___0_startIndex;
		int32_t L_7 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_6, L_7)), 1))) >= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}

IL_0036:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_003b:
	{
		int32_t L_8 = ___0_startIndex;
		int32_t L_9 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		int32_t L_10 = ___0_startIndex;
		V_1 = L_10;
		goto IL_005d;
	}

IL_0043:
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_11 = ___2_match;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_12 = __this->____items;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		NullCheck(L_11);
		bool L_16;
		L_16 = Predicate_1_Invoke_m91BF5E52B5443EC259EE465ECFE80677DA340F98_inline(L_11, L_15, NULL);
		if (!L_16)
		{
			goto IL_0059;
		}
	}
	{
		int32_t L_17 = V_1;
		return L_17;
	}

IL_0059:
	{
		int32_t L_18 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_18, 1));
	}

IL_005d:
	{
		int32_t L_19 = V_1;
		int32_t L_20 = V_0;
		if ((((int32_t)L_19) > ((int32_t)L_20)))
		{
			goto IL_0043;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_ForEach_mA5C50F2E961CBF3FD566EC636E04E15E5878DC49 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, Action_1_tC8CC18F6848AAE74469E78A12EC004873F4332A7* ___0_action, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Action_1_tC8CC18F6848AAE74469E78A12EC004873F4332A7* L_0 = ___0_action;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)33), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____version;
		V_0 = L_1;
		V_1 = 0;
		goto IL_0034;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____version;
		if ((!(((uint32_t)L_2) == ((uint32_t)L_3))))
		{
			goto IL_003d;
		}
	}
	{
		Action_1_tC8CC18F6848AAE74469E78A12EC004873F4332A7* L_4 = ___0_action;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_5 = __this->____items;
		int32_t L_6 = V_1;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		Action_1_Invoke_m14B5F0EFFF227D076151E00CACE917B6512254B8_inline(L_4, L_8, NULL);
		int32_t L_9 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0034:
	{
		int32_t L_10 = V_1;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0015;
		}
	}

IL_003d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____version;
		if ((((int32_t)L_12) == ((int32_t)L_13)))
		{
			goto IL_004b;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_004b:
	{
		return;
	}
}
// Method Definition Index: 11382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0 List_1_GetEnumerator_mB89A30406EEF5FDCEE5AF1043B26322952301CC5 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m5CBA8C17D9A8E5F5E385A4876B9B1745FA3CB8C0((&L_0), __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_Generic_IEnumerableU3CTU3E_GetEnumerator_mF0B7F334A99DCAF4683DC567B17D41A6533693AF (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m5CBA8C17D9A8E5F5E385A4876B9B1745FA3CB8C0((&L_0), __this, NULL);
		Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IEnumerable_GetEnumerator_m28BAB8390754C90274D3D56A124833D0EEC11046 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m5CBA8C17D9A8E5F5E385A4876B9B1745FA3CB8C0((&L_0), __this, NULL);
		Enumerator_tB41744EFD7274B3F0B75074E72D0EB8C658D06A0 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* List_1_GetRange_mB9031AD631C5CE205CA8A609E7609BE73F7EC115 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* L_6 = (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		List_1__ctor_m1742E82D5AF5200DFAC69B895C4A24E80D760C4D(L_6, L_5, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33));
		V_0 = L_6;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* L_9 = V_0;
		NullCheck(L_9);
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_10 = L_9->____items;
		int32_t L_11 = ___1_count;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_7, L_8, (RuntimeArray*)L_10, 0, L_11, NULL);
		List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* L_12 = V_0;
		int32_t L_13 = ___1_count;
		NullCheck(L_12);
		L_12->____size = L_13;
		List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* L_14 = V_0;
		return L_14;
	}
}
// Method Definition Index: 11386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m54DA0380F7731F2146DEDD98FC646038D9FC3923 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_0 = __this->____items;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_1 = ___0_item;
		int32_t L_2 = __this->____size;
		int32_t L_3;
		L_3 = Array_IndexOf_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mECE416DA9044547B55FF326280E3AFAB15850B37(L_0, L_1, 0, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34));
		return L_3;
	}
}
// Method Definition Index: 11387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_IndexOf_m092D30E460F94CBB0CA767F55E7FA83513870516 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m72B9D7AAB1A1E284232CC6C4CF613FEA6A227584(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		int32_t L_3;
		L_3 = List_1_IndexOf_m54DA0380F7731F2146DEDD98FC646038D9FC3923(__this, ((*(XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mBECAB59CCD69670908799776F1D6507EF7EA7D87 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___1_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)13), ((int32_t)27), NULL);
	}

IL_0012:
	{
		int32_t L_2 = __this->____size;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0030;
		}
	}
	{
		int32_t L_5 = __this->____size;
		List_1_EnsureCapacity_mB16BDFA4B73F390167601AF70BC65637BF9AE433(__this, ((int32_t)il2cpp_codegen_add(L_5, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
	}

IL_0030:
	{
		int32_t L_6 = ___0_index;
		int32_t L_7 = __this->____size;
		if ((((int32_t)L_6) >= ((int32_t)L_7)))
		{
			goto IL_0056;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_8 = __this->____items;
		int32_t L_9 = ___0_index;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_10 = __this->____items;
		int32_t L_11 = ___0_index;
		int32_t L_12 = __this->____size;
		int32_t L_13 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_8, L_9, (RuntimeArray*)L_10, ((int32_t)il2cpp_codegen_add(L_11, 1)), ((int32_t)il2cpp_codegen_subtract(L_12, L_13)), NULL);
	}

IL_0056:
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_16 = ___1_item;
		NullCheck(L_14);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(L_15), (XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2)L_16);
		int32_t L_17 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_17, 1));
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		return;
	}
}
// Method Definition Index: 11389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Insert_m9D19205B336741A33577DAA89A2E89EE6877607A (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, RuntimeObject* ___1_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mADBF15F169E5DF2762161FB96F64C99569429300(L_0, ((int32_t)20), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_item;
		List_1_Insert_mBECAB59CCD69670908799776F1D6507EF7EA7D87(__this, L_1, ((*(XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m2555E93F7C903532A49529BCC327427FC3B1C6EE (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	RuntimeObject* V_2 = NULL;
	{
		RuntimeObject* L_0 = ___1_collection;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___0_index;
		int32_t L_2 = __this->____size;
		if ((!(((uint32_t)L_1) > ((uint32_t)L_2))))
		{
			goto IL_0017;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ___1_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_4 = V_0;
		if (!L_4)
		{
			goto IL_00bc;
		}
	}
	{
		RuntimeObject* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_5);
		V_1 = L_6;
		int32_t L_7 = V_1;
		if ((((int32_t)L_7) <= ((int32_t)0)))
		{
			goto IL_00fb;
		}
	}
	{
		int32_t L_8 = __this->____size;
		int32_t L_9 = V_1;
		List_1_EnsureCapacity_mB16BDFA4B73F390167601AF70BC65637BF9AE433(__this, ((int32_t)il2cpp_codegen_add(L_8, L_9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_10 = ___0_index;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) >= ((int32_t)L_11)))
		{
			goto IL_0066;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_12 = __this->____items;
		int32_t L_13 = ___0_index;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = V_1;
		int32_t L_17 = __this->____size;
		int32_t L_18 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_12, L_13, (RuntimeArray*)L_14, ((int32_t)il2cpp_codegen_add(L_15, L_16)), ((int32_t)il2cpp_codegen_subtract(L_17, L_18)), NULL);
	}

IL_0066:
	{
		RuntimeObject* L_19 = V_0;
		if ((!(((RuntimeObject*)(List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB*)__this) == ((RuntimeObject*)(RuntimeObject*)L_19))))
		{
			goto IL_009f;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_20 = __this->____items;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_21 = __this->____items;
		int32_t L_22 = ___0_index;
		int32_t L_23 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_20, 0, (RuntimeArray*)L_21, L_22, L_23, NULL);
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_24 = __this->____items;
		int32_t L_25 = ___0_index;
		int32_t L_26 = V_1;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_27 = __this->____items;
		int32_t L_28 = ___0_index;
		int32_t L_29 = __this->____size;
		int32_t L_30 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_24, ((int32_t)il2cpp_codegen_add(L_25, L_26)), (RuntimeArray*)L_27, ((int32_t)il2cpp_codegen_multiply(L_28, 2)), ((int32_t)il2cpp_codegen_subtract(L_29, L_30)), NULL);
		goto IL_00ac;
	}

IL_009f:
	{
		RuntimeObject* L_31 = V_0;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_32 = __this->____items;
		int32_t L_33 = ___0_index;
		NullCheck(L_31);
		InterfaceActionInvoker2< XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_31, L_32, L_33);
	}

IL_00ac:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_34, L_35));
		goto IL_00fb;
	}

IL_00bc:
	{
		int32_t L_36 = ___0_index;
		int32_t L_37 = __this->____size;
		if ((((int32_t)L_36) >= ((int32_t)L_37)))
		{
			goto IL_00f4;
		}
	}
	{
		RuntimeObject* L_38 = ___1_collection;
		NullCheck(L_38);
		RuntimeObject* L_39;
		L_39 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_38);
		V_2 = L_39;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ea:
			{
				{
					RuntimeObject* L_40 = V_2;
					if (!L_40)
					{
						goto IL_00f3;
					}
				}
				{
					RuntimeObject* L_41 = V_2;
					NullCheck(L_41);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_41);
				}

IL_00f3:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00e0_1;
			}

IL_00ce_1:
			{
				int32_t L_42 = ___0_index;
				int32_t L_43 = L_42;
				___0_index = ((int32_t)il2cpp_codegen_add(L_43, 1));
				RuntimeObject* L_44 = V_2;
				NullCheck(L_44);
				XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_45;
				L_45 = InterfaceFuncInvoker0< XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_44);
				List_1_Insert_mBECAB59CCD69670908799776F1D6507EF7EA7D87(__this, L_43, L_45, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
			}

IL_00e0_1:
			{
				RuntimeObject* L_46 = V_2;
				NullCheck(L_46);
				bool L_47;
				L_47 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_46);
				if (L_47)
				{
					goto IL_00ce_1;
				}
			}
			{
				goto IL_00fb;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f4:
	{
		RuntimeObject* L_48 = ___1_collection;
		List_1_AddEnumerable_m6C3A37912654122DD8AE143EFEB1C2A4C1015C29(__this, L_48, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_00fb:
	{
		int32_t L_49 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_49, 1));
		return;
	}
}
// Method Definition Index: 11391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m15AEA673A2E9F2A482D49F374814A49D0734CEC6 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_0 = ___0_item;
		int32_t L_1;
		L_1 = List_1_IndexOf_m54DA0380F7731F2146DEDD98FC646038D9FC3923(__this, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		int32_t L_3 = V_0;
		List_1_RemoveAt_m53875FA8AA50D0F755EE525314DB9818741B6E2F(__this, L_3, NULL);
		return (bool)1;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Remove_m3FECF0FF7CFC70F9CAFD6638404006EDD1958497 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m72B9D7AAB1A1E284232CC6C4CF613FEA6A227584(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Remove_m15AEA673A2E9F2A482D49F374814A49D0734CEC6(__this, ((*(XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41));
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 11393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_RemoveAll_mA70F72C868278E8BFADAF3B4360A368940DD3C6E (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0011;
	}

IL_000d:
	{
		int32_t L_1 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_1, 1));
	}

IL_0011:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____size;
		if ((((int32_t)L_2) >= ((int32_t)L_3)))
		{
			goto IL_002e;
		}
	}
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_4 = ___0_match;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_5 = __this->____items;
		int32_t L_6 = V_0;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		bool L_9;
		L_9 = Predicate_1_Invoke_m91BF5E52B5443EC259EE465ECFE80677DA340F98_inline(L_4, L_8, NULL);
		if (!L_9)
		{
			goto IL_000d;
		}
	}

IL_002e:
	{
		int32_t L_10 = V_0;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0039;
		}
	}
	{
		return 0;
	}

IL_0039:
	{
		int32_t L_12 = V_0;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
		goto IL_0089;
	}

IL_003f:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_0043:
	{
		int32_t L_14 = V_1;
		int32_t L_15 = __this->____size;
		if ((((int32_t)L_14) >= ((int32_t)L_15)))
		{
			goto IL_0060;
		}
	}
	{
		Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* L_16 = ___0_match;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_17 = __this->____items;
		int32_t L_18 = V_1;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_20 = (L_17)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_16);
		bool L_21;
		L_21 = Predicate_1_Invoke_m91BF5E52B5443EC259EE465ECFE80677DA340F98_inline(L_16, L_20, NULL);
		if (L_21)
		{
			goto IL_003f;
		}
	}

IL_0060:
	{
		int32_t L_22 = V_1;
		int32_t L_23 = __this->____size;
		if ((((int32_t)L_22) >= ((int32_t)L_23)))
		{
			goto IL_0089;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_24 = __this->____items;
		int32_t L_25 = V_0;
		int32_t L_26 = L_25;
		V_0 = ((int32_t)il2cpp_codegen_add(L_26, 1));
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_27 = __this->____items;
		int32_t L_28 = V_1;
		int32_t L_29 = L_28;
		V_1 = ((int32_t)il2cpp_codegen_add(L_29, 1));
		NullCheck(L_27);
		int32_t L_30 = L_29;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_31 = (L_27)->GetAt(static_cast<il2cpp_array_size_t>(L_30));
		NullCheck(L_24);
		(L_24)->SetAt(static_cast<il2cpp_array_size_t>(L_26), (XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2)L_31);
	}

IL_0089:
	{
		int32_t L_32 = V_1;
		int32_t L_33 = __this->____size;
		if ((((int32_t)L_32) < ((int32_t)L_33)))
		{
			goto IL_0043;
		}
	}
	{
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_34 = __this->____items;
		int32_t L_35 = V_0;
		int32_t L_36 = __this->____size;
		int32_t L_37 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_34, L_35, ((int32_t)il2cpp_codegen_subtract(L_36, L_37)), NULL);
	}

IL_00ad:
	{
		int32_t L_38 = __this->____size;
		int32_t L_39 = V_0;
		int32_t L_40 = V_0;
		__this->____size = L_40;
		int32_t L_41 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_41, 1));
		return ((int32_t)il2cpp_codegen_subtract(L_38, L_39));
	}
}
// Method Definition Index: 11394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m53875FA8AA50D0F755EE525314DB9818741B6E2F (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		int32_t L_3 = ___0_index;
		int32_t L_4 = __this->____size;
		if ((((int32_t)L_3) >= ((int32_t)L_4)))
		{
			goto IL_0042;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		int32_t L_9 = __this->____size;
		int32_t L_10 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_5, ((int32_t)il2cpp_codegen_add(L_6, 1)), (RuntimeArray*)L_7, L_8, ((int32_t)il2cpp_codegen_subtract(L_9, L_10)), NULL);
	}

IL_0042:
	{
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_11 = __this->____items;
		int32_t L_12 = __this->____size;
		il2cpp_codegen_initobj((&V_0), sizeof(XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2));
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_13 = V_0;
		NullCheck(L_11);
		(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_12), (XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2)L_13);
	}

IL_0063:
	{
		int32_t L_14 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_14, 1));
		return;
	}
}
// Method Definition Index: 11395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveRange_mC1B1AB6D62C187164FAC266ABA9E2CCF9A82E319 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		int32_t L_6 = __this->____size;
		int32_t L_7 = __this->____size;
		int32_t L_8 = ___1_count;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_7, L_8));
		int32_t L_9 = ___0_index;
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_9) >= ((int32_t)L_10)))
		{
			goto IL_0066;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_11 = __this->____items;
		int32_t L_12 = ___0_index;
		int32_t L_13 = ___1_count;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = __this->____size;
		int32_t L_17 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_11, ((int32_t)il2cpp_codegen_add(L_12, L_13)), (RuntimeArray*)L_14, L_15, ((int32_t)il2cpp_codegen_subtract(L_16, L_17)), NULL);
	}

IL_0066:
	{
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_19 = __this->____items;
		int32_t L_20 = __this->____size;
		int32_t L_21 = ___1_count;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_19, L_20, L_21, NULL);
	}

IL_008d:
	{
		return;
	}
}
// Method Definition Index: 11396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m30B202196815C19AE8B0490CBAAB3BD9BFE7708D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m67096A69BCCCB33B2146561E2259883F084F5B95_inline(__this, NULL);
		List_1_Reverse_m3CE641659C3CD3760E5EE96E88FA7006B7D9995D(__this, 0, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42));
		return;
	}
}
// Method Definition Index: 11397
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m3CE641659C3CD3760E5EE96E88FA7006B7D9995D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0038;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		Array_Reverse_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_m6953DC1CD28104B40EF7B6C79E12EDA04CCC52F3(L_6, L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43));
	}

IL_0038:
	{
		int32_t L_9 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_9, 1));
		return;
	}
}
// Method Definition Index: 11398
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mBE8198663CB0C59B33595171E2AEB7AC234A928B (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m67096A69BCCCB33B2146561E2259883F084F5B95_inline(__this, NULL);
		List_1_Sort_m52E4077FCE9FF6DF21E5DCE7A99DACB700AB2E0D(__this, 0, L_0, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mDF03A517DD0404D794600FB10DB2E1C96B05CDDB (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m67096A69BCCCB33B2146561E2259883F084F5B95_inline(__this, NULL);
		RuntimeObject* L_1 = ___0_comparer;
		List_1_Sort_m52E4077FCE9FF6DF21E5DCE7A99DACB700AB2E0D(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m52E4077FCE9FF6DF21E5DCE7A99DACB700AB2E0D (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0039;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		RuntimeObject* L_9 = ___2_comparer;
		Array_Sort_TisXRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2_mACACE6CA017DAF3419A4C86E329FA9CFE4018C03(L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45));
	}

IL_0039:
	{
		int32_t L_10 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_10, 1));
		return;
	}
}
// Method Definition Index: 11401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m4516B76B51AD343F66ACF3B5A7F9759588DACAB1 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, Comparison_1_t55FCE173570442A2D451AE8BFC15B22D41D55DC9* ___0_comparison, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit);
	//<source_info:<no-source>:1>
	{
		Comparison_1_t55FCE173570442A2D451AE8BFC15B22D41D55DC9* L_0 = ___0_comparison;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)34), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_1) <= ((int32_t)1)))
		{
			goto IL_0026;
		}
	}
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_2 = __this->____items;
		int32_t L_3 = __this->____size;
		Comparison_1_t55FCE173570442A2D451AE8BFC15B22D41D55DC9* L_4 = ___0_comparison;
		CHECKED_LOCAL_INIT(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 47)),il2cpp_codegen_runtime_class_init_inline);
		ArraySortHelper_1_Sort_m479DAA4C8994B2FBFA82BCC2220725F25C35DCD9(L_2, 0, L_3, L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46));
	}

IL_0026:
	{
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11402
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* List_1_ToArray_m677619684B3EF4C720C66F624ADAB575895E7157 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* V_0 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_1 = ((List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_3 = (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)(XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_2);
		V_0 = L_3;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_4 = __this->____items;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_5 = V_0;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_4, 0, (RuntimeArray*)L_5, 0, L_6, NULL);
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 11403
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_TrimExcess_m6ED6F38E8DE4AB9707C793DAA411AEC7E220BD79 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		double L_2 = (il2cpp_codegen_conv<double,int32_t,int32_t,false,false>(L_1,NULL));
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,double,double,false,false>(((double)il2cpp_codegen_multiply(L_2, (0.90000000000000002))),NULL));
		V_0 = L_3;
		int32_t L_4 = __this->____size;
		int32_t L_5 = V_0;
		if ((((int32_t)L_4) >= ((int32_t)L_5)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = __this->____size;
		List_1_set_Capacity_m5593B7194363BCB1B716A4ECA594F6E2CC03F94C(__this, L_6, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11404
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m6C3A37912654122DD8AE143EFEB1C2A4C1015C29 (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		RuntimeObject* L_1 = ___0_enumerable;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_1);
		V_0 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0063:
			{
				{
					RuntimeObject* L_3 = V_0;
					if (!L_3)
					{
						goto IL_006c;
					}
				}
				{
					RuntimeObject* L_4 = V_0;
					NullCheck(L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_4);
				}

IL_006c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0059_1;
			}

IL_0017_1:
			{
				RuntimeObject* L_5 = V_0;
				NullCheck(L_5);
				XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_6;
				L_6 = InterfaceFuncInvoker0< XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_5);
				V_1 = L_6;
				int32_t L_7 = __this->____size;
				XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_8 = __this->____items;
				NullCheck(L_8);
				int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
				if ((!(((uint32_t)L_7) == ((uint32_t)L_9))))
				{
					goto IL_003c_1;
				}
			}
			{
				int32_t L_10 = __this->____size;
				List_1_EnsureCapacity_mB16BDFA4B73F390167601AF70BC65637BF9AE433(__this, ((int32_t)il2cpp_codegen_add(L_10, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
			}

IL_003c_1:
			{
				XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_11 = __this->____items;
				int32_t L_12 = __this->____size;
				V_2 = L_12;
				int32_t L_13 = V_2;
				__this->____size = ((int32_t)il2cpp_codegen_add(L_13, 1));
				int32_t L_14 = V_2;
				XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_15 = V_1;
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_14), (XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2)L_15);
			}

IL_0059_1:
			{
				RuntimeObject* L_16 = V_0;
				NullCheck(L_16);
				bool L_17;
				L_17 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_16);
				if (L_17)
				{
					goto IL_0017_1;
				}
			}
			{
				goto IL_006d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006d:
	{
		return;
	}
}
// Method Definition Index: 11405
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__cctor_m567B01372F1083CB9736FFF9FAA3FA8B86FA3FB2 (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_0 = (XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)(XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (uint32_t)0);
		((List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray), (void*)L_0);
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
// Method Definition Index: 11344
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7378BA5FBCA1E88080985FB810820B3F47DAD54C_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_0 = ((List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_0);
		return;
	}
}
// Method Definition Index: 11345
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m54D719307910A9DC9CD4DEFA8AE2626F49EB7516_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_capacity, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		int32_t L_0 = ___0_capacity;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)12), 4, NULL);
	}

IL_0012:
	{
		int32_t L_1 = ___0_capacity;
		if (L_1)
		{
			goto IL_0021;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_2 = ((List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_2);
		return;
	}

IL_0021:
	{
		int32_t L_3 = ___0_capacity;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_4 = (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)(__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_3);
		__this->____items = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_4);
		return;
	}
}
// Method Definition Index: 11346
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m4D165FBE2796C395527CD5404B0C5D0F2828736A_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_000f:
	{
		RuntimeObject* L_1 = ___0_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0050;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_3);
		V_1 = L_4;
		int32_t L_5 = V_1;
		if (L_5)
		{
			goto IL_002f;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_6 = ((List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_6);
		return;
	}

IL_002f:
	{
		int32_t L_7 = V_1;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_8 = (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)(__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_7);
		__this->____items = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_8);
		RuntimeObject* L_9 = V_0;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_10 = __this->____items;
		NullCheck(L_9);
		InterfaceActionInvoker2< __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_9, L_10, 0);
		int32_t L_11 = V_1;
		__this->____size = L_11;
		return;
	}

IL_0050:
	{
		__this->____size = 0;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_12 = ((List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		RuntimeObject* L_13 = ___0_collection;
		List_1_AddEnumerable_m44B5C2177E9403DB0E3DC2BC6E590CDA7266BE79(__this, L_13, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
		return;
	}
}
// Method Definition Index: 11347
// Method Definition Index: 11348
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m4B62593B8671BB60809057D57DC60568557315E6_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* V_0 = NULL;
	{
		int32_t L_0 = ___0_value;
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)15), ((int32_t)21), NULL);
	}

IL_0012:
	{
		int32_t L_2 = ___0_value;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_2) == ((int32_t)L_4)))
		{
			goto IL_0058;
		}
	}
	{
		int32_t L_5 = ___0_value;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_004d;
		}
	}
	{
		int32_t L_6 = ___0_value;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_7 = (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)(__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_6);
		V_0 = L_7;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0045;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_9 = __this->____items;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_10 = V_0;
		int32_t L_11 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_9, 0, (RuntimeArray*)L_10, 0, L_11, NULL);
	}

IL_0045:
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_12 = V_0;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		return;
	}

IL_004d:
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_13 = ((List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_13);
	}

IL_0058:
	{
		return;
	}
}
// Method Definition Index: 11349
// Method Definition Index: 11350
// Method Definition Index: 11351
// Method Definition Index: 11352
// Method Definition Index: 11353
// Method Definition Index: 11354
// Method Definition Index: 11355
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* List_1_get_Item_m02336594E8F76D7313E2BCB7A3ED5ABD13B4D515_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		Il2CppSharedGenericObject* L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		return L_5;
	}
}
// Method Definition Index: 11356
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_mCD8F7775F9C4842958A7188EBC0EBF1BE03101EF_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, Il2CppSharedGenericObject* ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		Il2CppSharedGenericObject* L_4 = ___1_value;
		NullCheck(L_2);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(L_3), (Il2CppSharedGenericObject*)L_4);
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11357
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m9E5E0DF2443D88570595AEA0FEF7D7122455EB26_gshared (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	Il2CppSharedGenericObject* V_0 = NULL;
	{
		RuntimeObject* L_0 = ___0_value;
		if (((Il2CppSharedGenericObject*)IsInst((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6))))
		{
			goto IL_001f;
		}
	}
	{
		RuntimeObject* L_1 = ___0_value;
		if (L_1)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_2 = V_0;
		return (bool)((((RuntimeObject*)(Il2CppSharedGenericObject*)L_2) == ((RuntimeObject*)(RuntimeObject*)NULL))? 1 : 0);
	}

IL_001d:
	{
		return (bool)0;
	}

IL_001f:
	{
		return (bool)1;
	}
}
// Method Definition Index: 11358
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IList_get_Item_m78BCF727FD3CF47C4FE97D00AFAD2781B6A7F004_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		Il2CppSharedGenericObject* L_1;
		L_1 = List_1_get_Item_m02336594E8F76D7313E2BCB7A3ED5ABD13B4D515(__this, L_0, NULL);
		return (RuntimeObject*)L_1;
	}
}
// Method Definition Index: 11359
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_set_Item_m5E8C43852B62BD7E2A9582754B44057F224BD12D_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_value;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisIl2CppSharedGenericObject_mE7A1EEC53480BA1ED6C827938ADAF45B127DD054(L_0, ((int32_t)15), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_value;
		List_1_set_Item_mCD8F7775F9C4842958A7188EBC0EBF1BE03101EF(__this, L_1, ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6))), NULL);
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_value;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11360
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Add_m0CE6E39234ED33F5195AABC8029D06D4756C75C1_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_7 = V_0;
		int32_t L_8 = V_1;
		Il2CppSharedGenericObject* L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (Il2CppSharedGenericObject*)L_9);
		return;
	}

IL_0034:
	{
		Il2CppSharedGenericObject* L_10 = ___0_item;
		List_1_AddWithResize_mCC24A812DFC30C3DE2230E3C8EE705871C1DFF81(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 11361
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_mCC24A812DFC30C3DE2230E3C8EE705871C1DFF81_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		int32_t L_1 = V_0;
		List_1_EnsureCapacity_m89719BB37EEDA80E6AD7FAC5D1AAC5A47DDE86B5(__this, ((int32_t)il2cpp_codegen_add(L_1, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_2 = V_0;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_2, 1));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_3 = __this->____items;
		int32_t L_4 = V_0;
		Il2CppSharedGenericObject* L_5 = ___0_item;
		NullCheck(L_3);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(L_4), (Il2CppSharedGenericObject*)L_5);
		return;
	}
}
// Method Definition Index: 11362
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_Add_mC916FB88482D2EF5E43527D9840C8E67FAC4D4CB_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___0_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisIl2CppSharedGenericObject_mE7A1EEC53480BA1ED6C827938ADAF45B127DD054(L_0, ((int32_t)20), NULL);
	}
	try
	{
		RuntimeObject* L_1 = ___0_item;
		List_1_Add_m0CE6E39234ED33F5195AABC8029D06D4756C75C1_inline(__this, ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		goto IL_0029;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0016;
		}
		throw e;
	}

CATCH_0016:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_2 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_3 = ___0_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_4 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_5;
		L_5 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_4, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_3, L_5, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0029;
	}

IL_0029:
	{
		int32_t L_6;
		L_6 = List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline(__this, NULL);
		return ((int32_t)il2cpp_codegen_subtract(L_6, 1));
	}
}
// Method Definition Index: 11363
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m655A34E010FCB41069350D2D616558180A0C0AFF_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		RuntimeObject* L_1 = ___0_collection;
		List_1_InsertRange_m7B9D6C4AB69B4EA07C23832C6109C25C21E99804(__this, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return;
	}
}
// Method Definition Index: 11364
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263* List_1_AsReadOnly_m55F73F8553E9885F803507A3650967BE90AA81C9_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263* L_0 = (ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
		ReadOnlyCollection_1__ctor_mC1890FAC00703F47A655C35CBCB613C74A811580(L_0, (RuntimeObject*)__this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11365
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mB2BE3DF9182ED9C862AA44566C878D046534474E_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, Il2CppSharedGenericObject* ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		int32_t L_7 = ___1_count;
		Il2CppSharedGenericObject* L_8 = ___2_item;
		RuntimeObject* L_9 = ___3_comparer;
		int32_t L_10;
		L_10 = Array_BinarySearch_TisIl2CppSharedGenericObject_m6D8A1F3C8EDCAAF226FE8B32CBB649F099D8C16B(L_5, L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18));
		return L_10;
	}
}
// Method Definition Index: 11366
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m541581D188E2E7A2C2FF99CDE1FA62559C4E22F7_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline(__this, NULL);
		Il2CppSharedGenericObject* L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_BinarySearch_mB2BE3DF9182ED9C862AA44566C878D046534474E(__this, 0, L_0, L_1, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_2;
	}
}
// Method Definition Index: 11367
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m1AE074244BC7C4672E92D4E5B8F2E1A5015F8C8F_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, RuntimeObject* ___1_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline(__this, NULL);
		Il2CppSharedGenericObject* L_1 = ___0_item;
		RuntimeObject* L_2 = ___1_comparer;
		int32_t L_3;
		L_3 = List_1_BinarySearch_mB2BE3DF9182ED9C862AA44566C878D046534474E(__this, 0, L_0, L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_3;
	}
}
// Method Definition Index: 11368
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Clear_mCCC05B98943E8F4E0F6FBF47063A269048AE71E5_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
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
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_3 = __this->____items;
		int32_t L_4 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_3, 0, L_4, NULL);
		return;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11369
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m63E643DC9A821297E2EBF9085D93B39BFC2F9DCE_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		Il2CppSharedGenericObject* L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_IndexOf_m4B13B6BE7AA6327431F1BE406264329892975DE9(__this, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return (bool)((((int32_t)((((int32_t)L_2) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_0016:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11370
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_Contains_m7E4E8EA0E50007EC7F90752FBB913B35DBD8C00B_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m9E5E0DF2443D88570595AEA0FEF7D7122455EB26(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Contains_m63E643DC9A821297E2EBF9085D93B39BFC2F9DCE(__this, ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11371
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_m04A1E702FBAB126758881A6A6D42052295136DCD_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_0 = ___0_array;
		List_1_CopyTo_m75919E1737F4EC8D7DD329629C11CACA64EF5BE3(__this, L_0, 0, NULL);
		return;
	}
}
// Method Definition Index: 11372
// Method Definition Index: 11373
// Method Definition Index: 11374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_m89719BB37EEDA80E6AD7FAC5D1AAC5A47DDE86B5_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_min, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t G_B4_0 = 0;
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int32_t L_2 = ___0_min;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003d;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_3 = __this->____items;
		NullCheck(L_3);
		if (!(((RuntimeArray*)L_3)->max_length))
		{
			goto IL_0020;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_4 = __this->____items;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		G_B4_0 = ((int32_t)il2cpp_codegen_multiply(L_5, 2));
		goto IL_0021;
	}

IL_0020:
	{
		G_B4_0 = 4;
	}

IL_0021:
	{
		V_0 = G_B4_0;
		int32_t L_6 = V_0;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)2146435071)))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = ((int32_t)2146435071);
	}

IL_0030:
	{
		int32_t L_7 = V_0;
		int32_t L_8 = ___0_min;
		if ((((int32_t)L_7) >= ((int32_t)L_8)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_9 = ___0_min;
		V_0 = L_9;
	}

IL_0036:
	{
		int32_t L_10 = V_0;
		List_1_set_Capacity_m4B62593B8671BB60809057D57DC60568557315E6(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_003d:
	{
		return;
	}
}
// Method Definition Index: 11375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_mEF102A40848CF9A0986FF9992D666F99F3DC79BB_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_0 = ___0_match;
		int32_t L_1;
		L_1 = List_1_FindIndex_mD56F0607D7DAD9622F7DE2C6C8957D9C7847EEC5(__this, L_0, NULL);
		return (bool)((((int32_t)((((int32_t)L_1) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 11376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* List_1_Find_m6620F179C7B4BFD1BB040EA342AEBDEC8B2C5B44_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	Il2CppSharedGenericObject* V_1 = NULL;
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0032;
	}

IL_000d:
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_1 = ___0_match;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_2 = __this->____items;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		Il2CppSharedGenericObject* L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		NullCheck(L_1);
		bool L_6;
		L_6 = Predicate_1_Invoke_mDF332C0CEB003C0D86A1E3A194ECFA96D424E34C_inline(L_1, L_5, NULL);
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_7 = __this->____items;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		Il2CppSharedGenericObject* L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		return L_10;
	}

IL_002e:
	{
		int32_t L_11 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0032:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____size;
		if ((((int32_t)L_12) < ((int32_t)L_13)))
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_14 = V_1;
		return L_14;
	}
}
// Method Definition Index: 11377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mD56F0607D7DAD9622F7DE2C6C8957D9C7847EEC5_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_1 = ___0_match;
		int32_t L_2;
		L_2 = List_1_FindIndex_m701B15E692EF4FFE95A773C8246D55779CB00B2D(__this, 0, L_0, L_1, NULL);
		return L_2;
	}
}
// Method Definition Index: 11378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m701B15E692EF4FFE95A773C8246D55779CB00B2D_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_startIndex;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_000e:
	{
		int32_t L_2 = ___1_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_3) <= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_4, L_5)))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_0022:
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_6 = ___2_match;
		if (L_6)
		{
			goto IL_002b;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_002b:
	{
		int32_t L_7 = ___0_startIndex;
		int32_t L_8 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, L_8));
		int32_t L_9 = ___0_startIndex;
		V_1 = L_9;
		goto IL_004d;
	}

IL_0033:
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_10 = ___2_match;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_11 = __this->____items;
		int32_t L_12 = V_1;
		NullCheck(L_11);
		int32_t L_13 = L_12;
		Il2CppSharedGenericObject* L_14 = (L_11)->GetAt(static_cast<il2cpp_array_size_t>(L_13));
		NullCheck(L_10);
		bool L_15;
		L_15 = Predicate_1_Invoke_mDF332C0CEB003C0D86A1E3A194ECFA96D424E34C_inline(L_10, L_14, NULL);
		if (!L_15)
		{
			goto IL_0049;
		}
	}
	{
		int32_t L_16 = V_1;
		return L_16;
	}

IL_0049:
	{
		int32_t L_17 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_004d:
	{
		int32_t L_18 = V_1;
		int32_t L_19 = V_0;
		if ((((int32_t)L_18) < ((int32_t)L_19)))
		{
			goto IL_0033;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m8F234B6692487E3D9AE864FB759FF51E5C35EEE5_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		int32_t L_1 = __this->____size;
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_2 = ___0_match;
		int32_t L_3;
		L_3 = List_1_FindLastIndex_m75AB13CBCA95A0E0722F6CDE89F0CBDAB27322DA(__this, ((int32_t)il2cpp_codegen_subtract(L_0, 1)), L_1, L_2, NULL);
		return L_3;
	}
}
// Method Definition Index: 11380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m75AB13CBCA95A0E0722F6CDE89F0CBDAB27322DA_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_0 = ___2_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		int32_t L_1 = __this->____size;
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = ___0_startIndex;
		if ((((int32_t)L_2) == ((int32_t)(-1))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
		goto IL_002a;
	}

IL_001c:
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		if ((!(((uint32_t)L_3) >= ((uint32_t)L_4))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_002a:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) < ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_6 = ___0_startIndex;
		int32_t L_7 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_6, L_7)), 1))) >= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}

IL_0036:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_003b:
	{
		int32_t L_8 = ___0_startIndex;
		int32_t L_9 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		int32_t L_10 = ___0_startIndex;
		V_1 = L_10;
		goto IL_005d;
	}

IL_0043:
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_11 = ___2_match;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_12 = __this->____items;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		Il2CppSharedGenericObject* L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		NullCheck(L_11);
		bool L_16;
		L_16 = Predicate_1_Invoke_mDF332C0CEB003C0D86A1E3A194ECFA96D424E34C_inline(L_11, L_15, NULL);
		if (!L_16)
		{
			goto IL_0059;
		}
	}
	{
		int32_t L_17 = V_1;
		return L_17;
	}

IL_0059:
	{
		int32_t L_18 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_18, 1));
	}

IL_005d:
	{
		int32_t L_19 = V_1;
		int32_t L_20 = V_0;
		if ((((int32_t)L_19) > ((int32_t)L_20)))
		{
			goto IL_0043;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_ForEach_m8ADDD90CD18FA47D7C9010951EE34AA49513AC50_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* ___0_action, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* L_0 = ___0_action;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)33), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____version;
		V_0 = L_1;
		V_1 = 0;
		goto IL_0034;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____version;
		if ((!(((uint32_t)L_2) == ((uint32_t)L_3))))
		{
			goto IL_003d;
		}
	}
	{
		Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* L_4 = ___0_action;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_5 = __this->____items;
		int32_t L_6 = V_1;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		Il2CppSharedGenericObject* L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		Action_1_Invoke_mDB8F7C7381C08B4DCE4E409D2FA4073BC17F90B5_inline(L_4, L_8, NULL);
		int32_t L_9 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0034:
	{
		int32_t L_10 = V_1;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0015;
		}
	}

IL_003d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____version;
		if ((((int32_t)L_12) == ((int32_t)L_13)))
		{
			goto IL_004b;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_004b:
	{
		return;
	}
}
// Method Definition Index: 11382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9 List_1_GetEnumerator_mD48177D95D4B5D6A9D8E84E2477668C2850DD5D9_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_mB0C2B8891214015BE286DC32F260DEBDFAD317E9((&L_0), __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_Generic_IEnumerableU3CTU3E_GetEnumerator_mBF03CBE09BFBA1E25B3E040FC761F6674F19D793_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_mB0C2B8891214015BE286DC32F260DEBDFAD317E9((&L_0), __this, NULL);
		Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IEnumerable_GetEnumerator_m2B6B507A19EBC0BBE5E87F276DB25C8B3388EC75_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_mB0C2B8891214015BE286DC32F260DEBDFAD317E9((&L_0), __this, NULL);
		Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* List_1_GetRange_m2C6E6EDDC70F1D5E30EF591172FA5590D17035FB_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_6 = (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		List_1__ctor_m54D719307910A9DC9CD4DEFA8AE2626F49EB7516(L_6, L_5, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33));
		V_0 = L_6;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_9 = V_0;
		NullCheck(L_9);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_10 = L_9->____items;
		int32_t L_11 = ___1_count;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_7, L_8, (RuntimeArray*)L_10, 0, L_11, NULL);
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_12 = V_0;
		int32_t L_13 = ___1_count;
		NullCheck(L_12);
		L_12->____size = L_13;
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_14 = V_0;
		return L_14;
	}
}
// Method Definition Index: 11386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m4B13B6BE7AA6327431F1BE406264329892975DE9_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_0 = __this->____items;
		Il2CppSharedGenericObject* L_1 = ___0_item;
		int32_t L_2 = __this->____size;
		int32_t L_3;
		L_3 = Array_IndexOf_TisIl2CppSharedGenericObject_m0C73DE72012C676D6E4354C82EA6550BBADC3D33(L_0, L_1, 0, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34));
		return L_3;
	}
}
// Method Definition Index: 11387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_IndexOf_m1C5FA1B6E2917EF7F5DDC5BF65147FE6EF7F8517_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m9E5E0DF2443D88570595AEA0FEF7D7122455EB26(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		int32_t L_3;
		L_3 = List_1_IndexOf_m4B13B6BE7AA6327431F1BE406264329892975DE9(__this, ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mC5A8238645AD1D203E75313802D4DC78BDE7857E_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, Il2CppSharedGenericObject* ___1_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)13), ((int32_t)27), NULL);
	}

IL_0012:
	{
		int32_t L_2 = __this->____size;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0030;
		}
	}
	{
		int32_t L_5 = __this->____size;
		List_1_EnsureCapacity_m89719BB37EEDA80E6AD7FAC5D1AAC5A47DDE86B5(__this, ((int32_t)il2cpp_codegen_add(L_5, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
	}

IL_0030:
	{
		int32_t L_6 = ___0_index;
		int32_t L_7 = __this->____size;
		if ((((int32_t)L_6) >= ((int32_t)L_7)))
		{
			goto IL_0056;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_8 = __this->____items;
		int32_t L_9 = ___0_index;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_10 = __this->____items;
		int32_t L_11 = ___0_index;
		int32_t L_12 = __this->____size;
		int32_t L_13 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_8, L_9, (RuntimeArray*)L_10, ((int32_t)il2cpp_codegen_add(L_11, 1)), ((int32_t)il2cpp_codegen_subtract(L_12, L_13)), NULL);
	}

IL_0056:
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		Il2CppSharedGenericObject* L_16 = ___1_item;
		NullCheck(L_14);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(L_15), (Il2CppSharedGenericObject*)L_16);
		int32_t L_17 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_17, 1));
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		return;
	}
}
// Method Definition Index: 11389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Insert_m09D3F4F2042326635CFB5369AC8F9760A7F81CE2_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, RuntimeObject* ___1_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisIl2CppSharedGenericObject_mE7A1EEC53480BA1ED6C827938ADAF45B127DD054(L_0, ((int32_t)20), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_item;
		List_1_Insert_mC5A8238645AD1D203E75313802D4DC78BDE7857E(__this, L_1, ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m7B9D6C4AB69B4EA07C23832C6109C25C21E99804_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	RuntimeObject* V_2 = NULL;
	{
		RuntimeObject* L_0 = ___1_collection;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___0_index;
		int32_t L_2 = __this->____size;
		if ((!(((uint32_t)L_1) > ((uint32_t)L_2))))
		{
			goto IL_0017;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ___1_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_4 = V_0;
		if (!L_4)
		{
			goto IL_00bc;
		}
	}
	{
		RuntimeObject* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_5);
		V_1 = L_6;
		int32_t L_7 = V_1;
		if ((((int32_t)L_7) <= ((int32_t)0)))
		{
			goto IL_00fb;
		}
	}
	{
		int32_t L_8 = __this->____size;
		int32_t L_9 = V_1;
		List_1_EnsureCapacity_m89719BB37EEDA80E6AD7FAC5D1AAC5A47DDE86B5(__this, ((int32_t)il2cpp_codegen_add(L_8, L_9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_10 = ___0_index;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) >= ((int32_t)L_11)))
		{
			goto IL_0066;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_12 = __this->____items;
		int32_t L_13 = ___0_index;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = V_1;
		int32_t L_17 = __this->____size;
		int32_t L_18 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_12, L_13, (RuntimeArray*)L_14, ((int32_t)il2cpp_codegen_add(L_15, L_16)), ((int32_t)il2cpp_codegen_subtract(L_17, L_18)), NULL);
	}

IL_0066:
	{
		RuntimeObject* L_19 = V_0;
		if ((!(((RuntimeObject*)(List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E*)__this) == ((RuntimeObject*)(RuntimeObject*)L_19))))
		{
			goto IL_009f;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_20 = __this->____items;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_21 = __this->____items;
		int32_t L_22 = ___0_index;
		int32_t L_23 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_20, 0, (RuntimeArray*)L_21, L_22, L_23, NULL);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_24 = __this->____items;
		int32_t L_25 = ___0_index;
		int32_t L_26 = V_1;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_27 = __this->____items;
		int32_t L_28 = ___0_index;
		int32_t L_29 = __this->____size;
		int32_t L_30 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_24, ((int32_t)il2cpp_codegen_add(L_25, L_26)), (RuntimeArray*)L_27, ((int32_t)il2cpp_codegen_multiply(L_28, 2)), ((int32_t)il2cpp_codegen_subtract(L_29, L_30)), NULL);
		goto IL_00ac;
	}

IL_009f:
	{
		RuntimeObject* L_31 = V_0;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_32 = __this->____items;
		int32_t L_33 = ___0_index;
		NullCheck(L_31);
		InterfaceActionInvoker2< __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_31, L_32, L_33);
	}

IL_00ac:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_34, L_35));
		goto IL_00fb;
	}

IL_00bc:
	{
		int32_t L_36 = ___0_index;
		int32_t L_37 = __this->____size;
		if ((((int32_t)L_36) >= ((int32_t)L_37)))
		{
			goto IL_00f4;
		}
	}
	{
		RuntimeObject* L_38 = ___1_collection;
		NullCheck(L_38);
		RuntimeObject* L_39;
		L_39 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_38);
		V_2 = L_39;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ea:
			{
				{
					RuntimeObject* L_40 = V_2;
					if (!L_40)
					{
						goto IL_00f3;
					}
				}
				{
					RuntimeObject* L_41 = V_2;
					NullCheck((RuntimeObject*)L_41);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_41);
				}

IL_00f3:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00e0_1;
			}

IL_00ce_1:
			{
				int32_t L_42 = ___0_index;
				int32_t L_43 = L_42;
				___0_index = ((int32_t)il2cpp_codegen_add(L_43, 1));
				RuntimeObject* L_44 = V_2;
				NullCheck(L_44);
				Il2CppSharedGenericObject* L_45;
				L_45 = InterfaceFuncInvoker0< Il2CppSharedGenericObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_44);
				List_1_Insert_mC5A8238645AD1D203E75313802D4DC78BDE7857E(__this, L_43, L_45, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
			}

IL_00e0_1:
			{
				RuntimeObject* L_46 = V_2;
				NullCheck((RuntimeObject*)L_46);
				bool L_47;
				L_47 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_46);
				if (L_47)
				{
					goto IL_00ce_1;
				}
			}
			{
				goto IL_00fb;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f4:
	{
		RuntimeObject* L_48 = ___1_collection;
		List_1_AddEnumerable_m44B5C2177E9403DB0E3DC2BC6E590CDA7266BE79(__this, L_48, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_00fb:
	{
		int32_t L_49 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_49, 1));
		return;
	}
}
// Method Definition Index: 11391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m0AC731A08F1400089DD478E2D366804833660796_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		Il2CppSharedGenericObject* L_0 = ___0_item;
		int32_t L_1;
		L_1 = List_1_IndexOf_m4B13B6BE7AA6327431F1BE406264329892975DE9(__this, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		int32_t L_3 = V_0;
		List_1_RemoveAt_m36F68EA689DB109415094085A496D5EA21E2F23C(__this, L_3, NULL);
		return (bool)1;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Remove_mCBBBF61C9D398AB54A3B84E64145D65DF44BA716_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m9E5E0DF2443D88570595AEA0FEF7D7122455EB26(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Remove_m0AC731A08F1400089DD478E2D366804833660796(__this, ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41));
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 11393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_RemoveAll_m4EE2013145E54D7A4FC8DC18C59D96E7DB7766FC_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0011;
	}

IL_000d:
	{
		int32_t L_1 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_1, 1));
	}

IL_0011:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____size;
		if ((((int32_t)L_2) >= ((int32_t)L_3)))
		{
			goto IL_002e;
		}
	}
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_4 = ___0_match;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_5 = __this->____items;
		int32_t L_6 = V_0;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		Il2CppSharedGenericObject* L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		bool L_9;
		L_9 = Predicate_1_Invoke_mDF332C0CEB003C0D86A1E3A194ECFA96D424E34C_inline(L_4, L_8, NULL);
		if (!L_9)
		{
			goto IL_000d;
		}
	}

IL_002e:
	{
		int32_t L_10 = V_0;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0039;
		}
	}
	{
		return 0;
	}

IL_0039:
	{
		int32_t L_12 = V_0;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
		goto IL_0089;
	}

IL_003f:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_0043:
	{
		int32_t L_14 = V_1;
		int32_t L_15 = __this->____size;
		if ((((int32_t)L_14) >= ((int32_t)L_15)))
		{
			goto IL_0060;
		}
	}
	{
		Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* L_16 = ___0_match;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_17 = __this->____items;
		int32_t L_18 = V_1;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		Il2CppSharedGenericObject* L_20 = (L_17)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_16);
		bool L_21;
		L_21 = Predicate_1_Invoke_mDF332C0CEB003C0D86A1E3A194ECFA96D424E34C_inline(L_16, L_20, NULL);
		if (L_21)
		{
			goto IL_003f;
		}
	}

IL_0060:
	{
		int32_t L_22 = V_1;
		int32_t L_23 = __this->____size;
		if ((((int32_t)L_22) >= ((int32_t)L_23)))
		{
			goto IL_0089;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_24 = __this->____items;
		int32_t L_25 = V_0;
		int32_t L_26 = L_25;
		V_0 = ((int32_t)il2cpp_codegen_add(L_26, 1));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_27 = __this->____items;
		int32_t L_28 = V_1;
		int32_t L_29 = L_28;
		V_1 = ((int32_t)il2cpp_codegen_add(L_29, 1));
		NullCheck(L_27);
		int32_t L_30 = L_29;
		Il2CppSharedGenericObject* L_31 = (L_27)->GetAt(static_cast<il2cpp_array_size_t>(L_30));
		NullCheck(L_24);
		(L_24)->SetAt(static_cast<il2cpp_array_size_t>(L_26), (Il2CppSharedGenericObject*)L_31);
	}

IL_0089:
	{
		int32_t L_32 = V_1;
		int32_t L_33 = __this->____size;
		if ((((int32_t)L_32) < ((int32_t)L_33)))
		{
			goto IL_0043;
		}
	}
	{
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_34 = __this->____items;
		int32_t L_35 = V_0;
		int32_t L_36 = __this->____size;
		int32_t L_37 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_34, L_35, ((int32_t)il2cpp_codegen_subtract(L_36, L_37)), NULL);
	}

IL_00ad:
	{
		int32_t L_38 = __this->____size;
		int32_t L_39 = V_0;
		int32_t L_40 = V_0;
		__this->____size = L_40;
		int32_t L_41 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_41, 1));
		return ((int32_t)il2cpp_codegen_subtract(L_38, L_39));
	}
}
// Method Definition Index: 11394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m36F68EA689DB109415094085A496D5EA21E2F23C_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	Il2CppSharedGenericObject* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		int32_t L_3 = ___0_index;
		int32_t L_4 = __this->____size;
		if ((((int32_t)L_3) >= ((int32_t)L_4)))
		{
			goto IL_0042;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		int32_t L_9 = __this->____size;
		int32_t L_10 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_5, ((int32_t)il2cpp_codegen_add(L_6, 1)), (RuntimeArray*)L_7, L_8, ((int32_t)il2cpp_codegen_subtract(L_9, L_10)), NULL);
	}

IL_0042:
	{
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_11 = __this->____items;
		int32_t L_12 = __this->____size;
		il2cpp_codegen_initobj((&V_0), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_13 = V_0;
		NullCheck(L_11);
		(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_12), (Il2CppSharedGenericObject*)L_13);
	}

IL_0063:
	{
		int32_t L_14 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_14, 1));
		return;
	}
}
// Method Definition Index: 11395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveRange_m62CC0A9594355982BB7665FB6F070950D3B89C30_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		int32_t L_6 = __this->____size;
		int32_t L_7 = __this->____size;
		int32_t L_8 = ___1_count;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_7, L_8));
		int32_t L_9 = ___0_index;
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_9) >= ((int32_t)L_10)))
		{
			goto IL_0066;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_11 = __this->____items;
		int32_t L_12 = ___0_index;
		int32_t L_13 = ___1_count;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = __this->____size;
		int32_t L_17 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_11, ((int32_t)il2cpp_codegen_add(L_12, L_13)), (RuntimeArray*)L_14, L_15, ((int32_t)il2cpp_codegen_subtract(L_16, L_17)), NULL);
	}

IL_0066:
	{
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_19 = __this->____items;
		int32_t L_20 = __this->____size;
		int32_t L_21 = ___1_count;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_19, L_20, L_21, NULL);
	}

IL_008d:
	{
		return;
	}
}
// Method Definition Index: 11396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m5D2819AF5937BFF72667A9AC1F0D657F4AFF58BC_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline(__this, NULL);
		List_1_Reverse_m97197478E9535E327B4509C065B30BB0DD6F1F99(__this, 0, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42));
		return;
	}
}
// Method Definition Index: 11397
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m97197478E9535E327B4509C065B30BB0DD6F1F99_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0038;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		Array_Reverse_TisIl2CppSharedGenericObject_mFAAB0AB623769BB8245CDF219832F2FE75427541(L_6, L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43));
	}

IL_0038:
	{
		int32_t L_9 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_9, 1));
		return;
	}
}
// Method Definition Index: 11398
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m07AFC087F5B3C54E8E292639DA39D7CBBBF047B2_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline(__this, NULL);
		List_1_Sort_m15AB808AB5390BF8B6D7A057EDA17A54A80A1A6A(__this, 0, L_0, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mA11C7B54C894C91320394703D2B0BB90E5FBF21B_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m9832942FE310479E64D9D0D23F29C65305BE1622_inline(__this, NULL);
		RuntimeObject* L_1 = ___0_comparer;
		List_1_Sort_m15AB808AB5390BF8B6D7A057EDA17A54A80A1A6A(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m15AB808AB5390BF8B6D7A057EDA17A54A80A1A6A_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0039;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		RuntimeObject* L_9 = ___2_comparer;
		Array_Sort_TisIl2CppSharedGenericObject_m8C8A5C187776FEA1D5CC7F053F0DC63B32764D61(L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45));
	}

IL_0039:
	{
		int32_t L_10 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_10, 1));
		return;
	}
}
// Method Definition Index: 11401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m5DC9EDD762287204259B029E98F1409BC3A63A81_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Comparison_1_tC9FBFF747FA0455CD35BD7D097691DE20DDCC2B5* ___0_comparison, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit);
	//<source_info:<no-source>:1>
	{
		Comparison_1_tC9FBFF747FA0455CD35BD7D097691DE20DDCC2B5* L_0 = ___0_comparison;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)34), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_1) <= ((int32_t)1)))
		{
			goto IL_0026;
		}
	}
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_2 = __this->____items;
		int32_t L_3 = __this->____size;
		Comparison_1_tC9FBFF747FA0455CD35BD7D097691DE20DDCC2B5* L_4 = ___0_comparison;
		CHECKED_LOCAL_INIT(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 47)),il2cpp_codegen_runtime_class_init_inline);
		ArraySortHelper_1_Sort_m745CBFE82472049A441AC9522C4B8FA33F5AF970(L_2, 0, L_3, L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46));
	}

IL_0026:
	{
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11402
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* List_1_ToArray_m90E6D9E57256EC0FB6B36FDC40F65B0F59D7B671_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* V_0 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_1 = ((List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_3 = (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)(__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_2);
		V_0 = L_3;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_4 = __this->____items;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_5 = V_0;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_4, 0, (RuntimeArray*)L_5, 0, L_6, NULL);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 11403
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_TrimExcess_m3837DE03BEA738B4FB4A586787ABAB0638DFCD7F_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		double L_2 = (il2cpp_codegen_conv<double,int32_t,int32_t,false,false>(L_1,NULL));
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,double,double,false,false>(((double)il2cpp_codegen_multiply(L_2, (0.90000000000000002))),NULL));
		V_0 = L_3;
		int32_t L_4 = __this->____size;
		int32_t L_5 = V_0;
		if ((((int32_t)L_4) >= ((int32_t)L_5)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = __this->____size;
		List_1_set_Capacity_m4B62593B8671BB60809057D57DC60568557315E6(__this, L_6, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11404
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m44B5C2177E9403DB0E3DC2BC6E590CDA7266BE79_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	Il2CppSharedGenericObject* V_1 = NULL;
	int32_t V_2 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		RuntimeObject* L_1 = ___0_enumerable;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_1);
		V_0 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0063:
			{
				{
					RuntimeObject* L_3 = V_0;
					if (!L_3)
					{
						goto IL_006c;
					}
				}
				{
					RuntimeObject* L_4 = V_0;
					NullCheck((RuntimeObject*)L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_4);
				}

IL_006c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0059_1;
			}

IL_0017_1:
			{
				RuntimeObject* L_5 = V_0;
				NullCheck(L_5);
				Il2CppSharedGenericObject* L_6;
				L_6 = InterfaceFuncInvoker0< Il2CppSharedGenericObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_5);
				V_1 = L_6;
				int32_t L_7 = __this->____size;
				__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_8 = __this->____items;
				NullCheck(L_8);
				int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
				if ((!(((uint32_t)L_7) == ((uint32_t)L_9))))
				{
					goto IL_003c_1;
				}
			}
			{
				int32_t L_10 = __this->____size;
				List_1_EnsureCapacity_m89719BB37EEDA80E6AD7FAC5D1AAC5A47DDE86B5(__this, ((int32_t)il2cpp_codegen_add(L_10, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
			}

IL_003c_1:
			{
				__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_11 = __this->____items;
				int32_t L_12 = __this->____size;
				V_2 = L_12;
				int32_t L_13 = V_2;
				__this->____size = ((int32_t)il2cpp_codegen_add(L_13, 1));
				int32_t L_14 = V_2;
				Il2CppSharedGenericObject* L_15 = V_1;
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_14), (Il2CppSharedGenericObject*)L_15);
			}

IL_0059_1:
			{
				RuntimeObject* L_16 = V_0;
				NullCheck((RuntimeObject*)L_16);
				bool L_17;
				L_17 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_16);
				if (L_17)
				{
					goto IL_0017_1;
				}
			}
			{
				goto IL_006d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006d:
	{
		return;
	}
}
// Method Definition Index: 11405
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__cctor_mF9EC44C2DCE6FA8CA8EFEC794130234F42A1D6DB_gshared (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_0 = (__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)(__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (uint32_t)0);
		((List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray), (void*)L_0);
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
// Method Definition Index: 11344
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m0AFBAEA7EC427E32CC9CA267B1930DC5DF67A374_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = ((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_0);
		return;
	}
}
// Method Definition Index: 11345
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m3069CACB5775E013107F559C825422266A09F9E8_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_capacity, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		int32_t L_0 = ___0_capacity;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)12), 4, NULL);
	}

IL_0012:
	{
		int32_t L_1 = ___0_capacity;
		if (L_1)
		{
			goto IL_0021;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2 = ((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_2);
		return;
	}

IL_0021:
	{
		int32_t L_3 = ___0_capacity;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_4 = (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_3);
		__this->____items = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_4);
		return;
	}
}
// Method Definition Index: 11346
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_mACCB7726C2E9728AE336F99C68B334459F9F30FB_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_000f:
	{
		RuntimeObject* L_1 = ___0_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0050;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_3);
		V_1 = L_4;
		int32_t L_5 = V_1;
		if (L_5)
		{
			goto IL_002f;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_6 = ((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_6);
		return;
	}

IL_002f:
	{
		int32_t L_7 = V_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_8 = (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_7);
		__this->____items = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_8);
		RuntimeObject* L_9 = V_0;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_10 = __this->____items;
		NullCheck(L_9);
		InterfaceActionInvoker2< __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_9, L_10, 0);
		int32_t L_11 = V_1;
		__this->____size = L_11;
		return;
	}

IL_0050:
	{
		__this->____size = 0;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_12 = ((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		RuntimeObject* L_13 = ___0_collection;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5)))(__this, L_13, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
		return;
	}
}
// Method Definition Index: 11347
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_get_Capacity_m5BE6D733C76E1AB093FB6D2783220A5B96EF61DF_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		return L_1;
	}
}
// Method Definition Index: 11348
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mD365C22FA3D32D70A2088AB13116E7BA5FBF0BFB_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* V_0 = NULL;
	{
		int32_t L_0 = ___0_value;
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)15), ((int32_t)21), NULL);
	}

IL_0012:
	{
		int32_t L_2 = ___0_value;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_2) == ((int32_t)L_4)))
		{
			goto IL_0058;
		}
	}
	{
		int32_t L_5 = ___0_value;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_004d;
		}
	}
	{
		int32_t L_6 = ___0_value;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_6);
		V_0 = L_7;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0045;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_9 = __this->____items;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_10 = V_0;
		int32_t L_11 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_9, 0, (RuntimeArray*)L_10, 0, L_11, NULL);
	}

IL_0045:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_12 = V_0;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		return;
	}

IL_004d:
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_13 = ((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_13);
	}

IL_0058:
	{
		return;
	}
}
// Method Definition Index: 11349
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		return L_0;
	}
}
// Method Definition Index: 11350
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_get_IsFixedSize_m9BB85C4C7B3D227CAA4B56F531CCA9112C5FE801_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		return (bool)0;
	}
}
// Method Definition Index: 11351
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_Generic_ICollectionU3CTU3E_get_IsReadOnly_m3F886C476FAC47C7E0B22BBEFA863E6E28CB6967_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		return (bool)0;
	}
}
// Method Definition Index: 11352
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_get_IsReadOnly_m14CBA0E79745928530A7F91F920E3B29BF5977C2_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		return (bool)0;
	}
}
// Method Definition Index: 11353
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_ICollection_get_IsSynchronized_m3F155097881E03657F32A11414B0E214B9356584_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		return (bool)0;
	}
}
// Method Definition Index: 11354
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_ICollection_get_SyncRoot_m81521F85F116B00354BB0F726158451F3F1543F8_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->____syncRoot;
		if (L_0)
		{
			goto IL_001a;
		}
	}
	{
		RuntimeObject** L_1 = (RuntimeObject**)(&__this->____syncRoot);
		RuntimeObject* L_2 = (RuntimeObject*)il2cpp_codegen_object_new(RuntimeObject_il2cpp_TypeInfo_var);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(L_2, NULL);
		RuntimeObject* L_3;
		L_3 = il2cpp_intrinsic_interlocked_compare_exchange(L_1, L_2, NULL);
	}

IL_001a:
	{
		RuntimeObject* L_4 = __this->____syncRoot;
		return L_4;
	}
}
// Method Definition Index: 11355
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_get_Item_m6E4BA37C1FB558E4A62AE4324212E45D09C5C937_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		il2cpp_codegen_memcpy(L_5, (L_2)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_4)), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		il2cpp_codegen_memcpy(il2cppRetVal, L_5, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		return;
	}
}
// Method Definition Index: 11356
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m9A958091885CC5363CCFE9F0BC472EAFCB56C813_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, Il2CppFullySharedGenericAny ___1_value, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		il2cpp_codegen_memcpy(L_4, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___1_value : &___1_value), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_2);
		il2cpp_codegen_memcpy((L_2)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_3)), L_4, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), (void**)(L_2)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_3)), (void*)L_4);
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11357
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_mF063A5AC0A6B869965B78A300A210C77D7D85A9E_fshared (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6));
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	Il2CppFullySharedGenericAny V_0 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	memset(V_0, 0, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInst((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6))))
		{
			goto IL_001f;
		}
	}
	{
		RuntimeObject* L_1 = ___0_value;
		if (L_1)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_0, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		il2cpp_codegen_memcpy(L_2, V_0, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		bool L_3 = !il2cpp_codegen_would_box_to_non_null(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6), L_2);
		bool L_4 = L_3;
		return L_4;
	}

IL_001d:
	{
		return (bool)0;
	}

IL_001f:
	{
		return (bool)1;
	}
}
// Method Definition Index: 11358
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IList_get_Item_m9468A95C180AB56C22EF37B808E722B82FA5DA1D_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		InvokerActionInvoker2< int32_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 7)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 7), __this, L_0, (Il2CppFullySharedGenericAny*)L_1);
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), L_1);
		return L_2;
	}
}
// Method Definition Index: 11359
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_set_Item_m0F05BC227468EA87CF5E5E2D0707EFEDD6DB1D48_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_value;
		((  void (*) (RuntimeObject*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 8)))(L_0, ((int32_t)15), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 8));
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_value;
		void* L_4 = UnBox_Any(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), L_3);
		InvokerActionInvoker2< int32_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 9), __this, L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4)): *(void**)(((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4))));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_5 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_6 = ___1_value;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_7 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_8;
		L_8 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_7, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_6, L_8, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11360
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Add_mD4F3498FBD3BDD3F03CBCFB38041CBAC9C28CAFC_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_9 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	const Il2CppFullySharedGenericAny L_10 = L_9;
	//<source_info:<no-source>:1>
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = V_0;
		int32_t L_8 = V_1;
		il2cpp_codegen_memcpy(L_9, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_7);
		il2cpp_codegen_memcpy((L_7)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_8)), L_9, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), (void**)(L_7)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_8)), (void*)L_9);
		return;
	}

IL_0034:
	{
		il2cpp_codegen_memcpy(L_10, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		InvokerActionInvoker1< Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11), __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_10: *(void**)L_10));
		return;
	}
}
// Method Definition Index: 11361
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_mA6DFDBC2B22D6318212C6989A34784BD8303AF33_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		int32_t L_1 = V_0;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12)))(__this, ((int32_t)il2cpp_codegen_add(L_1, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_2 = V_0;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_2, 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = __this->____items;
		int32_t L_4 = V_0;
		il2cpp_codegen_memcpy(L_5, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_3);
		il2cpp_codegen_memcpy((L_3)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_4)), L_5, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), (void**)(L_3)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_4)), (void*)L_5);
		return;
	}
}
// Method Definition Index: 11362
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_Add_m16AFA75CCE3355CE51D428B0931951485666BF3D_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___0_item;
		((  void (*) (RuntimeObject*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 8)))(L_0, ((int32_t)20), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 8));
	}
	try
	{
		RuntimeObject* L_1 = ___0_item;
		void* L_3 = UnBox_Any(L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), L_2);
		InvokerActionInvoker1< Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13), __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_3)): *(void**)(((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_3))));
		goto IL_0029;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0016;
		}
		throw e;
	}

CATCH_0016:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_4 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_5 = ___0_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_6 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_7;
		L_7 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_6, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_5, L_7, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0029;
	}

IL_0029:
	{
		int32_t L_8;
		L_8 = ((  int32_t (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14)))(__this, NULL);
		return ((int32_t)il2cpp_codegen_subtract(L_8, 1));
	}
}
// Method Definition Index: 11363
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m44DED39FFEBC8F2ACF6162DB065B649E888B9C57_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		RuntimeObject* L_1 = ___0_collection;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15)))(__this, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return;
	}
}
// Method Definition Index: 11364
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8* List_1_AsReadOnly_m7815E4F80542F2F7532AD847F4AA17739E5A7158_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8* L_0 = (ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
		((  void (*) (ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 17)))(L_0, (RuntimeObject*)__this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11365
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m432056F077882449B26AE6759DEB4EECB8BB743E_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, int32_t ___1_count, Il2CppFullySharedGenericAny ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_8 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		int32_t L_7 = ___1_count;
		il2cpp_codegen_memcpy(L_8, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___2_item : &___2_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		RuntimeObject* L_9 = ___3_comparer;
		int32_t L_10;
		L_10 = InvokerFuncInvoker5< int32_t, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, int32_t, int32_t, Il2CppFullySharedGenericAny, RuntimeObject* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18), NULL, L_5, L_6, L_7, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_8: *(void**)L_8), L_9);
		return L_10;
	}
}
// Method Definition Index: 11366
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m1A34A59B839C5FC7331DAA3F10BFCC8230174B15_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = ((  int32_t (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14)))(__this, NULL);
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		int32_t L_2;
		L_2 = InvokerFuncInvoker4< int32_t, int32_t, int32_t, Il2CppFullySharedGenericAny, RuntimeObject* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19), __this, 0, L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_1: *(void**)L_1), (RuntimeObject*)NULL);
		return L_2;
	}
}
// Method Definition Index: 11367
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m816AA1BC17CB3E2169E7E8E2DC777EE13B1CF439_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, RuntimeObject* ___1_comparer, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = ((  int32_t (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14)))(__this, NULL);
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		RuntimeObject* L_2 = ___1_comparer;
		int32_t L_3;
		L_3 = InvokerFuncInvoker4< int32_t, int32_t, int32_t, Il2CppFullySharedGenericAny, RuntimeObject* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19), __this, 0, L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_1: *(void**)L_1), L_2);
		return L_3;
	}
}
// Method Definition Index: 11368
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Clear_mD615D1BCB2C9DD91DAD86A2F9E5CF1DFFCBF7925_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		bool L_1;
		L_1 = il2cpp_codegen_is_reference_or_contains_references(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 20));
		if (!L_1)
		{
			goto IL_0035;
		}
	}
	{
		int32_t L_2 = __this->____size;
		V_0 = L_2;
		__this->____size = 0;
		int32_t L_3 = V_0;
		if ((((int32_t)L_3) <= ((int32_t)0)))
		{
			goto IL_003c;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_4 = __this->____items;
		int32_t L_5 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_4, 0, L_5, NULL);
		return;
	}

IL_0035:
	{
		__this->____size = 0;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11369
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m8DA550B703DFB328B69C4712064C667D7CA33DF1_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		int32_t L_2;
		L_2 = InvokerFuncInvoker1< int32_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21), __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_1: *(void**)L_1));
		return (bool)((((int32_t)((((int32_t)L_2) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_0016:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11370
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_Contains_m20FD9B23AB202EAE6A3EBD3271A64B10BB21C5D9_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = ((  bool (*) (RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22)))(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		void* L_4 = UnBox_Any(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), L_3);
		bool L_5;
		L_5 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23), __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4)): *(void**)(((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4))));
		return L_5;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11371
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_m9632CAFAB9E06DBDE71886CC36E6C910F8350F9C_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = ___0_array;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 24)))(__this, L_0, 0, NULL);
		return;
	}
}
// Method Definition Index: 11372
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_ICollection_CopyTo_mAAA2256D3B50B6A712DFB270B01B470759AB4FE9_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, RuntimeArray* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeArray* L_0 = ___0_array;
		if (!L_0)
		{
			goto IL_0012;
		}
	}
	{
		RuntimeArray* L_1 = ___0_array;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = il2cpp_codegen_array_get_rank(L_1);
		if ((((int32_t)L_2) == ((int32_t)1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(7, NULL);
	}

IL_0012:
	{
	}
	try
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = __this->____items;
		RuntimeArray* L_4 = ___0_array;
		int32_t L_5 = ___1_arrayIndex;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_3, 0, L_4, L_5, L_6, NULL);
		goto IL_0031;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0029;
		}
		throw e;
	}

CATCH_0029:
	{
		ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1* L_7 = ((ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*)IL2CPP_GET_ACTIVE_EXCEPTION(ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*));;
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0031;
	}

IL_0031:
	{
		return;
	}
}
// Method Definition Index: 11373
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_m5180D7A430072A21034A0692CDD1D565ADEE2ABB_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = __this->____items;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_1 = ___0_array;
		int32_t L_2 = ___1_arrayIndex;
		int32_t L_3 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_0, 0, (RuntimeArray*)L_1, L_2, L_3, NULL);
		return;
	}
}
// Method Definition Index: 11374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_m0116C3A6AF6FB7DCF2E909AD859A0ADC86A60C66_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_min, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t G_B4_0 = 0;
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int32_t L_2 = ___0_min;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003d;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = __this->____items;
		NullCheck(L_3);
		if (!(((RuntimeArray*)L_3)->max_length))
		{
			goto IL_0020;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_4 = __this->____items;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		G_B4_0 = ((int32_t)il2cpp_codegen_multiply(L_5, 2));
		goto IL_0021;
	}

IL_0020:
	{
		G_B4_0 = 4;
	}

IL_0021:
	{
		V_0 = G_B4_0;
		int32_t L_6 = V_0;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)2146435071)))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = ((int32_t)2146435071);
	}

IL_0030:
	{
		int32_t L_7 = V_0;
		int32_t L_8 = ___0_min;
		if ((((int32_t)L_7) >= ((int32_t)L_8)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_9 = ___0_min;
		V_0 = L_9;
	}

IL_0036:
	{
		int32_t L_10 = V_0;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25)))(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_003d:
	{
		return;
	}
}
// Method Definition Index: 11375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_m4D844228EBB80F1DFE1D49094731494B09C2B4E8_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_0 = ___0_match;
		int32_t L_1;
		L_1 = ((  int32_t (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 26)))(__this, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 26));
		return (bool)((((int32_t)((((int32_t)L_1) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 11376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Find_m25CD37B5BB59793EBDF3A50F8B982947F7D84B11_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* ___0_match, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	const Il2CppFullySharedGenericAny L_10 = L_5;
	const Il2CppFullySharedGenericAny L_14 = L_5;
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	Il2CppFullySharedGenericAny V_1 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	memset(V_1, 0, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0032;
	}

IL_000d:
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_1 = ___0_match;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2 = __this->____items;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		il2cpp_codegen_memcpy(L_5, (L_2)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_4)), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_1);
		bool L_6;
		L_6 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27), L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_5: *(void**)L_5));
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = __this->____items;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		il2cpp_codegen_memcpy(L_10, (L_7)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_9)), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		il2cpp_codegen_memcpy(il2cppRetVal, L_10, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		return;
	}

IL_002e:
	{
		int32_t L_11 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0032:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____size;
		if ((((int32_t)L_12) < ((int32_t)L_13)))
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_1, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		il2cpp_codegen_memcpy(L_14, V_1, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		il2cpp_codegen_memcpy(il2cppRetVal, L_14, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		return;
	}
}
// Method Definition Index: 11377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mE810B96CFE4683761086D777FD5947FA66EA9255_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_1 = ___0_match;
		int32_t L_2;
		L_2 = ((  int32_t (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, int32_t, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 28)))(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 28));
		return L_2;
	}
}
// Method Definition Index: 11378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m63AAB8CE007CDF25CF1952FBF0F1FF7234695BD0_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* ___2_match, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_14 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_startIndex;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_000e:
	{
		int32_t L_2 = ___1_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_3) <= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_4, L_5)))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_0022:
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_6 = ___2_match;
		if (L_6)
		{
			goto IL_002b;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_002b:
	{
		int32_t L_7 = ___0_startIndex;
		int32_t L_8 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, L_8));
		int32_t L_9 = ___0_startIndex;
		V_1 = L_9;
		goto IL_004d;
	}

IL_0033:
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_10 = ___2_match;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_11 = __this->____items;
		int32_t L_12 = V_1;
		NullCheck(L_11);
		int32_t L_13 = L_12;
		il2cpp_codegen_memcpy(L_14, (L_11)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_13)), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_10);
		bool L_15;
		L_15 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27), L_10, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_14: *(void**)L_14));
		if (!L_15)
		{
			goto IL_0049;
		}
	}
	{
		int32_t L_16 = V_1;
		return L_16;
	}

IL_0049:
	{
		int32_t L_17 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_004d:
	{
		int32_t L_18 = V_1;
		int32_t L_19 = V_0;
		if ((((int32_t)L_18) < ((int32_t)L_19)))
		{
			goto IL_0033;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mFD4CF7EBF3B162CECB31EC07527A12BE8835189B_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		int32_t L_1 = __this->____size;
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_2 = ___0_match;
		int32_t L_3;
		L_3 = ((  int32_t (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, int32_t, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 29)))(__this, ((int32_t)il2cpp_codegen_subtract(L_0, 1)), L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 29));
		return L_3;
	}
}
// Method Definition Index: 11380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mF6E379672297CF3C1B401461A2AB681686AC50A0_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* ___2_match, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_15 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_0 = ___2_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		int32_t L_1 = __this->____size;
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = ___0_startIndex;
		if ((((int32_t)L_2) == ((int32_t)(-1))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
		goto IL_002a;
	}

IL_001c:
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		if ((!(((uint32_t)L_3) >= ((uint32_t)L_4))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_002a:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) < ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_6 = ___0_startIndex;
		int32_t L_7 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_6, L_7)), 1))) >= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}

IL_0036:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_003b:
	{
		int32_t L_8 = ___0_startIndex;
		int32_t L_9 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		int32_t L_10 = ___0_startIndex;
		V_1 = L_10;
		goto IL_005d;
	}

IL_0043:
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_11 = ___2_match;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_12 = __this->____items;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		il2cpp_codegen_memcpy(L_15, (L_12)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_14)), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_11);
		bool L_16;
		L_16 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27), L_11, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_15: *(void**)L_15));
		if (!L_16)
		{
			goto IL_0059;
		}
	}
	{
		int32_t L_17 = V_1;
		return L_17;
	}

IL_0059:
	{
		int32_t L_18 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_18, 1));
	}

IL_005d:
	{
		int32_t L_19 = V_1;
		int32_t L_20 = V_0;
		if ((((int32_t)L_19) > ((int32_t)L_20)))
		{
			goto IL_0043;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_ForEach_mA959944628050F666BD4CD147F51232EAC2E1B64_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Action_1_t923A20D1D4F6B55B2ED5AE21B90F1A0CE0450D99* ___0_action, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_8 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Action_1_t923A20D1D4F6B55B2ED5AE21B90F1A0CE0450D99* L_0 = ___0_action;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)33), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____version;
		V_0 = L_1;
		V_1 = 0;
		goto IL_0034;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____version;
		if ((!(((uint32_t)L_2) == ((uint32_t)L_3))))
		{
			goto IL_003d;
		}
	}
	{
		Action_1_t923A20D1D4F6B55B2ED5AE21B90F1A0CE0450D99* L_4 = ___0_action;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_5 = __this->____items;
		int32_t L_6 = V_1;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		il2cpp_codegen_memcpy(L_8, (L_5)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_7)), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_4);
		InvokerActionInvoker1< Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 30)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 30), L_4, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_8: *(void**)L_8));
		int32_t L_9 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0034:
	{
		int32_t L_10 = V_1;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0015;
		}
	}

IL_003d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____version;
		if ((((int32_t)L_12) == ((int32_t)L_13)))
		{
			goto IL_004b;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_004b:
	{
		return;
	}
}
// Method Definition Index: 11382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_GetEnumerator_m8B2A92ACD4FBA5FBDC3F6F4F5C23A0DDF491DA61_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* il2cppRetVal, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31));
	const Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF L_0 = alloca(SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9);
	//<source_info:<no-source>:1>
	{
		memset(L_0, 0, SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9);
		Enumerator__ctor_m5C66A91DC68887BE203985B56EF2F74222E9AF50((Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)L_0, __this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 32));
		il2cpp_codegen_memcpy(il2cppRetVal, L_0, SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9);
		return;
	}
}
// Method Definition Index: 11383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_Generic_IEnumerableU3CTU3E_GetEnumerator_mF8AB18BC6656CC469BFB6E837C001C2DE0833859_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31));
	const Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF L_0 = alloca(SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9);
	//<source_info:<no-source>:1>
	{
		memset(L_0, 0, SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9);
		Enumerator__ctor_m5C66A91DC68887BE203985B56EF2F74222E9AF50((Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)L_0, __this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 32));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), L_0);
		return (RuntimeObject*)L_1;
	}
}
// Method Definition Index: 11384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IEnumerable_GetEnumerator_m83A6D1115899BF6BA842C33FE41FAD8D6EB43ED9_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31));
	const Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF L_0 = alloca(SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9);
	//<source_info:<no-source>:1>
	{
		memset(L_0, 0, SizeOf_Enumerator_t4982EA6BF5A270D5544CF8876DE60B125EDF8FB9);
		Enumerator__ctor_m5C66A91DC68887BE203985B56EF2F74222E9AF50((Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF*)L_0, __this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 32));
		RuntimeObject* L_1 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), L_0);
		return (RuntimeObject*)L_1;
	}
}
// Method Definition Index: 11385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* List_1_GetRange_mBB92D8804DB148ED81D02A0CCA79968FFD71CAD4_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_6 = (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33)))(L_6, L_5, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33));
		V_0 = L_6;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_9 = V_0;
		NullCheck(L_9);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_10 = L_9->____items;
		int32_t L_11 = ___1_count;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_7, L_8, (RuntimeArray*)L_10, 0, L_11, NULL);
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_12 = V_0;
		int32_t L_13 = ___1_count;
		NullCheck(L_12);
		L_12->____size = L_13;
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_14 = V_0;
		return L_14;
	}
}
// Method Definition Index: 11386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m1FFA888B47CE3256B0AC301302FAD79AAAFC0818_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = __this->____items;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		int32_t L_2 = __this->____size;
		int32_t L_3;
		L_3 = InvokerFuncInvoker4< int32_t, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, Il2CppFullySharedGenericAny, int32_t, int32_t >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34), NULL, L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_1: *(void**)L_1), 0, L_2);
		return L_3;
	}
}
// Method Definition Index: 11387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_IndexOf_mF0601617A54C360278652B7AEE34E69F21B5ADDA_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = ((  bool (*) (RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22)))(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		void* L_4 = UnBox_Any(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), L_3);
		int32_t L_5;
		L_5 = InvokerFuncInvoker1< int32_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21), __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4)): *(void**)(((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4))));
		return L_5;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mB8862677E04375531A30F3EC07AC2372FCE61F55_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, Il2CppFullySharedGenericAny ___1_item, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_16 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)13), ((int32_t)27), NULL);
	}

IL_0012:
	{
		int32_t L_2 = __this->____size;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0030;
		}
	}
	{
		int32_t L_5 = __this->____size;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12)))(__this, ((int32_t)il2cpp_codegen_add(L_5, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
	}

IL_0030:
	{
		int32_t L_6 = ___0_index;
		int32_t L_7 = __this->____size;
		if ((((int32_t)L_6) >= ((int32_t)L_7)))
		{
			goto IL_0056;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_8 = __this->____items;
		int32_t L_9 = ___0_index;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_10 = __this->____items;
		int32_t L_11 = ___0_index;
		int32_t L_12 = __this->____size;
		int32_t L_13 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_8, L_9, (RuntimeArray*)L_10, ((int32_t)il2cpp_codegen_add(L_11, 1)), ((int32_t)il2cpp_codegen_subtract(L_12, L_13)), NULL);
	}

IL_0056:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		il2cpp_codegen_memcpy(L_16, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___1_item : &___1_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_14);
		il2cpp_codegen_memcpy((L_14)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_15)), L_16, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), (void**)(L_14)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_15)), (void*)L_16);
		int32_t L_17 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_17, 1));
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		return;
	}
}
// Method Definition Index: 11389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Insert_m3A5BCE3337DF64C89354C0CDF5DB4EE39A30BCA7_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, RuntimeObject* ___1_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_item;
		((  void (*) (RuntimeObject*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 8)))(L_0, ((int32_t)20), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 8));
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_item;
		void* L_4 = UnBox_Any(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), L_3);
		InvokerActionInvoker2< int32_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35), __this, L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4)): *(void**)(((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4))));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_5 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_6 = ___1_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_7 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_8;
		L_8 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_7, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_6, L_8, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m729639E2C8D1AA7579AC78D259085921E7DC72F2_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_45 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	RuntimeObject* V_2 = NULL;
	{
		RuntimeObject* L_0 = ___1_collection;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___0_index;
		int32_t L_2 = __this->____size;
		if ((!(((uint32_t)L_1) > ((uint32_t)L_2))))
		{
			goto IL_0017;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ___1_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_4 = V_0;
		if (!L_4)
		{
			goto IL_00bc;
		}
	}
	{
		RuntimeObject* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_5);
		V_1 = L_6;
		int32_t L_7 = V_1;
		if ((((int32_t)L_7) <= ((int32_t)0)))
		{
			goto IL_00fb;
		}
	}
	{
		int32_t L_8 = __this->____size;
		int32_t L_9 = V_1;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12)))(__this, ((int32_t)il2cpp_codegen_add(L_8, L_9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_10 = ___0_index;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) >= ((int32_t)L_11)))
		{
			goto IL_0066;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_12 = __this->____items;
		int32_t L_13 = ___0_index;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = V_1;
		int32_t L_17 = __this->____size;
		int32_t L_18 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_12, L_13, (RuntimeArray*)L_14, ((int32_t)il2cpp_codegen_add(L_15, L_16)), ((int32_t)il2cpp_codegen_subtract(L_17, L_18)), NULL);
	}

IL_0066:
	{
		RuntimeObject* L_19 = V_0;
		if ((!(((RuntimeObject*)(List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*)__this) == ((RuntimeObject*)(RuntimeObject*)L_19))))
		{
			goto IL_009f;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_20 = __this->____items;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_21 = __this->____items;
		int32_t L_22 = ___0_index;
		int32_t L_23 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_20, 0, (RuntimeArray*)L_21, L_22, L_23, NULL);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_24 = __this->____items;
		int32_t L_25 = ___0_index;
		int32_t L_26 = V_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_27 = __this->____items;
		int32_t L_28 = ___0_index;
		int32_t L_29 = __this->____size;
		int32_t L_30 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_24, ((int32_t)il2cpp_codegen_add(L_25, L_26)), (RuntimeArray*)L_27, ((int32_t)il2cpp_codegen_multiply(L_28, 2)), ((int32_t)il2cpp_codegen_subtract(L_29, L_30)), NULL);
		goto IL_00ac;
	}

IL_009f:
	{
		RuntimeObject* L_31 = V_0;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_32 = __this->____items;
		int32_t L_33 = ___0_index;
		NullCheck(L_31);
		InterfaceActionInvoker2< __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_31, L_32, L_33);
	}

IL_00ac:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_34, L_35));
		goto IL_00fb;
	}

IL_00bc:
	{
		int32_t L_36 = ___0_index;
		int32_t L_37 = __this->____size;
		if ((((int32_t)L_36) >= ((int32_t)L_37)))
		{
			goto IL_00f4;
		}
	}
	{
		RuntimeObject* L_38 = ___1_collection;
		NullCheck(L_38);
		RuntimeObject* L_39;
		L_39 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_38);
		V_2 = L_39;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ea:
			{
				{
					RuntimeObject* L_40 = V_2;
					if (!L_40)
					{
						goto IL_00f3;
					}
				}
				{
					RuntimeObject* L_41 = V_2;
					NullCheck((RuntimeObject*)L_41);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_41);
				}

IL_00f3:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00e0_1;
			}

IL_00ce_1:
			{
				int32_t L_42 = ___0_index;
				int32_t L_43 = L_42;
				___0_index = ((int32_t)il2cpp_codegen_add(L_43, 1));
				RuntimeObject* L_44 = V_2;
				NullCheck(L_44);
				InterfaceActionInvoker1Invoker< Il2CppFullySharedGenericAny* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_44, (Il2CppFullySharedGenericAny*)L_45);
				InvokerActionInvoker2< int32_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35), __this, L_43, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_45: *(void**)L_45));
			}

IL_00e0_1:
			{
				RuntimeObject* L_46 = V_2;
				NullCheck((RuntimeObject*)L_46);
				bool L_47;
				L_47 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_46);
				if (L_47)
				{
					goto IL_00ce_1;
				}
			}
			{
				goto IL_00fb;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f4:
	{
		RuntimeObject* L_48 = ___1_collection;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5)))(__this, L_48, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_00fb:
	{
		int32_t L_49 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_49, 1));
		return;
	}
}
// Method Definition Index: 11391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m9BCE8CEF94E6F2BF8624D65214FF4F3CA686D60C_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Il2CppFullySharedGenericAny ___0_item, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_0 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		il2cpp_codegen_memcpy(L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? ___0_item : &___0_item), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		int32_t L_1;
		L_1 = InvokerFuncInvoker1< int32_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21), __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_0: *(void**)L_0));
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		int32_t L_3 = V_0;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 40)))(__this, L_3, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 40));
		return (bool)1;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Remove_m69B0EB46209420A0DF794AE7598F8C3E02515509_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = ((  bool (*) (RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22)))(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		void* L_4 = UnBox_Any(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), L_3);
		bool L_5;
		L_5 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41), __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4)): *(void**)(((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4))));
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 11393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_RemoveAll_m64ACE8FD442E00A79160BB070C0133B43A8C4075_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* ___0_match, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_8 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	const Il2CppFullySharedGenericAny L_20 = L_8;
	const Il2CppFullySharedGenericAny L_31 = L_8;
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0011;
	}

IL_000d:
	{
		int32_t L_1 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_1, 1));
	}

IL_0011:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____size;
		if ((((int32_t)L_2) >= ((int32_t)L_3)))
		{
			goto IL_002e;
		}
	}
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_4 = ___0_match;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_5 = __this->____items;
		int32_t L_6 = V_0;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		il2cpp_codegen_memcpy(L_8, (L_5)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_7)), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_4);
		bool L_9;
		L_9 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27), L_4, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_8: *(void**)L_8));
		if (!L_9)
		{
			goto IL_000d;
		}
	}

IL_002e:
	{
		int32_t L_10 = V_0;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0039;
		}
	}
	{
		return 0;
	}

IL_0039:
	{
		int32_t L_12 = V_0;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
		goto IL_0089;
	}

IL_003f:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_0043:
	{
		int32_t L_14 = V_1;
		int32_t L_15 = __this->____size;
		if ((((int32_t)L_14) >= ((int32_t)L_15)))
		{
			goto IL_0060;
		}
	}
	{
		Predicate_1_t81499D2838AC2641B3FA14CD4DBF7E1594A9E107* L_16 = ___0_match;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_17 = __this->____items;
		int32_t L_18 = V_1;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		il2cpp_codegen_memcpy(L_20, (L_17)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_19)), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_16);
		bool L_21;
		L_21 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 27), L_16, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)) ? L_20: *(void**)L_20));
		if (L_21)
		{
			goto IL_003f;
		}
	}

IL_0060:
	{
		int32_t L_22 = V_1;
		int32_t L_23 = __this->____size;
		if ((((int32_t)L_22) >= ((int32_t)L_23)))
		{
			goto IL_0089;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_24 = __this->____items;
		int32_t L_25 = V_0;
		int32_t L_26 = L_25;
		V_0 = ((int32_t)il2cpp_codegen_add(L_26, 1));
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_27 = __this->____items;
		int32_t L_28 = V_1;
		int32_t L_29 = L_28;
		V_1 = ((int32_t)il2cpp_codegen_add(L_29, 1));
		NullCheck(L_27);
		int32_t L_30 = L_29;
		il2cpp_codegen_memcpy(L_31, (L_27)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_30)), SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_24);
		il2cpp_codegen_memcpy((L_24)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_26)), L_31, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), (void**)(L_24)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_26)), (void*)L_31);
	}

IL_0089:
	{
		int32_t L_32 = V_1;
		int32_t L_33 = __this->____size;
		if ((((int32_t)L_32) < ((int32_t)L_33)))
		{
			goto IL_0043;
		}
	}
	{
		bool L_34;
		L_34 = il2cpp_codegen_is_reference_or_contains_references(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 20));
		if (!L_34)
		{
			goto IL_00ad;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_35 = __this->____items;
		int32_t L_36 = V_0;
		int32_t L_37 = __this->____size;
		int32_t L_38 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_35, L_36, ((int32_t)il2cpp_codegen_subtract(L_37, L_38)), NULL);
	}

IL_00ad:
	{
		int32_t L_39 = __this->____size;
		int32_t L_40 = V_0;
		int32_t L_41 = V_0;
		__this->____size = L_41;
		int32_t L_42 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_42, 1));
		return ((int32_t)il2cpp_codegen_subtract(L_39, L_40));
	}
}
// Method Definition Index: 11394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_mF6C23E9072D8CBE2872705E7E4D08BDB78770F5C_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_14 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	//<source_info:<no-source>:1>
	Il2CppFullySharedGenericAny V_0 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	memset(V_0, 0, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		int32_t L_3 = ___0_index;
		int32_t L_4 = __this->____size;
		if ((((int32_t)L_3) >= ((int32_t)L_4)))
		{
			goto IL_0042;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		int32_t L_9 = __this->____size;
		int32_t L_10 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_5, ((int32_t)il2cpp_codegen_add(L_6, 1)), (RuntimeArray*)L_7, L_8, ((int32_t)il2cpp_codegen_subtract(L_9, L_10)), NULL);
	}

IL_0042:
	{
		bool L_11;
		L_11 = il2cpp_codegen_is_reference_or_contains_references(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 20));
		if (!L_11)
		{
			goto IL_0063;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_12 = __this->____items;
		int32_t L_13 = __this->____size;
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_0, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		il2cpp_codegen_memcpy(L_14, V_0, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		NullCheck(L_12);
		il2cpp_codegen_memcpy((L_12)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_13)), L_14, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
		Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), (void**)(L_12)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_13)), (void*)L_14);
	}

IL_0063:
	{
		int32_t L_15 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_15, 1));
		return;
	}
}
// Method Definition Index: 11395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveRange_mCD6121D4FDE143767E6F0EC89183A9A349884DFF_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		int32_t L_6 = __this->____size;
		int32_t L_7 = __this->____size;
		int32_t L_8 = ___1_count;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_7, L_8));
		int32_t L_9 = ___0_index;
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_9) >= ((int32_t)L_10)))
		{
			goto IL_0066;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_11 = __this->____items;
		int32_t L_12 = ___0_index;
		int32_t L_13 = ___1_count;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = __this->____size;
		int32_t L_17 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_11, ((int32_t)il2cpp_codegen_add(L_12, L_13)), (RuntimeArray*)L_14, L_15, ((int32_t)il2cpp_codegen_subtract(L_16, L_17)), NULL);
	}

IL_0066:
	{
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		bool L_19;
		L_19 = il2cpp_codegen_is_reference_or_contains_references(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 20));
		if (!L_19)
		{
			goto IL_008d;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_20 = __this->____items;
		int32_t L_21 = __this->____size;
		int32_t L_22 = ___1_count;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_20, L_21, L_22, NULL);
	}

IL_008d:
	{
		return;
	}
}
// Method Definition Index: 11396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m31924FC6445F9A1633DA40A8930F89E271B477F2_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = ((  int32_t (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14)))(__this, NULL);
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42)))(__this, 0, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42));
		return;
	}
}
// Method Definition Index: 11397
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m4F224F8A44C29751CCF7ED914FC00BFE7F27CD01_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0038;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		((  void (*) (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, int32_t, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43)))(L_6, L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43));
	}

IL_0038:
	{
		int32_t L_9 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_9, 1));
		return;
	}
}
// Method Definition Index: 11398
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m3B418BED64218204CAA69CD89BAA05CC12EB5EE6_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = ((  int32_t (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14)))(__this, NULL);
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44)))(__this, 0, L_0, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mCBEFEE4479438557E591F7AEF3BFDE2297DA269D_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, RuntimeObject* ___0_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = ((  int32_t (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14)))(__this, NULL);
		RuntimeObject* L_1 = ___0_comparer;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44)))(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m851B4290E563560C0037434A52D469666ABDE79D_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0039;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		RuntimeObject* L_9 = ___2_comparer;
		((  void (*) (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, int32_t, int32_t, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45)))(L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45));
	}

IL_0039:
	{
		int32_t L_10 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_10, 1));
		return;
	}
}
// Method Definition Index: 11401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mF939EF9300267AF108323ABAECB490A27894309B_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, Comparison_1_tE94B5C4AE59DE10B7A54AD7C3291462A05D5CD49* ___0_comparison, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit);
	//<source_info:<no-source>:1>
	{
		Comparison_1_tE94B5C4AE59DE10B7A54AD7C3291462A05D5CD49* L_0 = ___0_comparison;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)34), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_1) <= ((int32_t)1)))
		{
			goto IL_0026;
		}
	}
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2 = __this->____items;
		int32_t L_3 = __this->____size;
		Comparison_1_tE94B5C4AE59DE10B7A54AD7C3291462A05D5CD49* L_4 = ___0_comparison;
		CHECKED_LOCAL_INIT(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 47)),il2cpp_codegen_runtime_class_init_inline);
		((  void (*) (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, int32_t, int32_t, Comparison_1_tE94B5C4AE59DE10B7A54AD7C3291462A05D5CD49*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46)))(L_2, 0, L_3, L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46));
	}

IL_0026:
	{
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11402
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* List_1_ToArray_m5160526E5983BD8ED24DFFE47C33B141C1B3187A_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* V_0 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_1 = ((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_2);
		V_0 = L_3;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_4 = __this->____items;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_5 = V_0;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_4, 0, (RuntimeArray*)L_5, 0, L_6, NULL);
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 11403
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_TrimExcess_m915005BC7D68487C2006E5A31D87D1CAED751407_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		double L_2 = (il2cpp_codegen_conv<double,int32_t,int32_t,false,false>(L_1,NULL));
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,double,double,false,false>(((double)il2cpp_codegen_multiply(L_2, (0.90000000000000002))),NULL));
		V_0 = L_3;
		int32_t L_4 = __this->____size;
		int32_t L_5 = V_0;
		if ((((int32_t)L_4) >= ((int32_t)L_5)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = __this->____size;
		((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25)))(__this, L_6, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11404
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mFD4E2919839A27C7CEACADF62222C63701302D02_fshared (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	const uint32_t SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6));
	const Il2CppFullySharedGenericAny L_6 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	const Il2CppFullySharedGenericAny L_15 = L_6;
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	Il2CppFullySharedGenericAny V_1 = alloca(SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	memset(V_1, 0, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
	int32_t V_2 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		RuntimeObject* L_1 = ___0_enumerable;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_1);
		V_0 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0063:
			{
				{
					RuntimeObject* L_3 = V_0;
					if (!L_3)
					{
						goto IL_006c;
					}
				}
				{
					RuntimeObject* L_4 = V_0;
					NullCheck((RuntimeObject*)L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_4);
				}

IL_006c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0059_1;
			}

IL_0017_1:
			{
				RuntimeObject* L_5 = V_0;
				NullCheck(L_5);
				InterfaceActionInvoker1Invoker< Il2CppFullySharedGenericAny* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_5, (Il2CppFullySharedGenericAny*)L_6);
				il2cpp_codegen_memcpy(V_1, L_6, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
				int32_t L_7 = __this->____size;
				__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_8 = __this->____items;
				NullCheck(L_8);
				int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
				if ((!(((uint32_t)L_7) == ((uint32_t)L_9))))
				{
					goto IL_003c_1;
				}
			}
			{
				int32_t L_10 = __this->____size;
				((  void (*) (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A*, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12)))(__this, ((int32_t)il2cpp_codegen_add(L_10, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
			}

IL_003c_1:
			{
				__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_11 = __this->____items;
				int32_t L_12 = __this->____size;
				V_2 = L_12;
				int32_t L_13 = V_2;
				__this->____size = ((int32_t)il2cpp_codegen_add(L_13, 1));
				int32_t L_14 = V_2;
				il2cpp_codegen_memcpy(L_15, V_1, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
				NullCheck(L_11);
				il2cpp_codegen_memcpy((L_11)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_14)), L_15, SizeOf_T_t664E2061A913AF1FEE499655BC64F0FDE10D2A5E);
				Il2CppCodeGenWriteBarrierForClass(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), (void**)(L_11)->GetAddressAt(static_cast<il2cpp_array_size_t>(L_14)), (void*)L_15);
			}

IL_0059_1:
			{
				RuntimeObject* L_16 = V_0;
				NullCheck((RuntimeObject*)L_16);
				bool L_17;
				L_17 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_16);
				if (L_17)
				{
					goto IL_0017_1;
				}
			}
			{
				goto IL_006d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006d:
	{
		return;
	}
}
// Method Definition Index: 11405
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__cctor_mFBD61154F711CE4EF252AA0B8B504EFC3E7E5B37_fshared (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = (__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)(__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (uint32_t)0);
		((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray), (void*)L_0);
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
// Method Definition Index: 11344
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m21C393E89D2891A0B17DA7721D14F694C8E87F0E_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_0 = ((List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_0);
		return;
	}
}
// Method Definition Index: 11345
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_mBEC4BB23DEF62EFA5D66833D82CEB6FABDD96E04_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_capacity, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		int32_t L_0 = ___0_capacity;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)12), 4, NULL);
	}

IL_0012:
	{
		int32_t L_1 = ___0_capacity;
		if (L_1)
		{
			goto IL_0021;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_2 = ((List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_2);
		return;
	}

IL_0021:
	{
		int32_t L_3 = ___0_capacity;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_4 = (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)(__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_3);
		__this->____items = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_4);
		return;
	}
}
// Method Definition Index: 11346
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m4176A00C23FE40B9269F7306A7F8170510EA966F_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_000f:
	{
		RuntimeObject* L_1 = ___0_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0050;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_3);
		V_1 = L_4;
		int32_t L_5 = V_1;
		if (L_5)
		{
			goto IL_002f;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_6 = ((List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_6);
		return;
	}

IL_002f:
	{
		int32_t L_7 = V_1;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_8 = (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)(__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_7);
		__this->____items = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_8);
		RuntimeObject* L_9 = V_0;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_10 = __this->____items;
		NullCheck(L_9);
		InterfaceActionInvoker2< __Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_9, L_10, 0);
		int32_t L_11 = V_1;
		__this->____size = L_11;
		return;
	}

IL_0050:
	{
		__this->____size = 0;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_12 = ((List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		RuntimeObject* L_13 = ___0_collection;
		List_1_AddEnumerable_m70DD3FB3C4E302AA0AACC45C39BCEC4C2A9DB230(__this, L_13, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
		return;
	}
}
// Method Definition Index: 11347
// Method Definition Index: 11348
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m7AA13FF7BE1A11F437B16EAE3E1FA50DBFC902AE_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* V_0 = NULL;
	{
		int32_t L_0 = ___0_value;
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)15), ((int32_t)21), NULL);
	}

IL_0012:
	{
		int32_t L_2 = ___0_value;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_2) == ((int32_t)L_4)))
		{
			goto IL_0058;
		}
	}
	{
		int32_t L_5 = ___0_value;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_004d;
		}
	}
	{
		int32_t L_6 = ___0_value;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_7 = (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)(__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_6);
		V_0 = L_7;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0045;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_9 = __this->____items;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_10 = V_0;
		int32_t L_11 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_9, 0, (RuntimeArray*)L_10, 0, L_11, NULL);
	}

IL_0045:
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_12 = V_0;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		return;
	}

IL_004d:
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_13 = ((List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_13);
	}

IL_0058:
	{
		return;
	}
}
// Method Definition Index: 11349
// Method Definition Index: 11350
// Method Definition Index: 11351
// Method Definition Index: 11352
// Method Definition Index: 11353
// Method Definition Index: 11354
// Method Definition Index: 11355
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_get_Item_mEAF67AD94AB1E060C2D03D7A6820C47270E06B75_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		int32_t L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		return L_5;
	}
}
// Method Definition Index: 11356
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_mCDBFFAC056B77CBE377B26CAED3E893C131F4EFB_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_value;
		NullCheck(L_2);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(L_3), (int32_t)L_4);
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11357
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_mD81F4794E5BA5AD31C7A25CA1B456BA04E37EC1D_gshared (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6))))
		{
			goto IL_001f;
		}
	}
	{
		RuntimeObject* L_1 = ___0_value;
		if (L_1)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(int32_t));
		int32_t L_2 = V_0;
		return false;
	}

IL_001d:
	{
		return (bool)0;
	}

IL_001f:
	{
		return (bool)1;
	}
}
// Method Definition Index: 11358
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IList_get_Item_m44527B1273F26E0B2A6025EAB639B8177DCBAD63_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1;
		L_1 = List_1_get_Item_mEAF67AD94AB1E060C2D03D7A6820C47270E06B75(__this, L_0, NULL);
		int32_t L_2 = L_1;
		RuntimeObject* L_3 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), &L_2);
		return L_3;
	}
}
// Method Definition Index: 11359
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_set_Item_mC92CD521E4329D204BCF0192DDFFBA3FE02D79E7_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_value;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m835718C92D2C5DA2051794F6922D21453EAC493E(L_0, ((int32_t)15), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_value;
		List_1_set_Item_mCDBFFAC056B77CBE377B26CAED3E893C131F4EFB(__this, L_1, ((*(int32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), NULL);
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_value;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11360
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Add_m24BA4DEDED426A5B0C6A4AB3E523C876B0B918A6_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_7 = V_0;
		int32_t L_8 = V_1;
		int32_t L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (int32_t)L_9);
		return;
	}

IL_0034:
	{
		int32_t L_10 = ___0_item;
		List_1_AddWithResize_m3B53F17525936469654379AC0F1F0B19EC3DCCE5(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 11361
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m3B53F17525936469654379AC0F1F0B19EC3DCCE5_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		int32_t L_1 = V_0;
		List_1_EnsureCapacity_mB5CE702B99DFF8E1BB13570D69ADA436D814DBBF(__this, ((int32_t)il2cpp_codegen_add(L_1, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_2 = V_0;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_2, 1));
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_3 = __this->____items;
		int32_t L_4 = V_0;
		int32_t L_5 = ___0_item;
		NullCheck(L_3);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(L_4), (int32_t)L_5);
		return;
	}
}
// Method Definition Index: 11362
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_Add_mBCF4F1C3031249C6D89E84F02EA59D632DDA997C_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___0_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m835718C92D2C5DA2051794F6922D21453EAC493E(L_0, ((int32_t)20), NULL);
	}
	try
	{
		RuntimeObject* L_1 = ___0_item;
		List_1_Add_m24BA4DEDED426A5B0C6A4AB3E523C876B0B918A6_inline(__this, ((*(int32_t*)UnBox(L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		goto IL_0029;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0016;
		}
		throw e;
	}

CATCH_0016:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_2 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_3 = ___0_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_4 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_5;
		L_5 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_4, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_3, L_5, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0029;
	}

IL_0029:
	{
		int32_t L_6;
		L_6 = List_1_get_Count_m8719A0E4E1F227424476DE81DF6DF6B58CA1CD71_inline(__this, NULL);
		return ((int32_t)il2cpp_codegen_subtract(L_6, 1));
	}
}
// Method Definition Index: 11363
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m9E2CD0E840D68FFF2E93915D330EB9681A0E0721_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		RuntimeObject* L_1 = ___0_collection;
		List_1_InsertRange_m99F96BAC4A4EE626A6141612A5C6793BE37E006C(__this, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return;
	}
}
// Method Definition Index: 11364
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_tA097AD399F1ED95CBDEFC0663790A10C4A43DFB8* List_1_AsReadOnly_m25EFCCC51703B079DBFEBF80EE3C8012FBBF1AA3_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlyCollection_1_tA097AD399F1ED95CBDEFC0663790A10C4A43DFB8* L_0 = (ReadOnlyCollection_1_tA097AD399F1ED95CBDEFC0663790A10C4A43DFB8*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
		ReadOnlyCollection_1__ctor_m401D0350AA4E4ACE83F75DE82712FF72120418D4(L_0, (RuntimeObject*)__this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11365
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mA5BE0B8B14B9B4F0EC39944DA4E226868121C961_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, int32_t ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		int32_t L_7 = ___1_count;
		int32_t L_8 = ___2_item;
		RuntimeObject* L_9 = ___3_comparer;
		int32_t L_10;
		L_10 = Array_BinarySearch_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m159F9F5904FFDBCBDB9ED09247CCE6E536AFEA23(L_5, L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18));
		return L_10;
	}
}
// Method Definition Index: 11366
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mDECC3BB81CADCBF898B675D76FE55BBDAD2B6C17_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m8719A0E4E1F227424476DE81DF6DF6B58CA1CD71_inline(__this, NULL);
		int32_t L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_BinarySearch_mA5BE0B8B14B9B4F0EC39944DA4E226868121C961(__this, 0, L_0, L_1, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_2;
	}
}
// Method Definition Index: 11367
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m23EB9AB4CE1C51289E2C55B58614DACE8D1C99E2_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, RuntimeObject* ___1_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m8719A0E4E1F227424476DE81DF6DF6B58CA1CD71_inline(__this, NULL);
		int32_t L_1 = ___0_item;
		RuntimeObject* L_2 = ___1_comparer;
		int32_t L_3;
		L_3 = List_1_BinarySearch_mA5BE0B8B14B9B4F0EC39944DA4E226868121C961(__this, 0, L_0, L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_3;
	}
}
// Method Definition Index: 11368
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Clear_m4E96FC2CDBD626013EB39C0428C26B9D88359E19_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		goto IL_0035;
	}

IL_0035:
	{
		__this->____size = 0;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11369
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m61A085CF349BFF87A90BE6FEE413C548F3F9E567_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_IndexOf_m22A847FE310BE1BA056D9344E43A4E5BB88759D7(__this, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return (bool)((((int32_t)((((int32_t)L_2) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_0016:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11370
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_Contains_m34CA912991B08316DC2FF3362B1E0BE2B10A180A_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_mD81F4794E5BA5AD31C7A25CA1B456BA04E37EC1D(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Contains_m61A085CF349BFF87A90BE6FEE413C548F3F9E567(__this, ((*(int32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11371
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_mAFEE5FF355F693C782528635F3A5D4C7407F3E59_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, __Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_0 = ___0_array;
		List_1_CopyTo_m02DA9C05536AB370B550B399953F68908C934B06(__this, L_0, 0, NULL);
		return;
	}
}
// Method Definition Index: 11372
// Method Definition Index: 11373
// Method Definition Index: 11374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mB5CE702B99DFF8E1BB13570D69ADA436D814DBBF_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_min, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t G_B4_0 = 0;
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int32_t L_2 = ___0_min;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003d;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_3 = __this->____items;
		NullCheck(L_3);
		if (!(((RuntimeArray*)L_3)->max_length))
		{
			goto IL_0020;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_4 = __this->____items;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		G_B4_0 = ((int32_t)il2cpp_codegen_multiply(L_5, 2));
		goto IL_0021;
	}

IL_0020:
	{
		G_B4_0 = 4;
	}

IL_0021:
	{
		V_0 = G_B4_0;
		int32_t L_6 = V_0;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)2146435071)))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = ((int32_t)2146435071);
	}

IL_0030:
	{
		int32_t L_7 = V_0;
		int32_t L_8 = ___0_min;
		if ((((int32_t)L_7) >= ((int32_t)L_8)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_9 = ___0_min;
		V_0 = L_9;
	}

IL_0036:
	{
		int32_t L_10 = V_0;
		List_1_set_Capacity_m7AA13FF7BE1A11F437B16EAE3E1FA50DBFC902AE(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_003d:
	{
		return;
	}
}
// Method Definition Index: 11375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_m1C555E1F765D78EF4E81FE626AA003EA3122C5FA_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_0 = ___0_match;
		int32_t L_1;
		L_1 = List_1_FindIndex_m334C39F1B647D613EE0A8043D6B30A64B63E713C(__this, L_0, NULL);
		return (bool)((((int32_t)((((int32_t)L_1) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 11376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_Find_mD2DE1BCA53139B5E19E30865EBC8F792693ED592_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0032;
	}

IL_000d:
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_1 = ___0_match;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_2 = __this->____items;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		int32_t L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		NullCheck(L_1);
		bool L_6;
		L_6 = Predicate_1_Invoke_m0D65AFF43065FE03318CBDDB2A1E63B51476FB9A_inline(L_1, L_5, NULL);
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_7 = __this->____items;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		int32_t L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		return L_10;
	}

IL_002e:
	{
		int32_t L_11 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0032:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____size;
		if ((((int32_t)L_12) < ((int32_t)L_13)))
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_1), sizeof(int32_t));
		int32_t L_14 = V_1;
		return L_14;
	}
}
// Method Definition Index: 11377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m334C39F1B647D613EE0A8043D6B30A64B63E713C_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_1 = ___0_match;
		int32_t L_2;
		L_2 = List_1_FindIndex_mC9F71DEB213A8D42EBC3A47798AAF6CD140438CA(__this, 0, L_0, L_1, NULL);
		return L_2;
	}
}
// Method Definition Index: 11378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mC9F71DEB213A8D42EBC3A47798AAF6CD140438CA_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_startIndex;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_000e:
	{
		int32_t L_2 = ___1_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_3) <= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_4, L_5)))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_0022:
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_6 = ___2_match;
		if (L_6)
		{
			goto IL_002b;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_002b:
	{
		int32_t L_7 = ___0_startIndex;
		int32_t L_8 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, L_8));
		int32_t L_9 = ___0_startIndex;
		V_1 = L_9;
		goto IL_004d;
	}

IL_0033:
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_10 = ___2_match;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_11 = __this->____items;
		int32_t L_12 = V_1;
		NullCheck(L_11);
		int32_t L_13 = L_12;
		int32_t L_14 = (L_11)->GetAt(static_cast<il2cpp_array_size_t>(L_13));
		NullCheck(L_10);
		bool L_15;
		L_15 = Predicate_1_Invoke_m0D65AFF43065FE03318CBDDB2A1E63B51476FB9A_inline(L_10, L_14, NULL);
		if (!L_15)
		{
			goto IL_0049;
		}
	}
	{
		int32_t L_16 = V_1;
		return L_16;
	}

IL_0049:
	{
		int32_t L_17 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_004d:
	{
		int32_t L_18 = V_1;
		int32_t L_19 = V_0;
		if ((((int32_t)L_18) < ((int32_t)L_19)))
		{
			goto IL_0033;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m3F9E4E3755E57DD4473CFCE5F62E9A19D405A75B_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		int32_t L_1 = __this->____size;
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_2 = ___0_match;
		int32_t L_3;
		L_3 = List_1_FindLastIndex_mF8BED79CAE6C97F36825E0CB1B54B4976D95ED56(__this, ((int32_t)il2cpp_codegen_subtract(L_0, 1)), L_1, L_2, NULL);
		return L_3;
	}
}
// Method Definition Index: 11380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mF8BED79CAE6C97F36825E0CB1B54B4976D95ED56_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_0 = ___2_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		int32_t L_1 = __this->____size;
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = ___0_startIndex;
		if ((((int32_t)L_2) == ((int32_t)(-1))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
		goto IL_002a;
	}

IL_001c:
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		if ((!(((uint32_t)L_3) >= ((uint32_t)L_4))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_002a:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) < ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_6 = ___0_startIndex;
		int32_t L_7 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_6, L_7)), 1))) >= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}

IL_0036:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_003b:
	{
		int32_t L_8 = ___0_startIndex;
		int32_t L_9 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		int32_t L_10 = ___0_startIndex;
		V_1 = L_10;
		goto IL_005d;
	}

IL_0043:
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_11 = ___2_match;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_12 = __this->____items;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		int32_t L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		NullCheck(L_11);
		bool L_16;
		L_16 = Predicate_1_Invoke_m0D65AFF43065FE03318CBDDB2A1E63B51476FB9A_inline(L_11, L_15, NULL);
		if (!L_16)
		{
			goto IL_0059;
		}
	}
	{
		int32_t L_17 = V_1;
		return L_17;
	}

IL_0059:
	{
		int32_t L_18 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_18, 1));
	}

IL_005d:
	{
		int32_t L_19 = V_1;
		int32_t L_20 = V_0;
		if ((((int32_t)L_19) > ((int32_t)L_20)))
		{
			goto IL_0043;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_ForEach_m2053CFA2DF20D359D7A94CCC9E7AA887BDD4E51A_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, Action_1_t5FD82308A537DD6AD12A51AF0790FA13C5BE2D65* ___0_action, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Action_1_t5FD82308A537DD6AD12A51AF0790FA13C5BE2D65* L_0 = ___0_action;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)33), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____version;
		V_0 = L_1;
		V_1 = 0;
		goto IL_0034;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____version;
		if ((!(((uint32_t)L_2) == ((uint32_t)L_3))))
		{
			goto IL_003d;
		}
	}
	{
		Action_1_t5FD82308A537DD6AD12A51AF0790FA13C5BE2D65* L_4 = ___0_action;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_5 = __this->____items;
		int32_t L_6 = V_1;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		int32_t L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		Action_1_Invoke_mE118EB8C2BFC9D370FE37C05DE4F2CD6CBE9DB54_inline(L_4, L_8, NULL);
		int32_t L_9 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0034:
	{
		int32_t L_10 = V_1;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0015;
		}
	}

IL_003d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____version;
		if ((((int32_t)L_12) == ((int32_t)L_13)))
		{
			goto IL_004b;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_004b:
	{
		return;
	}
}
// Method Definition Index: 11382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6 List_1_GetEnumerator_mEF61540761E14807B2930879741EF5E1E973FCE8_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m0B844022E6FED4C6DA8CD7C270989D9D34800C69((&L_0), __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_Generic_IEnumerableU3CTU3E_GetEnumerator_m78062C379A1F46A464E795C0849AB4EDFF868B42_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m0B844022E6FED4C6DA8CD7C270989D9D34800C69((&L_0), __this, NULL);
		Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IEnumerable_GetEnumerator_m45DBBE789A3F8786F76C5A2585457FD2639A75A6_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m0B844022E6FED4C6DA8CD7C270989D9D34800C69((&L_0), __this, NULL);
		Enumerator_t038D231EFD33CA6827B72B0B493F822DC96F8AC6 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* List_1_GetRange_m4404FB89F08759A506DD099FE60239E279FCDC9C_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* L_6 = (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		List_1__ctor_mBEC4BB23DEF62EFA5D66833D82CEB6FABDD96E04(L_6, L_5, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33));
		V_0 = L_6;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* L_9 = V_0;
		NullCheck(L_9);
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_10 = L_9->____items;
		int32_t L_11 = ___1_count;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_7, L_8, (RuntimeArray*)L_10, 0, L_11, NULL);
		List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* L_12 = V_0;
		int32_t L_13 = ___1_count;
		NullCheck(L_12);
		L_12->____size = L_13;
		List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* L_14 = V_0;
		return L_14;
	}
}
// Method Definition Index: 11386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m22A847FE310BE1BA056D9344E43A4E5BB88759D7_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_0 = __this->____items;
		int32_t L_1 = ___0_item;
		int32_t L_2 = __this->____size;
		int32_t L_3;
		L_3 = Array_IndexOf_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m9AE979627DAA17E8B9E3EF440A130C1B9C245059(L_0, L_1, 0, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34));
		return L_3;
	}
}
// Method Definition Index: 11387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_IndexOf_m4782CB7C7B19993DDAFEA39DB1C1E199BB6FF519_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_mD81F4794E5BA5AD31C7A25CA1B456BA04E37EC1D(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		int32_t L_3;
		L_3 = List_1_IndexOf_m22A847FE310BE1BA056D9344E43A4E5BB88759D7(__this, ((*(int32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_m8C3391D73E5396E7FD12A5DA1CEDDD0A83EAAB79_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)13), ((int32_t)27), NULL);
	}

IL_0012:
	{
		int32_t L_2 = __this->____size;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0030;
		}
	}
	{
		int32_t L_5 = __this->____size;
		List_1_EnsureCapacity_mB5CE702B99DFF8E1BB13570D69ADA436D814DBBF(__this, ((int32_t)il2cpp_codegen_add(L_5, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
	}

IL_0030:
	{
		int32_t L_6 = ___0_index;
		int32_t L_7 = __this->____size;
		if ((((int32_t)L_6) >= ((int32_t)L_7)))
		{
			goto IL_0056;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_8 = __this->____items;
		int32_t L_9 = ___0_index;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_10 = __this->____items;
		int32_t L_11 = ___0_index;
		int32_t L_12 = __this->____size;
		int32_t L_13 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_8, L_9, (RuntimeArray*)L_10, ((int32_t)il2cpp_codegen_add(L_11, 1)), ((int32_t)il2cpp_codegen_subtract(L_12, L_13)), NULL);
	}

IL_0056:
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = ___1_item;
		NullCheck(L_14);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(L_15), (int32_t)L_16);
		int32_t L_17 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_17, 1));
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		return;
	}
}
// Method Definition Index: 11389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Insert_m1DF6E9F34E488816C096E628611633BEB631D2F9_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, RuntimeObject* ___1_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_m835718C92D2C5DA2051794F6922D21453EAC493E(L_0, ((int32_t)20), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_item;
		List_1_Insert_m8C3391D73E5396E7FD12A5DA1CEDDD0A83EAAB79(__this, L_1, ((*(int32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m99F96BAC4A4EE626A6141612A5C6793BE37E006C_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	RuntimeObject* V_2 = NULL;
	{
		RuntimeObject* L_0 = ___1_collection;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___0_index;
		int32_t L_2 = __this->____size;
		if ((!(((uint32_t)L_1) > ((uint32_t)L_2))))
		{
			goto IL_0017;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ___1_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_4 = V_0;
		if (!L_4)
		{
			goto IL_00bc;
		}
	}
	{
		RuntimeObject* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_5);
		V_1 = L_6;
		int32_t L_7 = V_1;
		if ((((int32_t)L_7) <= ((int32_t)0)))
		{
			goto IL_00fb;
		}
	}
	{
		int32_t L_8 = __this->____size;
		int32_t L_9 = V_1;
		List_1_EnsureCapacity_mB5CE702B99DFF8E1BB13570D69ADA436D814DBBF(__this, ((int32_t)il2cpp_codegen_add(L_8, L_9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_10 = ___0_index;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) >= ((int32_t)L_11)))
		{
			goto IL_0066;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_12 = __this->____items;
		int32_t L_13 = ___0_index;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = V_1;
		int32_t L_17 = __this->____size;
		int32_t L_18 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_12, L_13, (RuntimeArray*)L_14, ((int32_t)il2cpp_codegen_add(L_15, L_16)), ((int32_t)il2cpp_codegen_subtract(L_17, L_18)), NULL);
	}

IL_0066:
	{
		RuntimeObject* L_19 = V_0;
		if ((!(((RuntimeObject*)(List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255*)__this) == ((RuntimeObject*)(RuntimeObject*)L_19))))
		{
			goto IL_009f;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_20 = __this->____items;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_21 = __this->____items;
		int32_t L_22 = ___0_index;
		int32_t L_23 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_20, 0, (RuntimeArray*)L_21, L_22, L_23, NULL);
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_24 = __this->____items;
		int32_t L_25 = ___0_index;
		int32_t L_26 = V_1;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_27 = __this->____items;
		int32_t L_28 = ___0_index;
		int32_t L_29 = __this->____size;
		int32_t L_30 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_24, ((int32_t)il2cpp_codegen_add(L_25, L_26)), (RuntimeArray*)L_27, ((int32_t)il2cpp_codegen_multiply(L_28, 2)), ((int32_t)il2cpp_codegen_subtract(L_29, L_30)), NULL);
		goto IL_00ac;
	}

IL_009f:
	{
		RuntimeObject* L_31 = V_0;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_32 = __this->____items;
		int32_t L_33 = ___0_index;
		NullCheck(L_31);
		InterfaceActionInvoker2< __Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_31, L_32, L_33);
	}

IL_00ac:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_34, L_35));
		goto IL_00fb;
	}

IL_00bc:
	{
		int32_t L_36 = ___0_index;
		int32_t L_37 = __this->____size;
		if ((((int32_t)L_36) >= ((int32_t)L_37)))
		{
			goto IL_00f4;
		}
	}
	{
		RuntimeObject* L_38 = ___1_collection;
		NullCheck(L_38);
		RuntimeObject* L_39;
		L_39 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_38);
		V_2 = L_39;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ea:
			{
				{
					RuntimeObject* L_40 = V_2;
					if (!L_40)
					{
						goto IL_00f3;
					}
				}
				{
					RuntimeObject* L_41 = V_2;
					NullCheck((RuntimeObject*)L_41);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_41);
				}

IL_00f3:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00e0_1;
			}

IL_00ce_1:
			{
				int32_t L_42 = ___0_index;
				int32_t L_43 = L_42;
				___0_index = ((int32_t)il2cpp_codegen_add(L_43, 1));
				RuntimeObject* L_44 = V_2;
				NullCheck(L_44);
				int32_t L_45;
				L_45 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_44);
				List_1_Insert_m8C3391D73E5396E7FD12A5DA1CEDDD0A83EAAB79(__this, L_43, L_45, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
			}

IL_00e0_1:
			{
				RuntimeObject* L_46 = V_2;
				NullCheck((RuntimeObject*)L_46);
				bool L_47;
				L_47 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_46);
				if (L_47)
				{
					goto IL_00ce_1;
				}
			}
			{
				goto IL_00fb;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f4:
	{
		RuntimeObject* L_48 = ___1_collection;
		List_1_AddEnumerable_m70DD3FB3C4E302AA0AACC45C39BCEC4C2A9DB230(__this, L_48, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_00fb:
	{
		int32_t L_49 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_49, 1));
		return;
	}
}
// Method Definition Index: 11391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_mAF82A7B6E01FEB6F200C76071F76554EE4B35DB8_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = ___0_item;
		int32_t L_1;
		L_1 = List_1_IndexOf_m22A847FE310BE1BA056D9344E43A4E5BB88759D7(__this, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		int32_t L_3 = V_0;
		List_1_RemoveAt_mBA121E6727BDC54EB17A84709997D6920BBE4D10(__this, L_3, NULL);
		return (bool)1;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Remove_m4E9543E3637A1201432E256AEBD495300BC773C2_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_mD81F4794E5BA5AD31C7A25CA1B456BA04E37EC1D(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Remove_mAF82A7B6E01FEB6F200C76071F76554EE4B35DB8(__this, ((*(int32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41));
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 11393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_RemoveAll_mCBA2DA3F26920D654BB85DCAF8EEFE5BE16377AE_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0011;
	}

IL_000d:
	{
		int32_t L_1 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_1, 1));
	}

IL_0011:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____size;
		if ((((int32_t)L_2) >= ((int32_t)L_3)))
		{
			goto IL_002e;
		}
	}
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_4 = ___0_match;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_5 = __this->____items;
		int32_t L_6 = V_0;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		int32_t L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		bool L_9;
		L_9 = Predicate_1_Invoke_m0D65AFF43065FE03318CBDDB2A1E63B51476FB9A_inline(L_4, L_8, NULL);
		if (!L_9)
		{
			goto IL_000d;
		}
	}

IL_002e:
	{
		int32_t L_10 = V_0;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0039;
		}
	}
	{
		return 0;
	}

IL_0039:
	{
		int32_t L_12 = V_0;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
		goto IL_0089;
	}

IL_003f:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_0043:
	{
		int32_t L_14 = V_1;
		int32_t L_15 = __this->____size;
		if ((((int32_t)L_14) >= ((int32_t)L_15)))
		{
			goto IL_0060;
		}
	}
	{
		Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* L_16 = ___0_match;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_17 = __this->____items;
		int32_t L_18 = V_1;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		int32_t L_20 = (L_17)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_16);
		bool L_21;
		L_21 = Predicate_1_Invoke_m0D65AFF43065FE03318CBDDB2A1E63B51476FB9A_inline(L_16, L_20, NULL);
		if (L_21)
		{
			goto IL_003f;
		}
	}

IL_0060:
	{
		int32_t L_22 = V_1;
		int32_t L_23 = __this->____size;
		if ((((int32_t)L_22) >= ((int32_t)L_23)))
		{
			goto IL_0089;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_24 = __this->____items;
		int32_t L_25 = V_0;
		int32_t L_26 = L_25;
		V_0 = ((int32_t)il2cpp_codegen_add(L_26, 1));
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_27 = __this->____items;
		int32_t L_28 = V_1;
		int32_t L_29 = L_28;
		V_1 = ((int32_t)il2cpp_codegen_add(L_29, 1));
		NullCheck(L_27);
		int32_t L_30 = L_29;
		int32_t L_31 = (L_27)->GetAt(static_cast<il2cpp_array_size_t>(L_30));
		NullCheck(L_24);
		(L_24)->SetAt(static_cast<il2cpp_array_size_t>(L_26), (int32_t)L_31);
	}

IL_0089:
	{
		int32_t L_32 = V_1;
		int32_t L_33 = __this->____size;
		if ((((int32_t)L_32) < ((int32_t)L_33)))
		{
			goto IL_0043;
		}
	}
	{
		goto IL_00ad;
	}

IL_00ad:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_0;
		int32_t L_36 = V_0;
		__this->____size = L_36;
		int32_t L_37 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_37, 1));
		return ((int32_t)il2cpp_codegen_subtract(L_34, L_35));
	}
}
// Method Definition Index: 11394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_mBA121E6727BDC54EB17A84709997D6920BBE4D10_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		int32_t L_3 = ___0_index;
		int32_t L_4 = __this->____size;
		if ((((int32_t)L_3) >= ((int32_t)L_4)))
		{
			goto IL_0042;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		int32_t L_9 = __this->____size;
		int32_t L_10 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_5, ((int32_t)il2cpp_codegen_add(L_6, 1)), (RuntimeArray*)L_7, L_8, ((int32_t)il2cpp_codegen_subtract(L_9, L_10)), NULL);
	}

IL_0042:
	{
		goto IL_0063;
	}

IL_0063:
	{
		int32_t L_11 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_11, 1));
		return;
	}
}
// Method Definition Index: 11395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveRange_m91A4C1021000C5BFE7D3463F5495536BED08D81B_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		int32_t L_6 = __this->____size;
		int32_t L_7 = __this->____size;
		int32_t L_8 = ___1_count;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_7, L_8));
		int32_t L_9 = ___0_index;
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_9) >= ((int32_t)L_10)))
		{
			goto IL_0066;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_11 = __this->____items;
		int32_t L_12 = ___0_index;
		int32_t L_13 = ___1_count;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = __this->____size;
		int32_t L_17 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_11, ((int32_t)il2cpp_codegen_add(L_12, L_13)), (RuntimeArray*)L_14, L_15, ((int32_t)il2cpp_codegen_subtract(L_16, L_17)), NULL);
	}

IL_0066:
	{
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		goto IL_008d;
	}

IL_008d:
	{
		return;
	}
}
// Method Definition Index: 11396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mE349E70153E21A54521C2E763EB22799EE479401_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m8719A0E4E1F227424476DE81DF6DF6B58CA1CD71_inline(__this, NULL);
		List_1_Reverse_m34A5681B1296F6E4F08C9F0986F4E4F1CF23FBE4(__this, 0, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42));
		return;
	}
}
// Method Definition Index: 11397
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m34A5681B1296F6E4F08C9F0986F4E4F1CF23FBE4_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0038;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		Array_Reverse_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_mA4406A8C17BBC29625F80F783EA91C3AECF7E3EA(L_6, L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43));
	}

IL_0038:
	{
		int32_t L_9 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_9, 1));
		return;
	}
}
// Method Definition Index: 11398
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mCA36792563B8A05B94A0555B0BD8143B4CF970B7_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m8719A0E4E1F227424476DE81DF6DF6B58CA1CD71_inline(__this, NULL);
		List_1_Sort_m29868C70BFD9DDFB44E31BF13302FB7AB5244913(__this, 0, L_0, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m9E9FB3F1E5821F9FFDDD6736D17DF336D7EB9553_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m8719A0E4E1F227424476DE81DF6DF6B58CA1CD71_inline(__this, NULL);
		RuntimeObject* L_1 = ___0_comparer;
		List_1_Sort_m29868C70BFD9DDFB44E31BF13302FB7AB5244913(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m29868C70BFD9DDFB44E31BF13302FB7AB5244913_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0039;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		RuntimeObject* L_9 = ___2_comparer;
		Array_Sort_Tis__Il2CppInt32Enum_tE5E90BB5D60D5281C33DB29D9DCBD70D5E2E1F7B_mE004CFE5BA4856AC83A08DEA9F09D3224F3EDD7B(L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45));
	}

IL_0039:
	{
		int32_t L_10 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_10, 1));
		return;
	}
}
// Method Definition Index: 11401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m37D4F3D2CBBCCB67F7E9AE1408053354CCADACB3_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, Comparison_1_t0F283DBF0EC45EE18464332F2713E3E393EED6C3* ___0_comparison, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit);
	//<source_info:<no-source>:1>
	{
		Comparison_1_t0F283DBF0EC45EE18464332F2713E3E393EED6C3* L_0 = ___0_comparison;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)34), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_1) <= ((int32_t)1)))
		{
			goto IL_0026;
		}
	}
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_2 = __this->____items;
		int32_t L_3 = __this->____size;
		Comparison_1_t0F283DBF0EC45EE18464332F2713E3E393EED6C3* L_4 = ___0_comparison;
		CHECKED_LOCAL_INIT(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 47)),il2cpp_codegen_runtime_class_init_inline);
		ArraySortHelper_1_Sort_mCDDF2BDFA2ACB2C2254C52BBD32AD774C7C05CDC(L_2, 0, L_3, L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46));
	}

IL_0026:
	{
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11402
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* List_1_ToArray_m043FE949D46CFF539CD3838BF2282014737B5DDB_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* V_0 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_1 = ((List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_3 = (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)(__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_2);
		V_0 = L_3;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_4 = __this->____items;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_5 = V_0;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_4, 0, (RuntimeArray*)L_5, 0, L_6, NULL);
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 11403
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_TrimExcess_m7452A27EF67119F672BA3F20ED8EB8FCACBE4CC3_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		double L_2 = (il2cpp_codegen_conv<double,int32_t,int32_t,false,false>(L_1,NULL));
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,double,double,false,false>(((double)il2cpp_codegen_multiply(L_2, (0.90000000000000002))),NULL));
		V_0 = L_3;
		int32_t L_4 = __this->____size;
		int32_t L_5 = V_0;
		if ((((int32_t)L_4) >= ((int32_t)L_5)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = __this->____size;
		List_1_set_Capacity_m7AA13FF7BE1A11F437B16EAE3E1FA50DBFC902AE(__this, L_6, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11404
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m70DD3FB3C4E302AA0AACC45C39BCEC4C2A9DB230_gshared (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		RuntimeObject* L_1 = ___0_enumerable;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_1);
		V_0 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0063:
			{
				{
					RuntimeObject* L_3 = V_0;
					if (!L_3)
					{
						goto IL_006c;
					}
				}
				{
					RuntimeObject* L_4 = V_0;
					NullCheck((RuntimeObject*)L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_4);
				}

IL_006c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0059_1;
			}

IL_0017_1:
			{
				RuntimeObject* L_5 = V_0;
				NullCheck(L_5);
				int32_t L_6;
				L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_5);
				V_1 = L_6;
				int32_t L_7 = __this->____size;
				__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_8 = __this->____items;
				NullCheck(L_8);
				int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
				if ((!(((uint32_t)L_7) == ((uint32_t)L_9))))
				{
					goto IL_003c_1;
				}
			}
			{
				int32_t L_10 = __this->____size;
				List_1_EnsureCapacity_mB5CE702B99DFF8E1BB13570D69ADA436D814DBBF(__this, ((int32_t)il2cpp_codegen_add(L_10, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
			}

IL_003c_1:
			{
				__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_11 = __this->____items;
				int32_t L_12 = __this->____size;
				V_2 = L_12;
				int32_t L_13 = V_2;
				__this->____size = ((int32_t)il2cpp_codegen_add(L_13, 1));
				int32_t L_14 = V_2;
				int32_t L_15 = V_1;
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_14), (int32_t)L_15);
			}

IL_0059_1:
			{
				RuntimeObject* L_16 = V_0;
				NullCheck((RuntimeObject*)L_16);
				bool L_17;
				L_17 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_16);
				if (L_17)
				{
					goto IL_0017_1;
				}
			}
			{
				goto IL_006d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006d:
	{
		return;
	}
}
// Method Definition Index: 11405
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__cctor_m9AA83899E865730828F96BB9658D55D09C2D86E0_gshared (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_0 = (__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)(__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (uint32_t)0);
		((List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray), (void*)L_0);
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
// Method Definition Index: 11344
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_mCB9111975275CFD66458FB66A2641C9C4C0521BD_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_0 = ((List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_0);
		return;
	}
}
// Method Definition Index: 11345
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m82F585DA43A63F3A8BB73B2523628CB488A11894_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_capacity, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		int32_t L_0 = ___0_capacity;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)12), 4, NULL);
	}

IL_0012:
	{
		int32_t L_1 = ___0_capacity;
		if (L_1)
		{
			goto IL_0021;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_2 = ((List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_2);
		return;
	}

IL_0021:
	{
		int32_t L_3 = ___0_capacity;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_4 = (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)(__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_3);
		__this->____items = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_4);
		return;
	}
}
// Method Definition Index: 11346
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_mF009E42DD9EE4D1F5B8CE3E24451D6454533465D_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2((RuntimeObject*)__this, NULL);
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_000f:
	{
		RuntimeObject* L_1 = ___0_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0050;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_3);
		V_1 = L_4;
		int32_t L_5 = V_1;
		if (L_5)
		{
			goto IL_002f;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_6 = ((List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_6);
		return;
	}

IL_002f:
	{
		int32_t L_7 = V_1;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_8 = (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)(__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_7);
		__this->____items = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_8);
		RuntimeObject* L_9 = V_0;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_10 = __this->____items;
		NullCheck(L_9);
		InterfaceActionInvoker2< __Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_9, L_10, 0);
		int32_t L_11 = V_1;
		__this->____size = L_11;
		return;
	}

IL_0050:
	{
		__this->____size = 0;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_12 = ((List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		RuntimeObject* L_13 = ___0_collection;
		List_1_AddEnumerable_m681B8A3C4E9973D4BBAAC17D1EF146A796DF11CB(__this, L_13, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
		return;
	}
}
// Method Definition Index: 11347
// Method Definition Index: 11348
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mCA75F83CA2F20BC02FFECCA387D4C021A2D83511_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* V_0 = NULL;
	{
		int32_t L_0 = ___0_value;
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)15), ((int32_t)21), NULL);
	}

IL_0012:
	{
		int32_t L_2 = ___0_value;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_2) == ((int32_t)L_4)))
		{
			goto IL_0058;
		}
	}
	{
		int32_t L_5 = ___0_value;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_004d;
		}
	}
	{
		int32_t L_6 = ___0_value;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_7 = (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)(__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_6);
		V_0 = L_7;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0045;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_9 = __this->____items;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_10 = V_0;
		int32_t L_11 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_9, 0, (RuntimeArray*)L_10, 0, L_11, NULL);
	}

IL_0045:
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_12 = V_0;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		return;
	}

IL_004d:
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_13 = ((List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_13);
	}

IL_0058:
	{
		return;
	}
}
// Method Definition Index: 11349
// Method Definition Index: 11350
// Method Definition Index: 11351
// Method Definition Index: 11352
// Method Definition Index: 11353
// Method Definition Index: 11354
// Method Definition Index: 11355
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR uint32_t List_1_get_Item_m996B509A482EA372164DE730F73C1E11BA2F3EAE_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		uint32_t L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		return L_5;
	}
}
// Method Definition Index: 11356
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m156A9F0E34470C01D118D6391E192E7D54189C0F_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, uint32_t ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		uint32_t L_4 = ___1_value;
		NullCheck(L_2);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(L_3), (uint32_t)L_4);
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11357
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_mED20F2A2B89C0BFF977AA52105F288790EEF484F_gshared (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	uint32_t V_0 = 0;
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6))))
		{
			goto IL_001f;
		}
	}
	{
		RuntimeObject* L_1 = ___0_value;
		if (L_1)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(uint32_t));
		uint32_t L_2 = V_0;
		return false;
	}

IL_001d:
	{
		return (bool)0;
	}

IL_001f:
	{
		return (bool)1;
	}
}
// Method Definition Index: 11358
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IList_get_Item_mECD72706B76CB2B078686E3AF00F999F90CFE183_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		uint32_t L_1;
		L_1 = List_1_get_Item_m996B509A482EA372164DE730F73C1E11BA2F3EAE(__this, L_0, NULL);
		uint32_t L_2 = L_1;
		RuntimeObject* L_3 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), &L_2);
		return L_3;
	}
}
// Method Definition Index: 11359
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_set_Item_m703DB2820D248D171D16DA33378E8CB2A5471A19_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_value;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mB550F72315A962B072051B4E80B0F9D124721798(L_0, ((int32_t)15), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_value;
		List_1_set_Item_m156A9F0E34470C01D118D6391E192E7D54189C0F(__this, L_1, ((*(uint32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), NULL);
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_value;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11360
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Add_mBDE95F2D32400D0DABA22511FE053AC8957D8D1E_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_7 = V_0;
		int32_t L_8 = V_1;
		uint32_t L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (uint32_t)L_9);
		return;
	}

IL_0034:
	{
		uint32_t L_10 = ___0_item;
		List_1_AddWithResize_m5D090D0E0EFAAA54D1A7D12657EE18BA7AD0F445(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 11361
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m5D090D0E0EFAAA54D1A7D12657EE18BA7AD0F445_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		int32_t L_1 = V_0;
		List_1_EnsureCapacity_m832EAA18FAB897AE9CE65E99ED96EF173701E507(__this, ((int32_t)il2cpp_codegen_add(L_1, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_2 = V_0;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_2, 1));
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_3 = __this->____items;
		int32_t L_4 = V_0;
		uint32_t L_5 = ___0_item;
		NullCheck(L_3);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(L_4), (uint32_t)L_5);
		return;
	}
}
// Method Definition Index: 11362
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_Add_m5D9E429D9E32710B8C35FBAC94BB8AE312993B21_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___0_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mB550F72315A962B072051B4E80B0F9D124721798(L_0, ((int32_t)20), NULL);
	}
	try
	{
		RuntimeObject* L_1 = ___0_item;
		List_1_Add_mBDE95F2D32400D0DABA22511FE053AC8957D8D1E_inline(__this, ((*(uint32_t*)UnBox(L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		goto IL_0029;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0016;
		}
		throw e;
	}

CATCH_0016:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_2 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_3 = ___0_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_4 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_5;
		L_5 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_4, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_3, L_5, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0029;
	}

IL_0029:
	{
		int32_t L_6;
		L_6 = List_1_get_Count_m4754598204C7F7240B07126FE194433F71E3B48E_inline(__this, NULL);
		return ((int32_t)il2cpp_codegen_subtract(L_6, 1));
	}
}
// Method Definition Index: 11363
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m1704844859AA63CBB8CAAD01A3A6B67AC06BF60F_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		RuntimeObject* L_1 = ___0_collection;
		List_1_InsertRange_m694C6D98C9BDD66E139B4FBDC95D06372CF2869E(__this, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return;
	}
}
// Method Definition Index: 11364
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_t57A9EA1D0B6B2A5908DD58EA8C84F29F53D509A1* List_1_AsReadOnly_mFEAF58ABD0695DDBBD8D1219081BD92103E5145B_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlyCollection_1_t57A9EA1D0B6B2A5908DD58EA8C84F29F53D509A1* L_0 = (ReadOnlyCollection_1_t57A9EA1D0B6B2A5908DD58EA8C84F29F53D509A1*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
		ReadOnlyCollection_1__ctor_m0764B150EBDC3AF359794E686D8C2B31B37100C9(L_0, (RuntimeObject*)__this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11365
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m15C2E0B120676758F22A35BED084390D6A444A09_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, uint32_t ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		int32_t L_7 = ___1_count;
		uint32_t L_8 = ___2_item;
		RuntimeObject* L_9 = ___3_comparer;
		int32_t L_10;
		L_10 = Array_BinarySearch_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m30014505CD15EFAF86A6FBE09AF0A55BD9377B5F(L_5, L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18));
		return L_10;
	}
}
// Method Definition Index: 11366
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mB5D19C7724EC014614A9499A1926CE488669571C_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4754598204C7F7240B07126FE194433F71E3B48E_inline(__this, NULL);
		uint32_t L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_BinarySearch_m15C2E0B120676758F22A35BED084390D6A444A09(__this, 0, L_0, L_1, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_2;
	}
}
// Method Definition Index: 11367
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m7FE7F2922AF50AB0E384D787FB01070ABF394439_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, RuntimeObject* ___1_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4754598204C7F7240B07126FE194433F71E3B48E_inline(__this, NULL);
		uint32_t L_1 = ___0_item;
		RuntimeObject* L_2 = ___1_comparer;
		int32_t L_3;
		L_3 = List_1_BinarySearch_m15C2E0B120676758F22A35BED084390D6A444A09(__this, 0, L_0, L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_3;
	}
}
// Method Definition Index: 11368
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Clear_m42ED12620848A505DEAFDD043D3284577FF215E4_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		goto IL_0035;
	}

IL_0035:
	{
		__this->____size = 0;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11369
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_mD62DF0807CE1CF205884252DA7D06B9EDACF5512_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		uint32_t L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_IndexOf_m2FB5B4F89323CDCB893E3BB9C2E256169024B872(__this, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return (bool)((((int32_t)((((int32_t)L_2) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_0016:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11370
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_Contains_m30E8B900C75C4732EE3C643EB81B867D9820EF72_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_mED20F2A2B89C0BFF977AA52105F288790EEF484F(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Contains_mD62DF0807CE1CF205884252DA7D06B9EDACF5512(__this, ((*(uint32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11371
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_mAE1CD09FE18A80F7C6EFE853D09130F6CE8C5E3A_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, __Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_0 = ___0_array;
		List_1_CopyTo_mF4A32F59001BD461521EF332C6C8876EBA635A43(__this, L_0, 0, NULL);
		return;
	}
}
// Method Definition Index: 11372
// Method Definition Index: 11373
// Method Definition Index: 11374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_m832EAA18FAB897AE9CE65E99ED96EF173701E507_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_min, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t G_B4_0 = 0;
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int32_t L_2 = ___0_min;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003d;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_3 = __this->____items;
		NullCheck(L_3);
		if (!(((RuntimeArray*)L_3)->max_length))
		{
			goto IL_0020;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_4 = __this->____items;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		G_B4_0 = ((int32_t)il2cpp_codegen_multiply(L_5, 2));
		goto IL_0021;
	}

IL_0020:
	{
		G_B4_0 = 4;
	}

IL_0021:
	{
		V_0 = G_B4_0;
		int32_t L_6 = V_0;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)2146435071)))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = ((int32_t)2146435071);
	}

IL_0030:
	{
		int32_t L_7 = V_0;
		int32_t L_8 = ___0_min;
		if ((((int32_t)L_7) >= ((int32_t)L_8)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_9 = ___0_min;
		V_0 = L_9;
	}

IL_0036:
	{
		int32_t L_10 = V_0;
		List_1_set_Capacity_mCA75F83CA2F20BC02FFECCA387D4C021A2D83511(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_003d:
	{
		return;
	}
}
// Method Definition Index: 11375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_m3C86B7CCEFD0BE25A5E6067689892F5119E76E4B_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_0 = ___0_match;
		int32_t L_1;
		L_1 = List_1_FindIndex_m5DFECA19BA1FF082CEE8E8EE854277501A214F17(__this, L_0, NULL);
		return (bool)((((int32_t)((((int32_t)L_1) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 11376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR uint32_t List_1_Find_mD52B71615EAF50CE5DC648618D88E4FD7222CAE8_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	uint32_t V_1 = 0;
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0032;
	}

IL_000d:
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_1 = ___0_match;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_2 = __this->____items;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		uint32_t L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		NullCheck(L_1);
		bool L_6;
		L_6 = Predicate_1_Invoke_m2603DCEFACCFCB054A6BC33E68531E62F317C8A4_inline(L_1, L_5, NULL);
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_7 = __this->____items;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		uint32_t L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		return L_10;
	}

IL_002e:
	{
		int32_t L_11 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0032:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____size;
		if ((((int32_t)L_12) < ((int32_t)L_13)))
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_1), sizeof(uint32_t));
		uint32_t L_14 = V_1;
		return L_14;
	}
}
// Method Definition Index: 11377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m5DFECA19BA1FF082CEE8E8EE854277501A214F17_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_1 = ___0_match;
		int32_t L_2;
		L_2 = List_1_FindIndex_mA09105FDEDF74F29015B15E28F5E73BA9C4BCAFE(__this, 0, L_0, L_1, NULL);
		return L_2;
	}
}
// Method Definition Index: 11378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mA09105FDEDF74F29015B15E28F5E73BA9C4BCAFE_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_startIndex;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_000e:
	{
		int32_t L_2 = ___1_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_3) <= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_4, L_5)))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_0022:
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_6 = ___2_match;
		if (L_6)
		{
			goto IL_002b;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_002b:
	{
		int32_t L_7 = ___0_startIndex;
		int32_t L_8 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, L_8));
		int32_t L_9 = ___0_startIndex;
		V_1 = L_9;
		goto IL_004d;
	}

IL_0033:
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_10 = ___2_match;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_11 = __this->____items;
		int32_t L_12 = V_1;
		NullCheck(L_11);
		int32_t L_13 = L_12;
		uint32_t L_14 = (L_11)->GetAt(static_cast<il2cpp_array_size_t>(L_13));
		NullCheck(L_10);
		bool L_15;
		L_15 = Predicate_1_Invoke_m2603DCEFACCFCB054A6BC33E68531E62F317C8A4_inline(L_10, L_14, NULL);
		if (!L_15)
		{
			goto IL_0049;
		}
	}
	{
		int32_t L_16 = V_1;
		return L_16;
	}

IL_0049:
	{
		int32_t L_17 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_004d:
	{
		int32_t L_18 = V_1;
		int32_t L_19 = V_0;
		if ((((int32_t)L_18) < ((int32_t)L_19)))
		{
			goto IL_0033;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m359B4E1CBD18376167D24C134363B88DA38E185E_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		int32_t L_1 = __this->____size;
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_2 = ___0_match;
		int32_t L_3;
		L_3 = List_1_FindLastIndex_mAC723FB10B249538A74DB3B20E203F0EF3764C88(__this, ((int32_t)il2cpp_codegen_subtract(L_0, 1)), L_1, L_2, NULL);
		return L_3;
	}
}
// Method Definition Index: 11380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mAC723FB10B249538A74DB3B20E203F0EF3764C88_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_0 = ___2_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		int32_t L_1 = __this->____size;
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = ___0_startIndex;
		if ((((int32_t)L_2) == ((int32_t)(-1))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
		goto IL_002a;
	}

IL_001c:
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		if ((!(((uint32_t)L_3) >= ((uint32_t)L_4))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_002a:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) < ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_6 = ___0_startIndex;
		int32_t L_7 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_6, L_7)), 1))) >= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}

IL_0036:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_003b:
	{
		int32_t L_8 = ___0_startIndex;
		int32_t L_9 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		int32_t L_10 = ___0_startIndex;
		V_1 = L_10;
		goto IL_005d;
	}

IL_0043:
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_11 = ___2_match;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_12 = __this->____items;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		uint32_t L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		NullCheck(L_11);
		bool L_16;
		L_16 = Predicate_1_Invoke_m2603DCEFACCFCB054A6BC33E68531E62F317C8A4_inline(L_11, L_15, NULL);
		if (!L_16)
		{
			goto IL_0059;
		}
	}
	{
		int32_t L_17 = V_1;
		return L_17;
	}

IL_0059:
	{
		int32_t L_18 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_18, 1));
	}

IL_005d:
	{
		int32_t L_19 = V_1;
		int32_t L_20 = V_0;
		if ((((int32_t)L_19) > ((int32_t)L_20)))
		{
			goto IL_0043;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_ForEach_m7B47D5473C5480EFBD5DCEDDF8D537D7ABB83681_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, Action_1_t6E017BD45EFAD0010771D0174C3A5653E776128E* ___0_action, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Action_1_t6E017BD45EFAD0010771D0174C3A5653E776128E* L_0 = ___0_action;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)33), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____version;
		V_0 = L_1;
		V_1 = 0;
		goto IL_0034;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____version;
		if ((!(((uint32_t)L_2) == ((uint32_t)L_3))))
		{
			goto IL_003d;
		}
	}
	{
		Action_1_t6E017BD45EFAD0010771D0174C3A5653E776128E* L_4 = ___0_action;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_5 = __this->____items;
		int32_t L_6 = V_1;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		uint32_t L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		Action_1_Invoke_m124DA039CD5C63AD48841B21065DFC6C03E63930_inline(L_4, L_8, NULL);
		int32_t L_9 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0034:
	{
		int32_t L_10 = V_1;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0015;
		}
	}

IL_003d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____version;
		if ((((int32_t)L_12) == ((int32_t)L_13)))
		{
			goto IL_004b;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_004b:
	{
		return;
	}
}
// Method Definition Index: 11382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t474D057FE73061C816292BC377C411C94495375A List_1_GetEnumerator_m778328E94119E9545A29DB38EF0418F2FBFAF5E8_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t474D057FE73061C816292BC377C411C94495375A L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m48AA10A08952E197C1709A3F3AE09FB34BE212C4((&L_0), __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_Generic_IEnumerableU3CTU3E_GetEnumerator_m7D059C776DA2A58671D0B3477EBCE8D9756DC519_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t474D057FE73061C816292BC377C411C94495375A L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m48AA10A08952E197C1709A3F3AE09FB34BE212C4((&L_0), __this, NULL);
		Enumerator_t474D057FE73061C816292BC377C411C94495375A L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IEnumerable_GetEnumerator_m5DC334FCA170041E12F5366E694CFDCEA4920AA2_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t474D057FE73061C816292BC377C411C94495375A L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m48AA10A08952E197C1709A3F3AE09FB34BE212C4((&L_0), __this, NULL);
		Enumerator_t474D057FE73061C816292BC377C411C94495375A L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* List_1_GetRange_mE6854021AEE99911D3EC1C79D75FC4CFF64C9C76_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* L_6 = (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		List_1__ctor_m82F585DA43A63F3A8BB73B2523628CB488A11894(L_6, L_5, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33));
		V_0 = L_6;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* L_9 = V_0;
		NullCheck(L_9);
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_10 = L_9->____items;
		int32_t L_11 = ___1_count;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_7, L_8, (RuntimeArray*)L_10, 0, L_11, NULL);
		List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* L_12 = V_0;
		int32_t L_13 = ___1_count;
		NullCheck(L_12);
		L_12->____size = L_13;
		List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* L_14 = V_0;
		return L_14;
	}
}
// Method Definition Index: 11386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m2FB5B4F89323CDCB893E3BB9C2E256169024B872_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_0 = __this->____items;
		uint32_t L_1 = ___0_item;
		int32_t L_2 = __this->____size;
		int32_t L_3;
		L_3 = Array_IndexOf_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m7CDD62DD0B0CCE0B04CF724866933C0921E81011(L_0, L_1, 0, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34));
		return L_3;
	}
}
// Method Definition Index: 11387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_IndexOf_m691901FDE66317FE64C5FF03D033F548F37286F9_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_mED20F2A2B89C0BFF977AA52105F288790EEF484F(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		int32_t L_3;
		L_3 = List_1_IndexOf_m2FB5B4F89323CDCB893E3BB9C2E256169024B872(__this, ((*(uint32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mAEFE99D044558897366103682E2EFFAAECA05AD2_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, uint32_t ___1_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)13), ((int32_t)27), NULL);
	}

IL_0012:
	{
		int32_t L_2 = __this->____size;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0030;
		}
	}
	{
		int32_t L_5 = __this->____size;
		List_1_EnsureCapacity_m832EAA18FAB897AE9CE65E99ED96EF173701E507(__this, ((int32_t)il2cpp_codegen_add(L_5, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
	}

IL_0030:
	{
		int32_t L_6 = ___0_index;
		int32_t L_7 = __this->____size;
		if ((((int32_t)L_6) >= ((int32_t)L_7)))
		{
			goto IL_0056;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_8 = __this->____items;
		int32_t L_9 = ___0_index;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_10 = __this->____items;
		int32_t L_11 = ___0_index;
		int32_t L_12 = __this->____size;
		int32_t L_13 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_8, L_9, (RuntimeArray*)L_10, ((int32_t)il2cpp_codegen_add(L_11, 1)), ((int32_t)il2cpp_codegen_subtract(L_12, L_13)), NULL);
	}

IL_0056:
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		uint32_t L_16 = ___1_item;
		NullCheck(L_14);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(L_15), (uint32_t)L_16);
		int32_t L_17 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_17, 1));
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		return;
	}
}
// Method Definition Index: 11389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Insert_m5868CA395DD18E8E822A143A16FBB486375C754E_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, RuntimeObject* ___1_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mB550F72315A962B072051B4E80B0F9D124721798(L_0, ((int32_t)20), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_item;
		List_1_Insert_mAEFE99D044558897366103682E2EFFAAECA05AD2(__this, L_1, ((*(uint32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m694C6D98C9BDD66E139B4FBDC95D06372CF2869E_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	RuntimeObject* V_2 = NULL;
	{
		RuntimeObject* L_0 = ___1_collection;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___0_index;
		int32_t L_2 = __this->____size;
		if ((!(((uint32_t)L_1) > ((uint32_t)L_2))))
		{
			goto IL_0017;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ___1_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_4 = V_0;
		if (!L_4)
		{
			goto IL_00bc;
		}
	}
	{
		RuntimeObject* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_5);
		V_1 = L_6;
		int32_t L_7 = V_1;
		if ((((int32_t)L_7) <= ((int32_t)0)))
		{
			goto IL_00fb;
		}
	}
	{
		int32_t L_8 = __this->____size;
		int32_t L_9 = V_1;
		List_1_EnsureCapacity_m832EAA18FAB897AE9CE65E99ED96EF173701E507(__this, ((int32_t)il2cpp_codegen_add(L_8, L_9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_10 = ___0_index;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) >= ((int32_t)L_11)))
		{
			goto IL_0066;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_12 = __this->____items;
		int32_t L_13 = ___0_index;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = V_1;
		int32_t L_17 = __this->____size;
		int32_t L_18 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_12, L_13, (RuntimeArray*)L_14, ((int32_t)il2cpp_codegen_add(L_15, L_16)), ((int32_t)il2cpp_codegen_subtract(L_17, L_18)), NULL);
	}

IL_0066:
	{
		RuntimeObject* L_19 = V_0;
		if ((!(((RuntimeObject*)(List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75*)__this) == ((RuntimeObject*)(RuntimeObject*)L_19))))
		{
			goto IL_009f;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_20 = __this->____items;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_21 = __this->____items;
		int32_t L_22 = ___0_index;
		int32_t L_23 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_20, 0, (RuntimeArray*)L_21, L_22, L_23, NULL);
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_24 = __this->____items;
		int32_t L_25 = ___0_index;
		int32_t L_26 = V_1;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_27 = __this->____items;
		int32_t L_28 = ___0_index;
		int32_t L_29 = __this->____size;
		int32_t L_30 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_24, ((int32_t)il2cpp_codegen_add(L_25, L_26)), (RuntimeArray*)L_27, ((int32_t)il2cpp_codegen_multiply(L_28, 2)), ((int32_t)il2cpp_codegen_subtract(L_29, L_30)), NULL);
		goto IL_00ac;
	}

IL_009f:
	{
		RuntimeObject* L_31 = V_0;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_32 = __this->____items;
		int32_t L_33 = ___0_index;
		NullCheck(L_31);
		InterfaceActionInvoker2< __Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_31, L_32, L_33);
	}

IL_00ac:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_34, L_35));
		goto IL_00fb;
	}

IL_00bc:
	{
		int32_t L_36 = ___0_index;
		int32_t L_37 = __this->____size;
		if ((((int32_t)L_36) >= ((int32_t)L_37)))
		{
			goto IL_00f4;
		}
	}
	{
		RuntimeObject* L_38 = ___1_collection;
		NullCheck(L_38);
		RuntimeObject* L_39;
		L_39 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_38);
		V_2 = L_39;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ea:
			{
				{
					RuntimeObject* L_40 = V_2;
					if (!L_40)
					{
						goto IL_00f3;
					}
				}
				{
					RuntimeObject* L_41 = V_2;
					NullCheck((RuntimeObject*)L_41);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_41);
				}

IL_00f3:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00e0_1;
			}

IL_00ce_1:
			{
				int32_t L_42 = ___0_index;
				int32_t L_43 = L_42;
				___0_index = ((int32_t)il2cpp_codegen_add(L_43, 1));
				RuntimeObject* L_44 = V_2;
				NullCheck(L_44);
				uint32_t L_45;
				L_45 = InterfaceFuncInvoker0< uint32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_44);
				List_1_Insert_mAEFE99D044558897366103682E2EFFAAECA05AD2(__this, L_43, L_45, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
			}

IL_00e0_1:
			{
				RuntimeObject* L_46 = V_2;
				NullCheck((RuntimeObject*)L_46);
				bool L_47;
				L_47 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_46);
				if (L_47)
				{
					goto IL_00ce_1;
				}
			}
			{
				goto IL_00fb;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f4:
	{
		RuntimeObject* L_48 = ___1_collection;
		List_1_AddEnumerable_m681B8A3C4E9973D4BBAAC17D1EF146A796DF11CB(__this, L_48, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_00fb:
	{
		int32_t L_49 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_49, 1));
		return;
	}
}
// Method Definition Index: 11391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_mDFC275828413520E93EC379BEC3B9B419975DD5C_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		uint32_t L_0 = ___0_item;
		int32_t L_1;
		L_1 = List_1_IndexOf_m2FB5B4F89323CDCB893E3BB9C2E256169024B872(__this, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		int32_t L_3 = V_0;
		List_1_RemoveAt_mFF22444E8F3DC86B69E3BC3E2E1F8D4618AC21A7(__this, L_3, NULL);
		return (bool)1;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Remove_m97D648E4051E6313D857BCE52BF4310BFB27DCD1_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_mED20F2A2B89C0BFF977AA52105F288790EEF484F(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Remove_mDFC275828413520E93EC379BEC3B9B419975DD5C(__this, ((*(uint32_t*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41));
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 11393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_RemoveAll_m239CFB8AAFB20EB0D8B38071A22DFC66C4A8824B_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0011;
	}

IL_000d:
	{
		int32_t L_1 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_1, 1));
	}

IL_0011:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____size;
		if ((((int32_t)L_2) >= ((int32_t)L_3)))
		{
			goto IL_002e;
		}
	}
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_4 = ___0_match;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_5 = __this->____items;
		int32_t L_6 = V_0;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		uint32_t L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		bool L_9;
		L_9 = Predicate_1_Invoke_m2603DCEFACCFCB054A6BC33E68531E62F317C8A4_inline(L_4, L_8, NULL);
		if (!L_9)
		{
			goto IL_000d;
		}
	}

IL_002e:
	{
		int32_t L_10 = V_0;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0039;
		}
	}
	{
		return 0;
	}

IL_0039:
	{
		int32_t L_12 = V_0;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
		goto IL_0089;
	}

IL_003f:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_0043:
	{
		int32_t L_14 = V_1;
		int32_t L_15 = __this->____size;
		if ((((int32_t)L_14) >= ((int32_t)L_15)))
		{
			goto IL_0060;
		}
	}
	{
		Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* L_16 = ___0_match;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_17 = __this->____items;
		int32_t L_18 = V_1;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		uint32_t L_20 = (L_17)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_16);
		bool L_21;
		L_21 = Predicate_1_Invoke_m2603DCEFACCFCB054A6BC33E68531E62F317C8A4_inline(L_16, L_20, NULL);
		if (L_21)
		{
			goto IL_003f;
		}
	}

IL_0060:
	{
		int32_t L_22 = V_1;
		int32_t L_23 = __this->____size;
		if ((((int32_t)L_22) >= ((int32_t)L_23)))
		{
			goto IL_0089;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_24 = __this->____items;
		int32_t L_25 = V_0;
		int32_t L_26 = L_25;
		V_0 = ((int32_t)il2cpp_codegen_add(L_26, 1));
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_27 = __this->____items;
		int32_t L_28 = V_1;
		int32_t L_29 = L_28;
		V_1 = ((int32_t)il2cpp_codegen_add(L_29, 1));
		NullCheck(L_27);
		int32_t L_30 = L_29;
		uint32_t L_31 = (L_27)->GetAt(static_cast<il2cpp_array_size_t>(L_30));
		NullCheck(L_24);
		(L_24)->SetAt(static_cast<il2cpp_array_size_t>(L_26), (uint32_t)L_31);
	}

IL_0089:
	{
		int32_t L_32 = V_1;
		int32_t L_33 = __this->____size;
		if ((((int32_t)L_32) < ((int32_t)L_33)))
		{
			goto IL_0043;
		}
	}
	{
		goto IL_00ad;
	}

IL_00ad:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_0;
		int32_t L_36 = V_0;
		__this->____size = L_36;
		int32_t L_37 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_37, 1));
		return ((int32_t)il2cpp_codegen_subtract(L_34, L_35));
	}
}
// Method Definition Index: 11394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_mFF22444E8F3DC86B69E3BC3E2E1F8D4618AC21A7_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	uint32_t V_0 = 0;
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		int32_t L_3 = ___0_index;
		int32_t L_4 = __this->____size;
		if ((((int32_t)L_3) >= ((int32_t)L_4)))
		{
			goto IL_0042;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		int32_t L_9 = __this->____size;
		int32_t L_10 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_5, ((int32_t)il2cpp_codegen_add(L_6, 1)), (RuntimeArray*)L_7, L_8, ((int32_t)il2cpp_codegen_subtract(L_9, L_10)), NULL);
	}

IL_0042:
	{
		goto IL_0063;
	}

IL_0063:
	{
		int32_t L_11 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_11, 1));
		return;
	}
}
// Method Definition Index: 11395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveRange_m901A1875B243576B5C651D06E6D5FB4CEDC61D58_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		int32_t L_6 = __this->____size;
		int32_t L_7 = __this->____size;
		int32_t L_8 = ___1_count;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_7, L_8));
		int32_t L_9 = ___0_index;
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_9) >= ((int32_t)L_10)))
		{
			goto IL_0066;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_11 = __this->____items;
		int32_t L_12 = ___0_index;
		int32_t L_13 = ___1_count;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = __this->____size;
		int32_t L_17 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_11, ((int32_t)il2cpp_codegen_add(L_12, L_13)), (RuntimeArray*)L_14, L_15, ((int32_t)il2cpp_codegen_subtract(L_16, L_17)), NULL);
	}

IL_0066:
	{
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		goto IL_008d;
	}

IL_008d:
	{
		return;
	}
}
// Method Definition Index: 11396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m76D3652B68BD27CE887E4741AD59D6A394BF853F_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4754598204C7F7240B07126FE194433F71E3B48E_inline(__this, NULL);
		List_1_Reverse_mCECB54C98550E945F76D6829C58B380B3440046C(__this, 0, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42));
		return;
	}
}
// Method Definition Index: 11397
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mCECB54C98550E945F76D6829C58B380B3440046C_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0038;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		Array_Reverse_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_m78A79F57D3A6C8A3500EA86FBF8AA0FF85D0FFE8(L_6, L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43));
	}

IL_0038:
	{
		int32_t L_9 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_9, 1));
		return;
	}
}
// Method Definition Index: 11398
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m6B59C8D8090546984E1DA7B0C412F23A9494EBFE_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4754598204C7F7240B07126FE194433F71E3B48E_inline(__this, NULL);
		List_1_Sort_m8EBB3D347F09C9774F9A22C94ABD938660496D63(__this, 0, L_0, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m32F1366E38FD0F58EB3B2FFAA17C769A23945439_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4754598204C7F7240B07126FE194433F71E3B48E_inline(__this, NULL);
		RuntimeObject* L_1 = ___0_comparer;
		List_1_Sort_m8EBB3D347F09C9774F9A22C94ABD938660496D63(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m8EBB3D347F09C9774F9A22C94ABD938660496D63_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0039;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		RuntimeObject* L_9 = ___2_comparer;
		Array_Sort_Tis__Il2CppUInt32Enum_t030A7AC35A8339F4C1125239BA7E0DF352914770_mC3212D949B6A74EE599F5294E85C6CE6E1B706BF(L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45));
	}

IL_0039:
	{
		int32_t L_10 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_10, 1));
		return;
	}
}
// Method Definition Index: 11401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mF285E0546E615626CE6F5253D02566922D4F6A29_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, Comparison_1_t3C004EF64D01B2F9DC8BB66F28EE82E69221F131* ___0_comparison, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit);
	//<source_info:<no-source>:1>
	{
		Comparison_1_t3C004EF64D01B2F9DC8BB66F28EE82E69221F131* L_0 = ___0_comparison;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)34), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_1) <= ((int32_t)1)))
		{
			goto IL_0026;
		}
	}
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_2 = __this->____items;
		int32_t L_3 = __this->____size;
		Comparison_1_t3C004EF64D01B2F9DC8BB66F28EE82E69221F131* L_4 = ___0_comparison;
		CHECKED_LOCAL_INIT(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 47)),il2cpp_codegen_runtime_class_init_inline);
		ArraySortHelper_1_Sort_m2BEA0432A46856F824268DAD47F6D6F0BAFB1D27(L_2, 0, L_3, L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46));
	}

IL_0026:
	{
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11402
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR __Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* List_1_ToArray_mF5840868D5F586C07FF76D75AB40D1375E83E2E0_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* V_0 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_1 = ((List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_3 = (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)(__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_2);
		V_0 = L_3;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_4 = __this->____items;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_5 = V_0;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_4, 0, (RuntimeArray*)L_5, 0, L_6, NULL);
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 11403
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_TrimExcess_mD80131C8C8789DF2A7B8850355E2A229F0E312F8_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		double L_2 = (il2cpp_codegen_conv<double,int32_t,int32_t,false,false>(L_1,NULL));
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,double,double,false,false>(((double)il2cpp_codegen_multiply(L_2, (0.90000000000000002))),NULL));
		V_0 = L_3;
		int32_t L_4 = __this->____size;
		int32_t L_5 = V_0;
		if ((((int32_t)L_4) >= ((int32_t)L_5)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = __this->____size;
		List_1_set_Capacity_mCA75F83CA2F20BC02FFECCA387D4C021A2D83511(__this, L_6, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11404
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m681B8A3C4E9973D4BBAAC17D1EF146A796DF11CB_gshared (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	uint32_t V_1 = 0;
	int32_t V_2 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		RuntimeObject* L_1 = ___0_enumerable;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_1);
		V_0 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0063:
			{
				{
					RuntimeObject* L_3 = V_0;
					if (!L_3)
					{
						goto IL_006c;
					}
				}
				{
					RuntimeObject* L_4 = V_0;
					NullCheck((RuntimeObject*)L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_4);
				}

IL_006c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0059_1;
			}

IL_0017_1:
			{
				RuntimeObject* L_5 = V_0;
				NullCheck(L_5);
				uint32_t L_6;
				L_6 = InterfaceFuncInvoker0< uint32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_5);
				V_1 = L_6;
				int32_t L_7 = __this->____size;
				__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_8 = __this->____items;
				NullCheck(L_8);
				int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
				if ((!(((uint32_t)L_7) == ((uint32_t)L_9))))
				{
					goto IL_003c_1;
				}
			}
			{
				int32_t L_10 = __this->____size;
				List_1_EnsureCapacity_m832EAA18FAB897AE9CE65E99ED96EF173701E507(__this, ((int32_t)il2cpp_codegen_add(L_10, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
			}

IL_003c_1:
			{
				__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_11 = __this->____items;
				int32_t L_12 = __this->____size;
				V_2 = L_12;
				int32_t L_13 = V_2;
				__this->____size = ((int32_t)il2cpp_codegen_add(L_13, 1));
				int32_t L_14 = V_2;
				uint32_t L_15 = V_1;
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_14), (uint32_t)L_15);
			}

IL_0059_1:
			{
				RuntimeObject* L_16 = V_0;
				NullCheck((RuntimeObject*)L_16);
				bool L_17;
				L_17 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_16);
				if (L_17)
				{
					goto IL_0017_1;
				}
			}
			{
				goto IL_006d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006d:
	{
		return;
	}
}
// Method Definition Index: 11405
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__cctor_m23E0CDEB6625FB2125CE81964A99D2C3D333AC92_gshared (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_0 = (__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)(__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (uint32_t)0);
		((List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray), (void*)L_0);
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
// Method Definition Index: 11344
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m5EEE8FB67B19E529FED48CD98395DABAADEAA15D (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_0 = ((List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_0);
		return;
	}
}
// Method Definition Index: 11345
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m69268FF8A42D99EBC95B85073ABCA95F734BDDAF (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_capacity, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___0_capacity;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)12), 4, NULL);
	}

IL_0012:
	{
		int32_t L_1 = ___0_capacity;
		if (L_1)
		{
			goto IL_0021;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_2 = ((List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_2);
		return;
	}

IL_0021:
	{
		int32_t L_3 = ___0_capacity;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_4 = (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)(float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_3);
		__this->____items = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_4);
		return;
	}
}
// Method Definition Index: 11346
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m8FD81C227762E6BAA880D08A0A154333D0BE8E87 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_000f:
	{
		RuntimeObject* L_1 = ___0_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0050;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_3);
		V_1 = L_4;
		int32_t L_5 = V_1;
		if (L_5)
		{
			goto IL_002f;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_6 = ((List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_6);
		return;
	}

IL_002f:
	{
		int32_t L_7 = V_1;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_8 = (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)(float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_7);
		__this->____items = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_8);
		RuntimeObject* L_9 = V_0;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_10 = __this->____items;
		NullCheck(L_9);
		InterfaceActionInvoker2< float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_9, L_10, 0);
		int32_t L_11 = V_1;
		__this->____size = L_11;
		return;
	}

IL_0050:
	{
		__this->____size = 0;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_12 = ((List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		RuntimeObject* L_13 = ___0_collection;
		List_1_AddEnumerable_mECF88FA26E05A64BB84A50E9AA2C455DC087884D(__this, L_13, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
		return;
	}
}
// Method Definition Index: 11347
// Method Definition Index: 11348
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mF9D4C2FC4283D46C7FF1285A449409DBB5B41FA9 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* V_0 = NULL;
	{
		int32_t L_0 = ___0_value;
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)15), ((int32_t)21), NULL);
	}

IL_0012:
	{
		int32_t L_2 = ___0_value;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_2) == ((int32_t)L_4)))
		{
			goto IL_0058;
		}
	}
	{
		int32_t L_5 = ___0_value;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_004d;
		}
	}
	{
		int32_t L_6 = ___0_value;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_7 = (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)(float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_6);
		V_0 = L_7;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0045;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_9 = __this->____items;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_10 = V_0;
		int32_t L_11 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_9, 0, (RuntimeArray*)L_10, 0, L_11, NULL);
	}

IL_0045:
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_12 = V_0;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		return;
	}

IL_004d:
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_13 = ((List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_13);
	}

IL_0058:
	{
		return;
	}
}
// Method Definition Index: 11349
// Method Definition Index: 11350
// Method Definition Index: 11351
// Method Definition Index: 11352
// Method Definition Index: 11353
// Method Definition Index: 11354
// Method Definition Index: 11355
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float3_t7600B73F092B37F484B12910A5269F30C778D31E List_1_get_Item_mDDBEBB5C8EAEE9D5B5759E5F786052C3CBF2EF66 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		return L_5;
	}
}
// Method Definition Index: 11356
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_mC016D5FEB9C6627EE3CE089B5E760F2BED4DB8B2 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_4 = ___1_value;
		NullCheck(L_2);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(L_3), (float3_t7600B73F092B37F484B12910A5269F30C778D31E)L_4);
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11357
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m8D42F842C68EA7DFED04A55428F0537587793D9A (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	float3_t7600B73F092B37F484B12910A5269F30C778D31E V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6))))
		{
			goto IL_001f;
		}
	}
	{
		RuntimeObject* L_1 = ___0_value;
		if (L_1)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(float3_t7600B73F092B37F484B12910A5269F30C778D31E));
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_2 = V_0;
		return false;
	}

IL_001d:
	{
		return (bool)0;
	}

IL_001f:
	{
		return (bool)1;
	}
}
// Method Definition Index: 11358
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IList_get_Item_m11E112056CCBDFB29D1E51B835AED77FB8D4603C (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_1;
		L_1 = List_1_get_Item_mDDBEBB5C8EAEE9D5B5759E5F786052C3CBF2EF66(__this, L_0, NULL);
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_2 = L_1;
		RuntimeObject* L_3 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), &L_2);
		return L_3;
	}
}
// Method Definition Index: 11359
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_set_Item_m43E9FB0B69BCA2D366BB596DCC67AB48B8ADA129 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_value;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_mFF49D2E8E8EC50D00B3105009D68AC629F188E93(L_0, ((int32_t)15), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_value;
		List_1_set_Item_mC016D5FEB9C6627EE3CE089B5E760F2BED4DB8B2(__this, L_1, ((*(float3_t7600B73F092B37F484B12910A5269F30C778D31E*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), NULL);
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_value;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11360
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Add_m641521748E28764A6131ED25D555ABC4FF9793D6 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_7 = V_0;
		int32_t L_8 = V_1;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (float3_t7600B73F092B37F484B12910A5269F30C778D31E)L_9);
		return;
	}

IL_0034:
	{
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_10 = ___0_item;
		List_1_AddWithResize_m5228B46F845892F0F239CC9D7314DF4BA90F6DDE(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 11361
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m5228B46F845892F0F239CC9D7314DF4BA90F6DDE (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		int32_t L_1 = V_0;
		List_1_EnsureCapacity_m2506538AE52D102AA80F184BC1DC5B422EC80DB6(__this, ((int32_t)il2cpp_codegen_add(L_1, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_2 = V_0;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_2, 1));
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_3 = __this->____items;
		int32_t L_4 = V_0;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_5 = ___0_item;
		NullCheck(L_3);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(L_4), (float3_t7600B73F092B37F484B12910A5269F30C778D31E)L_5);
		return;
	}
}
// Method Definition Index: 11362
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_Add_m623368F551DA8B55A11F2F878B0F1CBCA6A3464F (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___0_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_mFF49D2E8E8EC50D00B3105009D68AC629F188E93(L_0, ((int32_t)20), NULL);
	}
	try
	{
		RuntimeObject* L_1 = ___0_item;
		List_1_Add_m641521748E28764A6131ED25D555ABC4FF9793D6_inline(__this, ((*(float3_t7600B73F092B37F484B12910A5269F30C778D31E*)UnBox(L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		goto IL_0029;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0016;
		}
		throw e;
	}

CATCH_0016:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_2 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_3 = ___0_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_4 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_5;
		L_5 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_4, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_3, L_5, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0029;
	}

IL_0029:
	{
		int32_t L_6;
		L_6 = List_1_get_Count_m4FFB175F8DAE5AB81144E07755B2AC56551F84E4_inline(__this, NULL);
		return ((int32_t)il2cpp_codegen_subtract(L_6, 1));
	}
}
// Method Definition Index: 11363
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_mF5D79EDFF323E0EC4728678CA6C87C828289D1BA (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		RuntimeObject* L_1 = ___0_collection;
		List_1_InsertRange_mDDB22D94E651C9E4469BFD73D4AE7CA67E3FC3B4(__this, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return;
	}
}
// Method Definition Index: 11364
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_t1D742B1E1C22078FEF5E998EDD21A4405B13B076* List_1_AsReadOnly_m852B17D5AA2C2C7A3C227BCC1B16BD9B20581A04 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlyCollection_1_t1D742B1E1C22078FEF5E998EDD21A4405B13B076* L_0 = (ReadOnlyCollection_1_t1D742B1E1C22078FEF5E998EDD21A4405B13B076*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
		ReadOnlyCollection_1__ctor_m87B2FF05BB1173F36FEE8A953CB2851E0D63F92B(L_0, __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11365
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mC27F404E4D89E84B4DC061AF19AEE8DC4A3BCE18 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		int32_t L_7 = ___1_count;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_8 = ___2_item;
		RuntimeObject* L_9 = ___3_comparer;
		int32_t L_10;
		L_10 = Array_BinarySearch_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_mD97B04DABC7BA254E85C92D31872920799C700D4(L_5, L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18));
		return L_10;
	}
}
// Method Definition Index: 11366
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m8661E0FA482CD7316E199C7E8D5958C2C4494251 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4FFB175F8DAE5AB81144E07755B2AC56551F84E4_inline(__this, NULL);
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_BinarySearch_mC27F404E4D89E84B4DC061AF19AEE8DC4A3BCE18(__this, 0, L_0, L_1, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_2;
	}
}
// Method Definition Index: 11367
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mF1483A7F394EFEF6C7F74F1D024369EE6B4A441E (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, RuntimeObject* ___1_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4FFB175F8DAE5AB81144E07755B2AC56551F84E4_inline(__this, NULL);
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_1 = ___0_item;
		RuntimeObject* L_2 = ___1_comparer;
		int32_t L_3;
		L_3 = List_1_BinarySearch_mC27F404E4D89E84B4DC061AF19AEE8DC4A3BCE18(__this, 0, L_0, L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_3;
	}
}
// Method Definition Index: 11368
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Clear_m6C8D51A9406D6ED9B43B6F126A83EF286FB5DFB3 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		goto IL_0035;
	}

IL_0035:
	{
		__this->____size = 0;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11369
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_mA083F28DF5E7AB4AB25A1D0ABEB45C999B38DE50 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_IndexOf_m2F716DFC3073A2A7136124B4AB10DBD2D931454C(__this, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return (bool)((((int32_t)((((int32_t)L_2) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_0016:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11370
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_Contains_m98A17E4C8834C81D2B6387922BD8A81A0783D0FF (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m8D42F842C68EA7DFED04A55428F0537587793D9A(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Contains_mA083F28DF5E7AB4AB25A1D0ABEB45C999B38DE50(__this, ((*(float3_t7600B73F092B37F484B12910A5269F30C778D31E*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11371
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_m058AAC4FD1CA068614AC5157EF3BD9619D75AB0E (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_0 = ___0_array;
		List_1_CopyTo_m65718278D38F07F4FAE62C2B51BAA26421813DE8(__this, L_0, 0, NULL);
		return;
	}
}
// Method Definition Index: 11372
// Method Definition Index: 11373
// Method Definition Index: 11374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_m2506538AE52D102AA80F184BC1DC5B422EC80DB6 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_min, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t G_B4_0 = 0;
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int32_t L_2 = ___0_min;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003d;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_3 = __this->____items;
		NullCheck(L_3);
		if (!(((RuntimeArray*)L_3)->max_length))
		{
			goto IL_0020;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_4 = __this->____items;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		G_B4_0 = ((int32_t)il2cpp_codegen_multiply(L_5, 2));
		goto IL_0021;
	}

IL_0020:
	{
		G_B4_0 = 4;
	}

IL_0021:
	{
		V_0 = G_B4_0;
		int32_t L_6 = V_0;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)2146435071)))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = ((int32_t)2146435071);
	}

IL_0030:
	{
		int32_t L_7 = V_0;
		int32_t L_8 = ___0_min;
		if ((((int32_t)L_7) >= ((int32_t)L_8)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_9 = ___0_min;
		V_0 = L_9;
	}

IL_0036:
	{
		int32_t L_10 = V_0;
		List_1_set_Capacity_mF9D4C2FC4283D46C7FF1285A449409DBB5B41FA9(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_003d:
	{
		return;
	}
}
// Method Definition Index: 11375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_mF1C22092719FED3C500F2C0656F8A45D9C57FBE5 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_0 = ___0_match;
		int32_t L_1;
		L_1 = List_1_FindIndex_m290A0FB66360A5FB10E7DC5DA524278A1E4AFF4C(__this, L_0, NULL);
		return (bool)((((int32_t)((((int32_t)L_1) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 11376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float3_t7600B73F092B37F484B12910A5269F30C778D31E List_1_Find_mF817C5D8ED0C6722889855BD3B0FA5F536DCF19B (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0032;
	}

IL_000d:
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_1 = ___0_match;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_2 = __this->____items;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		NullCheck(L_1);
		bool L_6;
		L_6 = Predicate_1_Invoke_mBED251F2E28E4422575173E72B868C1A24BD5285_inline(L_1, L_5, NULL);
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_7 = __this->____items;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		return L_10;
	}

IL_002e:
	{
		int32_t L_11 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0032:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____size;
		if ((((int32_t)L_12) < ((int32_t)L_13)))
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_1), sizeof(float3_t7600B73F092B37F484B12910A5269F30C778D31E));
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_14 = V_1;
		return L_14;
	}
}
// Method Definition Index: 11377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m290A0FB66360A5FB10E7DC5DA524278A1E4AFF4C (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_1 = ___0_match;
		int32_t L_2;
		L_2 = List_1_FindIndex_m1D1E66E62875F1DEDA7A916BDF27434658D63A30(__this, 0, L_0, L_1, NULL);
		return L_2;
	}
}
// Method Definition Index: 11378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m1D1E66E62875F1DEDA7A916BDF27434658D63A30 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_startIndex;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_000e:
	{
		int32_t L_2 = ___1_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_3) <= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_4, L_5)))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_0022:
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_6 = ___2_match;
		if (L_6)
		{
			goto IL_002b;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_002b:
	{
		int32_t L_7 = ___0_startIndex;
		int32_t L_8 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, L_8));
		int32_t L_9 = ___0_startIndex;
		V_1 = L_9;
		goto IL_004d;
	}

IL_0033:
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_10 = ___2_match;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_11 = __this->____items;
		int32_t L_12 = V_1;
		NullCheck(L_11);
		int32_t L_13 = L_12;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_14 = (L_11)->GetAt(static_cast<il2cpp_array_size_t>(L_13));
		NullCheck(L_10);
		bool L_15;
		L_15 = Predicate_1_Invoke_mBED251F2E28E4422575173E72B868C1A24BD5285_inline(L_10, L_14, NULL);
		if (!L_15)
		{
			goto IL_0049;
		}
	}
	{
		int32_t L_16 = V_1;
		return L_16;
	}

IL_0049:
	{
		int32_t L_17 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_004d:
	{
		int32_t L_18 = V_1;
		int32_t L_19 = V_0;
		if ((((int32_t)L_18) < ((int32_t)L_19)))
		{
			goto IL_0033;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m09EDF64BAC43C8044DC4E6951BC52AFFF15758BE (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		int32_t L_1 = __this->____size;
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_2 = ___0_match;
		int32_t L_3;
		L_3 = List_1_FindLastIndex_m9365C3DFAC7F3B17896AD6C63197369902F4934E(__this, ((int32_t)il2cpp_codegen_subtract(L_0, 1)), L_1, L_2, NULL);
		return L_3;
	}
}
// Method Definition Index: 11380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m9365C3DFAC7F3B17896AD6C63197369902F4934E (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_0 = ___2_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		int32_t L_1 = __this->____size;
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = ___0_startIndex;
		if ((((int32_t)L_2) == ((int32_t)(-1))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
		goto IL_002a;
	}

IL_001c:
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		if ((!(((uint32_t)L_3) >= ((uint32_t)L_4))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_002a:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) < ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_6 = ___0_startIndex;
		int32_t L_7 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_6, L_7)), 1))) >= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}

IL_0036:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_003b:
	{
		int32_t L_8 = ___0_startIndex;
		int32_t L_9 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		int32_t L_10 = ___0_startIndex;
		V_1 = L_10;
		goto IL_005d;
	}

IL_0043:
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_11 = ___2_match;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_12 = __this->____items;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		NullCheck(L_11);
		bool L_16;
		L_16 = Predicate_1_Invoke_mBED251F2E28E4422575173E72B868C1A24BD5285_inline(L_11, L_15, NULL);
		if (!L_16)
		{
			goto IL_0059;
		}
	}
	{
		int32_t L_17 = V_1;
		return L_17;
	}

IL_0059:
	{
		int32_t L_18 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_18, 1));
	}

IL_005d:
	{
		int32_t L_19 = V_1;
		int32_t L_20 = V_0;
		if ((((int32_t)L_19) > ((int32_t)L_20)))
		{
			goto IL_0043;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_ForEach_mE160A04FD8D2DE55D380BBF4D98CB56D391916FF (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, Action_1_t9BCEE3E1405DA67DBE5A4E9F0882507B63BCEC2F* ___0_action, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Action_1_t9BCEE3E1405DA67DBE5A4E9F0882507B63BCEC2F* L_0 = ___0_action;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)33), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____version;
		V_0 = L_1;
		V_1 = 0;
		goto IL_0034;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____version;
		if ((!(((uint32_t)L_2) == ((uint32_t)L_3))))
		{
			goto IL_003d;
		}
	}
	{
		Action_1_t9BCEE3E1405DA67DBE5A4E9F0882507B63BCEC2F* L_4 = ___0_action;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_5 = __this->____items;
		int32_t L_6 = V_1;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		Action_1_Invoke_mDD4A1B1BE4B3607CBA93CC29D3C64CB95A6143CB_inline(L_4, L_8, NULL);
		int32_t L_9 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0034:
	{
		int32_t L_10 = V_1;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0015;
		}
	}

IL_003d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____version;
		if ((((int32_t)L_12) == ((int32_t)L_13)))
		{
			goto IL_004b;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_004b:
	{
		return;
	}
}
// Method Definition Index: 11382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t92A9B1D618B36DF47EC28CBA0FFF9405BE75926E List_1_GetEnumerator_m5AF53422A51F6DABA68F962882D75BD5A741701F (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t92A9B1D618B36DF47EC28CBA0FFF9405BE75926E L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_mD3CA065165D2EC0A9723D510ACA9142DBE5D1BBA((&L_0), __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_Generic_IEnumerableU3CTU3E_GetEnumerator_mD7DE8C1F9159AFCB0B038EB71CADCC2DB06DE7B3 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t92A9B1D618B36DF47EC28CBA0FFF9405BE75926E L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_mD3CA065165D2EC0A9723D510ACA9142DBE5D1BBA((&L_0), __this, NULL);
		Enumerator_t92A9B1D618B36DF47EC28CBA0FFF9405BE75926E L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IEnumerable_GetEnumerator_mCEF270070CF0EE23CF87A164B103512238AC933C (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t92A9B1D618B36DF47EC28CBA0FFF9405BE75926E L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_mD3CA065165D2EC0A9723D510ACA9142DBE5D1BBA((&L_0), __this, NULL);
		Enumerator_t92A9B1D618B36DF47EC28CBA0FFF9405BE75926E L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* List_1_GetRange_m4D61C38DCDAA89B0D93375A00A06C6F44A9BFE82 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* L_6 = (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		List_1__ctor_m69268FF8A42D99EBC95B85073ABCA95F734BDDAF(L_6, L_5, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33));
		V_0 = L_6;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* L_9 = V_0;
		NullCheck(L_9);
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_10 = L_9->____items;
		int32_t L_11 = ___1_count;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_7, L_8, (RuntimeArray*)L_10, 0, L_11, NULL);
		List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* L_12 = V_0;
		int32_t L_13 = ___1_count;
		NullCheck(L_12);
		L_12->____size = L_13;
		List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* L_14 = V_0;
		return L_14;
	}
}
// Method Definition Index: 11386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_m2F716DFC3073A2A7136124B4AB10DBD2D931454C (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_0 = __this->____items;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_1 = ___0_item;
		int32_t L_2 = __this->____size;
		int32_t L_3;
		L_3 = Array_IndexOf_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_m913059E880BC34EDEE917201B440B2E9CEE1A583(L_0, L_1, 0, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34));
		return L_3;
	}
}
// Method Definition Index: 11387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_IndexOf_mEDF62E55BF2F79092FDA5EDEAF4CB01DE75F5EFB (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m8D42F842C68EA7DFED04A55428F0537587793D9A(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		int32_t L_3;
		L_3 = List_1_IndexOf_m2F716DFC3073A2A7136124B4AB10DBD2D931454C(__this, ((*(float3_t7600B73F092B37F484B12910A5269F30C778D31E*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mDD9B88B581FB24BC9AF2FACB395FE66CE045509F (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___1_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)13), ((int32_t)27), NULL);
	}

IL_0012:
	{
		int32_t L_2 = __this->____size;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0030;
		}
	}
	{
		int32_t L_5 = __this->____size;
		List_1_EnsureCapacity_m2506538AE52D102AA80F184BC1DC5B422EC80DB6(__this, ((int32_t)il2cpp_codegen_add(L_5, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
	}

IL_0030:
	{
		int32_t L_6 = ___0_index;
		int32_t L_7 = __this->____size;
		if ((((int32_t)L_6) >= ((int32_t)L_7)))
		{
			goto IL_0056;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_8 = __this->____items;
		int32_t L_9 = ___0_index;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_10 = __this->____items;
		int32_t L_11 = ___0_index;
		int32_t L_12 = __this->____size;
		int32_t L_13 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_8, L_9, (RuntimeArray*)L_10, ((int32_t)il2cpp_codegen_add(L_11, 1)), ((int32_t)il2cpp_codegen_subtract(L_12, L_13)), NULL);
	}

IL_0056:
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_16 = ___1_item;
		NullCheck(L_14);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(L_15), (float3_t7600B73F092B37F484B12910A5269F30C778D31E)L_16);
		int32_t L_17 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_17, 1));
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		return;
	}
}
// Method Definition Index: 11389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Insert_mF571DCC44B969EA77EFF14DA905E3E6D7817750F (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, RuntimeObject* ___1_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_mFF49D2E8E8EC50D00B3105009D68AC629F188E93(L_0, ((int32_t)20), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_item;
		List_1_Insert_mDD9B88B581FB24BC9AF2FACB395FE66CE045509F(__this, L_1, ((*(float3_t7600B73F092B37F484B12910A5269F30C778D31E*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_mDDB22D94E651C9E4469BFD73D4AE7CA67E3FC3B4 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	RuntimeObject* V_2 = NULL;
	{
		RuntimeObject* L_0 = ___1_collection;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___0_index;
		int32_t L_2 = __this->____size;
		if ((!(((uint32_t)L_1) > ((uint32_t)L_2))))
		{
			goto IL_0017;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ___1_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_4 = V_0;
		if (!L_4)
		{
			goto IL_00bc;
		}
	}
	{
		RuntimeObject* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_5);
		V_1 = L_6;
		int32_t L_7 = V_1;
		if ((((int32_t)L_7) <= ((int32_t)0)))
		{
			goto IL_00fb;
		}
	}
	{
		int32_t L_8 = __this->____size;
		int32_t L_9 = V_1;
		List_1_EnsureCapacity_m2506538AE52D102AA80F184BC1DC5B422EC80DB6(__this, ((int32_t)il2cpp_codegen_add(L_8, L_9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_10 = ___0_index;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) >= ((int32_t)L_11)))
		{
			goto IL_0066;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_12 = __this->____items;
		int32_t L_13 = ___0_index;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = V_1;
		int32_t L_17 = __this->____size;
		int32_t L_18 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_12, L_13, (RuntimeArray*)L_14, ((int32_t)il2cpp_codegen_add(L_15, L_16)), ((int32_t)il2cpp_codegen_subtract(L_17, L_18)), NULL);
	}

IL_0066:
	{
		RuntimeObject* L_19 = V_0;
		if ((!(((RuntimeObject*)(List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6*)__this) == ((RuntimeObject*)(RuntimeObject*)L_19))))
		{
			goto IL_009f;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_20 = __this->____items;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_21 = __this->____items;
		int32_t L_22 = ___0_index;
		int32_t L_23 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_20, 0, (RuntimeArray*)L_21, L_22, L_23, NULL);
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_24 = __this->____items;
		int32_t L_25 = ___0_index;
		int32_t L_26 = V_1;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_27 = __this->____items;
		int32_t L_28 = ___0_index;
		int32_t L_29 = __this->____size;
		int32_t L_30 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_24, ((int32_t)il2cpp_codegen_add(L_25, L_26)), (RuntimeArray*)L_27, ((int32_t)il2cpp_codegen_multiply(L_28, 2)), ((int32_t)il2cpp_codegen_subtract(L_29, L_30)), NULL);
		goto IL_00ac;
	}

IL_009f:
	{
		RuntimeObject* L_31 = V_0;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_32 = __this->____items;
		int32_t L_33 = ___0_index;
		NullCheck(L_31);
		InterfaceActionInvoker2< float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_31, L_32, L_33);
	}

IL_00ac:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_34, L_35));
		goto IL_00fb;
	}

IL_00bc:
	{
		int32_t L_36 = ___0_index;
		int32_t L_37 = __this->____size;
		if ((((int32_t)L_36) >= ((int32_t)L_37)))
		{
			goto IL_00f4;
		}
	}
	{
		RuntimeObject* L_38 = ___1_collection;
		NullCheck(L_38);
		RuntimeObject* L_39;
		L_39 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_38);
		V_2 = L_39;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ea:
			{
				{
					RuntimeObject* L_40 = V_2;
					if (!L_40)
					{
						goto IL_00f3;
					}
				}
				{
					RuntimeObject* L_41 = V_2;
					NullCheck(L_41);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_41);
				}

IL_00f3:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00e0_1;
			}

IL_00ce_1:
			{
				int32_t L_42 = ___0_index;
				int32_t L_43 = L_42;
				___0_index = ((int32_t)il2cpp_codegen_add(L_43, 1));
				RuntimeObject* L_44 = V_2;
				NullCheck(L_44);
				float3_t7600B73F092B37F484B12910A5269F30C778D31E L_45;
				L_45 = InterfaceFuncInvoker0< float3_t7600B73F092B37F484B12910A5269F30C778D31E >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_44);
				List_1_Insert_mDD9B88B581FB24BC9AF2FACB395FE66CE045509F(__this, L_43, L_45, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
			}

IL_00e0_1:
			{
				RuntimeObject* L_46 = V_2;
				NullCheck(L_46);
				bool L_47;
				L_47 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_46);
				if (L_47)
				{
					goto IL_00ce_1;
				}
			}
			{
				goto IL_00fb;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f4:
	{
		RuntimeObject* L_48 = ___1_collection;
		List_1_AddEnumerable_mECF88FA26E05A64BB84A50E9AA2C455DC087884D(__this, L_48, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_00fb:
	{
		int32_t L_49 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_49, 1));
		return;
	}
}
// Method Definition Index: 11391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m8FDE8B0AF2E22424391F5B414677395A7FE5D325 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_0 = ___0_item;
		int32_t L_1;
		L_1 = List_1_IndexOf_m2F716DFC3073A2A7136124B4AB10DBD2D931454C(__this, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		int32_t L_3 = V_0;
		List_1_RemoveAt_m8B4F3036EA9C5AD476DA0B6BA7E5BC3F4CAAB0C8(__this, L_3, NULL);
		return (bool)1;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Remove_mB14A35AE2589180D28A87FE355D2812B6A51230E (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m8D42F842C68EA7DFED04A55428F0537587793D9A(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Remove_m8FDE8B0AF2E22424391F5B414677395A7FE5D325(__this, ((*(float3_t7600B73F092B37F484B12910A5269F30C778D31E*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41));
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 11393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_RemoveAll_mC0D143FB8909E47C0DF65BF108E56BFBB566116A (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0011;
	}

IL_000d:
	{
		int32_t L_1 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_1, 1));
	}

IL_0011:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____size;
		if ((((int32_t)L_2) >= ((int32_t)L_3)))
		{
			goto IL_002e;
		}
	}
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_4 = ___0_match;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_5 = __this->____items;
		int32_t L_6 = V_0;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		bool L_9;
		L_9 = Predicate_1_Invoke_mBED251F2E28E4422575173E72B868C1A24BD5285_inline(L_4, L_8, NULL);
		if (!L_9)
		{
			goto IL_000d;
		}
	}

IL_002e:
	{
		int32_t L_10 = V_0;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0039;
		}
	}
	{
		return 0;
	}

IL_0039:
	{
		int32_t L_12 = V_0;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
		goto IL_0089;
	}

IL_003f:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_0043:
	{
		int32_t L_14 = V_1;
		int32_t L_15 = __this->____size;
		if ((((int32_t)L_14) >= ((int32_t)L_15)))
		{
			goto IL_0060;
		}
	}
	{
		Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* L_16 = ___0_match;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_17 = __this->____items;
		int32_t L_18 = V_1;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_20 = (L_17)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_16);
		bool L_21;
		L_21 = Predicate_1_Invoke_mBED251F2E28E4422575173E72B868C1A24BD5285_inline(L_16, L_20, NULL);
		if (L_21)
		{
			goto IL_003f;
		}
	}

IL_0060:
	{
		int32_t L_22 = V_1;
		int32_t L_23 = __this->____size;
		if ((((int32_t)L_22) >= ((int32_t)L_23)))
		{
			goto IL_0089;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_24 = __this->____items;
		int32_t L_25 = V_0;
		int32_t L_26 = L_25;
		V_0 = ((int32_t)il2cpp_codegen_add(L_26, 1));
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_27 = __this->____items;
		int32_t L_28 = V_1;
		int32_t L_29 = L_28;
		V_1 = ((int32_t)il2cpp_codegen_add(L_29, 1));
		NullCheck(L_27);
		int32_t L_30 = L_29;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_31 = (L_27)->GetAt(static_cast<il2cpp_array_size_t>(L_30));
		NullCheck(L_24);
		(L_24)->SetAt(static_cast<il2cpp_array_size_t>(L_26), (float3_t7600B73F092B37F484B12910A5269F30C778D31E)L_31);
	}

IL_0089:
	{
		int32_t L_32 = V_1;
		int32_t L_33 = __this->____size;
		if ((((int32_t)L_32) < ((int32_t)L_33)))
		{
			goto IL_0043;
		}
	}
	{
		goto IL_00ad;
	}

IL_00ad:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_0;
		int32_t L_36 = V_0;
		__this->____size = L_36;
		int32_t L_37 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_37, 1));
		return ((int32_t)il2cpp_codegen_subtract(L_34, L_35));
	}
}
// Method Definition Index: 11394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m8B4F3036EA9C5AD476DA0B6BA7E5BC3F4CAAB0C8 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	float3_t7600B73F092B37F484B12910A5269F30C778D31E V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		int32_t L_3 = ___0_index;
		int32_t L_4 = __this->____size;
		if ((((int32_t)L_3) >= ((int32_t)L_4)))
		{
			goto IL_0042;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		int32_t L_9 = __this->____size;
		int32_t L_10 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_5, ((int32_t)il2cpp_codegen_add(L_6, 1)), (RuntimeArray*)L_7, L_8, ((int32_t)il2cpp_codegen_subtract(L_9, L_10)), NULL);
	}

IL_0042:
	{
		goto IL_0063;
	}

IL_0063:
	{
		int32_t L_11 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_11, 1));
		return;
	}
}
// Method Definition Index: 11395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveRange_m1F175D4ED7CD438EC6438FC83FAEB97498743DE1 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		int32_t L_6 = __this->____size;
		int32_t L_7 = __this->____size;
		int32_t L_8 = ___1_count;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_7, L_8));
		int32_t L_9 = ___0_index;
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_9) >= ((int32_t)L_10)))
		{
			goto IL_0066;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_11 = __this->____items;
		int32_t L_12 = ___0_index;
		int32_t L_13 = ___1_count;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = __this->____size;
		int32_t L_17 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_11, ((int32_t)il2cpp_codegen_add(L_12, L_13)), (RuntimeArray*)L_14, L_15, ((int32_t)il2cpp_codegen_subtract(L_16, L_17)), NULL);
	}

IL_0066:
	{
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		goto IL_008d;
	}

IL_008d:
	{
		return;
	}
}
// Method Definition Index: 11396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mAC1D671AAE2E3A1B7E13C49097878430C27BE07A (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4FFB175F8DAE5AB81144E07755B2AC56551F84E4_inline(__this, NULL);
		List_1_Reverse_m99D3C8B4AFB4E600AA8550AEBE0ED123B80E72E2(__this, 0, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42));
		return;
	}
}
// Method Definition Index: 11397
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m99D3C8B4AFB4E600AA8550AEBE0ED123B80E72E2 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0038;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		Array_Reverse_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_m86B3F9BB19458E58DBDEC044CF6D4F6518F6E609(L_6, L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43));
	}

IL_0038:
	{
		int32_t L_9 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_9, 1));
		return;
	}
}
// Method Definition Index: 11398
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m60E481F466490737D414B47E4D15F6AE6F95F0C9 (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4FFB175F8DAE5AB81144E07755B2AC56551F84E4_inline(__this, NULL);
		List_1_Sort_m8E0156C9CAA092C3F9810489E410391A52F8ED8A(__this, 0, L_0, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m0F1B45664CABE5196F18AD1107D9137EB105557A (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m4FFB175F8DAE5AB81144E07755B2AC56551F84E4_inline(__this, NULL);
		RuntimeObject* L_1 = ___0_comparer;
		List_1_Sort_m8E0156C9CAA092C3F9810489E410391A52F8ED8A(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m8E0156C9CAA092C3F9810489E410391A52F8ED8A (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0039;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		RuntimeObject* L_9 = ___2_comparer;
		Array_Sort_Tisfloat3_t7600B73F092B37F484B12910A5269F30C778D31E_m3C148D0E6D87B13570375D359B48243DC7753C71(L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45));
	}

IL_0039:
	{
		int32_t L_10 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_10, 1));
		return;
	}
}
// Method Definition Index: 11401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m102565577FC55F22359B4601C93B0DE443AD959A (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, Comparison_1_tEB713FAFEA92D337C218C257736265B7A7EDF135* ___0_comparison, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit);
	//<source_info:<no-source>:1>
	{
		Comparison_1_tEB713FAFEA92D337C218C257736265B7A7EDF135* L_0 = ___0_comparison;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)34), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_1) <= ((int32_t)1)))
		{
			goto IL_0026;
		}
	}
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_2 = __this->____items;
		int32_t L_3 = __this->____size;
		Comparison_1_tEB713FAFEA92D337C218C257736265B7A7EDF135* L_4 = ___0_comparison;
		CHECKED_LOCAL_INIT(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 47)),il2cpp_codegen_runtime_class_init_inline);
		ArraySortHelper_1_Sort_m28F4596182D9E9F98BEB3727027FFCEDC1D1CE8D(L_2, 0, L_3, L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46));
	}

IL_0026:
	{
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11402
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* List_1_ToArray_m5087190A831F5E966FB697FF4BEE5A85C2A8971F (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* V_0 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_1 = ((List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_3 = (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)(float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_2);
		V_0 = L_3;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_4 = __this->____items;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_5 = V_0;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_4, 0, (RuntimeArray*)L_5, 0, L_6, NULL);
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 11403
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_TrimExcess_m53FD191A91A54761FA5E5E8C058CE8D663ED6BBF (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		double L_2 = (il2cpp_codegen_conv<double,int32_t,int32_t,false,false>(L_1,NULL));
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,double,double,false,false>(((double)il2cpp_codegen_multiply(L_2, (0.90000000000000002))),NULL));
		V_0 = L_3;
		int32_t L_4 = __this->____size;
		int32_t L_5 = V_0;
		if ((((int32_t)L_4) >= ((int32_t)L_5)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = __this->____size;
		List_1_set_Capacity_mF9D4C2FC4283D46C7FF1285A449409DBB5B41FA9(__this, L_6, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11404
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mECF88FA26E05A64BB84A50E9AA2C455DC087884D (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		RuntimeObject* L_1 = ___0_enumerable;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_1);
		V_0 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0063:
			{
				{
					RuntimeObject* L_3 = V_0;
					if (!L_3)
					{
						goto IL_006c;
					}
				}
				{
					RuntimeObject* L_4 = V_0;
					NullCheck(L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_4);
				}

IL_006c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0059_1;
			}

IL_0017_1:
			{
				RuntimeObject* L_5 = V_0;
				NullCheck(L_5);
				float3_t7600B73F092B37F484B12910A5269F30C778D31E L_6;
				L_6 = InterfaceFuncInvoker0< float3_t7600B73F092B37F484B12910A5269F30C778D31E >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_5);
				V_1 = L_6;
				int32_t L_7 = __this->____size;
				float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_8 = __this->____items;
				NullCheck(L_8);
				int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
				if ((!(((uint32_t)L_7) == ((uint32_t)L_9))))
				{
					goto IL_003c_1;
				}
			}
			{
				int32_t L_10 = __this->____size;
				List_1_EnsureCapacity_m2506538AE52D102AA80F184BC1DC5B422EC80DB6(__this, ((int32_t)il2cpp_codegen_add(L_10, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
			}

IL_003c_1:
			{
				float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_11 = __this->____items;
				int32_t L_12 = __this->____size;
				V_2 = L_12;
				int32_t L_13 = V_2;
				__this->____size = ((int32_t)il2cpp_codegen_add(L_13, 1));
				int32_t L_14 = V_2;
				float3_t7600B73F092B37F484B12910A5269F30C778D31E L_15 = V_1;
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_14), (float3_t7600B73F092B37F484B12910A5269F30C778D31E)L_15);
			}

IL_0059_1:
			{
				RuntimeObject* L_16 = V_0;
				NullCheck(L_16);
				bool L_17;
				L_17 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_16);
				if (L_17)
				{
					goto IL_0017_1;
				}
			}
			{
				goto IL_006d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006d:
	{
		return;
	}
}
// Method Definition Index: 11405
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__cctor_m79C5A2E3EF428587C42C29B373DC4D12B2A84E47 (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_0 = (float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)(float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (uint32_t)0);
		((List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray), (void*)L_0);
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
// Method Definition Index: 11344
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m5F8A99119A8A90D72E21C46C659D7CBD99EE8128 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_0 = ((List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_0);
		return;
	}
}
// Method Definition Index: 11345
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m463F6FD2D31F18F6BC2A559E4D51030B8E9A8C11 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_capacity, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___0_capacity;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)12), 4, NULL);
	}

IL_0012:
	{
		int32_t L_1 = ___0_capacity;
		if (L_1)
		{
			goto IL_0021;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_2 = ((List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_2);
		return;
	}

IL_0021:
	{
		int32_t L_3 = ___0_capacity;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_4 = (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)(WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_3);
		__this->____items = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_4);
		return;
	}
}
// Method Definition Index: 11346
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m03900B51957F192A4417C2DF5D8F6A41787A586E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_000f:
	{
		RuntimeObject* L_1 = ___0_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0050;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_3);
		V_1 = L_4;
		int32_t L_5 = V_1;
		if (L_5)
		{
			goto IL_002f;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_6 = ((List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_6);
		return;
	}

IL_002f:
	{
		int32_t L_7 = V_1;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_8 = (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)(WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_7);
		__this->____items = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_8);
		RuntimeObject* L_9 = V_0;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_10 = __this->____items;
		NullCheck(L_9);
		InterfaceActionInvoker2< WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_9, L_10, 0);
		int32_t L_11 = V_1;
		__this->____size = L_11;
		return;
	}

IL_0050:
	{
		__this->____size = 0;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_12 = ((List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		RuntimeObject* L_13 = ___0_collection;
		List_1_AddEnumerable_m0B91306C3AE3C541A215663CBC439245F8E3679B(__this, L_13, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
		return;
	}
}
// Method Definition Index: 11347
// Method Definition Index: 11348
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mA50F327494067599F9981040CBA1CD52DC8BF6D7 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* V_0 = NULL;
	{
		int32_t L_0 = ___0_value;
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)15), ((int32_t)21), NULL);
	}

IL_0012:
	{
		int32_t L_2 = ___0_value;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_2) == ((int32_t)L_4)))
		{
			goto IL_0058;
		}
	}
	{
		int32_t L_5 = ___0_value;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_004d;
		}
	}
	{
		int32_t L_6 = ___0_value;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_7 = (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)(WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_6);
		V_0 = L_7;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0045;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_9 = __this->____items;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_10 = V_0;
		int32_t L_11 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_9, 0, (RuntimeArray*)L_10, 0, L_11, NULL);
	}

IL_0045:
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_12 = V_0;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		return;
	}

IL_004d:
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_13 = ((List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_13);
	}

IL_0058:
	{
		return;
	}
}
// Method Definition Index: 11349
// Method Definition Index: 11350
// Method Definition Index: 11351
// Method Definition Index: 11352
// Method Definition Index: 11353
// Method Definition Index: 11354
// Method Definition Index: 11355
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 List_1_get_Item_m16E2D277594D536BEF0EF8463D9DA3C1322F9263 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		return L_5;
	}
}
// Method Definition Index: 11356
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m8F84A4BC4BFAE1C6B80CB63095C9093716AE3E87 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_4 = ___1_value;
		NullCheck(L_2);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(L_3), (WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0)L_4);
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11357
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m91492C9C383B83819368A542FF0C7E2DB817BF42 (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6))))
		{
			goto IL_001f;
		}
	}
	{
		RuntimeObject* L_1 = ___0_value;
		if (L_1)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0));
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_2 = V_0;
		return false;
	}

IL_001d:
	{
		return (bool)0;
	}

IL_001f:
	{
		return (bool)1;
	}
}
// Method Definition Index: 11358
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IList_get_Item_m54D69AA133AEF04DB88B77D5E1A0A8AA652FB18A (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_1;
		L_1 = List_1_get_Item_m16E2D277594D536BEF0EF8463D9DA3C1322F9263(__this, L_0, NULL);
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_2 = L_1;
		RuntimeObject* L_3 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), &L_2);
		return L_3;
	}
}
// Method Definition Index: 11359
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_set_Item_mD75ED91421F5089EC624A654AAF1D603727E11AC (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_value;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m0A8BE23260E6E169A1D247D8EAA0631905874EC8(L_0, ((int32_t)15), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_value;
		List_1_set_Item_m8F84A4BC4BFAE1C6B80CB63095C9093716AE3E87(__this, L_1, ((*(WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), NULL);
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_value;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11360
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Add_m184A228230F65332F56708CBA3F3A88489B94D66 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_7 = V_0;
		int32_t L_8 = V_1;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0)L_9);
		return;
	}

IL_0034:
	{
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_10 = ___0_item;
		List_1_AddWithResize_m843E590839D8B44E5BF14AB39E42CAE86E2AEC9E(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 11361
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m843E590839D8B44E5BF14AB39E42CAE86E2AEC9E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		int32_t L_1 = V_0;
		List_1_EnsureCapacity_mD12A0B04025BCC2368FD9F8FE2B5CF9DC1AC7432(__this, ((int32_t)il2cpp_codegen_add(L_1, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_2 = V_0;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_2, 1));
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_3 = __this->____items;
		int32_t L_4 = V_0;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_5 = ___0_item;
		NullCheck(L_3);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(L_4), (WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0)L_5);
		return;
	}
}
// Method Definition Index: 11362
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_Add_m3B075EF48ABEB85AA5A39BBAD4EBA7CA7B8F8B5E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___0_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m0A8BE23260E6E169A1D247D8EAA0631905874EC8(L_0, ((int32_t)20), NULL);
	}
	try
	{
		RuntimeObject* L_1 = ___0_item;
		List_1_Add_m184A228230F65332F56708CBA3F3A88489B94D66_inline(__this, ((*(WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0*)UnBox(L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		goto IL_0029;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0016;
		}
		throw e;
	}

CATCH_0016:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_2 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_3 = ___0_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_4 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_5;
		L_5 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_4, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_3, L_5, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0029;
	}

IL_0029:
	{
		int32_t L_6;
		L_6 = List_1_get_Count_mAA4BC36A86EF945C37561EF64337148E583C211E_inline(__this, NULL);
		return ((int32_t)il2cpp_codegen_subtract(L_6, 1));
	}
}
// Method Definition Index: 11363
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m37DBF860BBD1E4C9211DDFAACAB478CB97682791 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		RuntimeObject* L_1 = ___0_collection;
		List_1_InsertRange_mE806EE6B9753E133B6809CDA1E26C6A048B5375C(__this, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return;
	}
}
// Method Definition Index: 11364
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_tEFFE9324903FBAB1A9A712578488B71F4CBC3BF5* List_1_AsReadOnly_mE107C4569C5864CAA7E79A525A29B7943A648F89 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlyCollection_1_tEFFE9324903FBAB1A9A712578488B71F4CBC3BF5* L_0 = (ReadOnlyCollection_1_tEFFE9324903FBAB1A9A712578488B71F4CBC3BF5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
		ReadOnlyCollection_1__ctor_m7E446C3BE50D15E107750A0A02FB85536C539DD2(L_0, __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11365
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mCE539D0C6EA0432518D2558F38362D0CE53DCBF8 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		int32_t L_7 = ___1_count;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_8 = ___2_item;
		RuntimeObject* L_9 = ___3_comparer;
		int32_t L_10;
		L_10 = Array_BinarySearch_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_mF87C294E9FC4D8FF1307E2EB9EA87171DA824427(L_5, L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18));
		return L_10;
	}
}
// Method Definition Index: 11366
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m6BB12112B594F793D0D2378467DE177A944C6E3C (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_mAA4BC36A86EF945C37561EF64337148E583C211E_inline(__this, NULL);
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_BinarySearch_mCE539D0C6EA0432518D2558F38362D0CE53DCBF8(__this, 0, L_0, L_1, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_2;
	}
}
// Method Definition Index: 11367
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m113BA96B2505267BEDDBE14691C2CF2414F64B55 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, RuntimeObject* ___1_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_mAA4BC36A86EF945C37561EF64337148E583C211E_inline(__this, NULL);
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_1 = ___0_item;
		RuntimeObject* L_2 = ___1_comparer;
		int32_t L_3;
		L_3 = List_1_BinarySearch_mCE539D0C6EA0432518D2558F38362D0CE53DCBF8(__this, 0, L_0, L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_3;
	}
}
// Method Definition Index: 11368
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Clear_m5743CDCD22D812BC3C8605963B1B76C2F175CB4E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		goto IL_0035;
	}

IL_0035:
	{
		__this->____size = 0;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11369
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m88F444D9C0089E32B73E8F5067B8A8D7B4A4FE03 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_IndexOf_mE63B255347079C50A47093D77B4CFAEA05B61455(__this, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return (bool)((((int32_t)((((int32_t)L_2) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_0016:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11370
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_Contains_mE50940BDE2C07E0843919B04F5A2889C933FCFB7 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m91492C9C383B83819368A542FF0C7E2DB817BF42(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Contains_m88F444D9C0089E32B73E8F5067B8A8D7B4A4FE03(__this, ((*(WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11371
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_m45909D7E82317A9AC357C694F444A1039FA9A1FA (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_0 = ___0_array;
		List_1_CopyTo_mD9D5C57DBC12217624E77B77D7BEA6C58FA5F94B(__this, L_0, 0, NULL);
		return;
	}
}
// Method Definition Index: 11372
// Method Definition Index: 11373
// Method Definition Index: 11374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mD12A0B04025BCC2368FD9F8FE2B5CF9DC1AC7432 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_min, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t G_B4_0 = 0;
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int32_t L_2 = ___0_min;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003d;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_3 = __this->____items;
		NullCheck(L_3);
		if (!(((RuntimeArray*)L_3)->max_length))
		{
			goto IL_0020;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_4 = __this->____items;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		G_B4_0 = ((int32_t)il2cpp_codegen_multiply(L_5, 2));
		goto IL_0021;
	}

IL_0020:
	{
		G_B4_0 = 4;
	}

IL_0021:
	{
		V_0 = G_B4_0;
		int32_t L_6 = V_0;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)2146435071)))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = ((int32_t)2146435071);
	}

IL_0030:
	{
		int32_t L_7 = V_0;
		int32_t L_8 = ___0_min;
		if ((((int32_t)L_7) >= ((int32_t)L_8)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_9 = ___0_min;
		V_0 = L_9;
	}

IL_0036:
	{
		int32_t L_10 = V_0;
		List_1_set_Capacity_mA50F327494067599F9981040CBA1CD52DC8BF6D7(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_003d:
	{
		return;
	}
}
// Method Definition Index: 11375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_m24236613DA7C73BB55EC54BC48CC1C80059F7443 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_0 = ___0_match;
		int32_t L_1;
		L_1 = List_1_FindIndex_mD6516F14C77C6B7F4BE00B8267F9967DC240B19E(__this, L_0, NULL);
		return (bool)((((int32_t)((((int32_t)L_1) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 11376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 List_1_Find_m3FE24FA2EEBF2BDD7143CF98FAF59D6D4C8AE4F5 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0032;
	}

IL_000d:
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_1 = ___0_match;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_2 = __this->____items;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		NullCheck(L_1);
		bool L_6;
		L_6 = Predicate_1_Invoke_mAB88F92ABF8A77B99B3BC9C74D9C9A83878CBE7F_inline(L_1, L_5, NULL);
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_7 = __this->____items;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		return L_10;
	}

IL_002e:
	{
		int32_t L_11 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0032:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____size;
		if ((((int32_t)L_12) < ((int32_t)L_13)))
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_1), sizeof(WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0));
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_14 = V_1;
		return L_14;
	}
}
// Method Definition Index: 11377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mD6516F14C77C6B7F4BE00B8267F9967DC240B19E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_1 = ___0_match;
		int32_t L_2;
		L_2 = List_1_FindIndex_m8190C5AD3B8E00871FF03E7886A9CFF2D1380332(__this, 0, L_0, L_1, NULL);
		return L_2;
	}
}
// Method Definition Index: 11378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m8190C5AD3B8E00871FF03E7886A9CFF2D1380332 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_startIndex;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_000e:
	{
		int32_t L_2 = ___1_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_3) <= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_4, L_5)))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_0022:
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_6 = ___2_match;
		if (L_6)
		{
			goto IL_002b;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_002b:
	{
		int32_t L_7 = ___0_startIndex;
		int32_t L_8 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, L_8));
		int32_t L_9 = ___0_startIndex;
		V_1 = L_9;
		goto IL_004d;
	}

IL_0033:
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_10 = ___2_match;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_11 = __this->____items;
		int32_t L_12 = V_1;
		NullCheck(L_11);
		int32_t L_13 = L_12;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_14 = (L_11)->GetAt(static_cast<il2cpp_array_size_t>(L_13));
		NullCheck(L_10);
		bool L_15;
		L_15 = Predicate_1_Invoke_mAB88F92ABF8A77B99B3BC9C74D9C9A83878CBE7F_inline(L_10, L_14, NULL);
		if (!L_15)
		{
			goto IL_0049;
		}
	}
	{
		int32_t L_16 = V_1;
		return L_16;
	}

IL_0049:
	{
		int32_t L_17 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_004d:
	{
		int32_t L_18 = V_1;
		int32_t L_19 = V_0;
		if ((((int32_t)L_18) < ((int32_t)L_19)))
		{
			goto IL_0033;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m9C2DE84513EE456E1ADDE0B42429572F7AD212E5 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		int32_t L_1 = __this->____size;
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_2 = ___0_match;
		int32_t L_3;
		L_3 = List_1_FindLastIndex_mE2B352FDE238EB11CC24B96E7554145C2BED755C(__this, ((int32_t)il2cpp_codegen_subtract(L_0, 1)), L_1, L_2, NULL);
		return L_3;
	}
}
// Method Definition Index: 11380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mE2B352FDE238EB11CC24B96E7554145C2BED755C (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_0 = ___2_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		int32_t L_1 = __this->____size;
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = ___0_startIndex;
		if ((((int32_t)L_2) == ((int32_t)(-1))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
		goto IL_002a;
	}

IL_001c:
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		if ((!(((uint32_t)L_3) >= ((uint32_t)L_4))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_002a:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) < ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_6 = ___0_startIndex;
		int32_t L_7 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_6, L_7)), 1))) >= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}

IL_0036:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_003b:
	{
		int32_t L_8 = ___0_startIndex;
		int32_t L_9 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		int32_t L_10 = ___0_startIndex;
		V_1 = L_10;
		goto IL_005d;
	}

IL_0043:
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_11 = ___2_match;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_12 = __this->____items;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		NullCheck(L_11);
		bool L_16;
		L_16 = Predicate_1_Invoke_mAB88F92ABF8A77B99B3BC9C74D9C9A83878CBE7F_inline(L_11, L_15, NULL);
		if (!L_16)
		{
			goto IL_0059;
		}
	}
	{
		int32_t L_17 = V_1;
		return L_17;
	}

IL_0059:
	{
		int32_t L_18 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_18, 1));
	}

IL_005d:
	{
		int32_t L_19 = V_1;
		int32_t L_20 = V_0;
		if ((((int32_t)L_19) > ((int32_t)L_20)))
		{
			goto IL_0043;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_ForEach_m850510150D61298BA9E5F5AD73070C73FB80502F (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, Action_1_t7D3408AF7B9CB908929DA7A8A01F6231B5EE5413* ___0_action, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Action_1_t7D3408AF7B9CB908929DA7A8A01F6231B5EE5413* L_0 = ___0_action;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)33), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____version;
		V_0 = L_1;
		V_1 = 0;
		goto IL_0034;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____version;
		if ((!(((uint32_t)L_2) == ((uint32_t)L_3))))
		{
			goto IL_003d;
		}
	}
	{
		Action_1_t7D3408AF7B9CB908929DA7A8A01F6231B5EE5413* L_4 = ___0_action;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_5 = __this->____items;
		int32_t L_6 = V_1;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		Action_1_Invoke_m1AA93F8CEDADD37A0C262AE89FCDFCF1102FC270_inline(L_4, L_8, NULL);
		int32_t L_9 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0034:
	{
		int32_t L_10 = V_1;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0015;
		}
	}

IL_003d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____version;
		if ((((int32_t)L_12) == ((int32_t)L_13)))
		{
			goto IL_004b;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_004b:
	{
		return;
	}
}
// Method Definition Index: 11382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t848512BD3FDC8088BBB0EFAC6CE52E9F2EDF4B18 List_1_GetEnumerator_m31C6A163386836ABFDDC8D2EC4BEDDA224395A00 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t848512BD3FDC8088BBB0EFAC6CE52E9F2EDF4B18 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m03C1E572D8D5FAD9E64579B8974701EDD1E8FE6C((&L_0), __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_Generic_IEnumerableU3CTU3E_GetEnumerator_m43EDC786D6864ED68796A89E4AB6232C1246011D (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t848512BD3FDC8088BBB0EFAC6CE52E9F2EDF4B18 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m03C1E572D8D5FAD9E64579B8974701EDD1E8FE6C((&L_0), __this, NULL);
		Enumerator_t848512BD3FDC8088BBB0EFAC6CE52E9F2EDF4B18 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IEnumerable_GetEnumerator_mFA28D431535B0D9F77E7110EDD58E4928C63EBE9 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t848512BD3FDC8088BBB0EFAC6CE52E9F2EDF4B18 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m03C1E572D8D5FAD9E64579B8974701EDD1E8FE6C((&L_0), __this, NULL);
		Enumerator_t848512BD3FDC8088BBB0EFAC6CE52E9F2EDF4B18 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* List_1_GetRange_m9B550F6AC6A68DCD93007965D662033198C6DAE5 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* L_6 = (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		List_1__ctor_m463F6FD2D31F18F6BC2A559E4D51030B8E9A8C11(L_6, L_5, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33));
		V_0 = L_6;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* L_9 = V_0;
		NullCheck(L_9);
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_10 = L_9->____items;
		int32_t L_11 = ___1_count;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_7, L_8, (RuntimeArray*)L_10, 0, L_11, NULL);
		List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* L_12 = V_0;
		int32_t L_13 = ___1_count;
		NullCheck(L_12);
		L_12->____size = L_13;
		List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* L_14 = V_0;
		return L_14;
	}
}
// Method Definition Index: 11386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_mE63B255347079C50A47093D77B4CFAEA05B61455 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_0 = __this->____items;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_1 = ___0_item;
		int32_t L_2 = __this->____size;
		int32_t L_3;
		L_3 = Array_IndexOf_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m5281F142B5974BB41AA0821D1665BDE631701A7B(L_0, L_1, 0, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34));
		return L_3;
	}
}
// Method Definition Index: 11387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_IndexOf_m850BF8F8D222D9B01857C992159C9E26E2DE9226 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m91492C9C383B83819368A542FF0C7E2DB817BF42(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		int32_t L_3;
		L_3 = List_1_IndexOf_mE63B255347079C50A47093D77B4CFAEA05B61455(__this, ((*(WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_m938317D1E4B79E29DA685118A4043BE7D595F35E (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___1_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)13), ((int32_t)27), NULL);
	}

IL_0012:
	{
		int32_t L_2 = __this->____size;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0030;
		}
	}
	{
		int32_t L_5 = __this->____size;
		List_1_EnsureCapacity_mD12A0B04025BCC2368FD9F8FE2B5CF9DC1AC7432(__this, ((int32_t)il2cpp_codegen_add(L_5, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
	}

IL_0030:
	{
		int32_t L_6 = ___0_index;
		int32_t L_7 = __this->____size;
		if ((((int32_t)L_6) >= ((int32_t)L_7)))
		{
			goto IL_0056;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_8 = __this->____items;
		int32_t L_9 = ___0_index;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_10 = __this->____items;
		int32_t L_11 = ___0_index;
		int32_t L_12 = __this->____size;
		int32_t L_13 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_8, L_9, (RuntimeArray*)L_10, ((int32_t)il2cpp_codegen_add(L_11, 1)), ((int32_t)il2cpp_codegen_subtract(L_12, L_13)), NULL);
	}

IL_0056:
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_16 = ___1_item;
		NullCheck(L_14);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(L_15), (WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0)L_16);
		int32_t L_17 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_17, 1));
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		return;
	}
}
// Method Definition Index: 11389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Insert_mC198C4355BE886E8FD014EDFADD36A5B73AB5520 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, RuntimeObject* ___1_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m0A8BE23260E6E169A1D247D8EAA0631905874EC8(L_0, ((int32_t)20), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_item;
		List_1_Insert_m938317D1E4B79E29DA685118A4043BE7D595F35E(__this, L_1, ((*(WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_mE806EE6B9753E133B6809CDA1E26C6A048B5375C (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	RuntimeObject* V_2 = NULL;
	{
		RuntimeObject* L_0 = ___1_collection;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___0_index;
		int32_t L_2 = __this->____size;
		if ((!(((uint32_t)L_1) > ((uint32_t)L_2))))
		{
			goto IL_0017;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ___1_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_4 = V_0;
		if (!L_4)
		{
			goto IL_00bc;
		}
	}
	{
		RuntimeObject* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_5);
		V_1 = L_6;
		int32_t L_7 = V_1;
		if ((((int32_t)L_7) <= ((int32_t)0)))
		{
			goto IL_00fb;
		}
	}
	{
		int32_t L_8 = __this->____size;
		int32_t L_9 = V_1;
		List_1_EnsureCapacity_mD12A0B04025BCC2368FD9F8FE2B5CF9DC1AC7432(__this, ((int32_t)il2cpp_codegen_add(L_8, L_9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_10 = ___0_index;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) >= ((int32_t)L_11)))
		{
			goto IL_0066;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_12 = __this->____items;
		int32_t L_13 = ___0_index;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = V_1;
		int32_t L_17 = __this->____size;
		int32_t L_18 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_12, L_13, (RuntimeArray*)L_14, ((int32_t)il2cpp_codegen_add(L_15, L_16)), ((int32_t)il2cpp_codegen_subtract(L_17, L_18)), NULL);
	}

IL_0066:
	{
		RuntimeObject* L_19 = V_0;
		if ((!(((RuntimeObject*)(List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2*)__this) == ((RuntimeObject*)(RuntimeObject*)L_19))))
		{
			goto IL_009f;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_20 = __this->____items;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_21 = __this->____items;
		int32_t L_22 = ___0_index;
		int32_t L_23 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_20, 0, (RuntimeArray*)L_21, L_22, L_23, NULL);
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_24 = __this->____items;
		int32_t L_25 = ___0_index;
		int32_t L_26 = V_1;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_27 = __this->____items;
		int32_t L_28 = ___0_index;
		int32_t L_29 = __this->____size;
		int32_t L_30 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_24, ((int32_t)il2cpp_codegen_add(L_25, L_26)), (RuntimeArray*)L_27, ((int32_t)il2cpp_codegen_multiply(L_28, 2)), ((int32_t)il2cpp_codegen_subtract(L_29, L_30)), NULL);
		goto IL_00ac;
	}

IL_009f:
	{
		RuntimeObject* L_31 = V_0;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_32 = __this->____items;
		int32_t L_33 = ___0_index;
		NullCheck(L_31);
		InterfaceActionInvoker2< WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_31, L_32, L_33);
	}

IL_00ac:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_34, L_35));
		goto IL_00fb;
	}

IL_00bc:
	{
		int32_t L_36 = ___0_index;
		int32_t L_37 = __this->____size;
		if ((((int32_t)L_36) >= ((int32_t)L_37)))
		{
			goto IL_00f4;
		}
	}
	{
		RuntimeObject* L_38 = ___1_collection;
		NullCheck(L_38);
		RuntimeObject* L_39;
		L_39 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_38);
		V_2 = L_39;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ea:
			{
				{
					RuntimeObject* L_40 = V_2;
					if (!L_40)
					{
						goto IL_00f3;
					}
				}
				{
					RuntimeObject* L_41 = V_2;
					NullCheck(L_41);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_41);
				}

IL_00f3:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00e0_1;
			}

IL_00ce_1:
			{
				int32_t L_42 = ___0_index;
				int32_t L_43 = L_42;
				___0_index = ((int32_t)il2cpp_codegen_add(L_43, 1));
				RuntimeObject* L_44 = V_2;
				NullCheck(L_44);
				WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_45;
				L_45 = InterfaceFuncInvoker0< WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_44);
				List_1_Insert_m938317D1E4B79E29DA685118A4043BE7D595F35E(__this, L_43, L_45, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
			}

IL_00e0_1:
			{
				RuntimeObject* L_46 = V_2;
				NullCheck(L_46);
				bool L_47;
				L_47 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_46);
				if (L_47)
				{
					goto IL_00ce_1;
				}
			}
			{
				goto IL_00fb;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f4:
	{
		RuntimeObject* L_48 = ___1_collection;
		List_1_AddEnumerable_m0B91306C3AE3C541A215663CBC439245F8E3679B(__this, L_48, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_00fb:
	{
		int32_t L_49 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_49, 1));
		return;
	}
}
// Method Definition Index: 11391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m77C714E4BACAF1AF9D076F35A3E1583738ED4CBF (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_0 = ___0_item;
		int32_t L_1;
		L_1 = List_1_IndexOf_mE63B255347079C50A47093D77B4CFAEA05B61455(__this, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		int32_t L_3 = V_0;
		List_1_RemoveAt_m65889D090EB27B87304CACE748AFF62A4F2D36E4(__this, L_3, NULL);
		return (bool)1;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Remove_m91B9AFFC6A6284A73789A6E41E2BDD420B69E5DF (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m91492C9C383B83819368A542FF0C7E2DB817BF42(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Remove_m77C714E4BACAF1AF9D076F35A3E1583738ED4CBF(__this, ((*(WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41));
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 11393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_RemoveAll_mD689B8ECE460AD89FE7B50C0C7D4483AF4CF1F9C (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0011;
	}

IL_000d:
	{
		int32_t L_1 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_1, 1));
	}

IL_0011:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____size;
		if ((((int32_t)L_2) >= ((int32_t)L_3)))
		{
			goto IL_002e;
		}
	}
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_4 = ___0_match;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_5 = __this->____items;
		int32_t L_6 = V_0;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		bool L_9;
		L_9 = Predicate_1_Invoke_mAB88F92ABF8A77B99B3BC9C74D9C9A83878CBE7F_inline(L_4, L_8, NULL);
		if (!L_9)
		{
			goto IL_000d;
		}
	}

IL_002e:
	{
		int32_t L_10 = V_0;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0039;
		}
	}
	{
		return 0;
	}

IL_0039:
	{
		int32_t L_12 = V_0;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
		goto IL_0089;
	}

IL_003f:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_0043:
	{
		int32_t L_14 = V_1;
		int32_t L_15 = __this->____size;
		if ((((int32_t)L_14) >= ((int32_t)L_15)))
		{
			goto IL_0060;
		}
	}
	{
		Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* L_16 = ___0_match;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_17 = __this->____items;
		int32_t L_18 = V_1;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_20 = (L_17)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_16);
		bool L_21;
		L_21 = Predicate_1_Invoke_mAB88F92ABF8A77B99B3BC9C74D9C9A83878CBE7F_inline(L_16, L_20, NULL);
		if (L_21)
		{
			goto IL_003f;
		}
	}

IL_0060:
	{
		int32_t L_22 = V_1;
		int32_t L_23 = __this->____size;
		if ((((int32_t)L_22) >= ((int32_t)L_23)))
		{
			goto IL_0089;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_24 = __this->____items;
		int32_t L_25 = V_0;
		int32_t L_26 = L_25;
		V_0 = ((int32_t)il2cpp_codegen_add(L_26, 1));
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_27 = __this->____items;
		int32_t L_28 = V_1;
		int32_t L_29 = L_28;
		V_1 = ((int32_t)il2cpp_codegen_add(L_29, 1));
		NullCheck(L_27);
		int32_t L_30 = L_29;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_31 = (L_27)->GetAt(static_cast<il2cpp_array_size_t>(L_30));
		NullCheck(L_24);
		(L_24)->SetAt(static_cast<il2cpp_array_size_t>(L_26), (WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0)L_31);
	}

IL_0089:
	{
		int32_t L_32 = V_1;
		int32_t L_33 = __this->____size;
		if ((((int32_t)L_32) < ((int32_t)L_33)))
		{
			goto IL_0043;
		}
	}
	{
		goto IL_00ad;
	}

IL_00ad:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_0;
		int32_t L_36 = V_0;
		__this->____size = L_36;
		int32_t L_37 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_37, 1));
		return ((int32_t)il2cpp_codegen_subtract(L_34, L_35));
	}
}
// Method Definition Index: 11394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m65889D090EB27B87304CACE748AFF62A4F2D36E4 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		int32_t L_3 = ___0_index;
		int32_t L_4 = __this->____size;
		if ((((int32_t)L_3) >= ((int32_t)L_4)))
		{
			goto IL_0042;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		int32_t L_9 = __this->____size;
		int32_t L_10 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_5, ((int32_t)il2cpp_codegen_add(L_6, 1)), (RuntimeArray*)L_7, L_8, ((int32_t)il2cpp_codegen_subtract(L_9, L_10)), NULL);
	}

IL_0042:
	{
		goto IL_0063;
	}

IL_0063:
	{
		int32_t L_11 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_11, 1));
		return;
	}
}
// Method Definition Index: 11395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveRange_m7CA03769B54850A943C05A832AC611782EBDB02B (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		int32_t L_6 = __this->____size;
		int32_t L_7 = __this->____size;
		int32_t L_8 = ___1_count;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_7, L_8));
		int32_t L_9 = ___0_index;
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_9) >= ((int32_t)L_10)))
		{
			goto IL_0066;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_11 = __this->____items;
		int32_t L_12 = ___0_index;
		int32_t L_13 = ___1_count;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = __this->____size;
		int32_t L_17 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_11, ((int32_t)il2cpp_codegen_add(L_12, L_13)), (RuntimeArray*)L_14, L_15, ((int32_t)il2cpp_codegen_subtract(L_16, L_17)), NULL);
	}

IL_0066:
	{
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		goto IL_008d;
	}

IL_008d:
	{
		return;
	}
}
// Method Definition Index: 11396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m9142A9FC84392DCED5A5180A82E2D8610DFF9AC2 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_mAA4BC36A86EF945C37561EF64337148E583C211E_inline(__this, NULL);
		List_1_Reverse_mA03FEC4E37DECFB8BAA2B89DC8BB3403F5C0DF39(__this, 0, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42));
		return;
	}
}
// Method Definition Index: 11397
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mA03FEC4E37DECFB8BAA2B89DC8BB3403F5C0DF39 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0038;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		Array_Reverse_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m4D835914AAD7FE14C0C90C15BB80EDCFA2979ACA(L_6, L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43));
	}

IL_0038:
	{
		int32_t L_9 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_9, 1));
		return;
	}
}
// Method Definition Index: 11398
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mB293F957753258EA50AF3E0F78B4640DE77437BA (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_mAA4BC36A86EF945C37561EF64337148E583C211E_inline(__this, NULL);
		List_1_Sort_mF0494466B503CD1E46368A6B7C34BCF0220E1DAD(__this, 0, L_0, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m995F532365AC6BF1803B262F74EF9F8B31F6EB54 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_mAA4BC36A86EF945C37561EF64337148E583C211E_inline(__this, NULL);
		RuntimeObject* L_1 = ___0_comparer;
		List_1_Sort_mF0494466B503CD1E46368A6B7C34BCF0220E1DAD(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mF0494466B503CD1E46368A6B7C34BCF0220E1DAD (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0039;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		RuntimeObject* L_9 = ___2_comparer;
		Array_Sort_TisWeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0_m875985D50B08BA88CDD753C2C19C09F1C71FCBBD(L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45));
	}

IL_0039:
	{
		int32_t L_10 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_10, 1));
		return;
	}
}
// Method Definition Index: 11401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mC72CDF417292E947F24078BED8CABCAB7930B03F (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, Comparison_1_tD83994F0AC2F81C51937890F172D6969927BB246* ___0_comparison, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit);
	//<source_info:<no-source>:1>
	{
		Comparison_1_tD83994F0AC2F81C51937890F172D6969927BB246* L_0 = ___0_comparison;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)34), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_1) <= ((int32_t)1)))
		{
			goto IL_0026;
		}
	}
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_2 = __this->____items;
		int32_t L_3 = __this->____size;
		Comparison_1_tD83994F0AC2F81C51937890F172D6969927BB246* L_4 = ___0_comparison;
		CHECKED_LOCAL_INIT(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 47)),il2cpp_codegen_runtime_class_init_inline);
		ArraySortHelper_1_Sort_m47F30B1CCA682496F46C591E3B37DA92C6B9533A(L_2, 0, L_3, L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46));
	}

IL_0026:
	{
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11402
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* List_1_ToArray_m8527539BC75E9198346B5D2B72E0DBFF234ACA88 (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* V_0 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_1 = ((List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_3 = (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)(WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_2);
		V_0 = L_3;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_4 = __this->____items;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_5 = V_0;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_4, 0, (RuntimeArray*)L_5, 0, L_6, NULL);
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 11403
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_TrimExcess_m65462A60F4254911153D4BC7779311299098903F (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		double L_2 = (il2cpp_codegen_conv<double,int32_t,int32_t,false,false>(L_1,NULL));
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,double,double,false,false>(((double)il2cpp_codegen_multiply(L_2, (0.90000000000000002))),NULL));
		V_0 = L_3;
		int32_t L_4 = __this->____size;
		int32_t L_5 = V_0;
		if ((((int32_t)L_4) >= ((int32_t)L_5)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = __this->____size;
		List_1_set_Capacity_mA50F327494067599F9981040CBA1CD52DC8BF6D7(__this, L_6, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11404
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_m0B91306C3AE3C541A215663CBC439245F8E3679B (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		RuntimeObject* L_1 = ___0_enumerable;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_1);
		V_0 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0063:
			{
				{
					RuntimeObject* L_3 = V_0;
					if (!L_3)
					{
						goto IL_006c;
					}
				}
				{
					RuntimeObject* L_4 = V_0;
					NullCheck(L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_4);
				}

IL_006c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0059_1;
			}

IL_0017_1:
			{
				RuntimeObject* L_5 = V_0;
				NullCheck(L_5);
				WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_6;
				L_6 = InterfaceFuncInvoker0< WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_5);
				V_1 = L_6;
				int32_t L_7 = __this->____size;
				WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_8 = __this->____items;
				NullCheck(L_8);
				int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
				if ((!(((uint32_t)L_7) == ((uint32_t)L_9))))
				{
					goto IL_003c_1;
				}
			}
			{
				int32_t L_10 = __this->____size;
				List_1_EnsureCapacity_mD12A0B04025BCC2368FD9F8FE2B5CF9DC1AC7432(__this, ((int32_t)il2cpp_codegen_add(L_10, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
			}

IL_003c_1:
			{
				WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_11 = __this->____items;
				int32_t L_12 = __this->____size;
				V_2 = L_12;
				int32_t L_13 = V_2;
				__this->____size = ((int32_t)il2cpp_codegen_add(L_13, 1));
				int32_t L_14 = V_2;
				WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_15 = V_1;
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_14), (WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0)L_15);
			}

IL_0059_1:
			{
				RuntimeObject* L_16 = V_0;
				NullCheck(L_16);
				bool L_17;
				L_17 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_16);
				if (L_17)
				{
					goto IL_0017_1;
				}
			}
			{
				goto IL_006d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006d:
	{
		return;
	}
}
// Method Definition Index: 11405
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__cctor_m1ECAF2C64F35DDE138730C8D02D01B83920C42EE (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_0 = (WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)(WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (uint32_t)0);
		((List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray), (void*)L_0);
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
// Method Definition Index: 11344
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m36D6C2C549C181076288DA383CC7B03DECFF997B (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_0 = ((List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_0);
		return;
	}
}
// Method Definition Index: 11345
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m963DC069890378B6A3E3359D767115A12793B2D5 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_capacity, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___0_capacity;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)12), 4, NULL);
	}

IL_0012:
	{
		int32_t L_1 = ___0_capacity;
		if (L_1)
		{
			goto IL_0021;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_2 = ((List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_2);
		return;
	}

IL_0021:
	{
		int32_t L_3 = ___0_capacity;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_4 = (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)(AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_3);
		__this->____items = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_4);
		return;
	}
}
// Method Definition Index: 11346
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m2D3B6D64E158185039022CF1DC6BF81232E4ECDD (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_000f:
	{
		RuntimeObject* L_1 = ___0_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0050;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_3);
		V_1 = L_4;
		int32_t L_5 = V_1;
		if (L_5)
		{
			goto IL_002f;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_6 = ((List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_6);
		return;
	}

IL_002f:
	{
		int32_t L_7 = V_1;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_8 = (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)(AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_7);
		__this->____items = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_8);
		RuntimeObject* L_9 = V_0;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_10 = __this->____items;
		NullCheck(L_9);
		InterfaceActionInvoker2< AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_9, L_10, 0);
		int32_t L_11 = V_1;
		__this->____size = L_11;
		return;
	}

IL_0050:
	{
		__this->____size = 0;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_12 = ((List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		RuntimeObject* L_13 = ___0_collection;
		List_1_AddEnumerable_mB565A7AD8E82EBD46650B3F0E052900CE12DF2C5(__this, L_13, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
		return;
	}
}
// Method Definition Index: 11347
// Method Definition Index: 11348
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_m3332E89182E608A4BF6BE672CB719B8B0067984C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* V_0 = NULL;
	{
		int32_t L_0 = ___0_value;
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)15), ((int32_t)21), NULL);
	}

IL_0012:
	{
		int32_t L_2 = ___0_value;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_2) == ((int32_t)L_4)))
		{
			goto IL_0058;
		}
	}
	{
		int32_t L_5 = ___0_value;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_004d;
		}
	}
	{
		int32_t L_6 = ___0_value;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_7 = (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)(AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_6);
		V_0 = L_7;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0045;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_9 = __this->____items;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_10 = V_0;
		int32_t L_11 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_9, 0, (RuntimeArray*)L_10, 0, L_11, NULL);
	}

IL_0045:
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_12 = V_0;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		return;
	}

IL_004d:
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_13 = ((List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_13);
	}

IL_0058:
	{
		return;
	}
}
// Method Definition Index: 11349
// Method Definition Index: 11350
// Method Definition Index: 11351
// Method Definition Index: 11352
// Method Definition Index: 11353
// Method Definition Index: 11354
// Method Definition Index: 11355
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 List_1_get_Item_m4077C3878C73CFD2C37863B24FFABC93CCB538C2 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		return L_5;
	}
}
// Method Definition Index: 11356
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m9EE840E6FBE06791D954E7A69D53CE90EFCB863D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_4 = ___1_value;
		NullCheck(L_2);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(L_3), (AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161)L_4);
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11357
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_m9AA36809C4850F3957B1A0B552645A8F328B7158 (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6))))
		{
			goto IL_001f;
		}
	}
	{
		RuntimeObject* L_1 = ___0_value;
		if (L_1)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161));
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_2 = V_0;
		return false;
	}

IL_001d:
	{
		return (bool)0;
	}

IL_001f:
	{
		return (bool)1;
	}
}
// Method Definition Index: 11358
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IList_get_Item_m947597FE12E4BA2A85804F95A352ACF345C94885 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_1;
		L_1 = List_1_get_Item_m4077C3878C73CFD2C37863B24FFABC93CCB538C2(__this, L_0, NULL);
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_2 = L_1;
		RuntimeObject* L_3 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), &L_2);
		return L_3;
	}
}
// Method Definition Index: 11359
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_set_Item_mD832AF33855D28D59717DB409E91D8526478AF36 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_value;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m668408E30034D9AF162BA065A1B8A9190CCB854C(L_0, ((int32_t)15), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_value;
		List_1_set_Item_m9EE840E6FBE06791D954E7A69D53CE90EFCB863D(__this, L_1, ((*(AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), NULL);
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_value;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11360
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Add_mC35682C02D89984B864B24B3BB4F92B5C2374273 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_7 = V_0;
		int32_t L_8 = V_1;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161)L_9);
		return;
	}

IL_0034:
	{
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_10 = ___0_item;
		List_1_AddWithResize_mCA55981D47453F8483BD503CA53B0DABBDB8844D(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 11361
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_mCA55981D47453F8483BD503CA53B0DABBDB8844D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		int32_t L_1 = V_0;
		List_1_EnsureCapacity_mC9206E89715229E345A1542F94AC519D11E191D3(__this, ((int32_t)il2cpp_codegen_add(L_1, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_2 = V_0;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_2, 1));
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_3 = __this->____items;
		int32_t L_4 = V_0;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_5 = ___0_item;
		NullCheck(L_3);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(L_4), (AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161)L_5);
		return;
	}
}
// Method Definition Index: 11362
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_Add_m8D6A9580DC8157A4B26413CB8E937EE0B1FD4673 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___0_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m668408E30034D9AF162BA065A1B8A9190CCB854C(L_0, ((int32_t)20), NULL);
	}
	try
	{
		RuntimeObject* L_1 = ___0_item;
		List_1_Add_mC35682C02D89984B864B24B3BB4F92B5C2374273_inline(__this, ((*(AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161*)UnBox(L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		goto IL_0029;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0016;
		}
		throw e;
	}

CATCH_0016:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_2 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_3 = ___0_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_4 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_5;
		L_5 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_4, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_3, L_5, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0029;
	}

IL_0029:
	{
		int32_t L_6;
		L_6 = List_1_get_Count_m5F8DAD406CF36D0DEEB854A55C356D94762E30CB_inline(__this, NULL);
		return ((int32_t)il2cpp_codegen_subtract(L_6, 1));
	}
}
// Method Definition Index: 11363
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m28CF9B6487F02A6E0944A0A371459D954C3E7408 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		RuntimeObject* L_1 = ___0_collection;
		List_1_InsertRange_m830DE1B798DA2F877FBFF052C8841D220839E942(__this, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return;
	}
}
// Method Definition Index: 11364
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_t0072B9B1BE9DDF9C8BF0ABE59E7EEBD11BFAA293* List_1_AsReadOnly_m89FA24319273BE413C39340547C288F96196F0A6 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlyCollection_1_t0072B9B1BE9DDF9C8BF0ABE59E7EEBD11BFAA293* L_0 = (ReadOnlyCollection_1_t0072B9B1BE9DDF9C8BF0ABE59E7EEBD11BFAA293*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
		ReadOnlyCollection_1__ctor_mB3BFC6EBF26339300AE50AEA966E9B13DA92D54F(L_0, __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11365
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mBA4F662F4752FE0603FA496605CBBF1672CC8CCA (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		int32_t L_7 = ___1_count;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_8 = ___2_item;
		RuntimeObject* L_9 = ___3_comparer;
		int32_t L_10;
		L_10 = Array_BinarySearch_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m6F2E3CD5948C6A2A38F39E5200575F0C57CA126C(L_5, L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18));
		return L_10;
	}
}
// Method Definition Index: 11366
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_mD50670D821DB74044004A51961FEF4206DA1DE02 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m5F8DAD406CF36D0DEEB854A55C356D94762E30CB_inline(__this, NULL);
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_BinarySearch_mBA4F662F4752FE0603FA496605CBBF1672CC8CCA(__this, 0, L_0, L_1, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_2;
	}
}
// Method Definition Index: 11367
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m97328118926763045FE1FDCE36B415C5C2224C6C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, RuntimeObject* ___1_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m5F8DAD406CF36D0DEEB854A55C356D94762E30CB_inline(__this, NULL);
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_1 = ___0_item;
		RuntimeObject* L_2 = ___1_comparer;
		int32_t L_3;
		L_3 = List_1_BinarySearch_mBA4F662F4752FE0603FA496605CBBF1672CC8CCA(__this, 0, L_0, L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_3;
	}
}
// Method Definition Index: 11368
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Clear_m88B956AEE7318EDD5FCB2CE2E870F681D69A4E4D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
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
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_3 = __this->____items;
		int32_t L_4 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_3, 0, L_4, NULL);
		return;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11369
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m82EC683EFB57714A07D26CB6E17D84FFAE7C82AE (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_IndexOf_mCB5DDE85A3BF1B0AE48D46EB6E22999A0E4D060D(__this, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return (bool)((((int32_t)((((int32_t)L_2) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_0016:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11370
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_Contains_m5A1933A3C5DEB75534FB49094CC9BAC2A3A7D0C5 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m9AA36809C4850F3957B1A0B552645A8F328B7158(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Contains_m82EC683EFB57714A07D26CB6E17D84FFAE7C82AE(__this, ((*(AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11371
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_mD3E6E1675B5D239D04EDEBDCC3BEC9ED314E0784 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_0 = ___0_array;
		List_1_CopyTo_mBB5FC483C17D644A9362EDE7FB61DD1D6233000C(__this, L_0, 0, NULL);
		return;
	}
}
// Method Definition Index: 11372
// Method Definition Index: 11373
// Method Definition Index: 11374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mC9206E89715229E345A1542F94AC519D11E191D3 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_min, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t G_B4_0 = 0;
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int32_t L_2 = ___0_min;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003d;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_3 = __this->____items;
		NullCheck(L_3);
		if (!(((RuntimeArray*)L_3)->max_length))
		{
			goto IL_0020;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_4 = __this->____items;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		G_B4_0 = ((int32_t)il2cpp_codegen_multiply(L_5, 2));
		goto IL_0021;
	}

IL_0020:
	{
		G_B4_0 = 4;
	}

IL_0021:
	{
		V_0 = G_B4_0;
		int32_t L_6 = V_0;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)2146435071)))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = ((int32_t)2146435071);
	}

IL_0030:
	{
		int32_t L_7 = V_0;
		int32_t L_8 = ___0_min;
		if ((((int32_t)L_7) >= ((int32_t)L_8)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_9 = ___0_min;
		V_0 = L_9;
	}

IL_0036:
	{
		int32_t L_10 = V_0;
		List_1_set_Capacity_m3332E89182E608A4BF6BE672CB719B8B0067984C(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_003d:
	{
		return;
	}
}
// Method Definition Index: 11375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_m5A42DD8C248F19293FA5E2A143C105CD41E6777D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_0 = ___0_match;
		int32_t L_1;
		L_1 = List_1_FindIndex_m26711BD95F0847B161016ECEBA1123F477AD8E11(__this, L_0, NULL);
		return (bool)((((int32_t)((((int32_t)L_1) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 11376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 List_1_Find_mFB2829A563DEBCB31F0D002085C70CA8DA4EF089 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0032;
	}

IL_000d:
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_1 = ___0_match;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_2 = __this->____items;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		NullCheck(L_1);
		bool L_6;
		L_6 = Predicate_1_Invoke_m8D40905BAEF5849C9DF55268BB4FD2C8CB0A1A79_inline(L_1, L_5, NULL);
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_7 = __this->____items;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		return L_10;
	}

IL_002e:
	{
		int32_t L_11 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0032:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____size;
		if ((((int32_t)L_12) < ((int32_t)L_13)))
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_1), sizeof(AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161));
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_14 = V_1;
		return L_14;
	}
}
// Method Definition Index: 11377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m26711BD95F0847B161016ECEBA1123F477AD8E11 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_1 = ___0_match;
		int32_t L_2;
		L_2 = List_1_FindIndex_m6C63371ECE0863BF14A70E4012C6A62D66B5F5D7(__this, 0, L_0, L_1, NULL);
		return L_2;
	}
}
// Method Definition Index: 11378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m6C63371ECE0863BF14A70E4012C6A62D66B5F5D7 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_startIndex;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_000e:
	{
		int32_t L_2 = ___1_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_3) <= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_4, L_5)))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_0022:
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_6 = ___2_match;
		if (L_6)
		{
			goto IL_002b;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_002b:
	{
		int32_t L_7 = ___0_startIndex;
		int32_t L_8 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, L_8));
		int32_t L_9 = ___0_startIndex;
		V_1 = L_9;
		goto IL_004d;
	}

IL_0033:
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_10 = ___2_match;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_11 = __this->____items;
		int32_t L_12 = V_1;
		NullCheck(L_11);
		int32_t L_13 = L_12;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_14 = (L_11)->GetAt(static_cast<il2cpp_array_size_t>(L_13));
		NullCheck(L_10);
		bool L_15;
		L_15 = Predicate_1_Invoke_m8D40905BAEF5849C9DF55268BB4FD2C8CB0A1A79_inline(L_10, L_14, NULL);
		if (!L_15)
		{
			goto IL_0049;
		}
	}
	{
		int32_t L_16 = V_1;
		return L_16;
	}

IL_0049:
	{
		int32_t L_17 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_004d:
	{
		int32_t L_18 = V_1;
		int32_t L_19 = V_0;
		if ((((int32_t)L_18) < ((int32_t)L_19)))
		{
			goto IL_0033;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_mD7B413580C0508A8F2A58DC3C480128A6EF4F33C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		int32_t L_1 = __this->____size;
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_2 = ___0_match;
		int32_t L_3;
		L_3 = List_1_FindLastIndex_m0452816BC1DA4957681EA738C6BF21B054B01DB1(__this, ((int32_t)il2cpp_codegen_subtract(L_0, 1)), L_1, L_2, NULL);
		return L_3;
	}
}
// Method Definition Index: 11380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m0452816BC1DA4957681EA738C6BF21B054B01DB1 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_0 = ___2_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		int32_t L_1 = __this->____size;
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = ___0_startIndex;
		if ((((int32_t)L_2) == ((int32_t)(-1))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
		goto IL_002a;
	}

IL_001c:
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		if ((!(((uint32_t)L_3) >= ((uint32_t)L_4))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_002a:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) < ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_6 = ___0_startIndex;
		int32_t L_7 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_6, L_7)), 1))) >= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}

IL_0036:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_003b:
	{
		int32_t L_8 = ___0_startIndex;
		int32_t L_9 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		int32_t L_10 = ___0_startIndex;
		V_1 = L_10;
		goto IL_005d;
	}

IL_0043:
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_11 = ___2_match;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_12 = __this->____items;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		NullCheck(L_11);
		bool L_16;
		L_16 = Predicate_1_Invoke_m8D40905BAEF5849C9DF55268BB4FD2C8CB0A1A79_inline(L_11, L_15, NULL);
		if (!L_16)
		{
			goto IL_0059;
		}
	}
	{
		int32_t L_17 = V_1;
		return L_17;
	}

IL_0059:
	{
		int32_t L_18 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_18, 1));
	}

IL_005d:
	{
		int32_t L_19 = V_1;
		int32_t L_20 = V_0;
		if ((((int32_t)L_19) > ((int32_t)L_20)))
		{
			goto IL_0043;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_ForEach_m51BB9D596D8D1547EA180A1EEB351599F683D0AE (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, Action_1_t24D24AE7BDA27ABE5E9058347421CD45845804B3* ___0_action, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Action_1_t24D24AE7BDA27ABE5E9058347421CD45845804B3* L_0 = ___0_action;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)33), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____version;
		V_0 = L_1;
		V_1 = 0;
		goto IL_0034;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____version;
		if ((!(((uint32_t)L_2) == ((uint32_t)L_3))))
		{
			goto IL_003d;
		}
	}
	{
		Action_1_t24D24AE7BDA27ABE5E9058347421CD45845804B3* L_4 = ___0_action;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_5 = __this->____items;
		int32_t L_6 = V_1;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		Action_1_Invoke_m87599A17D9A01B7E75A9868586C2C91F0AB8FC85_inline(L_4, L_8, NULL);
		int32_t L_9 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0034:
	{
		int32_t L_10 = V_1;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0015;
		}
	}

IL_003d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____version;
		if ((((int32_t)L_12) == ((int32_t)L_13)))
		{
			goto IL_004b;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_004b:
	{
		return;
	}
}
// Method Definition Index: 11382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5 List_1_GetEnumerator_m71067EDD82FBD477E1BE9694B892C63B2B999C71 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m3B885E0CB5C32229381BEF0AD2CE4682713CA0EC((&L_0), __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_Generic_IEnumerableU3CTU3E_GetEnumerator_mAA21DAF2832F61083599B8B7B0452AE2526F6244 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m3B885E0CB5C32229381BEF0AD2CE4682713CA0EC((&L_0), __this, NULL);
		Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IEnumerable_GetEnumerator_m6424C877E306A6AC27A249CC52D90BA718E4E7B7 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_m3B885E0CB5C32229381BEF0AD2CE4682713CA0EC((&L_0), __this, NULL);
		Enumerator_t6630402C90E9E1872D41DA78B1CA17C07D89A9F5 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* List_1_GetRange_m62EB90F59D6CA44444252B298BEBB3A89C2A129E (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* L_6 = (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		List_1__ctor_m963DC069890378B6A3E3359D767115A12793B2D5(L_6, L_5, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33));
		V_0 = L_6;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* L_9 = V_0;
		NullCheck(L_9);
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_10 = L_9->____items;
		int32_t L_11 = ___1_count;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_7, L_8, (RuntimeArray*)L_10, 0, L_11, NULL);
		List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* L_12 = V_0;
		int32_t L_13 = ___1_count;
		NullCheck(L_12);
		L_12->____size = L_13;
		List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* L_14 = V_0;
		return L_14;
	}
}
// Method Definition Index: 11386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_mCB5DDE85A3BF1B0AE48D46EB6E22999A0E4D060D (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_0 = __this->____items;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_1 = ___0_item;
		int32_t L_2 = __this->____size;
		int32_t L_3;
		L_3 = Array_IndexOf_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m075DCC6CC6C5E2D552CC01B5A5D93D761529CC5E(L_0, L_1, 0, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34));
		return L_3;
	}
}
// Method Definition Index: 11387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_IndexOf_mCE1105AAD9EAF0BAABBACF5D4F76F8647C74ECDC (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m9AA36809C4850F3957B1A0B552645A8F328B7158(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		int32_t L_3;
		L_3 = List_1_IndexOf_mCB5DDE85A3BF1B0AE48D46EB6E22999A0E4D060D(__this, ((*(AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mFF1B561AB8F89B1761C15D1863C00ED3AD7BF324 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___1_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)13), ((int32_t)27), NULL);
	}

IL_0012:
	{
		int32_t L_2 = __this->____size;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0030;
		}
	}
	{
		int32_t L_5 = __this->____size;
		List_1_EnsureCapacity_mC9206E89715229E345A1542F94AC519D11E191D3(__this, ((int32_t)il2cpp_codegen_add(L_5, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
	}

IL_0030:
	{
		int32_t L_6 = ___0_index;
		int32_t L_7 = __this->____size;
		if ((((int32_t)L_6) >= ((int32_t)L_7)))
		{
			goto IL_0056;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_8 = __this->____items;
		int32_t L_9 = ___0_index;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_10 = __this->____items;
		int32_t L_11 = ___0_index;
		int32_t L_12 = __this->____size;
		int32_t L_13 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_8, L_9, (RuntimeArray*)L_10, ((int32_t)il2cpp_codegen_add(L_11, 1)), ((int32_t)il2cpp_codegen_subtract(L_12, L_13)), NULL);
	}

IL_0056:
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_16 = ___1_item;
		NullCheck(L_14);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(L_15), (AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161)L_16);
		int32_t L_17 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_17, 1));
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		return;
	}
}
// Method Definition Index: 11389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Insert_m0840EEFEF9E5209E46C901C4BB6D266DFFF506B8 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, RuntimeObject* ___1_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m668408E30034D9AF162BA065A1B8A9190CCB854C(L_0, ((int32_t)20), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_item;
		List_1_Insert_mFF1B561AB8F89B1761C15D1863C00ED3AD7BF324(__this, L_1, ((*(AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m830DE1B798DA2F877FBFF052C8841D220839E942 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	RuntimeObject* V_2 = NULL;
	{
		RuntimeObject* L_0 = ___1_collection;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___0_index;
		int32_t L_2 = __this->____size;
		if ((!(((uint32_t)L_1) > ((uint32_t)L_2))))
		{
			goto IL_0017;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ___1_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_4 = V_0;
		if (!L_4)
		{
			goto IL_00bc;
		}
	}
	{
		RuntimeObject* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_5);
		V_1 = L_6;
		int32_t L_7 = V_1;
		if ((((int32_t)L_7) <= ((int32_t)0)))
		{
			goto IL_00fb;
		}
	}
	{
		int32_t L_8 = __this->____size;
		int32_t L_9 = V_1;
		List_1_EnsureCapacity_mC9206E89715229E345A1542F94AC519D11E191D3(__this, ((int32_t)il2cpp_codegen_add(L_8, L_9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_10 = ___0_index;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) >= ((int32_t)L_11)))
		{
			goto IL_0066;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_12 = __this->____items;
		int32_t L_13 = ___0_index;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = V_1;
		int32_t L_17 = __this->____size;
		int32_t L_18 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_12, L_13, (RuntimeArray*)L_14, ((int32_t)il2cpp_codegen_add(L_15, L_16)), ((int32_t)il2cpp_codegen_subtract(L_17, L_18)), NULL);
	}

IL_0066:
	{
		RuntimeObject* L_19 = V_0;
		if ((!(((RuntimeObject*)(List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50*)__this) == ((RuntimeObject*)(RuntimeObject*)L_19))))
		{
			goto IL_009f;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_20 = __this->____items;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_21 = __this->____items;
		int32_t L_22 = ___0_index;
		int32_t L_23 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_20, 0, (RuntimeArray*)L_21, L_22, L_23, NULL);
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_24 = __this->____items;
		int32_t L_25 = ___0_index;
		int32_t L_26 = V_1;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_27 = __this->____items;
		int32_t L_28 = ___0_index;
		int32_t L_29 = __this->____size;
		int32_t L_30 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_24, ((int32_t)il2cpp_codegen_add(L_25, L_26)), (RuntimeArray*)L_27, ((int32_t)il2cpp_codegen_multiply(L_28, 2)), ((int32_t)il2cpp_codegen_subtract(L_29, L_30)), NULL);
		goto IL_00ac;
	}

IL_009f:
	{
		RuntimeObject* L_31 = V_0;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_32 = __this->____items;
		int32_t L_33 = ___0_index;
		NullCheck(L_31);
		InterfaceActionInvoker2< AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_31, L_32, L_33);
	}

IL_00ac:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_34, L_35));
		goto IL_00fb;
	}

IL_00bc:
	{
		int32_t L_36 = ___0_index;
		int32_t L_37 = __this->____size;
		if ((((int32_t)L_36) >= ((int32_t)L_37)))
		{
			goto IL_00f4;
		}
	}
	{
		RuntimeObject* L_38 = ___1_collection;
		NullCheck(L_38);
		RuntimeObject* L_39;
		L_39 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_38);
		V_2 = L_39;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ea:
			{
				{
					RuntimeObject* L_40 = V_2;
					if (!L_40)
					{
						goto IL_00f3;
					}
				}
				{
					RuntimeObject* L_41 = V_2;
					NullCheck(L_41);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_41);
				}

IL_00f3:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00e0_1;
			}

IL_00ce_1:
			{
				int32_t L_42 = ___0_index;
				int32_t L_43 = L_42;
				___0_index = ((int32_t)il2cpp_codegen_add(L_43, 1));
				RuntimeObject* L_44 = V_2;
				NullCheck(L_44);
				AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_45;
				L_45 = InterfaceFuncInvoker0< AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_44);
				List_1_Insert_mFF1B561AB8F89B1761C15D1863C00ED3AD7BF324(__this, L_43, L_45, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
			}

IL_00e0_1:
			{
				RuntimeObject* L_46 = V_2;
				NullCheck(L_46);
				bool L_47;
				L_47 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_46);
				if (L_47)
				{
					goto IL_00ce_1;
				}
			}
			{
				goto IL_00fb;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f4:
	{
		RuntimeObject* L_48 = ___1_collection;
		List_1_AddEnumerable_mB565A7AD8E82EBD46650B3F0E052900CE12DF2C5(__this, L_48, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_00fb:
	{
		int32_t L_49 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_49, 1));
		return;
	}
}
// Method Definition Index: 11391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_mA7E179931948DD88B825B2851F0BB01856B70460 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_0 = ___0_item;
		int32_t L_1;
		L_1 = List_1_IndexOf_mCB5DDE85A3BF1B0AE48D46EB6E22999A0E4D060D(__this, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		int32_t L_3 = V_0;
		List_1_RemoveAt_m252525485F467F26DB5C91A957EA9F842D005C40(__this, L_3, NULL);
		return (bool)1;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Remove_m105698B1E1A84AD80594BDDC45CF25703B4D5B99 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_m9AA36809C4850F3957B1A0B552645A8F328B7158(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Remove_mA7E179931948DD88B825B2851F0BB01856B70460(__this, ((*(AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41));
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 11393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_RemoveAll_mE7121022B2FC3DE26CB98396D8E84A04A66A1CF5 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0011;
	}

IL_000d:
	{
		int32_t L_1 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_1, 1));
	}

IL_0011:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____size;
		if ((((int32_t)L_2) >= ((int32_t)L_3)))
		{
			goto IL_002e;
		}
	}
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_4 = ___0_match;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_5 = __this->____items;
		int32_t L_6 = V_0;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		bool L_9;
		L_9 = Predicate_1_Invoke_m8D40905BAEF5849C9DF55268BB4FD2C8CB0A1A79_inline(L_4, L_8, NULL);
		if (!L_9)
		{
			goto IL_000d;
		}
	}

IL_002e:
	{
		int32_t L_10 = V_0;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0039;
		}
	}
	{
		return 0;
	}

IL_0039:
	{
		int32_t L_12 = V_0;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
		goto IL_0089;
	}

IL_003f:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_0043:
	{
		int32_t L_14 = V_1;
		int32_t L_15 = __this->____size;
		if ((((int32_t)L_14) >= ((int32_t)L_15)))
		{
			goto IL_0060;
		}
	}
	{
		Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* L_16 = ___0_match;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_17 = __this->____items;
		int32_t L_18 = V_1;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_20 = (L_17)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_16);
		bool L_21;
		L_21 = Predicate_1_Invoke_m8D40905BAEF5849C9DF55268BB4FD2C8CB0A1A79_inline(L_16, L_20, NULL);
		if (L_21)
		{
			goto IL_003f;
		}
	}

IL_0060:
	{
		int32_t L_22 = V_1;
		int32_t L_23 = __this->____size;
		if ((((int32_t)L_22) >= ((int32_t)L_23)))
		{
			goto IL_0089;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_24 = __this->____items;
		int32_t L_25 = V_0;
		int32_t L_26 = L_25;
		V_0 = ((int32_t)il2cpp_codegen_add(L_26, 1));
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_27 = __this->____items;
		int32_t L_28 = V_1;
		int32_t L_29 = L_28;
		V_1 = ((int32_t)il2cpp_codegen_add(L_29, 1));
		NullCheck(L_27);
		int32_t L_30 = L_29;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_31 = (L_27)->GetAt(static_cast<il2cpp_array_size_t>(L_30));
		NullCheck(L_24);
		(L_24)->SetAt(static_cast<il2cpp_array_size_t>(L_26), (AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161)L_31);
	}

IL_0089:
	{
		int32_t L_32 = V_1;
		int32_t L_33 = __this->____size;
		if ((((int32_t)L_32) < ((int32_t)L_33)))
		{
			goto IL_0043;
		}
	}
	{
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_34 = __this->____items;
		int32_t L_35 = V_0;
		int32_t L_36 = __this->____size;
		int32_t L_37 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_34, L_35, ((int32_t)il2cpp_codegen_subtract(L_36, L_37)), NULL);
	}

IL_00ad:
	{
		int32_t L_38 = __this->____size;
		int32_t L_39 = V_0;
		int32_t L_40 = V_0;
		__this->____size = L_40;
		int32_t L_41 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_41, 1));
		return ((int32_t)il2cpp_codegen_subtract(L_38, L_39));
	}
}
// Method Definition Index: 11394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m252525485F467F26DB5C91A957EA9F842D005C40 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		int32_t L_3 = ___0_index;
		int32_t L_4 = __this->____size;
		if ((((int32_t)L_3) >= ((int32_t)L_4)))
		{
			goto IL_0042;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		int32_t L_9 = __this->____size;
		int32_t L_10 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_5, ((int32_t)il2cpp_codegen_add(L_6, 1)), (RuntimeArray*)L_7, L_8, ((int32_t)il2cpp_codegen_subtract(L_9, L_10)), NULL);
	}

IL_0042:
	{
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_11 = __this->____items;
		int32_t L_12 = __this->____size;
		il2cpp_codegen_initobj((&V_0), sizeof(AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161));
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_13 = V_0;
		NullCheck(L_11);
		(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_12), (AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161)L_13);
	}

IL_0063:
	{
		int32_t L_14 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_14, 1));
		return;
	}
}
// Method Definition Index: 11395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveRange_m2654478C22CA96104BAEF5400FC0008E9E36F941 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		int32_t L_6 = __this->____size;
		int32_t L_7 = __this->____size;
		int32_t L_8 = ___1_count;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_7, L_8));
		int32_t L_9 = ___0_index;
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_9) >= ((int32_t)L_10)))
		{
			goto IL_0066;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_11 = __this->____items;
		int32_t L_12 = ___0_index;
		int32_t L_13 = ___1_count;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = __this->____size;
		int32_t L_17 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_11, ((int32_t)il2cpp_codegen_add(L_12, L_13)), (RuntimeArray*)L_14, L_15, ((int32_t)il2cpp_codegen_subtract(L_16, L_17)), NULL);
	}

IL_0066:
	{
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_19 = __this->____items;
		int32_t L_20 = __this->____size;
		int32_t L_21 = ___1_count;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_19, L_20, L_21, NULL);
	}

IL_008d:
	{
		return;
	}
}
// Method Definition Index: 11396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m02298A1EF154FB8CDFCD47E1750CD852896CBF6C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m5F8DAD406CF36D0DEEB854A55C356D94762E30CB_inline(__this, NULL);
		List_1_Reverse_mBDDBB14EC973F164BA222AEB240F57B8460DAC1C(__this, 0, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42));
		return;
	}
}
// Method Definition Index: 11397
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mBDDBB14EC973F164BA222AEB240F57B8460DAC1C (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0038;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		Array_Reverse_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_mB7050783F04AD0453F190FBF1F3F6A2B6CB59506(L_6, L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43));
	}

IL_0038:
	{
		int32_t L_9 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_9, 1));
		return;
	}
}
// Method Definition Index: 11398
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mAE263606F38787640DE853935661966FC225E295 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m5F8DAD406CF36D0DEEB854A55C356D94762E30CB_inline(__this, NULL);
		List_1_Sort_m1CAB75B85D1547C184E1CA824E4A29E11269376E(__this, 0, L_0, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m4520F1071319806EC9F0F933DECDC8A87686A181 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m5F8DAD406CF36D0DEEB854A55C356D94762E30CB_inline(__this, NULL);
		RuntimeObject* L_1 = ___0_comparer;
		List_1_Sort_m1CAB75B85D1547C184E1CA824E4A29E11269376E(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m1CAB75B85D1547C184E1CA824E4A29E11269376E (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0039;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		RuntimeObject* L_9 = ___2_comparer;
		Array_Sort_TisAwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161_m9D7F79A00D6CDEEE91A7009E9C83C8A9190FCA23(L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45));
	}

IL_0039:
	{
		int32_t L_10 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_10, 1));
		return;
	}
}
// Method Definition Index: 11401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m7B6AA41984EC619E52012A1FB09301CFE446CB02 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, Comparison_1_t53C7968EAD02BE23D913BD2C7F7F58BBBDD8D096* ___0_comparison, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit);
	//<source_info:<no-source>:1>
	{
		Comparison_1_t53C7968EAD02BE23D913BD2C7F7F58BBBDD8D096* L_0 = ___0_comparison;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)34), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_1) <= ((int32_t)1)))
		{
			goto IL_0026;
		}
	}
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_2 = __this->____items;
		int32_t L_3 = __this->____size;
		Comparison_1_t53C7968EAD02BE23D913BD2C7F7F58BBBDD8D096* L_4 = ___0_comparison;
		CHECKED_LOCAL_INIT(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 47)),il2cpp_codegen_runtime_class_init_inline);
		ArraySortHelper_1_Sort_m46BFEDFA8C5CE90310AFB562E0DE7A7E83E3BFEF(L_2, 0, L_3, L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46));
	}

IL_0026:
	{
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11402
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* List_1_ToArray_m689FD9633B8CA4B6D791D424C63EB1E4A628D9F2 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* V_0 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_1 = ((List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_3 = (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)(AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_2);
		V_0 = L_3;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_4 = __this->____items;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_5 = V_0;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_4, 0, (RuntimeArray*)L_5, 0, L_6, NULL);
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 11403
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_TrimExcess_m73CFAF495BC2163C0F920A0D922CDDD8ED368B65 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		double L_2 = (il2cpp_codegen_conv<double,int32_t,int32_t,false,false>(L_1,NULL));
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,double,double,false,false>(((double)il2cpp_codegen_multiply(L_2, (0.90000000000000002))),NULL));
		V_0 = L_3;
		int32_t L_4 = __this->____size;
		int32_t L_5 = V_0;
		if ((((int32_t)L_4) >= ((int32_t)L_5)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = __this->____size;
		List_1_set_Capacity_m3332E89182E608A4BF6BE672CB719B8B0067984C(__this, L_6, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11404
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mB565A7AD8E82EBD46650B3F0E052900CE12DF2C5 (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		RuntimeObject* L_1 = ___0_enumerable;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_1);
		V_0 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0063:
			{
				{
					RuntimeObject* L_3 = V_0;
					if (!L_3)
					{
						goto IL_006c;
					}
				}
				{
					RuntimeObject* L_4 = V_0;
					NullCheck(L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_4);
				}

IL_006c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0059_1;
			}

IL_0017_1:
			{
				RuntimeObject* L_5 = V_0;
				NullCheck(L_5);
				AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_6;
				L_6 = InterfaceFuncInvoker0< AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_5);
				V_1 = L_6;
				int32_t L_7 = __this->____size;
				AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_8 = __this->____items;
				NullCheck(L_8);
				int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
				if ((!(((uint32_t)L_7) == ((uint32_t)L_9))))
				{
					goto IL_003c_1;
				}
			}
			{
				int32_t L_10 = __this->____size;
				List_1_EnsureCapacity_mC9206E89715229E345A1542F94AC519D11E191D3(__this, ((int32_t)il2cpp_codegen_add(L_10, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
			}

IL_003c_1:
			{
				AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_11 = __this->____items;
				int32_t L_12 = __this->____size;
				V_2 = L_12;
				int32_t L_13 = V_2;
				__this->____size = ((int32_t)il2cpp_codegen_add(L_13, 1));
				int32_t L_14 = V_2;
				AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_15 = V_1;
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_14), (AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161)L_15);
			}

IL_0059_1:
			{
				RuntimeObject* L_16 = V_0;
				NullCheck(L_16);
				bool L_17;
				L_17 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_16);
				if (L_17)
				{
					goto IL_0017_1;
				}
			}
			{
				goto IL_006d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006d:
	{
		return;
	}
}
// Method Definition Index: 11405
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__cctor_m97A6896725C5440AA408FFCB9ED5252043CB839A (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_0 = (AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)(AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (uint32_t)0);
		((List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray), (void*)L_0);
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
// Method Definition Index: 11344
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_mF5CCF832B2A17822B9C64E78D9814190165C1D7B (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_0 = ((List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_0);
		return;
	}
}
// Method Definition Index: 11345
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m3F4723F3305F67834CE85E425907F53C7AB30484 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_capacity, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___0_capacity;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)12), 4, NULL);
	}

IL_0012:
	{
		int32_t L_1 = ___0_capacity;
		if (L_1)
		{
			goto IL_0021;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_2 = ((List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_2);
		return;
	}

IL_0021:
	{
		int32_t L_3 = ___0_capacity;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_4 = (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)(OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_3);
		__this->____items = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_4);
		return;
	}
}
// Method Definition Index: 11346
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m704785946C26F21A75CF1D70A2DF69E177349DEC (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		RuntimeObject* L_0 = ___0_collection;
		if (L_0)
		{
			goto IL_000f;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_000f:
	{
		RuntimeObject* L_1 = ___0_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0050;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_3);
		V_1 = L_4;
		int32_t L_5 = V_1;
		if (L_5)
		{
			goto IL_002f;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_6 = ((List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_6);
		return;
	}

IL_002f:
	{
		int32_t L_7 = V_1;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_8 = (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)(OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_7);
		__this->____items = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_8);
		RuntimeObject* L_9 = V_0;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_10 = __this->____items;
		NullCheck(L_9);
		InterfaceActionInvoker2< OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_9, L_10, 0);
		int32_t L_11 = V_1;
		__this->____size = L_11;
		return;
	}

IL_0050:
	{
		__this->____size = 0;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_12 = ((List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		RuntimeObject* L_13 = ___0_collection;
		List_1_AddEnumerable_mA6E73354873BEAC5429A80AE43CB210589A7EC7E(__this, L_13, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
		return;
	}
}
// Method Definition Index: 11347
// Method Definition Index: 11348
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Capacity_mC49015D45A78A39909C4541BDB84DAA369E6A4C1 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* V_0 = NULL;
	{
		int32_t L_0 = ___0_value;
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_0) >= ((int32_t)L_1)))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)15), ((int32_t)21), NULL);
	}

IL_0012:
	{
		int32_t L_2 = ___0_value;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((((int32_t)L_2) == ((int32_t)L_4)))
		{
			goto IL_0058;
		}
	}
	{
		int32_t L_5 = ___0_value;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_004d;
		}
	}
	{
		int32_t L_6 = ___0_value;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_7 = (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)(OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_6);
		V_0 = L_7;
		int32_t L_8 = __this->____size;
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0045;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_9 = __this->____items;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_10 = V_0;
		int32_t L_11 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_9, 0, (RuntimeArray*)L_10, 0, L_11, NULL);
	}

IL_0045:
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_12 = V_0;
		__this->____items = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_12);
		return;
	}

IL_004d:
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_13 = ((List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		__this->____items = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____items), (void*)L_13);
	}

IL_0058:
	{
		return;
	}
}
// Method Definition Index: 11349
// Method Definition Index: 11350
// Method Definition Index: 11351
// Method Definition Index: 11352
// Method Definition Index: 11353
// Method Definition Index: 11354
// Method Definition Index: 11355
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 List_1_get_Item_mB65D799560ADD289EA08E717412A24EFADA7FB57 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		return L_5;
	}
}
// Method Definition Index: 11356
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_set_Item_m7533012D6C4CE083D5CAF670259B97F6D5D506F4 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_2 = __this->____items;
		int32_t L_3 = ___0_index;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_4 = ___1_value;
		NullCheck(L_2);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(L_3), (OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837)L_4);
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11357
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_IsCompatibleObject_mA2F95E82C59734A475035EF3B9DD4BCE9F8429DB (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6))))
		{
			goto IL_001f;
		}
	}
	{
		RuntimeObject* L_1 = ___0_value;
		if (L_1)
		{
			goto IL_001d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837));
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_2 = V_0;
		return false;
	}

IL_001d:
	{
		return (bool)0;
	}

IL_001f:
	{
		return (bool)1;
	}
}
// Method Definition Index: 11358
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IList_get_Item_m1E70F596E00E1A551FAA8FBC581829AC9F9C5380 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_1;
		L_1 = List_1_get_Item_mB65D799560ADD289EA08E717412A24EFADA7FB57(__this, L_0, NULL);
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_2 = L_1;
		RuntimeObject* L_3 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6), &L_2);
		return L_3;
	}
}
// Method Definition Index: 11359
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_set_Item_m1DA92BD10C1F7E8C92A5ADDC41B5B59E38B45DEC (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_value;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m054615FD6A62EE7702B344B2E24BA8423FEF9621(L_0, ((int32_t)15), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_value;
		List_1_set_Item_m7533012D6C4CE083D5CAF670259B97F6D5D506F4(__this, L_1, ((*(OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), NULL);
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_value;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11360
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Add_mB308EE5BFFD89A7B8C251644D76CA547456F3012 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_7 = V_0;
		int32_t L_8 = V_1;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837)L_9);
		return;
	}

IL_0034:
	{
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_10 = ___0_item;
		List_1_AddWithResize_m8B3D79788ADC2B54C9B12CC9EC1158E6A04CDEBC(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 11361
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m8B3D79788ADC2B54C9B12CC9EC1158E6A04CDEBC (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____size;
		V_0 = L_0;
		int32_t L_1 = V_0;
		List_1_EnsureCapacity_mB6EB012362AA419CAA8BF0C488F1010486273A69(__this, ((int32_t)il2cpp_codegen_add(L_1, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_2 = V_0;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_2, 1));
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_3 = __this->____items;
		int32_t L_4 = V_0;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_5 = ___0_item;
		NullCheck(L_3);
		(L_3)->SetAt(static_cast<il2cpp_array_size_t>(L_4), (OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837)L_5);
		return;
	}
}
// Method Definition Index: 11362
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_Add_mAC1736DC5831C59BD64FC09C14F03AF5B9802CBF (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___0_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m054615FD6A62EE7702B344B2E24BA8423FEF9621(L_0, ((int32_t)20), NULL);
	}
	try
	{
		RuntimeObject* L_1 = ___0_item;
		List_1_Add_mB308EE5BFFD89A7B8C251644D76CA547456F3012_inline(__this, ((*(OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837*)UnBox(L_1, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		goto IL_0029;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0016;
		}
		throw e;
	}

CATCH_0016:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_2 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_3 = ___0_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_4 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_5;
		L_5 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_4, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_3, L_5, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_0029;
	}

IL_0029:
	{
		int32_t L_6;
		L_6 = List_1_get_Count_m905EB6FD0B91CEE1CC0165F1EBB8C1309AEE4BEC_inline(__this, NULL);
		return ((int32_t)il2cpp_codegen_subtract(L_6, 1));
	}
}
// Method Definition Index: 11363
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m4C28D7F5EE927CF1E9858BE460EA4DFB7F5D3AFA (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		RuntimeObject* L_1 = ___0_collection;
		List_1_InsertRange_m9AA9E2C5627FB6CD9F3493CA8A99588EDA08125D(__this, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return;
	}
}
// Method Definition Index: 11364
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyCollection_1_t00EFCE24C7E2AF195B108C1FC1E1BE4F7007EC1E* List_1_AsReadOnly_m3E59B457103D2F1A4D283713A62EFBFC7F91322C (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlyCollection_1_t00EFCE24C7E2AF195B108C1FC1E1BE4F7007EC1E* L_0 = (ReadOnlyCollection_1_t00EFCE24C7E2AF195B108C1FC1E1BE4F7007EC1E*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
		ReadOnlyCollection_1__ctor_m5D03D2E5CE6AAFA9914B3F1E1707393948DA34CB(L_0, __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11365
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m04F6731E2C6708D1889CB5C7F06A85173A079914 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___2_item, RuntimeObject* ___3_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		int32_t L_7 = ___1_count;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_8 = ___2_item;
		RuntimeObject* L_9 = ___3_comparer;
		int32_t L_10;
		L_10 = Array_BinarySearch_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_mD97987A6223479E02D026B1D81268848C313482B(L_5, L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 18));
		return L_10;
	}
}
// Method Definition Index: 11366
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m4C9C05EAADF51F65768CB0E0DB41A28259CFF990 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m905EB6FD0B91CEE1CC0165F1EBB8C1309AEE4BEC_inline(__this, NULL);
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_BinarySearch_m04F6731E2C6708D1889CB5C7F06A85173A079914(__this, 0, L_0, L_1, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_2;
	}
}
// Method Definition Index: 11367
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_BinarySearch_m729EB30064F385C380594F2AB016B67253878E63 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, RuntimeObject* ___1_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m905EB6FD0B91CEE1CC0165F1EBB8C1309AEE4BEC_inline(__this, NULL);
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_1 = ___0_item;
		RuntimeObject* L_2 = ___1_comparer;
		int32_t L_3;
		L_3 = List_1_BinarySearch_m04F6731E2C6708D1889CB5C7F06A85173A079914(__this, 0, L_0, L_1, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 19));
		return L_3;
	}
}
// Method Definition Index: 11368
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Clear_mFCE2A5F0F23BC41E5B1E6F8ABA41D99A8A5AABA9 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
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
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_3 = __this->____items;
		int32_t L_4 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_3, 0, L_4, NULL);
		return;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11369
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m590D7926DA2B45C95850710F87644BCD235F1A70 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_1 = ___0_item;
		int32_t L_2;
		L_2 = List_1_IndexOf_mFA6A5CD427374237339591ED8A5218B6E7EA7F4C(__this, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return (bool)((((int32_t)((((int32_t)L_2) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}

IL_0016:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11370
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_System_Collections_IList_Contains_m04EF9F1B5EB5475A55429F987420A7AE443BE0BB (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_mA2F95E82C59734A475035EF3B9DD4BCE9F8429DB(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Contains_m590D7926DA2B45C95850710F87644BCD235F1A70(__this, ((*(OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11371
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_CopyTo_m901F0963BA82CF65FCD89C086C5D7B3ABA3765C2 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_0 = ___0_array;
		List_1_CopyTo_m96F4BF1E6FF691338A44AAB62506964ACAD70715(__this, L_0, 0, NULL);
		return;
	}
}
// Method Definition Index: 11372
// Method Definition Index: 11373
// Method Definition Index: 11374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_EnsureCapacity_mB6EB012362AA419CAA8BF0C488F1010486273A69 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_min, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t G_B4_0 = 0;
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		int32_t L_2 = ___0_min;
		if ((((int32_t)L_1) >= ((int32_t)L_2)))
		{
			goto IL_003d;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_3 = __this->____items;
		NullCheck(L_3);
		if (!(((RuntimeArray*)L_3)->max_length))
		{
			goto IL_0020;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_4 = __this->____items;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		G_B4_0 = ((int32_t)il2cpp_codegen_multiply(L_5, 2));
		goto IL_0021;
	}

IL_0020:
	{
		G_B4_0 = 4;
	}

IL_0021:
	{
		V_0 = G_B4_0;
		int32_t L_6 = V_0;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)2146435071)))))
		{
			goto IL_0030;
		}
	}
	{
		V_0 = ((int32_t)2146435071);
	}

IL_0030:
	{
		int32_t L_7 = V_0;
		int32_t L_8 = ___0_min;
		if ((((int32_t)L_7) >= ((int32_t)L_8)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_9 = ___0_min;
		V_0 = L_9;
	}

IL_0036:
	{
		int32_t L_10 = V_0;
		List_1_set_Capacity_mC49015D45A78A39909C4541BDB84DAA369E6A4C1(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_003d:
	{
		return;
	}
}
// Method Definition Index: 11375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_m9910BF16382C28911B35AB614F364FD591C08867 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_0 = ___0_match;
		int32_t L_1;
		L_1 = List_1_FindIndex_m8E32928E07448E4DE50B13A27ABDF776DE8F4DDD(__this, L_0, NULL);
		return (bool)((((int32_t)((((int32_t)L_1) == ((int32_t)(-1)))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 11376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 List_1_Find_mAA7DF0253D336711158305FABCA7FCF86274DFCC (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0032;
	}

IL_000d:
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_1 = ___0_match;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_2 = __this->____items;
		int32_t L_3 = V_0;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		NullCheck(L_1);
		bool L_6;
		L_6 = Predicate_1_Invoke_m182E0ACAA85455151F5750CE87F942DA6E3DD3D6_inline(L_1, L_5, NULL);
		if (!L_6)
		{
			goto IL_002e;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_7 = __this->____items;
		int32_t L_8 = V_0;
		NullCheck(L_7);
		int32_t L_9 = L_8;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_10 = (L_7)->GetAt(static_cast<il2cpp_array_size_t>(L_9));
		return L_10;
	}

IL_002e:
	{
		int32_t L_11 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_11, 1));
	}

IL_0032:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____size;
		if ((((int32_t)L_12) < ((int32_t)L_13)))
		{
			goto IL_000d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_1), sizeof(OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837));
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_14 = V_1;
		return L_14;
	}
}
// Method Definition Index: 11377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_m8E32928E07448E4DE50B13A27ABDF776DE8F4DDD (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_1 = ___0_match;
		int32_t L_2;
		L_2 = List_1_FindIndex_mA5DDCB0CBD1B539DB3213A4B3AE1C72E519C98A8(__this, 0, L_0, L_1, NULL);
		return L_2;
	}
}
// Method Definition Index: 11378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindIndex_mA5DDCB0CBD1B539DB3213A4B3AE1C72E519C98A8 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		int32_t L_0 = ___0_startIndex;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_000e:
	{
		int32_t L_2 = ___1_count;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_001d;
		}
	}
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_3) <= ((int32_t)((int32_t)il2cpp_codegen_subtract(L_4, L_5)))))
		{
			goto IL_0022;
		}
	}

IL_001d:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_0022:
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_6 = ___2_match;
		if (L_6)
		{
			goto IL_002b;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_002b:
	{
		int32_t L_7 = ___0_startIndex;
		int32_t L_8 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_add(L_7, L_8));
		int32_t L_9 = ___0_startIndex;
		V_1 = L_9;
		goto IL_004d;
	}

IL_0033:
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_10 = ___2_match;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_11 = __this->____items;
		int32_t L_12 = V_1;
		NullCheck(L_11);
		int32_t L_13 = L_12;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_14 = (L_11)->GetAt(static_cast<il2cpp_array_size_t>(L_13));
		NullCheck(L_10);
		bool L_15;
		L_15 = Predicate_1_Invoke_m182E0ACAA85455151F5750CE87F942DA6E3DD3D6_inline(L_10, L_14, NULL);
		if (!L_15)
		{
			goto IL_0049;
		}
	}
	{
		int32_t L_16 = V_1;
		return L_16;
	}

IL_0049:
	{
		int32_t L_17 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_17, 1));
	}

IL_004d:
	{
		int32_t L_18 = V_1;
		int32_t L_19 = V_0;
		if ((((int32_t)L_18) < ((int32_t)L_19)))
		{
			goto IL_0033;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m1470DE92407E3BF5BB1D29A0650D78E98F4F5A6E (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		int32_t L_1 = __this->____size;
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_2 = ___0_match;
		int32_t L_3;
		L_3 = List_1_FindLastIndex_m5AE304D5E24C284C548257B8911E3E6ADD047585(__this, ((int32_t)il2cpp_codegen_subtract(L_0, 1)), L_1, L_2, NULL);
		return L_3;
	}
}
// Method Definition Index: 11380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_FindLastIndex_m5AE304D5E24C284C548257B8911E3E6ADD047585 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_startIndex, int32_t ___1_count, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___2_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_0 = ___2_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		int32_t L_1 = __this->____size;
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		int32_t L_2 = ___0_startIndex;
		if ((((int32_t)L_2) == ((int32_t)(-1))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
		goto IL_002a;
	}

IL_001c:
	{
		int32_t L_3 = ___0_startIndex;
		int32_t L_4 = __this->____size;
		if ((!(((uint32_t)L_3) >= ((uint32_t)L_4))))
		{
			goto IL_002a;
		}
	}
	{
		ThrowHelper_ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index_m94BADCC6D7EBBD12BE8323775FFB43AF01499B0F(NULL);
	}

IL_002a:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) < ((int32_t)0)))
		{
			goto IL_0036;
		}
	}
	{
		int32_t L_6 = ___0_startIndex;
		int32_t L_7 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_subtract(L_6, L_7)), 1))) >= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}

IL_0036:
	{
		ThrowHelper_ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count_m6C4A7F645BDD8EB62B50CB76E84416D94620DCC7(NULL);
	}

IL_003b:
	{
		int32_t L_8 = ___0_startIndex;
		int32_t L_9 = ___1_count;
		V_0 = ((int32_t)il2cpp_codegen_subtract(L_8, L_9));
		int32_t L_10 = ___0_startIndex;
		V_1 = L_10;
		goto IL_005d;
	}

IL_0043:
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_11 = ___2_match;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_12 = __this->____items;
		int32_t L_13 = V_1;
		NullCheck(L_12);
		int32_t L_14 = L_13;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_15 = (L_12)->GetAt(static_cast<il2cpp_array_size_t>(L_14));
		NullCheck(L_11);
		bool L_16;
		L_16 = Predicate_1_Invoke_m182E0ACAA85455151F5750CE87F942DA6E3DD3D6_inline(L_11, L_15, NULL);
		if (!L_16)
		{
			goto IL_0059;
		}
	}
	{
		int32_t L_17 = V_1;
		return L_17;
	}

IL_0059:
	{
		int32_t L_18 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_18, 1));
	}

IL_005d:
	{
		int32_t L_19 = V_1;
		int32_t L_20 = V_0;
		if ((((int32_t)L_19) > ((int32_t)L_20)))
		{
			goto IL_0043;
		}
	}
	{
		return (-1);
	}
}
// Method Definition Index: 11381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_ForEach_m5C1E0FFD399F81B5E18DE08684C9DE90A930234D (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, Action_1_t43B3A21E3139B3E2E49698F940F979F37AD1E63E* ___0_action, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Action_1_t43B3A21E3139B3E2E49698F940F979F37AD1E63E* L_0 = ___0_action;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)33), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____version;
		V_0 = L_1;
		V_1 = 0;
		goto IL_0034;
	}

IL_0015:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____version;
		if ((!(((uint32_t)L_2) == ((uint32_t)L_3))))
		{
			goto IL_003d;
		}
	}
	{
		Action_1_t43B3A21E3139B3E2E49698F940F979F37AD1E63E* L_4 = ___0_action;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_5 = __this->____items;
		int32_t L_6 = V_1;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		Action_1_Invoke_m1D3248EEA732AE8F2AA160F73A481C0102193D15_inline(L_4, L_8, NULL);
		int32_t L_9 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_9, 1));
	}

IL_0034:
	{
		int32_t L_10 = V_1;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0015;
		}
	}

IL_003d:
	{
		int32_t L_12 = V_0;
		int32_t L_13 = __this->____version;
		if ((((int32_t)L_12) == ((int32_t)L_13)))
		{
			goto IL_004b;
		}
	}
	{
		ThrowHelper_ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion_m5331E2E0EC0E36843D53F439C2529530595ACE9F(NULL);
	}

IL_004b:
	{
		return;
	}
}
// Method Definition Index: 11382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251 List_1_GetEnumerator_mBE57F88655594CAF946932CCDC95E8773FC4C4A6 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_mCAD56839187542C3B25E2C1493BBD62861333E4E((&L_0), __this, NULL);
		return L_0;
	}
}
// Method Definition Index: 11383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_Generic_IEnumerableU3CTU3E_GetEnumerator_m8956517C0AA90A843C73167BF6CF0156995ADB13 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_mCAD56839187542C3B25E2C1493BBD62861333E4E((&L_0), __this, NULL);
		Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_System_Collections_IEnumerable_GetEnumerator_m8D77CF72C1A81595B4557A40BD1877C3EC630AA3 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Enumerator__ctor_mCAD56839187542C3B25E2C1493BBD62861333E4E((&L_0), __this, NULL);
		Enumerator_t389EF10F90069ED924478E5694DFE1BB5CBCF251 L_1 = L_0;
		RuntimeObject* L_2 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 31), &L_1);
		return (RuntimeObject*)L_2;
	}
}
// Method Definition Index: 11385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* List_1_GetRange_m6988C6F3CBF3D50124DA99F2EA95F5A3263BD400 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* V_0 = NULL;
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* L_6 = (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0));
		List_1__ctor_m3F4723F3305F67834CE85E425907F53C7AB30484(L_6, L_5, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 33));
		V_0 = L_6;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* L_9 = V_0;
		NullCheck(L_9);
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_10 = L_9->____items;
		int32_t L_11 = ___1_count;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_7, L_8, (RuntimeArray*)L_10, 0, L_11, NULL);
		List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* L_12 = V_0;
		int32_t L_13 = ___1_count;
		NullCheck(L_12);
		L_12->____size = L_13;
		List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* L_14 = V_0;
		return L_14;
	}
}
// Method Definition Index: 11386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_IndexOf_mFA6A5CD427374237339591ED8A5218B6E7EA7F4C (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_0 = __this->____items;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_1 = ___0_item;
		int32_t L_2 = __this->____size;
		int32_t L_3;
		L_3 = Array_IndexOf_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m972A3CE5298A6193E512B34C64201444B051D8E9(L_0, L_1, 0, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 34));
		return L_3;
	}
}
// Method Definition Index: 11387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_System_Collections_IList_IndexOf_m3FAB7DEDF1376DCB40FF595ECE24A82D09D133E9 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_mA2F95E82C59734A475035EF3B9DD4BCE9F8429DB(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		int32_t L_3;
		L_3 = List_1_IndexOf_mFA6A5CD427374237339591ED8A5218B6E7EA7F4C(__this, ((*(OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Insert_mF138D66498A5DE06E21A7149C35FA14729F4F5E1 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___1_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_0012;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)13), ((int32_t)27), NULL);
	}

IL_0012:
	{
		int32_t L_2 = __this->____size;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_3 = __this->____items;
		NullCheck(L_3);
		int32_t L_4 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_3)->max_length),NULL));
		if ((!(((uint32_t)L_2) == ((uint32_t)L_4))))
		{
			goto IL_0030;
		}
	}
	{
		int32_t L_5 = __this->____size;
		List_1_EnsureCapacity_mB6EB012362AA419CAA8BF0C488F1010486273A69(__this, ((int32_t)il2cpp_codegen_add(L_5, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
	}

IL_0030:
	{
		int32_t L_6 = ___0_index;
		int32_t L_7 = __this->____size;
		if ((((int32_t)L_6) >= ((int32_t)L_7)))
		{
			goto IL_0056;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_8 = __this->____items;
		int32_t L_9 = ___0_index;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_10 = __this->____items;
		int32_t L_11 = ___0_index;
		int32_t L_12 = __this->____size;
		int32_t L_13 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_8, L_9, (RuntimeArray*)L_10, ((int32_t)il2cpp_codegen_add(L_11, 1)), ((int32_t)il2cpp_codegen_subtract(L_12, L_13)), NULL);
	}

IL_0056:
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_16 = ___1_item;
		NullCheck(L_14);
		(L_14)->SetAt(static_cast<il2cpp_array_size_t>(L_15), (OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837)L_16);
		int32_t L_17 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_17, 1));
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
		return;
	}
}
// Method Definition Index: 11389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Insert_m22B1E1E9ABC66027ABDF98D71BAC7AE153F67C15 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, RuntimeObject* ___1_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeObject* L_0 = ___1_item;
		ThrowHelper_IfNullAndNullsAreIllegalThenThrow_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m054615FD6A62EE7702B344B2E24BA8423FEF9621(L_0, ((int32_t)20), NULL);
	}
	try
	{
		int32_t L_1 = ___0_index;
		RuntimeObject* L_2 = ___1_item;
		List_1_Insert_mF138D66498A5DE06E21A7149C35FA14729F4F5E1(__this, L_1, ((*(OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
		goto IL_002a;
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0017;
		}
		throw e;
	}

CATCH_0017:
	{
		InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E* L_3 = ((InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*)IL2CPP_GET_ACTIVE_EXCEPTION(InvalidCastException_t47FC62F21A3937E814D20381DDACEF240E95AC2E*));;
		RuntimeObject* L_4 = ___1_item;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 10)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		ThrowHelper_ThrowWrongValueTypeArgumentException_mC1A6BBE43C360583C1E2C463D5B0AADF1E3E1910(L_4, L_6, NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_002a;
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_InsertRange_m9AA9E2C5627FB6CD9F3493CA8A99588EDA08125D (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, RuntimeObject* ___1_collection, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	int32_t V_1 = 0;
	RuntimeObject* V_2 = NULL;
	{
		RuntimeObject* L_0 = ___1_collection;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(6, NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___0_index;
		int32_t L_2 = __this->____size;
		if ((!(((uint32_t)L_1) > ((uint32_t)L_2))))
		{
			goto IL_0017;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_0017:
	{
		RuntimeObject* L_3 = ___1_collection;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_3, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2)));
		RuntimeObject* L_4 = V_0;
		if (!L_4)
		{
			goto IL_00bc;
		}
	}
	{
		RuntimeObject* L_5 = V_0;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_5);
		V_1 = L_6;
		int32_t L_7 = V_1;
		if ((((int32_t)L_7) <= ((int32_t)0)))
		{
			goto IL_00fb;
		}
	}
	{
		int32_t L_8 = __this->____size;
		int32_t L_9 = V_1;
		List_1_EnsureCapacity_mB6EB012362AA419CAA8BF0C488F1010486273A69(__this, ((int32_t)il2cpp_codegen_add(L_8, L_9)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
		int32_t L_10 = ___0_index;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) >= ((int32_t)L_11)))
		{
			goto IL_0066;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_12 = __this->____items;
		int32_t L_13 = ___0_index;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = V_1;
		int32_t L_17 = __this->____size;
		int32_t L_18 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_12, L_13, (RuntimeArray*)L_14, ((int32_t)il2cpp_codegen_add(L_15, L_16)), ((int32_t)il2cpp_codegen_subtract(L_17, L_18)), NULL);
	}

IL_0066:
	{
		RuntimeObject* L_19 = V_0;
		if ((!(((RuntimeObject*)(List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A*)__this) == ((RuntimeObject*)(RuntimeObject*)L_19))))
		{
			goto IL_009f;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_20 = __this->____items;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_21 = __this->____items;
		int32_t L_22 = ___0_index;
		int32_t L_23 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_20, 0, (RuntimeArray*)L_21, L_22, L_23, NULL);
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_24 = __this->____items;
		int32_t L_25 = ___0_index;
		int32_t L_26 = V_1;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_27 = __this->____items;
		int32_t L_28 = ___0_index;
		int32_t L_29 = __this->____size;
		int32_t L_30 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_24, ((int32_t)il2cpp_codegen_add(L_25, L_26)), (RuntimeArray*)L_27, ((int32_t)il2cpp_codegen_multiply(L_28, 2)), ((int32_t)il2cpp_codegen_subtract(L_29, L_30)), NULL);
		goto IL_00ac;
	}

IL_009f:
	{
		RuntimeObject* L_31 = V_0;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_32 = __this->____items;
		int32_t L_33 = ___0_index;
		NullCheck(L_31);
		InterfaceActionInvoker2< OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_31, L_32, L_33);
	}

IL_00ac:
	{
		int32_t L_34 = __this->____size;
		int32_t L_35 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_34, L_35));
		goto IL_00fb;
	}

IL_00bc:
	{
		int32_t L_36 = ___0_index;
		int32_t L_37 = __this->____size;
		if ((((int32_t)L_36) >= ((int32_t)L_37)))
		{
			goto IL_00f4;
		}
	}
	{
		RuntimeObject* L_38 = ___1_collection;
		NullCheck(L_38);
		RuntimeObject* L_39;
		L_39 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_38);
		V_2 = L_39;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00ea:
			{
				{
					RuntimeObject* L_40 = V_2;
					if (!L_40)
					{
						goto IL_00f3;
					}
				}
				{
					RuntimeObject* L_41 = V_2;
					NullCheck(L_41);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_41);
				}

IL_00f3:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00e0_1;
			}

IL_00ce_1:
			{
				int32_t L_42 = ___0_index;
				int32_t L_43 = L_42;
				___0_index = ((int32_t)il2cpp_codegen_add(L_43, 1));
				RuntimeObject* L_44 = V_2;
				NullCheck(L_44);
				OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_45;
				L_45 = InterfaceFuncInvoker0< OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_44);
				List_1_Insert_mF138D66498A5DE06E21A7149C35FA14729F4F5E1(__this, L_43, L_45, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 35));
			}

IL_00e0_1:
			{
				RuntimeObject* L_46 = V_2;
				NullCheck(L_46);
				bool L_47;
				L_47 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_46);
				if (L_47)
				{
					goto IL_00ce_1;
				}
			}
			{
				goto IL_00fb;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_00f4:
	{
		RuntimeObject* L_48 = ___1_collection;
		List_1_AddEnumerable_mA6E73354873BEAC5429A80AE43CB210589A7EC7E(__this, L_48, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 5));
	}

IL_00fb:
	{
		int32_t L_49 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_49, 1));
		return;
	}
}
// Method Definition Index: 11391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m02DCEAFFE826B31F80767A9D191CB4587D82F7BC (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_0 = ___0_item;
		int32_t L_1;
		L_1 = List_1_IndexOf_mFA6A5CD427374237339591ED8A5218B6E7EA7F4C(__this, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		V_0 = L_1;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) < ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		int32_t L_3 = V_0;
		List_1_RemoveAt_m30CC1D4CA1984DCAEDBCD2228B538A0503AF854A(__this, L_3, NULL);
		return (bool)1;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_System_Collections_IList_Remove_m2E22ECF19C2591E73B083E2B26EED5B2E4B8B3BB (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_item;
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = List_1_IsCompatibleObject_mA2F95E82C59734A475035EF3B9DD4BCE9F8429DB(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_item;
		bool L_3;
		L_3 = List_1_Remove_m02DCEAFFE826B31F80767A9D191CB4587D82F7BC(__this, ((*(OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 6)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 41));
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 11393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_RemoveAll_m99F96CEBE01EC91E6838BFE939CF7B3C41D67CAE (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* ___0_match, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_0 = ___0_match;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(8, NULL);
	}

IL_0009:
	{
		V_0 = 0;
		goto IL_0011;
	}

IL_000d:
	{
		int32_t L_1 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_1, 1));
	}

IL_0011:
	{
		int32_t L_2 = V_0;
		int32_t L_3 = __this->____size;
		if ((((int32_t)L_2) >= ((int32_t)L_3)))
		{
			goto IL_002e;
		}
	}
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_4 = ___0_match;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_5 = __this->____items;
		int32_t L_6 = V_0;
		NullCheck(L_5);
		int32_t L_7 = L_6;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_8 = (L_5)->GetAt(static_cast<il2cpp_array_size_t>(L_7));
		NullCheck(L_4);
		bool L_9;
		L_9 = Predicate_1_Invoke_m182E0ACAA85455151F5750CE87F942DA6E3DD3D6_inline(L_4, L_8, NULL);
		if (!L_9)
		{
			goto IL_000d;
		}
	}

IL_002e:
	{
		int32_t L_10 = V_0;
		int32_t L_11 = __this->____size;
		if ((((int32_t)L_10) < ((int32_t)L_11)))
		{
			goto IL_0039;
		}
	}
	{
		return 0;
	}

IL_0039:
	{
		int32_t L_12 = V_0;
		V_1 = ((int32_t)il2cpp_codegen_add(L_12, 1));
		goto IL_0089;
	}

IL_003f:
	{
		int32_t L_13 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_13, 1));
	}

IL_0043:
	{
		int32_t L_14 = V_1;
		int32_t L_15 = __this->____size;
		if ((((int32_t)L_14) >= ((int32_t)L_15)))
		{
			goto IL_0060;
		}
	}
	{
		Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* L_16 = ___0_match;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_17 = __this->____items;
		int32_t L_18 = V_1;
		NullCheck(L_17);
		int32_t L_19 = L_18;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_20 = (L_17)->GetAt(static_cast<il2cpp_array_size_t>(L_19));
		NullCheck(L_16);
		bool L_21;
		L_21 = Predicate_1_Invoke_m182E0ACAA85455151F5750CE87F942DA6E3DD3D6_inline(L_16, L_20, NULL);
		if (L_21)
		{
			goto IL_003f;
		}
	}

IL_0060:
	{
		int32_t L_22 = V_1;
		int32_t L_23 = __this->____size;
		if ((((int32_t)L_22) >= ((int32_t)L_23)))
		{
			goto IL_0089;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_24 = __this->____items;
		int32_t L_25 = V_0;
		int32_t L_26 = L_25;
		V_0 = ((int32_t)il2cpp_codegen_add(L_26, 1));
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_27 = __this->____items;
		int32_t L_28 = V_1;
		int32_t L_29 = L_28;
		V_1 = ((int32_t)il2cpp_codegen_add(L_29, 1));
		NullCheck(L_27);
		int32_t L_30 = L_29;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_31 = (L_27)->GetAt(static_cast<il2cpp_array_size_t>(L_30));
		NullCheck(L_24);
		(L_24)->SetAt(static_cast<il2cpp_array_size_t>(L_26), (OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837)L_31);
	}

IL_0089:
	{
		int32_t L_32 = V_1;
		int32_t L_33 = __this->____size;
		if ((((int32_t)L_32) < ((int32_t)L_33)))
		{
			goto IL_0043;
		}
	}
	{
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_34 = __this->____items;
		int32_t L_35 = V_0;
		int32_t L_36 = __this->____size;
		int32_t L_37 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_34, L_35, ((int32_t)il2cpp_codegen_subtract(L_36, L_37)), NULL);
	}

IL_00ad:
	{
		int32_t L_38 = __this->____size;
		int32_t L_39 = V_0;
		int32_t L_40 = V_0;
		__this->____size = L_40;
		int32_t L_41 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_41, 1));
		return ((int32_t)il2cpp_codegen_subtract(L_38, L_39));
	}
}
// Method Definition Index: 11394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m30CC1D4CA1984DCAEDBCD2228B538A0503AF854A (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____size;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRange_IndexException_mC7661B8C6DD827679C95FC6B6C08F8724174936D(NULL);
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_2, 1));
		int32_t L_3 = ___0_index;
		int32_t L_4 = __this->____size;
		if ((((int32_t)L_3) >= ((int32_t)L_4)))
		{
			goto IL_0042;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_5 = __this->____items;
		int32_t L_6 = ___0_index;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_7 = __this->____items;
		int32_t L_8 = ___0_index;
		int32_t L_9 = __this->____size;
		int32_t L_10 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_5, ((int32_t)il2cpp_codegen_add(L_6, 1)), (RuntimeArray*)L_7, L_8, ((int32_t)il2cpp_codegen_subtract(L_9, L_10)), NULL);
	}

IL_0042:
	{
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_11 = __this->____items;
		int32_t L_12 = __this->____size;
		il2cpp_codegen_initobj((&V_0), sizeof(OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837));
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_13 = V_0;
		NullCheck(L_11);
		(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_12), (OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837)L_13);
	}

IL_0063:
	{
		int32_t L_14 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_14, 1));
		return;
	}
}
// Method Definition Index: 11395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveRange_mC34416B52737986556034E2111A5817EDEE0C189 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_008d;
		}
	}
	{
		int32_t L_6 = __this->____size;
		int32_t L_7 = __this->____size;
		int32_t L_8 = ___1_count;
		__this->____size = ((int32_t)il2cpp_codegen_subtract(L_7, L_8));
		int32_t L_9 = ___0_index;
		int32_t L_10 = __this->____size;
		if ((((int32_t)L_9) >= ((int32_t)L_10)))
		{
			goto IL_0066;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_11 = __this->____items;
		int32_t L_12 = ___0_index;
		int32_t L_13 = ___1_count;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_14 = __this->____items;
		int32_t L_15 = ___0_index;
		int32_t L_16 = __this->____size;
		int32_t L_17 = ___0_index;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_11, ((int32_t)il2cpp_codegen_add(L_12, L_13)), (RuntimeArray*)L_14, L_15, ((int32_t)il2cpp_codegen_subtract(L_16, L_17)), NULL);
	}

IL_0066:
	{
		int32_t L_18 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_18, 1));
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_19 = __this->____items;
		int32_t L_20 = __this->____size;
		int32_t L_21 = ___1_count;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_19, L_20, L_21, NULL);
	}

IL_008d:
	{
		return;
	}
}
// Method Definition Index: 11396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_mFF087DD699EA7B98F8636ED593ED83D64ABE078F (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m905EB6FD0B91CEE1CC0165F1EBB8C1309AEE4BEC_inline(__this, NULL);
		List_1_Reverse_m5903E8D5CE5B92A29530763E182314BDFA781664(__this, 0, L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 42));
		return;
	}
}
// Method Definition Index: 11397
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Reverse_m5903E8D5CE5B92A29530763E182314BDFA781664 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0038;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		Array_Reverse_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_mDCB49D07F30FD9D952124968159FDF346234BD04(L_6, L_7, L_8, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 43));
	}

IL_0038:
	{
		int32_t L_9 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_9, 1));
		return;
	}
}
// Method Definition Index: 11398
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m23AEAE23D4485673CD52C25B3A593A7A97579CA6 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m905EB6FD0B91CEE1CC0165F1EBB8C1309AEE4BEC_inline(__this, NULL);
		List_1_Sort_mEBED487B845A66E3298CA30654B7EC17F7481995(__this, 0, L_0, (RuntimeObject*)NULL, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mC839D031A5AAA84A68243F93E39544BA3A966203 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0;
		L_0 = List_1_get_Count_m905EB6FD0B91CEE1CC0165F1EBB8C1309AEE4BEC_inline(__this, NULL);
		RuntimeObject* L_1 = ___0_comparer;
		List_1_Sort_mEBED487B845A66E3298CA30654B7EC17F7481995(__this, 0, L_0, L_1, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 44));
		return;
	}
}
// Method Definition Index: 11400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_mEBED487B845A66E3298CA30654B7EC17F7481995 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, int32_t ___0_index, int32_t ___1_count, RuntimeObject* ___2_comparer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0009:
	{
		int32_t L_1 = ___1_count;
		if ((((int32_t)L_1) >= ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_m5430BC879B9D99E39DED788BB9451B8EF029756F(((int32_t)16), 4, NULL);
	}

IL_0015:
	{
		int32_t L_2 = __this->____size;
		int32_t L_3 = ___0_index;
		int32_t L_4 = ___1_count;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) >= ((int32_t)L_4)))
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(((int32_t)23), NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_count;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_0039;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_6 = __this->____items;
		int32_t L_7 = ___0_index;
		int32_t L_8 = ___1_count;
		RuntimeObject* L_9 = ___2_comparer;
		Array_Sort_TisOrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837_m438D9EDDBF93A09C81E89458C2362F85487923EC(L_6, L_7, L_8, L_9, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 45));
	}

IL_0039:
	{
		int32_t L_10 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_10, 1));
		return;
	}
}
// Method Definition Index: 11401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_Sort_m014D10ACC5ECFB795A9812357A6AD9FFF19F3C47 (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, Comparison_1_tC8BBA462D5337723AC34773EEE343018753C7FF3* ___0_comparison, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit);
	//<source_info:<no-source>:1>
	{
		Comparison_1_tC8BBA462D5337723AC34773EEE343018753C7FF3* L_0 = ___0_comparison;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(((int32_t)34), NULL);
	}

IL_000a:
	{
		int32_t L_1 = __this->____size;
		if ((((int32_t)L_1) <= ((int32_t)1)))
		{
			goto IL_0026;
		}
	}
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_2 = __this->____items;
		int32_t L_3 = __this->____size;
		Comparison_1_tC8BBA462D5337723AC34773EEE343018753C7FF3* L_4 = ___0_comparison;
		CHECKED_LOCAL_INIT(ArraySortHelper_1_t0B69B36E8E1F97490E6BE32C23AE1784AB1ADA3A_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 47)),il2cpp_codegen_runtime_class_init_inline);
		ArraySortHelper_1_Sort_m4A8362BBA870329F45001F17310B50E583BFF19F(L_2, 0, L_3, L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 46));
	}

IL_0026:
	{
		int32_t L_5 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_5, 1));
		return;
	}
}
// Method Definition Index: 11402
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* List_1_ToArray_m5E0DDBEF3289BA470AD71416C050605CE275FF3B (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit);
	//<source_info:<no-source>:1>
	OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* V_0 = NULL;
	{
		int32_t L_0 = __this->____size;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CHECKED_LOCAL_INIT(List_1_tA054E80C004998BFCB1ED5377160B32E4A19B78C_StaticInit,(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)),il2cpp_codegen_runtime_class_init_inline);
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_1 = ((List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 0)))->___s_emptyArray;
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____size;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_3 = (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)(OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)SZArrayNew(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 1), (uint32_t)L_2);
		V_0 = L_3;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_4 = __this->____items;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_5 = V_0;
		int32_t L_6 = __this->____size;
		Array_Copy_mB4904E17BD92E320613A3251C0205E0786B3BF41((RuntimeArray*)L_4, 0, (RuntimeArray*)L_5, 0, L_6, NULL);
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_7 = V_0;
		return L_7;
	}
}
// Method Definition Index: 11403
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_TrimExcess_m69153C4A7944BFD9A4B05B1269114DA8E499764B (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_0 = __this->____items;
		NullCheck(L_0);
		int32_t L_1 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_0)->max_length),NULL));
		double L_2 = (il2cpp_codegen_conv<double,int32_t,int32_t,false,false>(L_1,NULL));
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,double,double,false,false>(((double)il2cpp_codegen_multiply(L_2, (0.90000000000000002))),NULL));
		V_0 = L_3;
		int32_t L_4 = __this->____size;
		int32_t L_5 = V_0;
		if ((((int32_t)L_4) >= ((int32_t)L_5)))
		{
			goto IL_002a;
		}
	}
	{
		int32_t L_6 = __this->____size;
		List_1_set_Capacity_mC49015D45A78A39909C4541BDB84DAA369E6A4C1(__this, L_6, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25));
	}

IL_002a:
	{
		return;
	}
}
// Method Definition Index: 11404
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddEnumerable_mA6E73354873BEAC5429A80AE43CB210589A7EC7E (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, RuntimeObject* ___0_enumerable, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		RuntimeObject* L_1 = ___0_enumerable;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 36), L_1);
		V_0 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0063:
			{
				{
					RuntimeObject* L_3 = V_0;
					if (!L_3)
					{
						goto IL_006c;
					}
				}
				{
					RuntimeObject* L_4 = V_0;
					NullCheck(L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_4);
				}

IL_006c:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0059_1;
			}

IL_0017_1:
			{
				RuntimeObject* L_5 = V_0;
				NullCheck(L_5);
				OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_6;
				L_6 = InterfaceFuncInvoker0< OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 38), L_5);
				V_1 = L_6;
				int32_t L_7 = __this->____size;
				OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_8 = __this->____items;
				NullCheck(L_8);
				int32_t L_9 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_8)->max_length),NULL));
				if ((!(((uint32_t)L_7) == ((uint32_t)L_9))))
				{
					goto IL_003c_1;
				}
			}
			{
				int32_t L_10 = __this->____size;
				List_1_EnsureCapacity_mB6EB012362AA419CAA8BF0C488F1010486273A69(__this, ((int32_t)il2cpp_codegen_add(L_10, 1)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 12));
			}

IL_003c_1:
			{
				OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_11 = __this->____items;
				int32_t L_12 = __this->____size;
				V_2 = L_12;
				int32_t L_13 = V_2;
				__this->____size = ((int32_t)il2cpp_codegen_add(L_13, 1));
				int32_t L_14 = V_2;
				OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_15 = V_1;
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(L_14), (OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837)L_15);
			}

IL_0059_1:
			{
				RuntimeObject* L_16 = V_0;
				NullCheck(L_16);
				bool L_17;
				L_17 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_16);
				if (L_17)
				{
					goto IL_0017_1;
				}
			}
			{
				goto IL_006d;
			}
		}
		catch(Il2CppNativeThreadAbortException&)
		{
			__finallyBlock.SetNativeThreadAbortOccurred();
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_006d:
	{
		return;
	}
}
// Method Definition Index: 11405
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__cctor_mBA1A334B4DB00D75CD4BF205DE68E28F92477FF0 (const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_0 = (OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)(OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1), (uint32_t)0);
		((List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)))->___s_emptyArray), (void*)L_0);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Method Definition Index: 11360
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m50157FAAF8FE0562A862140A17AD2B1097B078E7_inline (List_1_tB5E6131382BD8F3FB4090AD7EA52B72231DBDDCB* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		XRFeatureDescriptorU5BU5D_tBB735747E74DA05050B124BFE3525E40EC4114A7* L_7 = V_0;
		int32_t L_8 = V_1;
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2)L_9);
		return;
	}

IL_0034:
	{
		XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 L_10 = ___0_item;
		List_1_AddWithResize_m55AFD45DC0ADD64C8749E1B59252B0BF0793CE49(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 11349
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mD2ED26ACAF3BAF386FFEA83893BA51DB9FD8BA30_fshared_inline (List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____size;
		return L_0;
	}
}
// Method Definition Index: 925
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m91BF5E52B5443EC259EE465ECFE80677DA340F98_inline (Predicate_1_tB9999C9C9C42FA89BBE6865404B6372BA3E8F5F5* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*FunctionPointerType) (RuntimeObject*, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 887
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m14B5F0EFFF227D076151E00CACE917B6512254B8_inline (Action_1_tC8CC18F6848AAE74469E78A12EC004873F4332A7* __this, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2 ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, XRFeatureDescriptor_t690E672A6BBBEFBB8AF221485C1884507EAFB5B2, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 11360
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m0CE6E39234ED33F5195AABC8029D06D4756C75C1_gshared_inline (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, Il2CppSharedGenericObject* ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_7 = V_0;
		int32_t L_8 = V_1;
		Il2CppSharedGenericObject* L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (Il2CppSharedGenericObject*)L_9);
		return;
	}

IL_0034:
	{
		Il2CppSharedGenericObject* L_10 = ___0_item;
		List_1_AddWithResize_mCC24A812DFC30C3DE2230E3C8EE705871C1DFF81(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 925
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_mDF332C0CEB003C0D86A1E3A194ECFA96D424E34C_gshared_inline (Predicate_1_t586817D6627D147076FFDCC73DA7BEC23132FA88* __this, Il2CppSharedGenericObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*FunctionPointerType) (RuntimeObject*, Il2CppSharedGenericObject*, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 887
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mDB8F7C7381C08B4DCE4E409D2FA4073BC17F90B5_gshared_inline (Action_1_tF0E1373A391479E6E235E0D82CE1EF1DE6B6C505* __this, Il2CppSharedGenericObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, Il2CppSharedGenericObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 11360
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m24BA4DEDED426A5B0C6A4AB3E523C876B0B918A6_gshared_inline (List_1_t4225839FAF98FA4E886FF2A1469F9292D739E255* __this, int32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		__Il2CppInt32EnumU5BU5D_t8FBDB13799B3615AA893F06F601FF09189B04297* L_7 = V_0;
		int32_t L_8 = V_1;
		int32_t L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (int32_t)L_9);
		return;
	}

IL_0034:
	{
		int32_t L_10 = ___0_item;
		List_1_AddWithResize_m3B53F17525936469654379AC0F1F0B19EC3DCCE5(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 925
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m0D65AFF43065FE03318CBDDB2A1E63B51476FB9A_gshared_inline (Predicate_1_tEFC0F0C58D1FDF98911A6A55B2C9A9F6A4AB3EE2* __this, int32_t ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 887
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mE118EB8C2BFC9D370FE37C05DE4F2CD6CBE9DB54_gshared_inline (Action_1_t5FD82308A537DD6AD12A51AF0790FA13C5BE2D65* __this, int32_t ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 11360
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mBDE95F2D32400D0DABA22511FE053AC8957D8D1E_gshared_inline (List_1_t805EF1ED5A4BE78B5A8442FDD522CC8CF0D82F75* __this, uint32_t ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		__Il2CppUInt32EnumU5BU5D_t59DD627323F45C05F28FE49E0DEA327606079F43* L_7 = V_0;
		int32_t L_8 = V_1;
		uint32_t L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (uint32_t)L_9);
		return;
	}

IL_0034:
	{
		uint32_t L_10 = ___0_item;
		List_1_AddWithResize_m5D090D0E0EFAAA54D1A7D12657EE18BA7AD0F445(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 925
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m2603DCEFACCFCB054A6BC33E68531E62F317C8A4_gshared_inline (Predicate_1_t0C015E6FC5D077221ED7DAF42CA78AC4B3BCA716* __this, uint32_t ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*FunctionPointerType) (RuntimeObject*, uint32_t, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 887
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m124DA039CD5C63AD48841B21065DFC6C03E63930_gshared_inline (Action_1_t6E017BD45EFAD0010771D0174C3A5653E776128E* __this, uint32_t ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, uint32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 11360
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m641521748E28764A6131ED25D555ABC4FF9793D6_inline (List_1_tE5F26F978E0CC0A731A4FA2B7B4F7D620D6250E6* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		float3U5BU5D_tD4EB6079482A23C25E32ADEEB6D23996502A6251* L_7 = V_0;
		int32_t L_8 = V_1;
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (float3_t7600B73F092B37F484B12910A5269F30C778D31E)L_9);
		return;
	}

IL_0034:
	{
		float3_t7600B73F092B37F484B12910A5269F30C778D31E L_10 = ___0_item;
		List_1_AddWithResize_m5228B46F845892F0F239CC9D7314DF4BA90F6DDE(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 925
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_mBED251F2E28E4422575173E72B868C1A24BD5285_inline (Predicate_1_t6FFF9DF3DE0AB84C49267B4454CCFF00D954FAEA* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*FunctionPointerType) (RuntimeObject*, float3_t7600B73F092B37F484B12910A5269F30C778D31E, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 887
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mDD4A1B1BE4B3607CBA93CC29D3C64CB95A6143CB_inline (Action_1_t9BCEE3E1405DA67DBE5A4E9F0882507B63BCEC2F* __this, float3_t7600B73F092B37F484B12910A5269F30C778D31E ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, float3_t7600B73F092B37F484B12910A5269F30C778D31E, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 11360
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m184A228230F65332F56708CBA3F3A88489B94D66_inline (List_1_t34937A123BB6F1345F58E1BE04611AC43FA110C2* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		WeightInfoU5BU5D_tA309FB0796574412C67FC42ACC7519BC99AC7A80* L_7 = V_0;
		int32_t L_8 = V_1;
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0)L_9);
		return;
	}

IL_0034:
	{
		WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 L_10 = ___0_item;
		List_1_AddWithResize_m843E590839D8B44E5BF14AB39E42CAE86E2AEC9E(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 925
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_mAB88F92ABF8A77B99B3BC9C74D9C9A83878CBE7F_inline (Predicate_1_tBFE57AFB40F122ABA33A14C3E1EED7AAE31221E1* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*FunctionPointerType) (RuntimeObject*, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 887
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m1AA93F8CEDADD37A0C262AE89FCDFCF1102FC270_inline (Action_1_t7D3408AF7B9CB908929DA7A8A01F6231B5EE5413* __this, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0 ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, WeightInfo_t9942B0D2C77A00A5C9824732AEAA0AB0A55620B0, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 11360
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mC35682C02D89984B864B24B3BB4F92B5C2374273_inline (List_1_t55FBBCA3B61F4092747F5BB1DE63DCF56C27DE50* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		AwaitableAndFrameIndexU5BU5D_tFE5E09101F753E8B28D639C161FBC4D34F939560* L_7 = V_0;
		int32_t L_8 = V_1;
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161)L_9);
		return;
	}

IL_0034:
	{
		AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 L_10 = ___0_item;
		List_1_AddWithResize_mCA55981D47453F8483BD503CA53B0DABBDB8844D(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 925
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m8D40905BAEF5849C9DF55268BB4FD2C8CB0A1A79_inline (Predicate_1_t3F38457B9EC912152CADD8A303B97559060B734C* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*FunctionPointerType) (RuntimeObject*, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 887
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m87599A17D9A01B7E75A9868586C2C91F0AB8FC85_inline (Action_1_t24D24AE7BDA27ABE5E9058347421CD45845804B3* __this, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161 ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, AwaitableAndFrameIndex_t5B42EA520B0415D199F9487706ABCD53407C0161, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 11360
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mB308EE5BFFD89A7B8C251644D76CA547456F3012_inline (List_1_tB663AF9B0DBD0D685E595C4B50CF1535137C768A* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) < ((uint32_t)L_5))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_6 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_6, 1));
		OrderBlockU5BU5D_tD67C39FB212889AFAAB991BE1AE144ED8D35EE88* L_7 = V_0;
		int32_t L_8 = V_1;
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_9 = ___0_item;
		NullCheck(L_7);
		(L_7)->SetAt(static_cast<il2cpp_array_size_t>(L_8), (OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837)L_9);
		return;
	}

IL_0034:
	{
		OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 L_10 = ___0_item;
		List_1_AddWithResize_m8B3D79788ADC2B54C9B12CC9EC1158E6A04CDEBC(__this, L_10, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return;
	}
}
// Method Definition Index: 925
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Predicate_1_Invoke_m182E0ACAA85455151F5750CE87F942DA6E3DD3D6_inline (Predicate_1_t8194F57B81937B278B5EFEF0F5B667BBB054B0D0* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef bool (*FunctionPointerType) (RuntimeObject*, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837, const RuntimeMethod*);
	return ((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 887
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_m1D3248EEA732AE8F2AA160F73A481C0102193D15_inline (Action_1_t43B3A21E3139B3E2E49698F940F979F37AD1E63E* __this, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837 ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, OrderBlock_t62FD6F6544F34B5298DEF2F77AAE446F269B7837, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
