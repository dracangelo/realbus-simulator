#include "pch-cpp.hpp"





template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*,T1,const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj,p1,invokeData.method);
	}
};
template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*,const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj,invokeData.method);
	}
};
template <typename R, typename T1>
struct VirtualFuncInvoker1
{
	typedef R (*Func)(void*,T1,const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj,p1,invokeData.method);
	}
};
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*,const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj,invokeData.method);
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
template <typename T1, typename T2>
struct InvokerActionInvoker2<T1*, T2*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2)
	{
		void* params[2] = { p1, p2 };
		method->invoker_method(methodPtr, method, obj, params, params[1]);
	}
};
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3;
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3<T1, T2*, T3*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1 p1, T2* p2, T3* p3)
	{
		void* params[3] = { &p1, p2, p3 };
		method->invoker_method(methodPtr, method, obj, params, params[2]);
	}
};
template <typename T1, typename T2, typename T3>
struct InvokerActionInvoker3<T1*, T2*, T3*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2, T3* p3)
	{
		void* params[3] = { p1, p2, p3 };
		method->invoker_method(methodPtr, method, obj, params, params[2]);
	}
};

struct U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6;
struct U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5;
struct ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79;
struct Dictionary_2_t54C1839F4EE32A546D24F6AD598F3A048EE2AB9A;
struct Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83;
struct Dictionary_2_tE7CDB026141A092A986D0DA3CB2DFCE6BB552069;
struct Dictionary_2_t0E78110F7AC151EB839DC1E384451EB29D89A92A;
struct Dictionary_2_t5C32AF17A5801FB3109E5B0E622BA8402A04E08E;
struct Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A;
struct IEnumerable_1_tCE758D940790D6D0D56B457E522C195F8C413AF2;
struct IEnumerable_1_t349E66EC5F09B881A8E52EE40A1AB9EC60E08E44;
struct IEnumerable_1_t6D5FFB1B4A97F30426C5D394135AD0D2FAEAE680;
struct IEnumerable_1_t29E7244AE33B71FA0981E50D5BC73B7938F35C66;
struct IEnumerator_1_t73FD060C436E3C4264A734C8F8DCC01DFF6046B8;
struct IEqualityComparer_1_tC153F65F278AE0C32BA0CC809B12AA724AF65F85;
struct KeyCollection_t59AB6D7A56AAB8B527E4EEBED39397629A9430E0;
struct KeyCollection_t5E448F9EF31B8C6F2D80CA7C7858AB780AB912A9;
struct LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599;
struct List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14;
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD;
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E;
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A;
struct Queue_1_tF7C2F79F3487A05259C04F0FA9E0DE6DB85009FF;
struct Queue_1_t0213AA1BB5D12D3BCD5727E71F92BE13870D391A;
struct ValueCollection_tB7C740A97431C55C62CC43DA7712B5896DBED97F;
struct ValueCollection_t7F798658A969389BB4AAA8E1ECC40A4682EF43A9;
struct EntryU5BU5D_tAABC49AF86D770891D85331C61A2CF1F7BCB6899;
struct ActiveLifecycleScopeBaseU5BU5D_t838742F7550A7877BD0F3E2C6EB34055C35B57B2;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
struct ScopeTransitionRequestBaseU5BU5D_t607E74D78847B27124900B6FFF0ED216E09E95C7;
struct ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C;
struct ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7;
struct AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03;
struct AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0;
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D;
struct AnimationClip_t00BD2F131D308A4AD2C6B0BF66644FC25FECE712;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct Exception_t;
struct GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct INativeCallbackProvider_t9C3AAA36B250E1A3DD05DF6F0A4C236CA6DD6DFF;
struct ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38;
struct LifecycleMethodRegistry_t683994E033865F86C96A14E5F2674E6AD8B97EA8;
struct LifecycleScope_tEA98D004E7CFF981DC51524B020E4CBD58D964CB;
struct LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct MethodInfo_t;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347;
struct StackOrderedAssemblyList_t57E870538D7E4DC7B2168FFB75368CE0C906AF97;
struct String_t;
struct Type_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16;

IL2CPP_EXTERN_C RuntimeClass* AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerable_1_t349E66EC5F09B881A8E52EE40A1AB9EC60E08E44_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_1_t73FD060C436E3C4264A734C8F8DCC01DFF6046B8_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RuntimeArray_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* String_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* _AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral00B9D6CD2F31410E688E8B500A17CB53A5159DC7;
IL2CPP_EXTERN_C String_t* _stringLiteral085E2DF95CBA541D8E08411BA60759DB8E782A9E;
IL2CPP_EXTERN_C String_t* _stringLiteral13E8ABAF4FCA9340FD639F44DC79CC73FDE724C1;
IL2CPP_EXTERN_C String_t* _stringLiteral190A3A7EF1BCDD282CB0B05FB99BEDDD2405B809;
IL2CPP_EXTERN_C String_t* _stringLiteral1D86DE1FC0445C55F712ED0F6D3E32FED0A1E724;
IL2CPP_EXTERN_C String_t* _stringLiteral24A0D1C954F6F30A64E2AB214561ABB5C363D424;
IL2CPP_EXTERN_C String_t* _stringLiteral3EE5A4671A4E5AEBD31CA5F013A7773DC18ED22B;
IL2CPP_EXTERN_C String_t* _stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D;
IL2CPP_EXTERN_C String_t* _stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92;
IL2CPP_EXTERN_C String_t* _stringLiteral50E02893F5B6939534A49C535241D6B7BAFCF599;
IL2CPP_EXTERN_C String_t* _stringLiteral84DED65F0B23E6F91065E5F2EFCC5631A498E3E4;
IL2CPP_EXTERN_C String_t* _stringLiteral9C503A4CDCF098739894D1E4B88AEFF08E94B305;
IL2CPP_EXTERN_C String_t* _stringLiteral9F7B27B5BF4E1DCCB7582CE12885371A3C21BF8C;
IL2CPP_EXTERN_C String_t* _stringLiteralA4EC0E533020275EA02CC859DA22C7578B5C0119;
IL2CPP_EXTERN_C String_t* _stringLiteralB9EDD49DDBC303A2087668B4B6E4ACAC60995136;
IL2CPP_EXTERN_C String_t* _stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E;
IL2CPP_EXTERN_C String_t* _stringLiteralC5C3A106612C3457D1A199E43E729AE62EDAD988;
IL2CPP_EXTERN_C String_t* _stringLiteralCC59788671EF0B981BF46F9F140D4B1454A1146D;
IL2CPP_EXTERN_C String_t* _stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D;
IL2CPP_EXTERN_C String_t* _stringLiteralDC955F06F1DA8A3B06C4755211822B06D698CF11;
IL2CPP_EXTERN_C String_t* _stringLiteralF0C0210C1550DE6C68F93F6E0EEC8AD3FE4F23F3;
IL2CPP_EXTERN_C const RuntimeMethod* AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Add_mA42C372CA350E01B08326C78A32C2D4331199A3D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Remove_mAE2ECC0F5BDAA58524500656449EA1A387079726_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_TryGetValue_m23A14CBF4DD0FFE6985742AB5960E012A3D29D89_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_get_Keys_m9CB10267CF4FC83CDDD3FDC4B898C73488C93FB8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_get_Values_m79563C16853F891CCBBC22B07DC63DA063874A49_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_m4CA597288EC15016493B62A210A4A63E27177117_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_m132E4E286C55B63A011879B1F62299A7E6CF8DE2_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_0_0_0_var;
IL2CPP_EXTERN_C const RuntimeType* LifecycleScope_tEA98D004E7CFF981DC51524B020E4CBD58D964CB_0_0_0_var;
IL2CPP_EXTERN_C const RuntimeType* RuntimeArray_0_0_0_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6  : public RuntimeObject
{
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* ___androidJavaList;
};
struct U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5  : public RuntimeObject
{
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* ___androidJavaList;
};
struct Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83  : public RuntimeObject
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets;
	EntryU5BU5D_tAABC49AF86D770891D85331C61A2CF1F7BCB6899* ____entries;
	int32_t ____count;
	int32_t ____freeList;
	int32_t ____freeCount;
	int32_t ____version;
	RuntimeObject* ____comparer;
	KeyCollection_t59AB6D7A56AAB8B527E4EEBED39397629A9430E0* ____keys;
	ValueCollection_tB7C740A97431C55C62CC43DA7712B5896DBED97F* ____values;
	RuntimeObject* ____syncRoot;
};
struct EmptyArray_1_tF2F83411E5DC963DBFFD1F75B10B58A1ED224DD5  : public RuntimeObject
{
};
struct KeyCollection_t59AB6D7A56AAB8B527E4EEBED39397629A9430E0  : public RuntimeObject
{
	Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* ____dictionary;
};
struct List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14  : public RuntimeObject
{
	ActiveLifecycleScopeBaseU5BU5D_t838742F7550A7877BD0F3E2C6EB34055C35B57B2* ____items;
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
struct Queue_1_t0213AA1BB5D12D3BCD5727E71F92BE13870D391A  : public RuntimeObject
{
	ScopeTransitionRequestBaseU5BU5D_t607E74D78847B27124900B6FFF0ED216E09E95C7* ____array;
	int32_t ____head;
	int32_t ____tail;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct ValueCollection_tB7C740A97431C55C62CC43DA7712B5896DBED97F  : public RuntimeObject
{
	Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* ____dictionary;
};
struct ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C  : public RuntimeObject
{
	LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* ___U3CScopeU3Ek__BackingField;
};
struct ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7  : public RuntimeObject
{
	Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* ____activeScopes;
	ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347* ____scopeTransitionHelper;
	Queue_1_t0213AA1BB5D12D3BCD5727E71F92BE13870D391A* ____transitionRequestQueue;
	Dictionary_2_t54C1839F4EE32A546D24F6AD598F3A048EE2AB9A* ____autoCleanups;
	RuntimeObject* ____autoCleanupsLock;
};
struct AndroidJNI_t531BC9A6383F7C0F76A1270297952462F52308EE  : public RuntimeObject
{
};
struct AndroidJNIHelper_t2C1AB9F6B2295C20B24108936A003F65F02D71DD  : public RuntimeObject
{
};
struct AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0  : public RuntimeObject
{
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___m_jobject;
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___m_jclass;
};
struct AndroidJavaObjectExtensions_tEAA7955C5DF9AD87054BEF3EED0F48A9565A0C1A  : public RuntimeObject
{
};
struct AnimationPlayableExtensions_t3F93F70729DACA99F423992CFC1B380C1942AD4D  : public RuntimeObject
{
};
struct DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11  : public RuntimeObject
{
};
struct LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755  : public RuntimeObject
{
	String_t* ___U3CNameU3Ek__BackingField;
	List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* ___U3CExplicitRequiredOuterScopesU3Ek__BackingField;
	bool ___U3CAllowNestedTransitionsU3Ek__BackingField;
	ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* ___U3CImplicitOuterScopesU3Ek__BackingField;
};
struct MemberInfo_t  : public RuntimeObject
{
};
struct ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347  : public RuntimeObject
{
	StackOrderedAssemblyList_t57E870538D7E4DC7B2168FFB75368CE0C906AF97* ____assemblyList;
	LifecycleMethodRegistry_t683994E033865F86C96A14E5F2674E6AD8B97EA8* ____lifecycleMethodRegistry;
	Dictionary_2_t0E78110F7AC151EB839DC1E384451EB29D89A92A* ____processMarkers;
	RuntimeObject* ___U3CNativeCallbackProviderU3Ek__BackingField;
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
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
struct Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352 
{
	List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14* ____list;
	int32_t ____index;
	int32_t ____version;
	ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* ____current;
};
struct Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9 
{
	List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* ____list;
	int32_t ____index;
	int32_t ____version;
	Il2CppSharedGenericObject* ____current;
};
struct Enumerator_t539AC62A1BE9934C67FD41CBB84103B4E3E98AC1 
{
	Queue_1_tF7C2F79F3487A05259C04F0FA9E0DE6DB85009FF* ____q;
	int32_t ____version;
	int32_t ____index;
	Il2CppSharedGenericObject* ____currentElement;
};
typedef Il2CppFullySharedGenericStruct Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF;
struct Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201 
{
	Queue_1_t0213AA1BB5D12D3BCD5727E71F92BE13870D391A* ____q;
	int32_t ____version;
	int32_t ____index;
	ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* ____currentElement;
};
struct Enumerator_t624742B8D885649511A5739F11184E2221942252 
{
	Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* ____dictionary;
	int32_t ____index;
	int32_t ____version;
	ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* ____currentValue;
};
struct Enumerator_tADBE53A4D403A692FEE79405C385A6A1DC739F7D 
{
	Dictionary_2_tE7CDB026141A092A986D0DA3CB2DFCE6BB552069* ____dictionary;
	int32_t ____index;
	int32_t ____version;
	Il2CppSharedGenericObject* ____currentValue;
};
typedef Il2CppFullySharedGenericStruct Enumerator_tE10E03C65C00BC14DE96993EA5248F610F88C977;
typedef Il2CppFullySharedGenericStruct Enumerator_tEEF12A9EDAAA0461A72ED9491F6AC20B22440222;
struct LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599  : public LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755
{
	Il2CppSharedGenericObject* ___U3CContextU3Ek__BackingField;
};
struct AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03  : public AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	uint8_t ___m_value;
};
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17 
{
	Il2CppChar ___m_value;
};
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	double ___m_value;
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
struct Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175 
{
	int16_t ___m_value;
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3 
{
	int64_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct LifecycleScope_tEA98D004E7CFF981DC51524B020E4CBD58D964CB  : public LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755
{
};
struct LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D 
{
	Type_t* ____scopeType;
	RuntimeObject* ____context;
};
struct LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D_marshaled_pinvoke
{
	Type_t* ____scopeType;
	Il2CppIUnknown* ____context;
};
struct LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D_marshaled_com
{
	Type_t* ____scopeType;
	Il2CppIUnknown* ____context;
};
struct SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5 
{
	int8_t ___m_value;
};
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	float ___m_value;
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
struct ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79  : public ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C
{
	LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ___U3CScopeKeyU3Ek__BackingField;
};
struct ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 
{
	intptr_t ____value;
};
struct ByReference_1_tE0748F88701C64E729013351521FC3EED28DE1DC 
{
	intptr_t ____value;
};
struct Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE 
{
	Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* ____dictionary;
	int32_t ____index;
	int32_t ____version;
	LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ____currentKey;
};
struct Enumerator_t1A261715170E73393537C60CCC568DE6296C8F99 
{
	Dictionary_2_tE7CDB026141A092A986D0DA3CB2DFCE6BB552069* ____dictionary;
	int32_t ____index;
	int32_t ____version;
	LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ____currentKey;
};
struct Allocator_t996642592271AAD9EE688F142741D512C07B5824 
{
	int32_t ___value__;
};
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D  : public RuntimeObject
{
	AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* ___javaInterface;
	intptr_t ___proxyObject;
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
struct GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8  : public RuntimeObject
{
	bool ___m_disposed;
	intptr_t ___m_jobject;
};
struct ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38  : public LifecycleScope_tEA98D004E7CFF981DC51524B020E4CBD58D964CB
{
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
struct PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 
{
	intptr_t ___m_Handle;
	uint32_t ___m_Version;
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			bool ___z;
		};
		#pragma pack(pop, tp)
		struct
		{
			bool ___z_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int8_t ___b;
		};
		#pragma pack(pop, tp)
		struct
		{
			int8_t ___b_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			Il2CppChar ___c;
		};
		#pragma pack(pop, tp)
		struct
		{
			Il2CppChar ___c_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int16_t ___s;
		};
		#pragma pack(pop, tp)
		struct
		{
			int16_t ___s_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___i;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___i_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int64_t ___j;
		};
		#pragma pack(pop, tp)
		struct
		{
			int64_t ___j_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			float ___f;
		};
		#pragma pack(pop, tp)
		struct
		{
			float ___f_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			double ___d;
		};
		#pragma pack(pop, tp)
		struct
		{
			double ___d_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			intptr_t ___l;
		};
		#pragma pack(pop, tp)
		struct
		{
			intptr_t ___l_forAlignmentOnly;
		};
	};
};
struct jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225_marshaled_pinvoke
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___z;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___z_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int8_t ___b;
		};
		#pragma pack(pop, tp)
		struct
		{
			int8_t ___b_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			uint8_t ___c;
		};
		#pragma pack(pop, tp)
		struct
		{
			uint8_t ___c_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int16_t ___s;
		};
		#pragma pack(pop, tp)
		struct
		{
			int16_t ___s_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___i;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___i_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int64_t ___j;
		};
		#pragma pack(pop, tp)
		struct
		{
			int64_t ___j_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			float ___f;
		};
		#pragma pack(pop, tp)
		struct
		{
			float ___f_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			double ___d;
		};
		#pragma pack(pop, tp)
		struct
		{
			double ___d_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			intptr_t ___l;
		};
		#pragma pack(pop, tp)
		struct
		{
			intptr_t ___l_forAlignmentOnly;
		};
	};
};
struct jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225_marshaled_com
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___z;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___z_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int8_t ___b;
		};
		#pragma pack(pop, tp)
		struct
		{
			int8_t ___b_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			uint8_t ___c;
		};
		#pragma pack(pop, tp)
		struct
		{
			uint8_t ___c_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int16_t ___s;
		};
		#pragma pack(pop, tp)
		struct
		{
			int16_t ___s_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___i;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___i_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			int64_t ___j;
		};
		#pragma pack(pop, tp)
		struct
		{
			int64_t ___j_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			float ___f;
		};
		#pragma pack(pop, tp)
		struct
		{
			float ___f_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			double ___d;
		};
		#pragma pack(pop, tp)
		struct
		{
			double ___d_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			intptr_t ___l;
		};
		#pragma pack(pop, tp)
		struct
		{
			intptr_t ___l_forAlignmentOnly;
		};
	};
};
struct ScopeTransitionType_t6E07CFC7F104E66937274EA67AF2704D03670F82 
{
	int32_t ___value__;
};
struct NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 
{
	void* ___m_Buffer;
	int32_t ___m_Length;
	int32_t ___m_AllocatorLabel;
};
struct Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54 
{
	ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 ____pointer;
	int32_t ____length;
};
struct Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 
{
	ByReference_1_tE0748F88701C64E729013351521FC3EED28DE1DC ____pointer;
	int32_t ____length;
};
struct Motion_tBCD49FBF5608AD21FC03B63C8182FABCEF2707AC  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
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
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16  : public RuntimeObject
{
	LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* ___U3CScopeU3Ek__BackingField;
	int32_t ___U3CTransitionTypeU3Ek__BackingField;
	bool ___U3CAlsoExitNestedScopesU3Ek__BackingField;
};
struct Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A  : public MulticastDelegate_t
{
};
struct Func_2_t8C8978DB83B81CEAC25C00451C27A47420AA8F84  : public MulticastDelegate_t
{
};
struct AnimationClip_t00BD2F131D308A4AD2C6B0BF66644FC25FECE712  : public Motion_tBCD49FBF5608AD21FC03B63C8182FABCEF2707AC
{
};
struct EmptyArray_1_tF2F83411E5DC963DBFFD1F75B10B58A1ED224DD5_StaticFields
{
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___Value;
};
struct List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14_StaticFields
{
	ActiveLifecycleScopeBaseU5BU5D_t838742F7550A7877BD0F3E2C6EB34055C35B57B2* ___s_emptyArray;
};
struct List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E_StaticFields
{
	__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* ___s_emptyArray;
};
struct List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A_StaticFields
{
	__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___s_emptyArray;
};
struct AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_StaticFields
{
	bool ___enableDebugPrints;
};
struct DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticFields
{
	bool ___loggingEnabled;
	bool ___verificationEnabled;
};
struct ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347_StaticFields
{
	String_t* ___k_ProfilerMarkerPrefix;
	String_t* ___k_DetailedInvokeMarkerPrefix;
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
struct Char_t521A6F19B456D956AF452D926C32709DC03D6B17_StaticFields
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___s_categoryForLatin1;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
};
struct AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_StaticFields
{
	GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___s_JavaLangSystemClass;
	intptr_t ___s_HashCodeMethodID;
};
struct Exception_t_StaticFields
{
	RuntimeObject* ___s_EDILock;
};
struct PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4_StaticFields
{
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 ___m_Null;
};
struct Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_StaticFields
{
	Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F ___m_NullPlayable;
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
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248  : public RuntimeArray
{
	ALIGN_FIELD (8) String_t* m_Items[1];

	inline String_t* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline String_t** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, String_t* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline String_t* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline String_t** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, String_t* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D  : public RuntimeArray
{
	ALIGN_FIELD (8) ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38* m_Items[1];

	inline ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
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


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_ContainsKey_m4FE70573C5E0F31040F3B0DE433DF7996E83A5B3_gshared (Dictionary_2_tE7CDB026141A092A986D0DA3CB2DFCE6BB552069* __this, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ___0_key, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR KeyCollection_t5E448F9EF31B8C6F2D80CA7C7858AB780AB912A9* Dictionary_2_get_Keys_m3EE74149DC6731CDFEE5F22F0CA2058B59A8D2D9_gshared (Dictionary_2_tE7CDB026141A092A986D0DA3CB2DFCE6BB552069* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t1A261715170E73393537C60CCC568DE6296C8F99 KeyCollection_GetEnumerator_m563049CEC0F536822CF486D50D493664B71E875E_gshared (KeyCollection_t5E448F9EF31B8C6F2D80CA7C7858AB780AB912A9* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m3E0B734E98D35978EEE706FCEC2FE08FEA74FD35_fshared (Enumerator_tE10E03C65C00BC14DE96993EA5248F610F88C977* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D Enumerator_get_Current_m9C78D88F0F2E50F8F748978518338E28736E4522_gshared_inline (Enumerator_t1A261715170E73393537C60CCC568DE6296C8F99* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m519DFDF4A50759F18C329CF995A277BDCD05D5A4_gshared (Enumerator_t1A261715170E73393537C60CCC568DE6296C8F99* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* LifecycleScopeWithContext_1_get_Context_m71715E88B71B9A07D4F70E53A5142B8F5383AC27_gshared_inline (LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_TisIl2CppSharedGenericObject_m5267E4C686C348E337CB4A9ED51372D608957B0F_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, Il2CppSharedGenericObject* ___0_scopeContext, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t539AC62A1BE9934C67FD41CBB84103B4E3E98AC1 Queue_1_GetEnumerator_m931CF5C152723DE6DD0786DF3D5606A59C1BB1BF_gshared (Queue_1_tF7C2F79F3487A05259C04F0FA9E0DE6DB85009FF* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_m30F4DD6AE424AEA73D1DA1D37B83EFC74AC44BDF_gshared (Enumerator_t539AC62A1BE9934C67FD41CBB84103B4E3E98AC1* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* Enumerator_get_Current_m818AA47F6124D3EC9A3CA48178D60E2D11D9C075_gshared (Enumerator_t539AC62A1BE9934C67FD41CBB84103B4E3E98AC1* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mDDBE6898D653A86324F3FDF8B4F6EC84E9812EEC_gshared (Enumerator_t539AC62A1BE9934C67FD41CBB84103B4E3E98AC1* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_m65AF7C880B3A8B7B6F7C6F59C699EA3FC8FC1BEE_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_scopeWithContext, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ValueCollection_t7F798658A969389BB4AAA8E1ECC40A4682EF43A9* Dictionary_2_get_Values_mEC10CAC4B24A0C0E57AE9BF73D0378DA6D2C852A_gshared (Dictionary_2_tE7CDB026141A092A986D0DA3CB2DFCE6BB552069* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_tADBE53A4D403A692FEE79405C385A6A1DC739F7D ValueCollection_GetEnumerator_mB5FEF01E8979132319CCEE6B77862DC944001B00_gshared (ValueCollection_t7F798658A969389BB4AAA8E1ECC40A4682EF43A9* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mB05777B182DD6D8C5ED21811C2CA95405D2743F8_fshared (Enumerator_tEEF12A9EDAAA0461A72ED9491F6AC20B22440222* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* Enumerator_get_Current_mFB92CF0C22891AA06481368B806933878E96848C_gshared_inline (Enumerator_tADBE53A4D403A692FEE79405C385A6A1DC739F7D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mEB114AA8470C0E221193BA19C62E0D16284971C0_gshared (Enumerator_tADBE53A4D403A692FEE79405C385A6A1DC739F7D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9 List_1_GetEnumerator_mD48177D95D4B5D6A9D8E84E2477668C2850DD5D9_gshared (List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mFE1EBE6F6425283FEAEAE7C79D02CDE4F9D367E8_fshared (Enumerator_tF5AC6CD19D283FBD724440520CEE68FE2602F7AF* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* Enumerator_get_Current_mA50CED82C4671CC4E1D82333FAC2587F700565D0_gshared_inline (Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_m2B096A69E95EF2C7A223BA853D66AEC59C4A5C25_gshared (Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_Remove_m9B8C23C73DCE5610BAD4E4506C7260A603E3BD82_gshared (Dictionary_2_tE7CDB026141A092A986D0DA3CB2DFCE6BB552069* __this, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ___0_key, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActiveLifecycleScopesTracker_CreateScopeTransitionRequest_TisIl2CppSharedGenericObject_m781DD3D4A86DAE2D7A3F31DD07A1A4B6C4ACFFB7_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, int32_t ___1_scopeTransitionType, bool ___2_alsoExitNestedScopes, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_PrepareTryEnterScopeWithContext_TisIl2CppSharedGenericObject_m3D8B69D9E50547113449972B07933BE902744D59_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActiveLifecycleScopeWithContext_1__ctor_mDD6FD21DC68167495A10EE8C4AB04C783A8A809C_gshared (ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_scope, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D LifecycleScopeKey_CreateFromScope_TisIl2CppSharedGenericObject_m613ECBEDFFC63C9393C625204DC116D94C244FBB_gshared (LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_scope, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_Add_m0C37ECCAD4CA418B95EEE2AFCAEDCA430B3D6C6D_gshared (Dictionary_2_tE7CDB026141A092A986D0DA3CB2DFCE6BB552069* __this, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ___0_key, Il2CppSharedGenericObject* ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_PrepareTryExitScope_TisIl2CppSharedGenericObject_mB20E9E3D9D7A9E4B5F58A5783EB2EBB2733EF4E7_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, Il2CppSharedGenericObject* ___1_activationContext, bool ___2_alsoExitNestedScopes, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* ActiveLifecycleScopeWithContext_1_get_Context_m45FFE4CAC1E3D5639C3DEE4433FAE9E204851CC0_gshared (ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_TryGetValue_mA238034AD68BB6D92BDE20B1C8BED7498E6A28A8_gshared (Dictionary_2_tE7CDB026141A092A986D0DA3CB2DFCE6BB552069* __this, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ___0_key, Il2CppSharedGenericObject** ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisIl2CppFullySharedGenericStruct_m6920C14D4E38FAB84BD2B5F148CE70DF7F224F52_fshared (void* ___0_dataPointer, int32_t ___1_length, int32_t ___2_allocator, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool NativeArray_1_get_IsCreated_m527A2C3B75C25BFF29D1D9EA88C81172FF4F5A5A_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void* NativeArrayUnsafeUtility_GetUnsafePtr_TisIl2CppFullySharedGenericStruct_m4AB802D5B1B296D0F976C1E7631699B0C4A4D00F_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_nativeArray, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool _AndroidJNIHelper_ConvertFromJNIArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mBF1AB84DE0B50E7C206BAD3C170D1F5AD2D9E343 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar _AndroidJNIHelper_ConvertFromJNIArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m9ECB51B699AB7B96368F39CC9F7C481340DD0BBC (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double _AndroidJNIHelper_ConvertFromJNIArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m1F7F249CC7CDA2D6E136C627D983569B3674EA73 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t _AndroidJNIHelper_ConvertFromJNIArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m901F767E3189385BB47B5B021C40507AAF67D477 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t _AndroidJNIHelper_ConvertFromJNIArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m37A94A4CA9F6456009CABE27D4AA2ED7B8365168 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t _AndroidJNIHelper_ConvertFromJNIArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mBE697F1C4D60A7F2BB06784B5CB0C540B7FBF664 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t _AndroidJNIHelper_ConvertFromJNIArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m01777984AFB915C1167148BC6056336EB200B151 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float _AndroidJNIHelper_ConvertFromJNIArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m6634501B2BC58937A4D852F6F2D8D55A9DFB6F64 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* _AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_mAD2432704589B67F3F93D55DF8B948DCBEBA4444_gshared (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetFieldID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE35E2A7AB2A1739DB9AEE2664DB6FA8E85B8897A (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetFieldID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mFD988C7487D7A7810D33F985E48AB82A006E1B7B (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mC4E70171E726BFD2EDF128963FAD90873B0289C5_gshared (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m5C5661781E8C49E962C2556FC75101FD0E862AFD (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m465627642B00BC71088A5883058BB86DDBADABE0 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_mA32D3320555971B585058A28FC5F5EC84DC10A34 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mF0D2DB5B885B01587820A9F88C6CAD67C8DC1512 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mB340108BAAC2DA25B45971B719093FDF9B13E745 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mF114D8205E13517DE4F8DB9772E9C40C7C43BD85 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m362BBB1CC3B8834A056A8F709649BD182E24E9BD (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m86B190CF71EDCB08177B90F98C76A9545AF09FCB (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_mFEAB91A44B644CE56BB6182931D1065B679E0B3A_gshared (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Call_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m72E1F5D2CE435E45651E8B970A73B9F5E3E03815 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject__Call_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m22710813CDD982DC5F88C2A4067FBFE1D57A3065 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject__Call_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m946CEC22A94586DE93BE0725E5F25E04589F69DE (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject__Call_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m08A0CE3C4BDDA756095A0A816C659587814F649E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1BFF486C91846170C07AAF27EF84971FF0596B90 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject__Call_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mC6DBECDD44973120E0A56068ABE886D9A7016F5C (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject__Call_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mAE2F0D59E0CE54D36BB86C53218704FA72DC0350 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject__Call_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m71549B5031FEE0FBB78A9CE02EDADD14DBFFB56D (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m39A1E2B88CD5364F83417EFB65F1C296656D2F62_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m41529A6A47CC3B57286AC8F9C6388E54918DF450 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mEE64E87A5A2BC0F38910643A9B91032AF38D882C (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_m8E1513A9554AAA1822CA51D0A816DCF16775AA37_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJNIHelper_ConvertFromJNIArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m0207DDD0AB7EE1136083B85289D4F16FD9A64ECC (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJNIHelper_ConvertFromJNIArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m51DC292506277213F541434A51F81430E11E7226 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJNIHelper_ConvertFromJNIArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_mF6AF89E7365D44A201C5A2C86C49EF49E2D6ADAD (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJNIHelper_ConvertFromJNIArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mF6FCB78A4CF7C2FB0375E88C31C70458F55F422C (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJNIHelper_ConvertFromJNIArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m02707C23958FF03FA0345C2A17F66EE379A1468C (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJNIHelper_ConvertFromJNIArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m0CADE835010213386C1C07E63F70E55498142C20 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJNIHelper_ConvertFromJNIArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m2BD2E924FBD117B44A2D5E741144FD7B1FCE918E (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJNIHelper_ConvertFromJNIArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m93BCF3156F5C823F8C2B005523A618966B0A6DBB (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_m6F1CD10E4BE61A7211FB87DA1D7D92C7FE85F8E4_gshared (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Get_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m68F3AF483142B771BE6BCD51B30F7CED871DB766 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Get_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDED207A2CABEE9D78BEC022F1D4338A5091DCC50 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__Get_TisIl2CppSharedGenericObject_m3FBD43AC7986C7B2DAE4F575360C5B6A7DBDE4AB_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__GetStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m4289B7D740963C5007AD93A07FF6A0A01857DD28 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_m177F1A057E8F5EF05D9158BA272E890C6210044B_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline (Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline (Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_FromJavaArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mA1CB7EC9473B0674763C89AC566EC664159108E9 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE3914411BE312CBBA869835158E535A06CD3EEB4 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Call_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m73D49A76D8F24AD1815CF94E87F1EF4683A38EA6 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject_FromJavaArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m18459EC016E866EF307EC4D9442CD86FD9BC28B4 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m791647AA6EAD3B88467418F51CF53F5755AB38BA (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject__Call_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m8B3C7C06394410B77DB9953832CB9F8C29C28E89 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject_FromJavaArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m3CAD187DEBCBD88D94604015E86A37B8944847BC (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m44C60AAE64EE97BFD5D9D302E7BE5BE20E6862B3 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject__Call_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m0BA90472306DF5C3A1491FC3EB052DD58FA468C3 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject_FromJavaArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m62441E1DA5798B8F585F5EF18386C4FB739BFFC8 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m3B57915B048BD436E00741CD146D62AC775E7BA3 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject__Call_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mA616697346ABF4A696B8F63B5F85AA92777EA400 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_FromJavaArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC1390BDE8BF42FAA78215DFE076A25F7AEA674D7 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mF87CC238DDBB3C90C35846E626548847CC7E319D (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m0D60BAB951F2C313123D62CAB81EEA263F2F0750 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject_FromJavaArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m6A8E0C87ADD6FC1CA3F936C0CC0AC4AB379A8422 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m9665934CC08FD80EB16882C4EA6FDBC5D9C3A175 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject__Call_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m4B925872A7B673BC9981AB72E3A229BBE0E1119E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject_FromJavaArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mB474FE99696A6DEB269E0F1C3C7DA32CD8854410 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mEF6ACD1C6C5237E0446DB9DBDAC40BA2660849BB (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject__Call_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mA3300F1D8CC0B110426EAE23F5FB7F27FD8AE76B (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject_FromJavaArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_mD5A794B10305822230E288BE2D72329A6EE0C8A4 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_mA3FFDC6948922BEFC787A297D5483BA277EEFF0C (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject__Call_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m8EDEAF89FE2C5B65277B0FC57FAD4FA9253E1140 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject_FromJavaArray_TisIl2CppSharedGenericObject_m22D9898503E929D77563392822CD11A056613AFF_gshared (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_m95122CB55DFAF463B14CBBA51C2B0811790DB90B_gshared (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m34A45129A0AA339AC2E31492EF928112EDECC197_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m9EDC162A865064239ADA4058CD2A81B1A2CF6D0F (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m4F31B91D2F03C3068A4BE95DB368B6DF715CE316 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_mF9D318147E29742DAA4B5C033BFC68D29CC1123B_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mC408C8B80896737BA7DE6C18A904D2157E5FD364 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetFieldID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6F960B6375B61125FD05A03EBE8CEC790A986044 (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Get_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mBEAFA1EB54999ED8C620F9B4F0E2D3CA2530CECE (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m61DD76EF7E812BC3ED41BBA0D0DA1B430EC61256 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetFieldID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1B394DCE5ED40DFA09EB18874BA2FFC3B5E50B2E (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Get_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mB506BB319D4A193AE597F3C3166DDB165593D4D6 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisIl2CppSharedGenericObject_mB1CEFE624E92B82D234C1982AA2A7DF6AC5FD284_gshared (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mDFF491584A820BE0629A47ED3B790A199050D589_gshared (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__Get_TisIl2CppSharedGenericObject_mD7317316E4D5BBE57FE9495B6E117EEFFA6F149E_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__GetStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m86131C9F9AC1A6082963096E96DDDDDD7366189E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_mF12B3A136CBDB98DF0876DF1FC47EA4A14D98A17_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass0_0_1__ctor_m949C96554EA19FE4E66F6E8641BC1144CFF4164E_fshared (U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* Array_Empty_TisIl2CppSharedGenericObject_m688909407CF299AE69145CC33ACB1E90C775C653_gshared_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Func_2__ctor_m266186AD54086DE797B83FC2FF930C212522BB9A_gshared (Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerable_Select_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_TisIl2CppSharedGenericObject_m9A1A985DC3BC579FA94A34322A551416C1A99804_gshared (RuntimeObject* ___0_source, Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A* ___1_selector, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* Enumerable_ToList_TisIl2CppSharedGenericObject_mFF96E16527D742B009531AEA8672FF89A8A379E2_gshared (RuntimeObject* ___0_source, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LifecycleScopeKey__ctor_m7DCCBE2ED25A7746808F3555AB845EE71FEB6A71 (LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D* __this, Type_t* ___0_scopeType, const RuntimeMethod* method) ;
inline bool Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281 (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* __this, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ___0_key, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83*, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D, const RuntimeMethod*))Dictionary_2_ContainsKey_m4FE70573C5E0F31040F3B0DE433DF7996E83A5B3_gshared)(__this, ___0_key, method);
}
inline KeyCollection_t59AB6D7A56AAB8B527E4EEBED39397629A9430E0* Dictionary_2_get_Keys_m9CB10267CF4FC83CDDD3FDC4B898C73488C93FB8 (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* __this, const RuntimeMethod* method)
{
	return ((  KeyCollection_t59AB6D7A56AAB8B527E4EEBED39397629A9430E0* (*) (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83*, const RuntimeMethod*))Dictionary_2_get_Keys_m3EE74149DC6731CDFEE5F22F0CA2058B59A8D2D9_gshared)(__this, method);
}
inline Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE KeyCollection_GetEnumerator_mB40FC79A8A11DB8F002E9192693045C2F194E3E7 (KeyCollection_t59AB6D7A56AAB8B527E4EEBED39397629A9430E0* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE (*) (KeyCollection_t59AB6D7A56AAB8B527E4EEBED39397629A9430E0*, const RuntimeMethod*))KeyCollection_GetEnumerator_m563049CEC0F536822CF486D50D493664B71E875E_gshared)(__this, method);
}
inline void Enumerator_Dispose_mD343B871CBC9CC3590A9CB7326BB5E263AD03BE1 (Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE*, const RuntimeMethod*))Enumerator_Dispose_m3E0B734E98D35978EEE706FCEC2FE08FEA74FD35_fshared)(__this, method);
}
inline LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D Enumerator_get_Current_m21C7125C1D35261357C56A874C207249FC3E4F4A_inline (Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE* __this, const RuntimeMethod* method)
{
	return ((  LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D (*) (Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE*, const RuntimeMethod*))Enumerator_get_Current_m9C78D88F0F2E50F8F748978518338E28736E4522_gshared_inline)(__this, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Type_t* LifecycleScopeKey_get_Type_mEC585239B2BC794EDFDE68B89A6533971E12C752_inline (LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Type_op_Equality_m99930A0E44E420A685FABA60E60BA1CC5FA0EBDC (Type_t* ___0_left, Type_t* ___1_right, const RuntimeMethod* method) ;
inline bool Enumerator_MoveNext_mFEAE796204BD77488174AB22E0DE4C2721159335 (Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE*, const RuntimeMethod*))Enumerator_MoveNext_m519DFDF4A50759F18C329CF995A277BDCD05D5A4_gshared)(__this, method);
}
inline Il2CppSharedGenericObject* LifecycleScopeWithContext_1_get_Context_m71715E88B71B9A07D4F70E53A5142B8F5383AC27_inline (LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* __this, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599*, const RuntimeMethod*))LifecycleScopeWithContext_1_get_Context_m71715E88B71B9A07D4F70E53A5142B8F5383AC27_gshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LifecycleScopeKey__ctor_m3BFDFB377FD3F489668C9B18AF174EF462838E6C (LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D* __this, Type_t* ___0_scopeType, RuntimeObject* ___1_context, const RuntimeMethod* method) ;
inline bool ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_TisIl2CppSharedGenericObject_m5267E4C686C348E337CB4A9ED51372D608957B0F (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, Il2CppSharedGenericObject* ___0_scopeContext, const RuntimeMethod* method)
{
	return ((  bool (*) (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7*, Il2CppSharedGenericObject*, const RuntimeMethod*))ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_TisIl2CppSharedGenericObject_m5267E4C686C348E337CB4A9ED51372D608957B0F_gshared)(__this, ___0_scopeContext, method);
}
inline Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201 Queue_1_GetEnumerator_m311A1E04C52E396219D02261B478965B9C97BD3D (Queue_1_t0213AA1BB5D12D3BCD5727E71F92BE13870D391A* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201 (*) (Queue_1_t0213AA1BB5D12D3BCD5727E71F92BE13870D391A*, const RuntimeMethod*))Queue_1_GetEnumerator_m931CF5C152723DE6DD0786DF3D5606A59C1BB1BF_gshared)(__this, method);
}
inline void Enumerator_Dispose_m5560B98567B989C4754E5C182136BE4AC88848CB (Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201*, const RuntimeMethod*))Enumerator_Dispose_m30F4DD6AE424AEA73D1DA1D37B83EFC74AC44BDF_gshared)(__this, method);
}
inline ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* Enumerator_get_Current_m132E4E286C55B63A011879B1F62299A7E6CF8DE2 (Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201* __this, const RuntimeMethod* method)
{
	return ((  ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* (*) (Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201*, const RuntimeMethod*))Enumerator_get_Current_m818AA47F6124D3EC9A3CA48178D60E2D11D9C075_gshared)(__this, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* ScopeTransitionRequestBase_get_Scope_mEB7F256D6FF4393EB627213D2E7343BD35BDCB50_inline (ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t ScopeTransitionRequestBase_get_TransitionType_m64E9CBFD2036C2727F62B4276B07594B1AD0244B_inline (ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* __this, const RuntimeMethod* method) ;
inline bool Enumerator_MoveNext_m4CA597288EC15016493B62A210A4A63E27177117 (Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201*, const RuntimeMethod*))Enumerator_MoveNext_mDDBE6898D653A86324F3FDF8B4F6EC84E9812EEC_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* LifecycleScopeBase_get_RequiredOuterScopes_mE1B35D619BCE1A19CF63F53E917F8D86731225B7 (LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_IsInsideScope_m326DF0210C805B6FEFF0848D0A1C1891D1AD8ABE (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, String_t* ___0_scopeName, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline (LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___0_values, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3 (String_t* ___0_message, bool ___1_criticalError, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B (String_t* ___0_str0, String_t* ___1_str1, String_t* ___2_str2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_IsInsideScope_m6A9229A85BC7290BB7CABC0F5726CF29CE197886 (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScope_tEA98D004E7CFF981DC51524B020E4CBD58D964CB* ___0_scope, const RuntimeMethod* method) ;
inline bool ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_m65AF7C880B3A8B7B6F7C6F59C699EA3FC8FC1BEE (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_scopeWithContext, const RuntimeMethod* method)
{
	return ((  bool (*) (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7*, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599*, const RuntimeMethod*))ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_m65AF7C880B3A8B7B6F7C6F59C699EA3FC8FC1BEE_gshared)(__this, ___0_scopeWithContext, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987 (String_t* ___0_format, RuntimeObject* ___1_arg0, RuntimeObject* ___2_arg1, const RuntimeMethod* method) ;
inline ValueCollection_tB7C740A97431C55C62CC43DA7712B5896DBED97F* Dictionary_2_get_Values_m79563C16853F891CCBBC22B07DC63DA063874A49 (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* __this, const RuntimeMethod* method)
{
	return ((  ValueCollection_tB7C740A97431C55C62CC43DA7712B5896DBED97F* (*) (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83*, const RuntimeMethod*))Dictionary_2_get_Values_mEC10CAC4B24A0C0E57AE9BF73D0378DA6D2C852A_gshared)(__this, method);
}
inline Enumerator_t624742B8D885649511A5739F11184E2221942252 ValueCollection_GetEnumerator_m1B01F115659146468CB5EE9025E45905AB1279DE (ValueCollection_tB7C740A97431C55C62CC43DA7712B5896DBED97F* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t624742B8D885649511A5739F11184E2221942252 (*) (ValueCollection_tB7C740A97431C55C62CC43DA7712B5896DBED97F*, const RuntimeMethod*))ValueCollection_GetEnumerator_mB5FEF01E8979132319CCEE6B77862DC944001B00_gshared)(__this, method);
}
inline void Enumerator_Dispose_m6E582A0ECA46CE69AFD14D67DE233D2B4E1069BE (Enumerator_t624742B8D885649511A5739F11184E2221942252* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t624742B8D885649511A5739F11184E2221942252*, const RuntimeMethod*))Enumerator_Dispose_mB05777B182DD6D8C5ED21811C2CA95405D2743F8_fshared)(__this, method);
}
inline ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* Enumerator_get_Current_m2E4C1612FFBB402B6CF496C40499B9B84734E878_inline (Enumerator_t624742B8D885649511A5739F11184E2221942252* __this, const RuntimeMethod* method)
{
	return ((  ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* (*) (Enumerator_t624742B8D885649511A5739F11184E2221942252*, const RuntimeMethod*))Enumerator_get_Current_mFB92CF0C22891AA06481368B806933878E96848C_gshared_inline)(__this, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline (ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1 (String_t* ___0_a, String_t* ___1_b, const RuntimeMethod* method) ;
inline bool Enumerator_MoveNext_mC8DD05ED5A94E9D71568B93F1FBC1A03B3A4E385 (Enumerator_t624742B8D885649511A5739F11184E2221942252* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t624742B8D885649511A5739F11184E2221942252*, const RuntimeMethod*))Enumerator_MoveNext_mEB114AA8470C0E221193BA19C62E0D16284971C0_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActiveLifecycleScopesTracker_CollectNestedScopesToExit_m7489DDE99FDA05A011E202D63B4146E96B7761AD (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, String_t* ___0_scopeName, List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14** ___1_nestedActiveScopesInOrder, const RuntimeMethod* method) ;
inline Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352 List_1_GetEnumerator_m3A7E9AECB2ED791F09FADAD0BC7B826FB62A85A4 (List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352 (*) (List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14*, const RuntimeMethod*))List_1_GetEnumerator_mD48177D95D4B5D6A9D8E84E2477668C2850DD5D9_gshared)(__this, method);
}
inline void Enumerator_Dispose_m80B75619D3D43280CFC1D0556BBFE923F8643835 (Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352*, const RuntimeMethod*))Enumerator_Dispose_mFE1EBE6F6425283FEAEAE7C79D02CDE4F9D367E8_fshared)(__this, method);
}
inline ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* Enumerator_get_Current_m422894DCBE54CC53F8C3729BEE72B601B02C8686_inline (Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352* __this, const RuntimeMethod* method)
{
	return ((  ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* (*) (Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352*, const RuntimeMethod*))Enumerator_get_Current_mA50CED82C4671CC4E1D82333FAC2587F700565D0_gshared_inline)(__this, method);
}
inline bool Enumerator_MoveNext_mD85ED63385062F8013FBC6C57320F2B23C975EFE (Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352*, const RuntimeMethod*))Enumerator_MoveNext_m2B096A69E95EF2C7A223BA853D66AEC59C4A5C25_gshared)(__this, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DebugLifecycle_get_LoggingEnabled_mADB463016DF27BC61A483A646E5B999ABB3A8D94_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DebugLifecycle_Log_mF5AF374C4A49186C856B489B40F56163BE723BBB (String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActiveLifecycleScopesTracker_RaiseAutoCleanups_m5620B257E893397698E4CA9CBD38E695BADBA8ED (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, Type_t* ___0_lifecycleScopeType, int32_t ___1_scopeTransitionType, const RuntimeMethod* method) ;
inline bool Dictionary_2_Remove_mAE2ECC0F5BDAA58524500656449EA1A387079726 (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* __this, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ___0_key, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83*, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D, const RuntimeMethod*))Dictionary_2_Remove_m9B8C23C73DCE5610BAD4E4506C7260A603E3BD82_gshared)(__this, ___0_key, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActiveLifecycleScopesTracker_CreateScopeTransitionRequest_m9994ACEAA4926C9C17BB51EEA3411DA150DB15D9 (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScope_tEA98D004E7CFF981DC51524B020E4CBD58D964CB* ___0_lifecycleScope, int32_t ___1_scopeTransitionType, bool ___2_alsoExitNestedScopes, const RuntimeMethod* method) ;
inline void ActiveLifecycleScopesTracker_CreateScopeTransitionRequest_TisIl2CppSharedGenericObject_m781DD3D4A86DAE2D7A3F31DD07A1A4B6C4ACFFB7 (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, int32_t ___1_scopeTransitionType, bool ___2_alsoExitNestedScopes, const RuntimeMethod* method)
{
	((  void (*) (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7*, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599*, int32_t, bool, const RuntimeMethod*))ActiveLifecycleScopesTracker_CreateScopeTransitionRequest_TisIl2CppSharedGenericObject_m781DD3D4A86DAE2D7A3F31DD07A1A4B6C4ACFFB7_gshared)(__this, ___0_lifecycleScope, ___1_scopeTransitionType, ___2_alsoExitNestedScopes, method);
}
inline bool ActiveLifecycleScopesTracker_PrepareTryEnterScopeWithContext_TisIl2CppSharedGenericObject_m3D8B69D9E50547113449972B07933BE902744D59 (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, const RuntimeMethod* method)
{
	return ((  bool (*) (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7*, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599*, const RuntimeMethod*))ActiveLifecycleScopesTracker_PrepareTryEnterScopeWithContext_TisIl2CppSharedGenericObject_m3D8B69D9E50547113449972B07933BE902744D59_gshared)(__this, ___0_lifecycleScope, method);
}
inline void ActiveLifecycleScopeWithContext_1__ctor_mDD6FD21DC68167495A10EE8C4AB04C783A8A809C (ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_scope, const RuntimeMethod* method)
{
	((  void (*) (ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79*, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599*, const RuntimeMethod*))ActiveLifecycleScopeWithContext_1__ctor_mDD6FD21DC68167495A10EE8C4AB04C783A8A809C_gshared)(__this, ___0_scope, method);
}
inline LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D LifecycleScopeKey_CreateFromScope_TisIl2CppSharedGenericObject_m613ECBEDFFC63C9393C625204DC116D94C244FBB (LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_scope, const RuntimeMethod* method)
{
	return ((  LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D (*) (LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599*, const RuntimeMethod*))LifecycleScopeKey_CreateFromScope_TisIl2CppSharedGenericObject_m613ECBEDFFC63C9393C625204DC116D94C244FBB_gshared)(___0_scope, method);
}
inline void Dictionary_2_Add_mA42C372CA350E01B08326C78A32C2D4331199A3D (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* __this, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ___0_key, ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* ___1_value, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83*, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D, ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C*, const RuntimeMethod*))Dictionary_2_Add_m0C37ECCAD4CA418B95EEE2AFCAEDCA430B3D6C6D_gshared)(__this, ___0_key, ___1_value, method);
}
inline bool ActiveLifecycleScopesTracker_PrepareTryExitScope_TisIl2CppSharedGenericObject_mB20E9E3D9D7A9E4B5F58A5783EB2EBB2733EF4E7 (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, Il2CppSharedGenericObject* ___1_activationContext, bool ___2_alsoExitNestedScopes, const RuntimeMethod* method)
{
	return ((  bool (*) (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7*, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599*, Il2CppSharedGenericObject*, bool, const RuntimeMethod*))ActiveLifecycleScopesTracker_PrepareTryExitScope_TisIl2CppSharedGenericObject_mB20E9E3D9D7A9E4B5F58A5783EB2EBB2733EF4E7_gshared)(__this, ___0_lifecycleScope, ___1_activationContext, ___2_alsoExitNestedScopes, method);
}
inline Il2CppSharedGenericObject* ActiveLifecycleScopeWithContext_1_get_Context_m45FFE4CAC1E3D5639C3DEE4433FAE9E204851CC0 (ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* __this, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79*, const RuntimeMethod*))ActiveLifecycleScopeWithContext_1_get_Context_m45FFE4CAC1E3D5639C3DEE4433FAE9E204851CC0_gshared)(__this, method);
}
inline bool Dictionary_2_TryGetValue_m23A14CBF4DD0FFE6985742AB5960E012A3D29D89 (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* __this, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D ___0_key, ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C** ___1_value, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83*, LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D, ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C**, const RuntimeMethod*))Dictionary_2_TryGetValue_mA238034AD68BB6D92BDE20B1C8BED7498E6A28A8_gshared)(__this, ___0_key, ___1_value, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t* AndroidJNI_GetDirectBufferAddress_m0E0B127BFEB7AAF065829DC6AE11163D5616EBE8 (intptr_t ___0_buffer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJNI_GetDirectBufferCapacity_mCAC8D9C8E45481BE59FB17406E1E16D4F9628183 (intptr_t ___0_buffer, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F (Exception_t* __this, String_t* ___0_message, const RuntimeMethod* method) ;
inline NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m181D7F12EB826B7D6B73742BFD85A667D533BABA (void* ___0_dataPointer, int32_t ___1_length, int32_t ___2_allocator, const RuntimeMethod* method)
{
	return ((  NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF (*) (void*, int32_t, int32_t, const RuntimeMethod*))NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisIl2CppFullySharedGenericStruct_m6920C14D4E38FAB84BD2B5F148CE70DF7F224F52_fshared)(___0_dataPointer, ___1_length, ___2_allocator, method);
}
inline NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mD9D29EE5CE27F4BFD0168818BF064EC5B3672F39 (void* ___0_dataPointer, int32_t ___1_length, int32_t ___2_allocator, const RuntimeMethod* method)
{
	return ((  NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 (*) (void*, int32_t, int32_t, const RuntimeMethod*))NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisIl2CppFullySharedGenericStruct_m6920C14D4E38FAB84BD2B5F148CE70DF7F224F52_fshared)(___0_dataPointer, ___1_length, ___2_allocator, method);
}
inline bool NativeArray_1_get_IsCreated_mD74FCA194584E6EA7916853B62401EB78240A081_inline (NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF*, const RuntimeMethod*))NativeArray_1_get_IsCreated_m527A2C3B75C25BFF29D1D9EA88C81172FF4F5A5A_fshared_inline)(__this, method);
}
inline void* NativeArrayUnsafeUtility_GetUnsafePtr_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m8CFDB2DF56E810A2E2FB3686AF676FCAC65AFCC2_inline (NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF ___0_nativeArray, const RuntimeMethod* method)
{
	return ((  void* (*) (NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF, const RuntimeMethod*))NativeArrayUnsafeUtility_GetUnsafePtr_TisIl2CppFullySharedGenericStruct_m4AB802D5B1B296D0F976C1E7631699B0C4A4D00F_fshared_inline)(___0_nativeArray, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNI_NewDirectByteBuffer_m17389ED6D98CC0364180BAB43F2747B48FFDB107 (uint8_t* ___0_buffer, int64_t ___1_capacity, const RuntimeMethod* method) ;
inline bool NativeArray_1_get_IsCreated_mF1154ABCB79F43B3A71BFA8DCB85ACBDFA2838D5_inline (NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190*, const RuntimeMethod*))NativeArray_1_get_IsCreated_m527A2C3B75C25BFF29D1D9EA88C81172FF4F5A5A_fshared_inline)(__this, method);
}
inline void* NativeArrayUnsafeUtility_GetUnsafePtr_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mCD832837FF5CE235B506272835BA049D56968E2F_inline (NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 ___0_nativeArray, const RuntimeMethod* method)
{
	return ((  void* (*) (NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190, const RuntimeMethod*))NativeArrayUnsafeUtility_GetUnsafePtr_TisIl2CppFullySharedGenericStruct_m4AB802D5B1B296D0F976C1E7631699B0C4A4D00F_fshared_inline)(___0_nativeArray, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool _AndroidJNIHelper_ConvertFromJNIArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mBF1AB84DE0B50E7C206BAD3C170D1F5AD2D9E343 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar _AndroidJNIHelper_ConvertFromJNIArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m9ECB51B699AB7B96368F39CC9F7C481340DD0BBC (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double _AndroidJNIHelper_ConvertFromJNIArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m1F7F249CC7CDA2D6E136C627D983569B3674EA73 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t _AndroidJNIHelper_ConvertFromJNIArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m901F767E3189385BB47B5B021C40507AAF67D477 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t _AndroidJNIHelper_ConvertFromJNIArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m37A94A4CA9F6456009CABE27D4AA2ED7B8365168 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t _AndroidJNIHelper_ConvertFromJNIArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mBE697F1C4D60A7F2BB06784B5CB0C540B7FBF664 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t _AndroidJNIHelper_ConvertFromJNIArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m01777984AFB915C1167148BC6056336EB200B151 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float _AndroidJNIHelper_ConvertFromJNIArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m6634501B2BC58937A4D852F6F2D8D55A9DFB6F64 (intptr_t ___0_array, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* _AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_mAD2432704589B67F3F93D55DF8B948DCBEBA4444 (intptr_t ___0_array, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (intptr_t, const RuntimeMethod*))_AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_mAD2432704589B67F3F93D55DF8B948DCBEBA4444_gshared)(___0_array, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetFieldID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE35E2A7AB2A1739DB9AEE2664DB6FA8E85B8897A (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetFieldID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mFD988C7487D7A7810D33F985E48AB82A006E1B7B (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
inline intptr_t _AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mC4E70171E726BFD2EDF128963FAD90873B0289C5 (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (intptr_t, String_t*, bool, const RuntimeMethod*))_AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mC4E70171E726BFD2EDF128963FAD90873B0289C5_gshared)(___0_jclass, ___1_fieldName, ___2_isStatic, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m5C5661781E8C49E962C2556FC75101FD0E862AFD (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m465627642B00BC71088A5883058BB86DDBADABE0 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_mA32D3320555971B585058A28FC5F5EC84DC10A34 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mF0D2DB5B885B01587820A9F88C6CAD67C8DC1512 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mB340108BAAC2DA25B45971B719093FDF9B13E745 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mF114D8205E13517DE4F8DB9772E9C40C7C43BD85 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m362BBB1CC3B8834A056A8F709649BD182E24E9BD (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t _AndroidJNIHelper_GetMethodID_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m86B190CF71EDCB08177B90F98C76A9545AF09FCB (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
inline intptr_t _AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_mFEAB91A44B644CE56BB6182931D1065B679E0B3A (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (intptr_t, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, bool, const RuntimeMethod*))_AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_mFEAB91A44B644CE56BB6182931D1065B679E0B3A_gshared)(___0_jclass, ___1_methodName, ___2_args, ___3_isStatic, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Call_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m72E1F5D2CE435E45651E8B970A73B9F5E3E03815 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject__Call_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m22710813CDD982DC5F88C2A4067FBFE1D57A3065 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject__Call_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m946CEC22A94586DE93BE0725E5F25E04589F69DE (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject__Call_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m08A0CE3C4BDDA756095A0A816C659587814F649E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1BFF486C91846170C07AAF27EF84971FF0596B90 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject__Call_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mC6DBECDD44973120E0A56068ABE886D9A7016F5C (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject__Call_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mAE2F0D59E0CE54D36BB86C53218704FA72DC0350 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject__Call_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m71549B5031FEE0FBB78A9CE02EDADD14DBFFB56D (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m39A1E2B88CD5364F83417EFB65F1C296656D2F62 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m39A1E2B88CD5364F83417EFB65F1C296656D2F62_gshared)(__this, ___0_methodName, ___1_args, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__Call_m2126160FB635069207535BD0E700C3605FDB3308 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__Call_m4C4D7D7287030773175BDF47681EA018DFA4DF1A (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m41529A6A47CC3B57286AC8F9C6388E54918DF450 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mEE64E87A5A2BC0F38910643A9B91032AF38D882C (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_m8E1513A9554AAA1822CA51D0A816DCF16775AA37 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_m8E1513A9554AAA1822CA51D0A816DCF16775AA37_gshared)(__this, ___0_methodName, ___1_args, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__CallStatic_mE917E474DB9801610FB7ABE5BE749DF84CEFD48A (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__CallStatic_mD63902D30CD5626DAEAD1D6484AF7A9ACA85590E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJNIHelper_ConvertFromJNIArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m0207DDD0AB7EE1136083B85289D4F16FD9A64ECC (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJNIHelper_ConvertFromJNIArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m51DC292506277213F541434A51F81430E11E7226 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJNIHelper_ConvertFromJNIArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_mF6AF89E7365D44A201C5A2C86C49EF49E2D6ADAD (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJNIHelper_ConvertFromJNIArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mF6FCB78A4CF7C2FB0375E88C31C70458F55F422C (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJNIHelper_ConvertFromJNIArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m02707C23958FF03FA0345C2A17F66EE379A1468C (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJNIHelper_ConvertFromJNIArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m0CADE835010213386C1C07E63F70E55498142C20 (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJNIHelper_ConvertFromJNIArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m2BD2E924FBD117B44A2D5E741144FD7B1FCE918E (intptr_t ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJNIHelper_ConvertFromJNIArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m93BCF3156F5C823F8C2B005523A618966B0A6DBB (intptr_t ___0_array, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_m6F1CD10E4BE61A7211FB87DA1D7D92C7FE85F8E4 (intptr_t ___0_array, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (intptr_t, const RuntimeMethod*))AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_m6F1CD10E4BE61A7211FB87DA1D7D92C7FE85F8E4_gshared)(___0_array, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_DeleteLocalRef_m20303564C88A1B90E3D8D7A7D893392E18967094 (intptr_t ___0_localref, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Get_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m68F3AF483142B771BE6BCD51B30F7CED871DB766 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Get_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDED207A2CABEE9D78BEC022F1D4338A5091DCC50 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* AndroidJavaObject__Get_TisIl2CppSharedGenericObject_m3FBD43AC7986C7B2DAE4F575360C5B6A7DBDE4AB (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, const RuntimeMethod*))AndroidJavaObject__Get_TisIl2CppSharedGenericObject_m3FBD43AC7986C7B2DAE4F575360C5B6A7DBDE4AB_gshared)(__this, ___0_fieldName, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__GetStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m4289B7D740963C5007AD93A07FF6A0A01857DD28 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_m177F1A057E8F5EF05D9158BA272E890C6210044B (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, String_t*, const RuntimeMethod*))AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_m177F1A057E8F5EF05D9158BA272E890C6210044B_gshared)(__this, ___0_fieldName, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline (Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) ;
inline int32_t Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline (Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10 (int32_t ___0_capacity, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___0_args, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___1_jniArgs, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609 (intptr_t ___0_ptr, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25 (Type_t* ___0_t, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline (GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972 (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7 (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97 (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6 (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF (intptr_t ___0_obj, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0 (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* __this, intptr_t ___0_jclass, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6 (Type_t* ___0_t, Type_t* ___1_from, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_FromJavaArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mA1CB7EC9473B0674763C89AC566EC664159108E9 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE3914411BE312CBBA869835158E535A06CD3EEB4 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Call_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m73D49A76D8F24AD1815CF94E87F1EF4683A38EA6 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject_FromJavaArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m18459EC016E866EF307EC4D9442CD86FD9BC28B4 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m791647AA6EAD3B88467418F51CF53F5755AB38BA (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject__Call_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m8B3C7C06394410B77DB9953832CB9F8C29C28E89 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject_FromJavaArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m3CAD187DEBCBD88D94604015E86A37B8944847BC (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m44C60AAE64EE97BFD5D9D302E7BE5BE20E6862B3 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject__Call_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m0BA90472306DF5C3A1491FC3EB052DD58FA468C3 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject_FromJavaArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m62441E1DA5798B8F585F5EF18386C4FB739BFFC8 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m3B57915B048BD436E00741CD146D62AC775E7BA3 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject__Call_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mA616697346ABF4A696B8F63B5F85AA92777EA400 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_FromJavaArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC1390BDE8BF42FAA78215DFE076A25F7AEA674D7 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mF87CC238DDBB3C90C35846E626548847CC7E319D (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m0D60BAB951F2C313123D62CAB81EEA263F2F0750 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject_FromJavaArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m6A8E0C87ADD6FC1CA3F936C0CC0AC4AB379A8422 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m9665934CC08FD80EB16882C4EA6FDBC5D9C3A175 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject__Call_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m4B925872A7B673BC9981AB72E3A229BBE0E1119E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject_FromJavaArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mB474FE99696A6DEB269E0F1C3C7DA32CD8854410 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mEF6ACD1C6C5237E0446DB9DBDAC40BA2660849BB (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject__Call_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mA3300F1D8CC0B110426EAE23F5FB7F27FD8AE76B (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject_FromJavaArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_mD5A794B10305822230E288BE2D72329A6EE0C8A4 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_mA3FFDC6948922BEFC787A297D5483BA277EEFF0C (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject__Call_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m8EDEAF89FE2C5B65277B0FC57FAD4FA9253E1140 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* AndroidJavaObject_FromJavaArray_TisIl2CppSharedGenericObject_m22D9898503E929D77563392822CD11A056613AFF (intptr_t ___0_jobject, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (intptr_t, const RuntimeMethod*))AndroidJavaObject_FromJavaArray_TisIl2CppSharedGenericObject_m22D9898503E929D77563392822CD11A056613AFF_gshared)(___0_jobject, method);
}
inline intptr_t AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_m95122CB55DFAF463B14CBBA51C2B0811790DB90B (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (intptr_t, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, bool, const RuntimeMethod*))AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_m95122CB55DFAF463B14CBBA51C2B0811790DB90B_gshared)(___0_jclass, ___1_methodName, ___2_args, ___3_isStatic, method);
}
inline Il2CppSharedGenericObject* AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m34A45129A0AA339AC2E31492EF928112EDECC197 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, intptr_t, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m34A45129A0AA339AC2E31492EF928112EDECC197_gshared)(__this, ___0_methodID, ___1_args, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJNISafe_CallStaticIntMethod_m915549FA8FD7FB93B57A9708AD759488EA64418C (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJNISafe_CallStaticBooleanMethod_m652685AC18F590965249C0F9B107C00C142595BB (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJNISafe_CallStaticSByteMethod_m3E1F75978A2D686BC32DBF5A2F1F70F0D746C2B7 (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJNISafe_CallStaticShortMethod_m8330383670ECCD7E24CDD68C419745E486FA6426 (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJNISafe_CallStaticLongMethod_mDDE01239BEFCF007ECE05E51A249B3EB5BB61234 (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJNISafe_CallStaticFloatMethod_m3F5419A10B9DF599352938B2BAD8866F8F112364 (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJNISafe_CallStaticDoubleMethod_m73F1D51601D6849EE480389B4E43AED68C42B2B5 (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJNISafe_CallStaticCharMethod_mC4B40190CE095728E823AB8B724ECDC8F4B36155 (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AndroidJNISafe_CallStaticStringMethod_m4E150E34CC6DBF27A955F8DAEE5941D6E10879C0 (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387 (intptr_t ___0_clazz, intptr_t ___1_methodID, Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 ___2_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m9EDC162A865064239ADA4058CD2A81B1A2CF6D0F (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m4F31B91D2F03C3068A4BE95DB368B6DF715CE316 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_mF9D318147E29742DAA4B5C033BFC68D29CC1123B (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, intptr_t, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_mF9D318147E29742DAA4B5C033BFC68D29CC1123B_gshared)(__this, ___0_methodID, ___1_args, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJNISafe_GetBooleanField_m34F37B560A6AEC81B9061FB3B72698C84720435D (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNISafe_GetObjectField_mCF3BB1C38718D6F55081126BC7F6C286B382B275 (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mC408C8B80896737BA7DE6C18A904D2157E5FD364 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetFieldID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6F960B6375B61125FD05A03EBE8CEC790A986044 (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Get_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mBEAFA1EB54999ED8C620F9B4F0E2D3CA2530CECE (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJNISafe_GetIntField_mBD983688B73063DE5C55D320F60F266443FAC97C (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m61DD76EF7E812BC3ED41BBA0D0DA1B430EC61256 (intptr_t ___0_jobject, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetFieldID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1B394DCE5ED40DFA09EB18874BA2FFC3B5E50B2E (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Get_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mB506BB319D4A193AE597F3C3166DDB165593D4D6 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AndroidJNISafe_GetStringField_mADFCA05D6DE790600B57E90B20F2E75AFC036B0F (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* AndroidJavaObject_AndroidJavaClassDeleteLocalRef_m56C84D7516BCB51A84E8AFDB3FCA46BAF494548F (intptr_t ___0_jclass, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* AndroidJavaObject_AndroidJavaObjectDeleteLocalRef_m2ECEEAF6389ABB9D6B963B8A98568ECD9413DF3C (intptr_t ___0_jobject, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisIl2CppSharedGenericObject_mB1CEFE624E92B82D234C1982AA2A7DF6AC5FD284 (intptr_t ___0_jobject, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (intptr_t, const RuntimeMethod*))AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisIl2CppSharedGenericObject_mB1CEFE624E92B82D234C1982AA2A7DF6AC5FD284_gshared)(___0_jobject, method);
}
inline intptr_t AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mDFF491584A820BE0629A47ED3B790A199050D589 (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method)
{
	return ((  intptr_t (*) (intptr_t, String_t*, bool, const RuntimeMethod*))AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mDFF491584A820BE0629A47ED3B790A199050D589_gshared)(___0_jclass, ___1_fieldName, ___2_isStatic, method);
}
inline Il2CppSharedGenericObject* AndroidJavaObject__Get_TisIl2CppSharedGenericObject_mD7317316E4D5BBE57FE9495B6E117EEFFA6F149E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, intptr_t, const RuntimeMethod*))AndroidJavaObject__Get_TisIl2CppSharedGenericObject_mD7317316E4D5BBE57FE9495B6E117EEFFA6F149E_gshared)(__this, ___0_fieldID, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJNISafe_GetSByteField_mAD3B08AA8A97F77CAE17DD25B0F389AFAC2023B1 (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJNISafe_GetShortField_m5D21E87061C1DAC89DF58671C53432D0361F0C6E (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJNISafe_GetLongField_m7DD751358D10BB276D8A95D413B9DFB1E8EE81D8 (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJNISafe_GetFloatField_m1EAA1ED33002BBA28CA2B630521D6BF1B7D3A2E7 (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJNISafe_GetDoubleField_mBCBD5E80223EDECC06FA783F34149E3625219074 (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJNISafe_GetCharField_m8301FA96B40E27C032590FE3F8E84A777A4739C3 (intptr_t ___0_obj, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJNISafe_GetStaticIntField_m0698D50C44E490A009E8388C7321630DED5973BD (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNISafe_GetStaticObjectField_mB6B9A9EB2619DFDF1DA56300BF9FEC19BF883867 (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__GetStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m86131C9F9AC1A6082963096E96DDDDDD7366189E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AndroidJNISafe_GetStaticStringField_mB3D1325B08A38C7DAF1FA3E6CB52F6D8E0A2CB47 (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
inline Il2CppSharedGenericObject* AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_mF12B3A136CBDB98DF0876DF1FC47EA4A14D98A17 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method)
{
	return ((  Il2CppSharedGenericObject* (*) (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*, intptr_t, const RuntimeMethod*))AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_mF12B3A136CBDB98DF0876DF1FC47EA4A14D98A17_gshared)(__this, ___0_fieldID, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJNISafe_GetStaticBooleanField_m172BEAA3F0AB6754EA5F1AD30C36DAA0D3D7C666 (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJNISafe_GetStaticSByteField_m77596E5B1AE58DAFF39268AC954CAD53974A688D (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJNISafe_GetStaticShortField_m83716D4D85B30F26803F866AC47D5C04AAB5D320 (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJNISafe_GetStaticLongField_mABC2B933CEB757E3FAF1FD6C60AA0C4D38E9C49D (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJNISafe_GetStaticFloatField_mD1456B729026959309A839C2647279C0B6541356 (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJNISafe_GetStaticDoubleField_mEB86F2CE1F3879AAA9DEDA4B496F882C0E1DCBC2 (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJNISafe_GetStaticCharField_mF70F6D197261364AF2A9E875D84DDDA35BD0ED96 (intptr_t ___0_clazz, intptr_t ___1_fieldID, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetIntField_mD238DA37BA1B3D7693484237951A6EFEA9C62120 (intptr_t ___0_obj, intptr_t ___1_fieldID, int32_t ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetBooleanField_m5279EA41B214699E79733DC6C93259CC9DCA1D9E (intptr_t ___0_obj, intptr_t ___1_fieldID, bool ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetSByteField_mB021168746571E7CAA8C0EAD7AA7F02C18B5EE33 (intptr_t ___0_obj, intptr_t ___1_fieldID, int8_t ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetShortField_mF95E569C142DEDD604CE8BA7617328B3EDDD2F0D (intptr_t ___0_obj, intptr_t ___1_fieldID, int16_t ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetLongField_m13905547F5CDC7E01AB0D8C787BF98DC2870EC35 (intptr_t ___0_obj, intptr_t ___1_fieldID, int64_t ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetFloatField_m589CA6B8DD2BFD4515C5AEAE3772782B293F02C3 (intptr_t ___0_obj, intptr_t ___1_fieldID, float ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetDoubleField_mE93D0C5EC2019A1B657BD32970FE6EFC9B005A58 (intptr_t ___0_obj, intptr_t ___1_fieldID, double ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetCharField_m69D09A6A2CEA55D84B240FE32D90300AAB1334F9 (intptr_t ___0_obj, intptr_t ___1_fieldID, Il2CppChar ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStringField_m649363D4E87763D6A9760359EAFB29802E90B409 (intptr_t ___0_obj, intptr_t ___1_fieldID, String_t* ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetObjectField_mFE500926F9C963FF106E8AA30A16F4C671BAA8CA (intptr_t ___0_obj, intptr_t ___1_fieldID, intptr_t ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJavaProxy_GetRawProxy_m685E066A4D378B596CD88385B954AE90CBF328A9 (AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_ConvertToJNIArray_mBEAE4605FF297D19AFB8CE4E8443C9C0F87E9A13 (RuntimeArray* ___0_array, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticIntField_m1E20F6C72260CAFBF73207DCEC1816B2816EEBE1 (intptr_t ___0_clazz, intptr_t ___1_fieldID, int32_t ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticBooleanField_mBE4E40DA1B07A29D356AEEE6CB9519F2B3621AC9 (intptr_t ___0_clazz, intptr_t ___1_fieldID, bool ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticSByteField_m242120982A9227E1E8344FFE9F06FD74986D15E9 (intptr_t ___0_clazz, intptr_t ___1_fieldID, int8_t ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticShortField_m92534AAA86D7E1055E12936C8A7BD6B865B7DB81 (intptr_t ___0_clazz, intptr_t ___1_fieldID, int16_t ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticLongField_m299AAC2DE8B6747B0B5E109BABB2F3A4FC1F486E (intptr_t ___0_clazz, intptr_t ___1_fieldID, int64_t ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticFloatField_mB2EDDE632AB2088CD12F1FD12174FB86990BCBEE (intptr_t ___0_clazz, intptr_t ___1_fieldID, float ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticDoubleField_mA0253927D476917D2158A9CE29F1BF535485B956 (intptr_t ___0_clazz, intptr_t ___1_fieldID, double ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticCharField_m2B8245275C36525798C869B7B1088B25BA663613 (intptr_t ___0_clazz, intptr_t ___1_fieldID, Il2CppChar ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticStringField_m445D977B2374056C6E4607FAEDB7E99A1353E2EE (intptr_t ___0_clazz, intptr_t ___1_fieldID, String_t* ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNISafe_SetStaticObjectField_m7757F7E30F8122DAF89F138A8AE727CB896BC721 (intptr_t ___0_clazz, intptr_t ___1_fieldID, intptr_t ___2_val, const RuntimeMethod* method) ;
inline void U3CU3Ec__DisplayClass0_0_1__ctor_mF0054910492777A7D64055D8F895832306C7FF6B (U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6* __this, const RuntimeMethod* method)
{
	((  void (*) (U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6*, const RuntimeMethod*))U3CU3Ec__DisplayClass0_0_1__ctor_m949C96554EA19FE4E66F6E8641BC1144CFF4164E_fshared)(__this, method);
}
inline ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_inline (const RuntimeMethod* method)
{
	return ((  ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* (*) (const RuntimeMethod*))Array_Empty_TisIl2CppSharedGenericObject_m688909407CF299AE69145CC33ACB1E90C775C653_gshared_inline)(method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerable_Range_m4FB9BBBA09BEF5177C13506DB385CF4467C15FD8 (int32_t ___0_start, int32_t ___1_count, const RuntimeMethod* method) ;
inline void Func_2__ctor_m266186AD54086DE797B83FC2FF930C212522BB9A (Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A*, RuntimeObject*, intptr_t, const RuntimeMethod*))Func_2__ctor_m266186AD54086DE797B83FC2FF930C212522BB9A_gshared)(__this, ___0_object, ___1_method, method);
}
inline RuntimeObject* Enumerable_Select_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_TisIl2CppSharedGenericObject_m9A1A985DC3BC579FA94A34322A551416C1A99804 (RuntimeObject* ___0_source, Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A* ___1_selector, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (RuntimeObject*, Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A*, const RuntimeMethod*))Enumerable_Select_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_TisIl2CppSharedGenericObject_m9A1A985DC3BC579FA94A34322A551416C1A99804_gshared)(___0_source, ___1_selector, method);
}
inline List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* Enumerable_ToList_TisIl2CppSharedGenericObject_mFF96E16527D742B009531AEA8672FF89A8A379E2 (RuntimeObject* ___0_source, const RuntimeMethod* method)
{
	return ((  List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* (*) (RuntimeObject*, const RuntimeMethod*))Enumerable_ToList_TisIl2CppSharedGenericObject_mFF96E16527D742B009531AEA8672FF89A8A379E2_gshared)(___0_source, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 Playable_GetHandle_m39356D23E849DC5428B262092657662C064E04F8_inline (Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnimationPlayableExtensions_SetAnimatedPropertiesInternal_m9038247416A38E252EF0DCBCCBFE990589F4C51F (PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4* ___0_playable, AnimationClip_t00BD2F131D308A4AD2C6B0BF66644FC25FECE712* ___1_animatedProperties, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56 (const RuntimeMethod* method) ;
// Method Definition Index: 83103
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_IsInsideScope_TisIl2CppSharedGenericObject_m24EFB513F5A18B9400EB5F08E463BF44AF9EA8CE_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_get_Keys_m9CB10267CF4FC83CDDD3FDC4B898C73488C93FB8_RuntimeMethod_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D V_0;
	memset((&V_0), 0, sizeof(V_0));
	Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE V_1;
	memset((&V_1), 0, sizeof(V_1));
	LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D V_2;
	memset((&V_2), 0, sizeof(V_2));
	bool V_3 = false;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (LifecycleScope_tEA98D004E7CFF981DC51524B020E4CBD58D964CB_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_2 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_3;
		L_3 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_2, NULL);
		NullCheck(L_1);
		bool L_4;
		L_4 = VirtualFuncInvoker1< bool, Type_t* >::Invoke(24, L_1, L_3);
		if (!L_4)
		{
			goto IL_0039;
		}
	}
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		LifecycleScopeKey__ctor_m7DCCBE2ED25A7746808F3555AB845EE71FEB6A71((&V_0), L_6, NULL);
		Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_7 = __this->____activeScopes;
		LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_8 = V_0;
		NullCheck(L_7);
		bool L_9;
		L_9 = Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281(L_7, L_8, Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281_RuntimeMethod_var);
		return L_9;
	}

IL_0039:
	{
		Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_10 = __this->____activeScopes;
		NullCheck(L_10);
		KeyCollection_t59AB6D7A56AAB8B527E4EEBED39397629A9430E0* L_11;
		L_11 = Dictionary_2_get_Keys_m9CB10267CF4FC83CDDD3FDC4B898C73488C93FB8(L_10, Dictionary_2_get_Keys_m9CB10267CF4FC83CDDD3FDC4B898C73488C93FB8_RuntimeMethod_var);
		NullCheck(L_11);
		Enumerator_t1D172B50FF172AB824E07EA102C607D9282D3CAE L_12;
		L_12 = KeyCollection_GetEnumerator_mB40FC79A8A11DB8F002E9192693045C2F194E3E7(L_11, NULL);
		V_1 = L_12;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_007b:
			{
				Enumerator_Dispose_mD343B871CBC9CC3590A9CB7326BB5E263AD03BE1((&V_1), NULL);
				return;
			}
		});
		try
		{
			{
				goto IL_0070_1;
			}

IL_004c_1:
			{
				LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_13;
				L_13 = Enumerator_get_Current_m21C7125C1D35261357C56A874C207249FC3E4F4A_inline((&V_1), NULL);
				V_2 = L_13;
				Type_t* L_14;
				L_14 = LifecycleScopeKey_get_Type_mEC585239B2BC794EDFDE68B89A6533971E12C752_inline((&V_2), NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_15 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_16;
				L_16 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_15, NULL);
				bool L_17;
				L_17 = Type_op_Equality_m99930A0E44E420A685FABA60E60BA1CC5FA0EBDC(L_14, L_16, NULL);
				if (!L_17)
				{
					goto IL_0070_1;
				}
			}
			{
				V_3 = (bool)1;
				goto IL_008b;
			}

IL_0070_1:
			{
				bool L_18;
				L_18 = Enumerator_MoveNext_mFEAE796204BD77488174AB22E0DE4C2721159335((&V_1), NULL);
				if (L_18)
				{
					goto IL_004c_1;
				}
			}
			{
				goto IL_0089;
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

IL_0089:
	{
		return (bool)0;
	}

IL_008b:
	{
		bool L_19 = V_3;
		return L_19;
	}
}
// Method Definition Index: 83106
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_m65AF7C880B3A8B7B6F7C6F59C699EA3FC8FC1BEE_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_scopeWithContext, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_0 = ___0_scopeWithContext;
		NullCheck((RuntimeObject*)L_0);
		Type_t* L_1;
		L_1 = il2cpp_codegen_object_get_type((RuntimeObject*)L_0);
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_2 = ___0_scopeWithContext;
		NullCheck(L_2);
		Il2CppSharedGenericObject* L_3;
		L_3 = LifecycleScopeWithContext_1_get_Context_m71715E88B71B9A07D4F70E53A5142B8F5383AC27_inline(L_2, NULL);
		LifecycleScopeKey__ctor_m3BFDFB377FD3F489668C9B18AF174EF462838E6C((&V_0), L_1, (RuntimeObject*)L_3, NULL);
		Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_4 = __this->____activeScopes;
		LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_5 = V_0;
		NullCheck(L_4);
		bool L_6;
		L_6 = Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281(L_4, L_5, Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281_RuntimeMethod_var);
		return L_6;
	}
}
// Method Definition Index: 83105
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_TisIl2CppSharedGenericObject_m5267E4C686C348E337CB4A9ED51372D608957B0F_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, Il2CppSharedGenericObject* ___0_scopeContext, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281_RuntimeMethod_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		Il2CppSharedGenericObject* L_2 = ___0_scopeContext;
		LifecycleScopeKey__ctor_m3BFDFB377FD3F489668C9B18AF174EF462838E6C((&V_0), L_1, (RuntimeObject*)L_2, NULL);
		Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_3 = __this->____activeScopes;
		LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_4 = V_0;
		NullCheck(L_3);
		bool L_5;
		L_5 = Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281(L_3, L_4, Dictionary_2_ContainsKey_m43D6ABEDBE5F0A936823EEB675589E5EAFD43281_RuntimeMethod_var);
		return L_5;
	}
}
// Method Definition Index: 83108
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_IsOrWillBeInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_TisIl2CppSharedGenericObject_m419B96676C8763DBFC6F67EFD3F1F972EBBC03B0_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, Il2CppSharedGenericObject* ___0_activationContext, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_m4CA597288EC15016493B62A210A4A63E27177117_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_m132E4E286C55B63A011879B1F62299A7E6CF8DE2_RuntimeMethod_var);
		il2cpp_rgctx_method_init(method);
	}
	//<source_info:<no-source>:1>
	bool V_0 = false;
	Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201 V_1;
	memset((&V_1), 0, sizeof(V_1));
	ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* V_2 = NULL;
	Il2CppSharedGenericObject* V_3 = NULL;
	bool V_4 = false;
	int32_t V_5 = 0;
	{
		Il2CppSharedGenericObject* L_0 = ___0_activationContext;
		bool L_1;
		L_1 = ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_TisIl2CppSharedGenericObject_m5267E4C686C348E337CB4A9ED51372D608957B0F(__this, L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_1;
		Queue_1_t0213AA1BB5D12D3BCD5727E71F92BE13870D391A* L_2 = __this->____transitionRequestQueue;
		NullCheck(L_2);
		Enumerator_t6E1C3DAF9D5E130F31F921E6BBBA1D4E27891201 L_3;
		L_3 = Queue_1_GetEnumerator_m311A1E04C52E396219D02261B478965B9C97BD3D(L_2, NULL);
		V_1 = L_3;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_007d:
			{
				Enumerator_Dispose_m5560B98567B989C4754E5C182136BE4AC88848CB((&V_1), NULL);
				return;
			}
		});
		try
		{
			{
				goto IL_0072_1;
			}

IL_0016_1:
			{
				ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* L_4;
				L_4 = Enumerator_get_Current_m132E4E286C55B63A011879B1F62299A7E6CF8DE2((&V_1), Enumerator_get_Current_m132E4E286C55B63A011879B1F62299A7E6CF8DE2_RuntimeMethod_var);
				V_2 = L_4;
				ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* L_5 = V_2;
				NullCheck(L_5);
				LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_6;
				L_6 = ScopeTransitionRequestBase_get_Scope_mEB7F256D6FF4393EB627213D2E7343BD35BDCB50_inline(L_5, NULL);
				V_3 = ((Il2CppSharedGenericObject*)IsInst((RuntimeObject*)L_6, il2cpp_rgctx_data(method->rgctx_data, 1)));
				Il2CppSharedGenericObject* L_7 = V_3;
				if (!L_7)
				{
					goto IL_0072_1;
				}
			}
			{
				Il2CppSharedGenericObject* L_8 = V_3;
				NullCheck((LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599*)L_8);
				Il2CppSharedGenericObject* L_9;
				L_9 = LifecycleScopeWithContext_1_get_Context_m71715E88B71B9A07D4F70E53A5142B8F5383AC27_inline((LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599*)L_8, NULL);
				Il2CppSharedGenericObject* L_10 = ___0_activationContext;
				if ((!(((RuntimeObject*)(Il2CppSharedGenericObject*)L_9) == ((RuntimeObject*)(Il2CppSharedGenericObject*)L_10))))
				{
					goto IL_0072_1;
				}
			}
			{
				ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* L_11 = V_2;
				NullCheck(L_11);
				int32_t L_12;
				L_12 = ScopeTransitionRequestBase_get_TransitionType_m64E9CBFD2036C2727F62B4276B07594B1AD0244B_inline(L_11, NULL);
				V_5 = L_12;
				int32_t L_13 = V_5;
				if (!L_13)
				{
					goto IL_0062_1;
				}
			}
			{
				int32_t L_14 = V_5;
				if ((((int32_t)L_14) == ((int32_t)1)))
				{
					goto IL_0067_1;
				}
			}
			{
				goto IL_006c_1;
			}

IL_0062_1:
			{
				V_4 = (bool)1;
				goto IL_006f_1;
			}

IL_0067_1:
			{
				V_4 = (bool)0;
				goto IL_006f_1;
			}

IL_006c_1:
			{
				bool L_15 = V_0;
				V_4 = L_15;
			}

IL_006f_1:
			{
				bool L_16 = V_4;
				V_0 = L_16;
			}

IL_0072_1:
			{
				bool L_17;
				L_17 = Enumerator_MoveNext_m4CA597288EC15016493B62A210A4A63E27177117((&V_1), Enumerator_MoveNext_m4CA597288EC15016493B62A210A4A63E27177117_RuntimeMethod_var);
				if (L_17)
				{
					goto IL_0016_1;
				}
			}
			{
				goto IL_008b;
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

IL_008b:
	{
		bool L_18 = V_0;
		return L_18;
	}
}
// Method Definition Index: 83111
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_PrepareTryEnterScope_TisIl2CppSharedGenericObject_m1169BAF322245C6864C2461777208938DEC47599_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, Il2CppSharedGenericObject* ___0_lifecycleScope, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_1_t349E66EC5F09B881A8E52EE40A1AB9EC60E08E44_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_1_t73FD060C436E3C4264A734C8F8DCC01DFF6046B8_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral13E8ABAF4FCA9340FD639F44DC79CC73FDE724C1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral84DED65F0B23E6F91065E5F2EFCC5631A498E3E4);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC5C3A106612C3457D1A199E43E729AE62EDAD988);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralCC59788671EF0B981BF46F9F140D4B1454A1146D);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF0C0210C1550DE6C68F93F6E0EEC8AD3FE4F23F3);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit);
	//<source_info:<no-source>:1>
	bool V_0 = false;
	RuntimeObject* V_1 = NULL;
	String_t* V_2 = NULL;
	bool V_3 = false;
	{
		Il2CppSharedGenericObject* L_0 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_0);
		RuntimeObject* L_1;
		L_1 = LifecycleScopeBase_get_RequiredOuterScopes_mE1B35D619BCE1A19CF63F53E917F8D86731225B7((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_0, NULL);
		V_0 = (bool)1;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, IEnumerable_1_t349E66EC5F09B881A8E52EE40A1AB9EC60E08E44_il2cpp_TypeInfo_var, L_1);
		V_1 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0070:
			{
				{
					RuntimeObject* L_3 = V_1;
					if (!L_3)
					{
						goto IL_0079;
					}
				}
				{
					RuntimeObject* L_4 = V_1;
					NullCheck(L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_4);
				}

IL_0079:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0066_1;
			}

IL_0015_1:
			{
				RuntimeObject* L_5 = V_1;
				NullCheck(L_5);
				String_t* L_6;
				L_6 = InterfaceFuncInvoker0< String_t* >::Invoke(0, IEnumerator_1_t73FD060C436E3C4264A734C8F8DCC01DFF6046B8_il2cpp_TypeInfo_var, L_5);
				V_2 = L_6;
				String_t* L_7 = V_2;
				bool L_8;
				L_8 = ActiveLifecycleScopesTracker_IsInsideScope_m326DF0210C805B6FEFF0848D0A1C1891D1AD8ABE(__this, L_7, NULL);
				if (L_8)
				{
					goto IL_0066_1;
				}
			}
			{
				V_0 = (bool)0;
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_9 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)5);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_10 = L_9;
				NullCheck(L_10);
				(L_10)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)_stringLiteralF0C0210C1550DE6C68F93F6E0EEC8AD3FE4F23F3);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_11 = L_10;
				Il2CppSharedGenericObject* L_12 = ___0_lifecycleScope;
				NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_12);
				String_t* L_13;
				L_13 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_12, NULL);
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(1), (String_t*)L_13);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_14 = L_11;
				NullCheck(L_14);
				(L_14)->SetAt(static_cast<il2cpp_array_size_t>(2), (String_t*)_stringLiteralC5C3A106612C3457D1A199E43E729AE62EDAD988);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_15 = L_14;
				String_t* L_16 = V_2;
				NullCheck(L_15);
				(L_15)->SetAt(static_cast<il2cpp_array_size_t>(3), (String_t*)L_16);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_17 = L_15;
				NullCheck(L_17);
				(L_17)->SetAt(static_cast<il2cpp_array_size_t>(4), (String_t*)_stringLiteral13E8ABAF4FCA9340FD639F44DC79CC73FDE724C1);
				String_t* L_18;
				L_18 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_17, NULL);
				CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3(L_18, (bool)1, NULL);
				V_3 = (bool)0;
				goto IL_00d1;
			}

IL_0066_1:
			{
				RuntimeObject* L_19 = V_1;
				NullCheck(L_19);
				bool L_20;
				L_20 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_19);
				if (L_20)
				{
					goto IL_0015_1;
				}
			}
			{
				goto IL_007a;
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

IL_007a:
	{
		bool L_21 = V_0;
		if (L_21)
		{
			goto IL_009f;
		}
	}
	{
		Il2CppSharedGenericObject* L_22 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_22);
		String_t* L_23;
		L_23 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_22, NULL);
		String_t* L_24;
		L_24 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteralF0C0210C1550DE6C68F93F6E0EEC8AD3FE4F23F3, L_23, _stringLiteral84DED65F0B23E6F91065E5F2EFCC5631A498E3E4, NULL);
		CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3(L_24, (bool)1, NULL);
		return (bool)0;
	}

IL_009f:
	{
		Il2CppSharedGenericObject* L_25 = ___0_lifecycleScope;
		bool L_26;
		L_26 = ActiveLifecycleScopesTracker_IsInsideScope_m6A9229A85BC7290BB7CABC0F5726CF29CE197886(__this, (LifecycleScope_tEA98D004E7CFF981DC51524B020E4CBD58D964CB*)L_25, NULL);
		if (!L_26)
		{
			goto IL_00cf;
		}
	}
	{
		Il2CppSharedGenericObject* L_27 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_27);
		String_t* L_28;
		L_28 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_27, NULL);
		String_t* L_29;
		L_29 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteralF0C0210C1550DE6C68F93F6E0EEC8AD3FE4F23F3, L_28, _stringLiteralCC59788671EF0B981BF46F9F140D4B1454A1146D, NULL);
		CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3(L_29, (bool)1, NULL);
		return (bool)0;
	}

IL_00cf:
	{
		return (bool)1;
	}

IL_00d1:
	{
		bool L_30 = V_3;
		return L_30;
	}
}
// Method Definition Index: 83112
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_PrepareTryEnterScopeWithContext_TisIl2CppSharedGenericObject_m3D8B69D9E50547113449972B07933BE902744D59_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_1_t349E66EC5F09B881A8E52EE40A1AB9EC60E08E44_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_1_t73FD060C436E3C4264A734C8F8DCC01DFF6046B8_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral13E8ABAF4FCA9340FD639F44DC79CC73FDE724C1);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral84DED65F0B23E6F91065E5F2EFCC5631A498E3E4);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB9EDD49DDBC303A2087668B4B6E4ACAC60995136);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC5C3A106612C3457D1A199E43E729AE62EDAD988);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF0C0210C1550DE6C68F93F6E0EEC8AD3FE4F23F3);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit);
	//<source_info:<no-source>:1>
	bool V_0 = false;
	RuntimeObject* V_1 = NULL;
	String_t* V_2 = NULL;
	bool V_3 = false;
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_0 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_0);
		RuntimeObject* L_1;
		L_1 = LifecycleScopeBase_get_RequiredOuterScopes_mE1B35D619BCE1A19CF63F53E917F8D86731225B7((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_0, NULL);
		V_0 = (bool)1;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, IEnumerable_1_t349E66EC5F09B881A8E52EE40A1AB9EC60E08E44_il2cpp_TypeInfo_var, L_1);
		V_1 = L_2;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0066:
			{
				{
					RuntimeObject* L_3 = V_1;
					if (!L_3)
					{
						goto IL_006f;
					}
				}
				{
					RuntimeObject* L_4 = V_1;
					NullCheck(L_4);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_4);
				}

IL_006f:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_005c_1;
			}

IL_0010_1:
			{
				RuntimeObject* L_5 = V_1;
				NullCheck(L_5);
				String_t* L_6;
				L_6 = InterfaceFuncInvoker0< String_t* >::Invoke(0, IEnumerator_1_t73FD060C436E3C4264A734C8F8DCC01DFF6046B8_il2cpp_TypeInfo_var, L_5);
				V_2 = L_6;
				String_t* L_7 = V_2;
				bool L_8;
				L_8 = ActiveLifecycleScopesTracker_IsInsideScope_m326DF0210C805B6FEFF0848D0A1C1891D1AD8ABE(__this, L_7, NULL);
				if (L_8)
				{
					goto IL_005c_1;
				}
			}
			{
				V_0 = (bool)0;
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_9 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)5);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_10 = L_9;
				NullCheck(L_10);
				(L_10)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)_stringLiteralF0C0210C1550DE6C68F93F6E0EEC8AD3FE4F23F3);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_11 = L_10;
				LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_12 = ___0_lifecycleScope;
				NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_12);
				String_t* L_13;
				L_13 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_12, NULL);
				NullCheck(L_11);
				(L_11)->SetAt(static_cast<il2cpp_array_size_t>(1), (String_t*)L_13);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_14 = L_11;
				NullCheck(L_14);
				(L_14)->SetAt(static_cast<il2cpp_array_size_t>(2), (String_t*)_stringLiteralC5C3A106612C3457D1A199E43E729AE62EDAD988);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_15 = L_14;
				String_t* L_16 = V_2;
				NullCheck(L_15);
				(L_15)->SetAt(static_cast<il2cpp_array_size_t>(3), (String_t*)L_16);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_17 = L_15;
				NullCheck(L_17);
				(L_17)->SetAt(static_cast<il2cpp_array_size_t>(4), (String_t*)_stringLiteral13E8ABAF4FCA9340FD639F44DC79CC73FDE724C1);
				String_t* L_18;
				L_18 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_17, NULL);
				CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3(L_18, (bool)1, NULL);
				V_3 = (bool)0;
				goto IL_00be;
			}

IL_005c_1:
			{
				RuntimeObject* L_19 = V_1;
				NullCheck(L_19);
				bool L_20;
				L_20 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_19);
				if (L_20)
				{
					goto IL_0010_1;
				}
			}
			{
				goto IL_0070;
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

IL_0070:
	{
		bool L_21 = V_0;
		if (L_21)
		{
			goto IL_0090;
		}
	}
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_22 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_22);
		String_t* L_23;
		L_23 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_22, NULL);
		String_t* L_24;
		L_24 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteralF0C0210C1550DE6C68F93F6E0EEC8AD3FE4F23F3, L_23, _stringLiteral84DED65F0B23E6F91065E5F2EFCC5631A498E3E4, NULL);
		CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3(L_24, (bool)1, NULL);
		return (bool)0;
	}

IL_0090:
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_25 = ___0_lifecycleScope;
		bool L_26;
		L_26 = ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_m65AF7C880B3A8B7B6F7C6F59C699EA3FC8FC1BEE(__this, L_25, il2cpp_rgctx_method(method->rgctx_data, 0));
		if (!L_26)
		{
			goto IL_00bc;
		}
	}
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_27 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_27);
		String_t* L_28;
		L_28 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_27, NULL);
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_29 = ___0_lifecycleScope;
		NullCheck(L_29);
		Il2CppSharedGenericObject* L_30;
		L_30 = LifecycleScopeWithContext_1_get_Context_m71715E88B71B9A07D4F70E53A5142B8F5383AC27_inline(L_29, NULL);
		String_t* L_31;
		L_31 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteralB9EDD49DDBC303A2087668B4B6E4ACAC60995136, L_28, (RuntimeObject*)L_30, NULL);
		CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3(L_31, (bool)1, NULL);
		return (bool)0;
	}

IL_00bc:
	{
		return (bool)1;
	}

IL_00be:
	{
		bool L_32 = V_3;
		return L_32;
	}
}
// Method Definition Index: 83118
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_PrepareTryExitScope_TisIl2CppSharedGenericObject_mB20E9E3D9D7A9E4B5F58A5783EB2EBB2733EF4E7_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, Il2CppSharedGenericObject* ___1_activationContext, bool ___2_alsoExitNestedScopes, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Remove_mAE2ECC0F5BDAA58524500656449EA1A387079726_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_get_Values_m79563C16853F891CCBBC22B07DC63DA063874A49_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral00B9D6CD2F31410E688E8B500A17CB53A5159DC7);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral085E2DF95CBA541D8E08411BA60759DB8E782A9E);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral190A3A7EF1BCDD282CB0B05FB99BEDDD2405B809);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral24A0D1C954F6F30A64E2AB214561ABB5C363D424);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9C503A4CDCF098739894D1E4B88AEFF08E94B305);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9F7B27B5BF4E1DCCB7582CE12885371A3C21BF8C);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA4EC0E533020275EA02CC859DA22C7578B5C0119);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit);
	//<source_info:<no-source>:1>
	List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14* V_0 = NULL;
	int32_t V_1 = 0;
	Enumerator_t624742B8D885649511A5739F11184E2221942252 V_2;
	memset((&V_2), 0, sizeof(V_2));
	Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352 V_3;
	memset((&V_3), 0, sizeof(V_3));
	ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* V_4 = NULL;
	ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* V_5 = NULL;
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_0 = ___0_lifecycleScope;
		bool L_1;
		L_1 = ActiveLifecycleScopesTracker_IsInsideScopeWithActivationContext_TisIl2CppSharedGenericObject_m65AF7C880B3A8B7B6F7C6F59C699EA3FC8FC1BEE(__this, L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		if (L_1)
		{
			goto IL_0027;
		}
	}
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_2 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_2);
		String_t* L_3;
		L_3 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_2, NULL);
		Il2CppSharedGenericObject* L_4 = ___1_activationContext;
		String_t* L_5;
		L_5 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral9F7B27B5BF4E1DCCB7582CE12885371A3C21BF8C, L_3, (RuntimeObject*)L_4, NULL);
		CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3(L_5, (bool)1, NULL);
		return (bool)0;
	}

IL_0027:
	{
		V_0 = (List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14*)NULL;
		V_1 = 0;
		Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_6 = __this->____activeScopes;
		NullCheck(L_6);
		ValueCollection_tB7C740A97431C55C62CC43DA7712B5896DBED97F* L_7;
		L_7 = Dictionary_2_get_Values_m79563C16853F891CCBBC22B07DC63DA063874A49(L_6, Dictionary_2_get_Values_m79563C16853F891CCBBC22B07DC63DA063874A49_RuntimeMethod_var);
		NullCheck(L_7);
		Enumerator_t624742B8D885649511A5739F11184E2221942252 L_8;
		L_8 = ValueCollection_GetEnumerator_m1B01F115659146468CB5EE9025E45905AB1279DE(L_7, NULL);
		V_2 = L_8;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_006b:
			{
				Enumerator_Dispose_m6E582A0ECA46CE69AFD14D67DE233D2B4E1069BE((&V_2), NULL);
				return;
			}
		});
		try
		{
			{
				goto IL_0060_1;
			}

IL_003e_1:
			{
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_9;
				L_9 = Enumerator_get_Current_m2E4C1612FFBB402B6CF496C40499B9B84734E878_inline((&V_2), NULL);
				NullCheck(L_9);
				LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_10;
				L_10 = ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline(L_9, NULL);
				NullCheck(L_10);
				String_t* L_11;
				L_11 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline(L_10, NULL);
				LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_12 = ___0_lifecycleScope;
				NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_12);
				String_t* L_13;
				L_13 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_12, NULL);
				bool L_14;
				L_14 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_11, L_13, NULL);
				if (!L_14)
				{
					goto IL_0060_1;
				}
			}
			{
				int32_t L_15 = V_1;
				V_1 = ((int32_t)il2cpp_codegen_add(L_15, 1));
			}

IL_0060_1:
			{
				bool L_16;
				L_16 = Enumerator_MoveNext_mC8DD05ED5A94E9D71568B93F1FBC1A03B3A4E385((&V_2), NULL);
				if (L_16)
				{
					goto IL_003e_1;
				}
			}
			{
				goto IL_0079;
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

IL_0079:
	{
		int32_t L_17 = V_1;
		if ((!(((uint32_t)L_17) == ((uint32_t)1))))
		{
			goto IL_008b;
		}
	}
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_18 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_18);
		String_t* L_19;
		L_19 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_18, NULL);
		ActiveLifecycleScopesTracker_CollectNestedScopesToExit_m7489DDE99FDA05A011E202D63B4146E96B7761AD(__this, L_19, (&V_0), NULL);
	}

IL_008b:
	{
		List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14* L_20 = V_0;
		if (!L_20)
		{
			goto IL_01cb;
		}
	}
	{
		bool L_21 = ___2_alsoExitNestedScopes;
		if (L_21)
		{
			goto IL_0120;
		}
	}
	{
		List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14* L_22 = V_0;
		NullCheck(L_22);
		Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352 L_23;
		L_23 = List_1_GetEnumerator_m3A7E9AECB2ED791F09FADAD0BC7B826FB62A85A4(L_22, NULL);
		V_3 = L_23;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00f5:
			{
				Enumerator_Dispose_m80B75619D3D43280CFC1D0556BBFE923F8643835((&V_3), NULL);
				return;
			}
		});
		try
		{
			{
				goto IL_00ea_1;
			}

IL_00a0_1:
			{
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_24;
				L_24 = Enumerator_get_Current_m422894DCBE54CC53F8C3729BEE72B601B02C8686_inline((&V_3), NULL);
				V_4 = L_24;
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_25 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)5);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_26 = L_25;
				NullCheck(L_26);
				(L_26)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)_stringLiteral24A0D1C954F6F30A64E2AB214561ABB5C363D424);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_27 = L_26;
				LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_28 = ___0_lifecycleScope;
				NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_28);
				String_t* L_29;
				L_29 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_28, NULL);
				NullCheck(L_27);
				(L_27)->SetAt(static_cast<il2cpp_array_size_t>(1), (String_t*)L_29);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_30 = L_27;
				NullCheck(L_30);
				(L_30)->SetAt(static_cast<il2cpp_array_size_t>(2), (String_t*)_stringLiteral085E2DF95CBA541D8E08411BA60759DB8E782A9E);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_31 = L_30;
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_32 = V_4;
				NullCheck(L_32);
				LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_33;
				L_33 = ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline(L_32, NULL);
				NullCheck(L_33);
				String_t* L_34;
				L_34 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline(L_33, NULL);
				NullCheck(L_31);
				(L_31)->SetAt(static_cast<il2cpp_array_size_t>(3), (String_t*)L_34);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_35 = L_31;
				NullCheck(L_35);
				(L_35)->SetAt(static_cast<il2cpp_array_size_t>(4), (String_t*)_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D);
				String_t* L_36;
				L_36 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_35, NULL);
				CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3(L_36, (bool)0, NULL);
			}

IL_00ea_1:
			{
				bool L_37;
				L_37 = Enumerator_MoveNext_mD85ED63385062F8013FBC6C57320F2B23C975EFE((&V_3), NULL);
				if (L_37)
				{
					goto IL_00a0_1;
				}
			}
			{
				goto IL_0103;
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

IL_0103:
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_38 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_38);
		String_t* L_39;
		L_39 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_38, NULL);
		String_t* L_40;
		L_40 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(_stringLiteral00B9D6CD2F31410E688E8B500A17CB53A5159DC7, L_39, _stringLiteral9C503A4CDCF098739894D1E4B88AEFF08E94B305, NULL);
		CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		DebugLifecycle_ReportError_mDAF7BB6F8AD0C44C843238B5340CBB0F0A76F1B3(L_40, (bool)1, NULL);
		return (bool)0;
	}

IL_0120:
	{
		List_1_t9CDC351A1FD4D84CA6FE8FF3EAD218C909593E14* L_41 = V_0;
		NullCheck(L_41);
		Enumerator_t01FAC2D987248212FF45463152AFF3B57F39A352 L_42;
		L_42 = List_1_GetEnumerator_m3A7E9AECB2ED791F09FADAD0BC7B826FB62A85A4(L_41, NULL);
		V_3 = L_42;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_01bd:
			{
				Enumerator_Dispose_m80B75619D3D43280CFC1D0556BBFE923F8643835((&V_3), NULL);
				return;
			}
		});
		try
		{
			{
				goto IL_01af_1;
			}

IL_012c_1:
			{
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_43;
				L_43 = Enumerator_get_Current_m422894DCBE54CC53F8C3729BEE72B601B02C8686_inline((&V_3), NULL);
				V_5 = L_43;
				CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_44;
				L_44 = DebugLifecycle_get_LoggingEnabled_mADB463016DF27BC61A483A646E5B999ABB3A8D94_inline(NULL);
				if (!L_44)
				{
					goto IL_017c_1;
				}
			}
			{
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_45 = (StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248*)SZArrayNew(StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248_il2cpp_TypeInfo_var, (uint32_t)5);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_46 = L_45;
				NullCheck(L_46);
				(L_46)->SetAt(static_cast<il2cpp_array_size_t>(0), (String_t*)_stringLiteralA4EC0E533020275EA02CC859DA22C7578B5C0119);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_47 = L_46;
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_48 = V_5;
				NullCheck(L_48);
				LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_49;
				L_49 = ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline(L_48, NULL);
				NullCheck(L_49);
				String_t* L_50;
				L_50 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline(L_49, NULL);
				NullCheck(L_47);
				(L_47)->SetAt(static_cast<il2cpp_array_size_t>(1), (String_t*)L_50);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_51 = L_47;
				NullCheck(L_51);
				(L_51)->SetAt(static_cast<il2cpp_array_size_t>(2), (String_t*)_stringLiteral190A3A7EF1BCDD282CB0B05FB99BEDDD2405B809);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_52 = L_51;
				LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_53 = ___0_lifecycleScope;
				NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_53);
				String_t* L_54;
				L_54 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_53, NULL);
				NullCheck(L_52);
				(L_52)->SetAt(static_cast<il2cpp_array_size_t>(3), (String_t*)L_54);
				StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* L_55 = L_52;
				NullCheck(L_55);
				(L_55)->SetAt(static_cast<il2cpp_array_size_t>(4), (String_t*)_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D);
				String_t* L_56;
				L_56 = String_Concat_m647EBF831F54B6DF7D5AFA5FD012CF4EE7571B6A(L_55, NULL);
				CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				DebugLifecycle_Log_mF5AF374C4A49186C856B489B40F56163BE723BBB(L_56, NULL);
			}

IL_017c_1:
			{
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_57 = V_5;
				ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347* L_58 = __this->____scopeTransitionHelper;
				NullCheck(L_57);
				VirtualActionInvoker1< ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347* >::Invoke(6, L_57, L_58);
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_59 = V_5;
				NullCheck(L_59);
				LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_60;
				L_60 = ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline(L_59, NULL);
				NullCheck(L_60);
				Type_t* L_61;
				L_61 = il2cpp_codegen_object_get_type(L_60);
				ActiveLifecycleScopesTracker_RaiseAutoCleanups_m5620B257E893397698E4CA9CBD38E695BADBA8ED(__this, L_61, 1, NULL);
				Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_62 = __this->____activeScopes;
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_63 = V_5;
				NullCheck(L_63);
				LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_64;
				L_64 = VirtualFuncInvoker0< LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D >::Invoke(4, L_63);
				NullCheck(L_62);
				bool L_65;
				L_65 = Dictionary_2_Remove_mAE2ECC0F5BDAA58524500656449EA1A387079726(L_62, L_64, Dictionary_2_Remove_mAE2ECC0F5BDAA58524500656449EA1A387079726_RuntimeMethod_var);
			}

IL_01af_1:
			{
				bool L_66;
				L_66 = Enumerator_MoveNext_mD85ED63385062F8013FBC6C57320F2B23C975EFE((&V_3), NULL);
				if (L_66)
				{
					goto IL_012c_1;
				}
			}
			{
				goto IL_01cb;
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

IL_01cb:
	{
		return (bool)1;
	}
}
// Method Definition Index: 83098
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActiveLifecycleScopesTracker_RequestEnterScope_TisIl2CppSharedGenericObject_mED5FE3A313CDEC3F9FB3AAEDC4E753519B30D42E_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* V_0 = NULL;
	int32_t V_1 = 0;
	ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38* V_2 = NULL;
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_0 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_0);
		ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* L_1;
		L_1 = VirtualFuncInvoker0< ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* >::Invoke(4, (LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_0);
		V_0 = L_1;
		V_1 = 0;
		goto IL_001c;
	}

IL_000b:
	{
		ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* L_2 = V_0;
		int32_t L_3 = V_1;
		NullCheck(L_2);
		int32_t L_4 = L_3;
		ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38* L_5 = (L_2)->GetAt(static_cast<il2cpp_array_size_t>(L_4));
		V_2 = L_5;
		ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38* L_6 = V_2;
		ActiveLifecycleScopesTracker_CreateScopeTransitionRequest_m9994ACEAA4926C9C17BB51EEA3411DA150DB15D9(__this, L_6, 0, (bool)0, NULL);
		int32_t L_7 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_7, 1));
	}

IL_001c:
	{
		int32_t L_8 = V_1;
		ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* L_9 = V_0;
		NullCheck(L_9);
		int32_t L_10 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_9)->max_length),NULL));
		if ((((int32_t)L_8) < ((int32_t)L_10)))
		{
			goto IL_000b;
		}
	}
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_11 = ___0_lifecycleScope;
		ActiveLifecycleScopesTracker_CreateScopeTransitionRequest_TisIl2CppSharedGenericObject_m781DD3D4A86DAE2D7A3F31DD07A1A4B6C4ACFFB7(__this, L_11, 0, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return;
	}
}
// Method Definition Index: 83100
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActiveLifecycleScopesTracker_RequestExitScope_TisIl2CppSharedGenericObject_m4EB341898A21003EFE763B5E5C78B4A74F0C495D_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, bool ___1_alsoExitNestedScopes, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* V_0 = NULL;
	int32_t V_1 = 0;
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_0 = ___0_lifecycleScope;
		bool L_1 = ___1_alsoExitNestedScopes;
		ActiveLifecycleScopesTracker_CreateScopeTransitionRequest_TisIl2CppSharedGenericObject_m781DD3D4A86DAE2D7A3F31DD07A1A4B6C4ACFFB7(__this, L_0, 1, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_2 = ___0_lifecycleScope;
		NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_2);
		ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* L_3;
		L_3 = VirtualFuncInvoker0< ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* >::Invoke(4, (LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_2);
		V_0 = L_3;
		ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* L_4 = V_0;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_5, 1));
		goto IL_0027;
	}

IL_0018:
	{
		ImplicitLifecycleScopeU5BU5D_t1C10C825AEAC8ACF7250C9F8748855C85884D38D* L_6 = V_0;
		int32_t L_7 = V_1;
		NullCheck(L_6);
		int32_t L_8 = L_7;
		ImplicitLifecycleScope_tB1D07078E6300A31DA61E2FFAB8D8A56AD296E38* L_9 = (L_6)->GetAt(static_cast<il2cpp_array_size_t>(L_8));
		ActiveLifecycleScopesTracker_CreateScopeTransitionRequest_m9994ACEAA4926C9C17BB51EEA3411DA150DB15D9(__this, L_9, 1, (bool)0, NULL);
		int32_t L_10 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_subtract(L_10, 1));
	}

IL_0027:
	{
		int32_t L_11 = V_1;
		if ((((int32_t)L_11) >= ((int32_t)0)))
		{
			goto IL_0018;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 83115
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActiveLifecycleScopesTracker_TryEnterScope_TisIl2CppSharedGenericObject_m21425F19F5B67EE7574D33E6C2EB5F06DBE9E93A_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Add_mA42C372CA350E01B08326C78A32C2D4331199A3D_RuntimeMethod_var);
		il2cpp_rgctx_method_init(method);
	}
	//<source_info:<no-source>:1>
	ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* V_0 = NULL;
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_0 = ___0_lifecycleScope;
		bool L_1;
		L_1 = ActiveLifecycleScopesTracker_PrepareTryEnterScopeWithContext_TisIl2CppSharedGenericObject_m3D8B69D9E50547113449972B07933BE902744D59(__this, L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		if (L_1)
		{
			goto IL_000a;
		}
	}
	{
		return;
	}

IL_000a:
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_2 = ___0_lifecycleScope;
		ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* L_3 = (ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->rgctx_data, 1));
		ActiveLifecycleScopeWithContext_1__ctor_mDD6FD21DC68167495A10EE8C4AB04C783A8A809C(L_3, L_2, il2cpp_rgctx_method(method->rgctx_data, 2));
		V_0 = L_3;
		Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_4 = __this->____activeScopes;
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_5 = ___0_lifecycleScope;
		LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_6;
		L_6 = LifecycleScopeKey_CreateFromScope_TisIl2CppSharedGenericObject_m613ECBEDFFC63C9393C625204DC116D94C244FBB(L_5, il2cpp_rgctx_method(method->rgctx_data, 3));
		ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* L_7 = V_0;
		NullCheck(L_4);
		Dictionary_2_Add_mA42C372CA350E01B08326C78A32C2D4331199A3D(L_4, L_6, (ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C*)L_7, Dictionary_2_Add_mA42C372CA350E01B08326C78A32C2D4331199A3D_RuntimeMethod_var);
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_8 = ___0_lifecycleScope;
		NullCheck((RuntimeObject*)L_8);
		Type_t* L_9;
		L_9 = il2cpp_codegen_object_get_type((RuntimeObject*)L_8);
		ActiveLifecycleScopesTracker_RaiseAutoCleanups_m5620B257E893397698E4CA9CBD38E695BADBA8ED(__this, L_9, 0, NULL);
		ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* L_10 = V_0;
		ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347* L_11 = __this->____scopeTransitionHelper;
		NullCheck((ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C*)L_10);
		VirtualActionInvoker1< ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347* >::Invoke(5, (ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C*)L_10, L_11);
		return;
	}
}
// Method Definition Index: 83120
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ActiveLifecycleScopesTracker_TryExitScope_TisIl2CppSharedGenericObject_m486A3BADE7D0AA96A07075C46B4AFF3C3DE51A66_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* ___0_lifecycleScope, Il2CppSharedGenericObject* ___1_activationContext, bool ___2_alsoExitNestedScopes, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Remove_mAE2ECC0F5BDAA58524500656449EA1A387079726_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_get_Values_m79563C16853F891CCBBC22B07DC63DA063874A49_RuntimeMethod_var);
		il2cpp_rgctx_method_init(method);
	}
	//<source_info:<no-source>:1>
	Enumerator_t624742B8D885649511A5739F11184E2221942252 V_0;
	memset((&V_0), 0, sizeof(V_0));
	ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* V_1 = NULL;
	ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* V_2 = NULL;
	{
		LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_0 = ___0_lifecycleScope;
		Il2CppSharedGenericObject* L_1 = ___1_activationContext;
		bool L_2 = ___2_alsoExitNestedScopes;
		bool L_3;
		L_3 = ActiveLifecycleScopesTracker_PrepareTryExitScope_TisIl2CppSharedGenericObject_mB20E9E3D9D7A9E4B5F58A5783EB2EBB2733EF4E7(__this, L_0, L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		if (L_3)
		{
			goto IL_000c;
		}
	}
	{
		return;
	}

IL_000c:
	{
		Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_4 = __this->____activeScopes;
		NullCheck(L_4);
		ValueCollection_tB7C740A97431C55C62CC43DA7712B5896DBED97F* L_5;
		L_5 = Dictionary_2_get_Values_m79563C16853F891CCBBC22B07DC63DA063874A49(L_4, Dictionary_2_get_Values_m79563C16853F891CCBBC22B07DC63DA063874A49_RuntimeMethod_var);
		NullCheck(L_5);
		Enumerator_t624742B8D885649511A5739F11184E2221942252 L_6;
		L_6 = ValueCollection_GetEnumerator_m1B01F115659146468CB5EE9025E45905AB1279DE(L_5, NULL);
		V_0 = L_6;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0099:
			{
				Enumerator_Dispose_m6E582A0ECA46CE69AFD14D67DE233D2B4E1069BE((&V_0), NULL);
				return;
			}
		});
		try
		{
			{
				goto IL_008e_1;
			}

IL_001f_1:
			{
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_7;
				L_7 = Enumerator_get_Current_m2E4C1612FFBB402B6CF496C40499B9B84734E878_inline((&V_0), NULL);
				V_1 = L_7;
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_8 = V_1;
				NullCheck(L_8);
				LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_9;
				L_9 = ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline(L_8, NULL);
				NullCheck(L_9);
				String_t* L_10;
				L_10 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline(L_9, NULL);
				LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* L_11 = ___0_lifecycleScope;
				NullCheck((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_11);
				String_t* L_12;
				L_12 = LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline((LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755*)L_11, NULL);
				bool L_13;
				L_13 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_10, L_12, NULL);
				if (!L_13)
				{
					goto IL_008e_1;
				}
			}
			{
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_14 = V_1;
				V_2 = ((ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79*)IsInstClass((RuntimeObject*)L_14, il2cpp_rgctx_data(method->rgctx_data, 1)));
				ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* L_15 = V_2;
				if (!L_15)
				{
					goto IL_008e_1;
				}
			}
			{
				ActiveLifecycleScopeWithContext_1_tD3998CF55244EF4B194F8B1E059746B5D9A85F79* L_16 = V_2;
				NullCheck(L_16);
				Il2CppSharedGenericObject* L_17;
				L_17 = ActiveLifecycleScopeWithContext_1_get_Context_m45FFE4CAC1E3D5639C3DEE4433FAE9E204851CC0(L_16, il2cpp_rgctx_method(method->rgctx_data, 2));
				Il2CppSharedGenericObject* L_18 = ___1_activationContext;
				if ((!(((RuntimeObject*)(Il2CppSharedGenericObject*)L_17) == ((RuntimeObject*)(Il2CppSharedGenericObject*)L_18))))
				{
					goto IL_008e_1;
				}
			}
			{
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_19 = V_1;
				ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347* L_20 = __this->____scopeTransitionHelper;
				NullCheck(L_19);
				VirtualActionInvoker1< ScopeTransitionHelper_t069C5DE337C0C8F615857F4D731C5CBE3B0F4347* >::Invoke(6, L_19, L_20);
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_21 = V_1;
				NullCheck(L_21);
				LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_22;
				L_22 = ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline(L_21, NULL);
				NullCheck(L_22);
				Type_t* L_23;
				L_23 = il2cpp_codegen_object_get_type(L_22);
				ActiveLifecycleScopesTracker_RaiseAutoCleanups_m5620B257E893397698E4CA9CBD38E695BADBA8ED(__this, L_23, 1, NULL);
				Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_24 = __this->____activeScopes;
				ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_25 = V_1;
				NullCheck(L_25);
				LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_26;
				L_26 = VirtualFuncInvoker0< LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D >::Invoke(4, L_25);
				NullCheck(L_24);
				bool L_27;
				L_27 = Dictionary_2_Remove_mAE2ECC0F5BDAA58524500656449EA1A387079726(L_24, L_26, Dictionary_2_Remove_mAE2ECC0F5BDAA58524500656449EA1A387079726_RuntimeMethod_var);
				goto IL_00a7;
			}

IL_008e_1:
			{
				bool L_28;
				L_28 = Enumerator_MoveNext_mC8DD05ED5A94E9D71568B93F1FBC1A03B3A4E385((&V_0), NULL);
				if (L_28)
				{
					goto IL_001f_1;
				}
			}
			{
				goto IL_00a7;
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

IL_00a7:
	{
		return;
	}
}
// Method Definition Index: 83109
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_TryGetActiveScope_TisIl2CppSharedGenericObject_mD4C10B4553156820DC083CB6E542984B5DF90F55_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, Il2CppSharedGenericObject** ___0_scope, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_TryGetValue_m23A14CBF4DD0FFE6985742AB5960E012A3D29D89_RuntimeMethod_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D V_0;
	memset((&V_0), 0, sizeof(V_0));
	ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* V_1 = NULL;
	Il2CppSharedGenericObject* V_2 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		LifecycleScopeKey__ctor_m7DCCBE2ED25A7746808F3555AB845EE71FEB6A71((&V_0), L_1, NULL);
		Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_2 = __this->____activeScopes;
		LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_3 = V_0;
		NullCheck(L_2);
		bool L_4;
		L_4 = Dictionary_2_TryGetValue_m23A14CBF4DD0FFE6985742AB5960E012A3D29D89(L_2, L_3, (&V_1), Dictionary_2_TryGetValue_m23A14CBF4DD0FFE6985742AB5960E012A3D29D89_RuntimeMethod_var);
		if (!L_4)
		{
			goto IL_0043;
		}
	}
	{
		ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_5 = V_1;
		NullCheck(L_5);
		LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_6;
		L_6 = ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline(L_5, NULL);
		V_2 = ((Il2CppSharedGenericObject*)IsInst((RuntimeObject*)L_6, il2cpp_rgctx_data(method->rgctx_data, 1)));
		Il2CppSharedGenericObject* L_7 = V_2;
		if (!L_7)
		{
			goto IL_0043;
		}
	}
	{
		Il2CppSharedGenericObject** L_8 = ___0_scope;
		Il2CppSharedGenericObject* L_9 = V_2;
		*(Il2CppSharedGenericObject**)L_8 = L_9;
		Il2CppCodeGenWriteBarrier((void**)(Il2CppSharedGenericObject**)L_8, (void*)L_9);
		return (bool)1;
	}

IL_0043:
	{
		Il2CppSharedGenericObject** L_10 = ___0_scope;
		il2cpp_codegen_initobj(L_10, sizeof(Il2CppSharedGenericObject*));
		return (bool)0;
	}
}
// Method Definition Index: 83110
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ActiveLifecycleScopesTracker_TryGetActiveScope_TisIl2CppSharedGenericObject_TisIl2CppSharedGenericObject_m8BE6AF6B2FBB31FDC0A8C52C68EBA36AA80545E0_gshared (ActiveLifecycleScopesTracker_t21B0C18F9AC8D5A5832FC8DAD92E8759C6E6A8E7* __this, Il2CppSharedGenericObject* ___0_context, Il2CppSharedGenericObject** ___1_scope, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_TryGetValue_m23A14CBF4DD0FFE6985742AB5960E012A3D29D89_RuntimeMethod_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D V_0;
	memset((&V_0), 0, sizeof(V_0));
	ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* V_1 = NULL;
	Il2CppSharedGenericObject* V_2 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		Il2CppSharedGenericObject* L_2 = ___0_context;
		LifecycleScopeKey__ctor_m3BFDFB377FD3F489668C9B18AF174EF462838E6C((&V_0), L_1, (RuntimeObject*)L_2, NULL);
		Dictionary_2_tD1D7C3C9439A06F868D4705EB293B45335991F83* L_3 = __this->____activeScopes;
		LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_4 = V_0;
		NullCheck(L_3);
		bool L_5;
		L_5 = Dictionary_2_TryGetValue_m23A14CBF4DD0FFE6985742AB5960E012A3D29D89(L_3, L_4, (&V_1), Dictionary_2_TryGetValue_m23A14CBF4DD0FFE6985742AB5960E012A3D29D89_RuntimeMethod_var);
		if (!L_5)
		{
			goto IL_0049;
		}
	}
	{
		ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* L_6 = V_1;
		NullCheck(L_6);
		LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_7;
		L_7 = ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline(L_6, NULL);
		V_2 = ((Il2CppSharedGenericObject*)IsInst((RuntimeObject*)L_7, il2cpp_rgctx_data(method->rgctx_data, 1)));
		Il2CppSharedGenericObject* L_8 = V_2;
		if (!L_8)
		{
			goto IL_0049;
		}
	}
	{
		Il2CppSharedGenericObject** L_9 = ___1_scope;
		Il2CppSharedGenericObject* L_10 = V_2;
		*(Il2CppSharedGenericObject**)L_9 = L_10;
		Il2CppCodeGenWriteBarrier((void**)(Il2CppSharedGenericObject**)L_9, (void*)L_10);
		return (bool)1;
	}

IL_0049:
	{
		Il2CppSharedGenericObject** L_11 = ___1_scope;
		il2cpp_codegen_initobj(L_11, sizeof(Il2CppSharedGenericObject*));
		return (bool)0;
	}
}
// Method Definition Index: 81526
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF AndroidJNI_GetDirectBuffer_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m971CFD6732DCF55F07C2820EA7F2D685FED934CD (intptr_t ___0_buffer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int8_t* V_0 = NULL;
	int64_t V_1 = 0;
	NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		intptr_t L_0 = ___0_buffer;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_2), sizeof(NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF));
		NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF L_2 = V_2;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_buffer;
		int8_t* L_4;
		L_4 = AndroidJNI_GetDirectBufferAddress_m0E0B127BFEB7AAF065829DC6AE11163D5616EBE8(L_3, NULL);
		V_0 = L_4;
		int8_t* L_5 = V_0;
		uintptr_t L_6 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		if ((!(((uintptr_t)L_5) == ((uintptr_t)L_6))))
		{
			goto IL_002d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_2), sizeof(NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF));
		NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF L_7 = V_2;
		return L_7;
	}

IL_002d:
	{
		intptr_t L_8 = ___0_buffer;
		int64_t L_9;
		L_9 = AndroidJNI_GetDirectBufferCapacity_mCAC8D9C8E45481BE59FB17406E1E16D4F9628183(L_8, NULL);
		V_1 = L_9;
		int64_t L_10 = V_1;
		int64_t L_11 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(((int32_t)2147483647LL),NULL));
		if ((((int64_t)L_10) <= ((int64_t)L_11)))
		{
			goto IL_005d;
		}
	}
	{
		int64_t L_12 = V_1;
		int64_t L_13 = L_12;
		RuntimeObject* L_14 = Box(Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, &L_13);
		int32_t L_15 = ((int32_t)2147483647LL);
		RuntimeObject* L_16 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_15);
		String_t* L_17;
		L_17 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralDC955F06F1DA8A3B06C4755211822B06D698CF11)), L_14, L_16, NULL);
		Exception_t* L_18 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_18, L_17, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_18, method);
	}

IL_005d:
	{
		int8_t* L_19 = V_0;
		int64_t L_20 = V_1;
		int32_t L_21 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>(L_20,NULL));
		NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF L_22;
		L_22 = NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m181D7F12EB826B7D6B73742BFD85A667D533BABA((void*)L_19, L_21, 1, NULL);
		return L_22;
	}
}
// Method Definition Index: 81526
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 AndroidJNI_GetDirectBuffer_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mD58C4C3DF41E0AFB0378C0FF602335B30319D382 (intptr_t ___0_buffer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	int8_t* V_0 = NULL;
	int64_t V_1 = 0;
	NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		intptr_t L_0 = ___0_buffer;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_2), sizeof(NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190));
		NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 L_2 = V_2;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_buffer;
		int8_t* L_4;
		L_4 = AndroidJNI_GetDirectBufferAddress_m0E0B127BFEB7AAF065829DC6AE11163D5616EBE8(L_3, NULL);
		V_0 = L_4;
		int8_t* L_5 = V_0;
		uintptr_t L_6 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		if ((!(((uintptr_t)L_5) == ((uintptr_t)L_6))))
		{
			goto IL_002d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_2), sizeof(NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190));
		NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 L_7 = V_2;
		return L_7;
	}

IL_002d:
	{
		intptr_t L_8 = ___0_buffer;
		int64_t L_9;
		L_9 = AndroidJNI_GetDirectBufferCapacity_mCAC8D9C8E45481BE59FB17406E1E16D4F9628183(L_8, NULL);
		V_1 = L_9;
		int64_t L_10 = V_1;
		int64_t L_11 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(((int32_t)2147483647LL),NULL));
		if ((((int64_t)L_10) <= ((int64_t)L_11)))
		{
			goto IL_005d;
		}
	}
	{
		int64_t L_12 = V_1;
		int64_t L_13 = L_12;
		RuntimeObject* L_14 = Box(Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, &L_13);
		int32_t L_15 = ((int32_t)2147483647LL);
		RuntimeObject* L_16 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_15);
		String_t* L_17;
		L_17 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralDC955F06F1DA8A3B06C4755211822B06D698CF11)), L_14, L_16, NULL);
		Exception_t* L_18 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_18, L_17, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_18, method);
	}

IL_005d:
	{
		int8_t* L_19 = V_0;
		int64_t L_20 = V_1;
		int32_t L_21 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>(L_20,NULL));
		NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 L_22;
		L_22 = NativeArrayUnsafeUtility_ConvertExistingDataToNativeArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mD9D29EE5CE27F4BFD0168818BF064EC5B3672F39((void*)L_19, L_21, 1, NULL);
		return L_22;
	}
}
// Method Definition Index: 81526
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 AndroidJNI_GetDirectBuffer_TisIl2CppFullySharedGenericStruct_mA240D97969B6DC457DEC6F6EB97FDE5D94F28C29_fshared (intptr_t ___0_buffer, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	int8_t* V_0 = NULL;
	int64_t V_1 = 0;
	NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		intptr_t L_0 = ___0_buffer;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_2), sizeof(NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18));
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_2 = V_2;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_buffer;
		int8_t* L_4;
		L_4 = AndroidJNI_GetDirectBufferAddress_m0E0B127BFEB7AAF065829DC6AE11163D5616EBE8(L_3, NULL);
		V_0 = L_4;
		int8_t* L_5 = V_0;
		uintptr_t L_6 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		if ((!(((uintptr_t)L_5) == ((uintptr_t)L_6))))
		{
			goto IL_002d;
		}
	}
	{
		il2cpp_codegen_initobj((&V_2), sizeof(NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18));
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_7 = V_2;
		return L_7;
	}

IL_002d:
	{
		intptr_t L_8 = ___0_buffer;
		int64_t L_9;
		L_9 = AndroidJNI_GetDirectBufferCapacity_mCAC8D9C8E45481BE59FB17406E1E16D4F9628183(L_8, NULL);
		V_1 = L_9;
		int64_t L_10 = V_1;
		int64_t L_11 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(((int32_t)2147483647LL),NULL));
		if ((((int64_t)L_10) <= ((int64_t)L_11)))
		{
			goto IL_005d;
		}
	}
	{
		int64_t L_12 = V_1;
		int64_t L_13 = L_12;
		RuntimeObject* L_14 = Box(Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, &L_13);
		int32_t L_15 = ((int32_t)2147483647LL);
		RuntimeObject* L_16 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_15);
		String_t* L_17;
		L_17 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralDC955F06F1DA8A3B06C4755211822B06D698CF11)), L_14, L_16, NULL);
		Exception_t* L_18 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_18, L_17, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_18, method);
	}

IL_005d:
	{
		int8_t* L_19 = V_0;
		int64_t L_20 = V_1;
		int32_t L_21 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>(L_20,NULL));
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_22;
		L_22 = ((  NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 (*) (void*, int32_t, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))((void*)L_19, L_21, 1, NULL);
		return L_22;
	}
}
// Method Definition Index: 81523
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNI_NewDirectByteBufferFromNativeArray_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m5CA9D512889E4FD11E06BC83A1E6FE316CD1BC0F (NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF ___0_buffer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		bool L_0;
		L_0 = NativeArray_1_get_IsCreated_mD74FCA194584E6EA7916853B62401EB78240A081_inline((&___0_buffer), NULL);
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		int32_t L_1;
		L_1 = IL2CPP_NATIVEARRAY_GET_LENGTH(((&___0_buffer))->___m_Length);
		if ((((int32_t)L_1) > ((int32_t)0)))
		{
			goto IL_0019;
		}
	}

IL_0013:
	{
		return 0;
	}

IL_0019:
	{
		NativeArray_1_t81F55263465517B73C455D3400CF67B4BADD85CF L_2 = ___0_buffer;
		void* L_3;
		L_3 = NativeArrayUnsafeUtility_GetUnsafePtr_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m8CFDB2DF56E810A2E2FB3686AF676FCAC65AFCC2_inline(L_2, NULL);
		int32_t L_4;
		L_4 = IL2CPP_NATIVEARRAY_GET_LENGTH(((&___0_buffer))->___m_Length);
		int64_t L_5 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_4,NULL));
		intptr_t L_6;
		L_6 = AndroidJNI_NewDirectByteBuffer_m17389ED6D98CC0364180BAB43F2747B48FFDB107((uint8_t*)L_3, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 81523
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNI_NewDirectByteBufferFromNativeArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mB9A49EBBCCE6CE5AF7544594285D5F56E7EF279C (NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 ___0_buffer, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		bool L_0;
		L_0 = NativeArray_1_get_IsCreated_mF1154ABCB79F43B3A71BFA8DCB85ACBDFA2838D5_inline((&___0_buffer), NULL);
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		int32_t L_1;
		L_1 = IL2CPP_NATIVEARRAY_GET_LENGTH(((&___0_buffer))->___m_Length);
		if ((((int32_t)L_1) > ((int32_t)0)))
		{
			goto IL_0019;
		}
	}

IL_0013:
	{
		return 0;
	}

IL_0019:
	{
		NativeArray_1_tC3AFDC9012293850DE671F2A1E55484968716190 L_2 = ___0_buffer;
		void* L_3;
		L_3 = NativeArrayUnsafeUtility_GetUnsafePtr_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mCD832837FF5CE235B506272835BA049D56968E2F_inline(L_2, NULL);
		int32_t L_4;
		L_4 = IL2CPP_NATIVEARRAY_GET_LENGTH(((&___0_buffer))->___m_Length);
		int64_t L_5 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_4,NULL));
		intptr_t L_6;
		L_6 = AndroidJNI_NewDirectByteBuffer_m17389ED6D98CC0364180BAB43F2747B48FFDB107((uint8_t*)L_3, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 81523
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNI_NewDirectByteBufferFromNativeArray_TisIl2CppFullySharedGenericStruct_mBA315BD28FAF24143A02065102FEC37584F134D1_fshared (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_buffer, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		bool L_0;
		L_0 = ((  bool (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))((&___0_buffer), NULL);
		if (!L_0)
		{
			goto IL_0013;
		}
	}
	{
		int32_t L_1;
		L_1 = ((  int32_t (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)))((&___0_buffer), NULL);
		if ((((int32_t)L_1) > ((int32_t)0)))
		{
			goto IL_0019;
		}
	}

IL_0013:
	{
		return 0;
	}

IL_0019:
	{
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_2 = ___0_buffer;
		void* L_3;
		L_3 = ((  void* (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 2)))(L_2, NULL);
		int32_t L_4;
		L_4 = ((  int32_t (*) (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)))((&___0_buffer), NULL);
		int64_t L_5 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_4,NULL));
		intptr_t L_6;
		L_6 = AndroidJNI_NewDirectByteBuffer_m17389ED6D98CC0364180BAB43F2747B48FFDB107((uint8_t*)L_3, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJNIHelper_ConvertFromJNIArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m0207DDD0AB7EE1136083B85289D4F16FD9A64ECC (intptr_t ___0_array, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_1;
		L_1 = _AndroidJNIHelper_ConvertFromJNIArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mBF1AB84DE0B50E7C206BAD3C170D1F5AD2D9E343(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJNIHelper_ConvertFromJNIArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m51DC292506277213F541434A51F81430E11E7226 (intptr_t ___0_array, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Il2CppChar L_1;
		L_1 = _AndroidJNIHelper_ConvertFromJNIArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m9ECB51B699AB7B96368F39CC9F7C481340DD0BBC(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJNIHelper_ConvertFromJNIArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_mF6AF89E7365D44A201C5A2C86C49EF49E2D6ADAD (intptr_t ___0_array, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		double L_1;
		L_1 = _AndroidJNIHelper_ConvertFromJNIArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m1F7F249CC7CDA2D6E136C627D983569B3674EA73(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJNIHelper_ConvertFromJNIArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mF6FCB78A4CF7C2FB0375E88C31C70458F55F422C (intptr_t ___0_array, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		int16_t L_1;
		L_1 = _AndroidJNIHelper_ConvertFromJNIArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m901F767E3189385BB47B5B021C40507AAF67D477(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJNIHelper_ConvertFromJNIArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m02707C23958FF03FA0345C2A17F66EE379A1468C (intptr_t ___0_array, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		int32_t L_1;
		L_1 = _AndroidJNIHelper_ConvertFromJNIArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m37A94A4CA9F6456009CABE27D4AA2ED7B8365168(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJNIHelper_ConvertFromJNIArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m0CADE835010213386C1C07E63F70E55498142C20 (intptr_t ___0_array, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		int64_t L_1;
		L_1 = _AndroidJNIHelper_ConvertFromJNIArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mBE697F1C4D60A7F2BB06784B5CB0C540B7FBF664(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJNIHelper_ConvertFromJNIArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m2BD2E924FBD117B44A2D5E741144FD7B1FCE918E (intptr_t ___0_array, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		int8_t L_1;
		L_1 = _AndroidJNIHelper_ConvertFromJNIArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m01777984AFB915C1167148BC6056336EB200B151(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJNIHelper_ConvertFromJNIArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m93BCF3156F5C823F8C2B005523A618966B0A6DBB (intptr_t ___0_array, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		float L_1;
		L_1 = _AndroidJNIHelper_ConvertFromJNIArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m6634501B2BC58937A4D852F6F2D8D55A9DFB6F64(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_m6F1CD10E4BE61A7211FB87DA1D7D92C7FE85F8E4_gshared (intptr_t ___0_array, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Il2CppSharedGenericObject* L_1;
		L_1 = _AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_mAD2432704589B67F3F93D55DF8B948DCBEBA4444(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81268
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppFullySharedGenericAny_mE2FE04F1AE05E31AFCB6C62A0D8015274310BBE7_fshared (intptr_t ___0_array, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	const uint32_t SizeOf_ArrayType_t33FA9A7F66F041B4E2878FF619DA2A8FCDD39085 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_ArrayType_t33FA9A7F66F041B4E2878FF619DA2A8FCDD39085);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_array;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), NULL, L_0, (Il2CppFullySharedGenericAny*)L_1);
		il2cpp_codegen_memcpy(il2cppRetVal, L_1, SizeOf_ArrayType_t33FA9A7F66F041B4E2878FF619DA2A8FCDD39085);
		return;
	}
}
// Method Definition Index: 81270
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetFieldID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6F960B6375B61125FD05A03EBE8CEC790A986044 (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_fieldName;
		bool L_2 = ___2_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_3;
		L_3 = _AndroidJNIHelper_GetFieldID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE35E2A7AB2A1739DB9AEE2664DB6FA8E85B8897A(L_0, L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_3;
	}
}
// Method Definition Index: 81270
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetFieldID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1B394DCE5ED40DFA09EB18874BA2FFC3B5E50B2E (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_fieldName;
		bool L_2 = ___2_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_3;
		L_3 = _AndroidJNIHelper_GetFieldID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mFD988C7487D7A7810D33F985E48AB82A006E1B7B(L_0, L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_3;
	}
}
// Method Definition Index: 81270
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mDFF491584A820BE0629A47ED3B790A199050D589_gshared (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_fieldName;
		bool L_2 = ___2_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_3;
		L_3 = _AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mC4E70171E726BFD2EDF128963FAD90873B0289C5(L_0, L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_3;
	}
}
// Method Definition Index: 81270
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetFieldID_TisIl2CppFullySharedGenericAny_mD4DFA0128576804E9AE4FF17BD3D1398F2196069_fshared (intptr_t ___0_jclass, String_t* ___1_fieldName, bool ___2_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_fieldName;
		bool L_2 = ___2_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_3;
		L_3 = ((  intptr_t (*) (intptr_t, String_t*, bool, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))(L_0, L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_3;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE3914411BE312CBBA869835158E535A06CD3EEB4 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = _AndroidJNIHelper_GetMethodID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m5C5661781E8C49E962C2556FC75101FD0E862AFD(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m791647AA6EAD3B88467418F51CF53F5755AB38BA (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = _AndroidJNIHelper_GetMethodID_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m465627642B00BC71088A5883058BB86DDBADABE0(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m44C60AAE64EE97BFD5D9D302E7BE5BE20E6862B3 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = _AndroidJNIHelper_GetMethodID_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_mA32D3320555971B585058A28FC5F5EC84DC10A34(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m3B57915B048BD436E00741CD146D62AC775E7BA3 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = _AndroidJNIHelper_GetMethodID_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mF0D2DB5B885B01587820A9F88C6CAD67C8DC1512(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mF87CC238DDBB3C90C35846E626548847CC7E319D (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = _AndroidJNIHelper_GetMethodID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mB340108BAAC2DA25B45971B719093FDF9B13E745(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m9665934CC08FD80EB16882C4EA6FDBC5D9C3A175 (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = _AndroidJNIHelper_GetMethodID_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mF114D8205E13517DE4F8DB9772E9C40C7C43BD85(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mEF6ACD1C6C5237E0446DB9DBDAC40BA2660849BB (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = _AndroidJNIHelper_GetMethodID_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m362BBB1CC3B8834A056A8F709649BD182E24E9BD(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_mA3FFDC6948922BEFC787A297D5483BA277EEFF0C (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = _AndroidJNIHelper_GetMethodID_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m86B190CF71EDCB08177B90F98C76A9545AF09FCB(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_m95122CB55DFAF463B14CBBA51C2B0811790DB90B_gshared (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = _AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_mFEAB91A44B644CE56BB6182931D1065B679E0B3A(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81269
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR intptr_t AndroidJNIHelper_GetMethodID_TisIl2CppFullySharedGenericAny_mEB124826B537513D20B0E11A265E04D0612E1CE8_fshared (intptr_t ___0_jclass, String_t* ___1_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___2_args, bool ___3_isStatic, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_jclass;
		String_t* L_1 = ___1_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___2_args;
		bool L_3 = ___3_isStatic;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		intptr_t L_4;
		L_4 = ((  intptr_t (*) (intptr_t, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, bool, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))(L_0, L_1, L_2, L_3, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_4;
	}
}
// Method Definition Index: 81271
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AndroidJNIHelper_GetSignature_TisIl2CppFullySharedGenericAny_m4D04F21DE5CB13BC4E2A08505DA57AC729F3BA70_fshared (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___0_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit);
	//<source_info:<no-source>:1>
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___0_args;
		CHECKED_LOCAL_INIT(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_StaticInit,(_AndroidJNIHelper_tA796944DDB1B1459DF68C9FFA518F452C81364F3_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		String_t* L_1;
		L_1 = ((  String_t* (*) (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))(L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_Call_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m05D3284A3FA772D032190A0FE82363C61000F1DF (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		bool L_2;
		L_2 = AndroidJavaObject__Call_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m72E1F5D2CE435E45651E8B970A73B9F5E3E03815(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject_Call_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_mE43BB2CCE4BEBFBF19C2E7C22F4E07A52002E80C (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		Il2CppChar L_2;
		L_2 = AndroidJavaObject__Call_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m22710813CDD982DC5F88C2A4067FBFE1D57A3065(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject_Call_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m980F8BA95C5B796FDACBDF865A7BAE431DE77362 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		double L_2;
		L_2 = AndroidJavaObject__Call_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m946CEC22A94586DE93BE0725E5F25E04589F69DE(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject_Call_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mE0567B52AC481152BD742506C5AB912185B2C345 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		int16_t L_2;
		L_2 = AndroidJavaObject__Call_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m08A0CE3C4BDDA756095A0A816C659587814F649E(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		int32_t L_2;
		L_2 = AndroidJavaObject__Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1BFF486C91846170C07AAF27EF84971FF0596B90(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject_Call_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m63E24F733CDC205FED0CF4659E49DB4AE06ADBB9 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		int64_t L_2;
		L_2 = AndroidJavaObject__Call_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mC6DBECDD44973120E0A56068ABE886D9A7016F5C(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject_Call_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m517389999DF08FDB831BB7337DE4FA8D8158AF7B (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		int8_t L_2;
		L_2 = AndroidJavaObject__Call_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mAE2F0D59E0CE54D36BB86C53218704FA72DC0350(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject_Call_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_mC6CF02CBA2C4A23EF8CD0BF612F5759B8C26DFF1 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		float L_2;
		L_2 = AndroidJavaObject__Call_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m71549B5031FEE0FBB78A9CE02EDADD14DBFFB56D(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject_Call_TisIl2CppSharedGenericObject_m8DEE2A0D06B4978C5E5000815E66E6484606D138_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		Il2CppSharedGenericObject* L_2;
		L_2 = AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m39A1E2B88CD5364F83417EFB65F1C296656D2F62(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81717
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Call_TisIl2CppFullySharedGenericAny_mC798093D4EE507666B4D04C1237E82DFBBACE706_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_ReturnType_t7ECE3320BEFB3505409302EC3AA6B828AE7E1DB5 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_ReturnType_t7ECE3320BEFB3505409302EC3AA6B828AE7E1DB5);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_methodID;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		InvokerActionInvoker3< intptr_t, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, L_1, (Il2CppFullySharedGenericAny*)L_2);
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_ReturnType_t7ECE3320BEFB3505409302EC3AA6B828AE7E1DB5);
		return;
	}
}
// Method Definition Index: 81716
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Call_TisIl2CppFullySharedGenericAny_mCA7EED8FFBB862858FF426BD7D6B191F9C24234B_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_ReturnType_t7C9CEFF53F7F785E3B0A2AA52BF0599DB9E4C7A7 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_ReturnType_t7C9CEFF53F7F785E3B0A2AA52BF0599DB9E4C7A7);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		InvokerActionInvoker3< String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, L_1, (Il2CppFullySharedGenericAny*)L_2);
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_ReturnType_t7C9CEFF53F7F785E3B0A2AA52BF0599DB9E4C7A7);
		return;
	}
}
// Method Definition Index: 81696
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Call_TisIl2CppFullySharedGenericAny_mDA393DFB3EA03B230F3982EC0F62EED04C1B755E_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___1_args, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_methodID;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = ___1_args;
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		AndroidJavaObject__Call_m2126160FB635069207535BD0E700C3605FDB3308(__this, L_0, L_2, NULL);
		return;
	}
}
// Method Definition Index: 81695
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Call_TisIl2CppFullySharedGenericAny_mF7CDE80510F82B3C335324838828B788E1F0FAD4_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___1_args, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = ___1_args;
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		AndroidJavaObject__Call_m4C4D7D7287030773175BDF47681EA018DFA4DF1A(__this, L_0, L_2, NULL);
		return;
	}
}
// Method Definition Index: 81715
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Call_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_mE94FE031A180D9514AA8E28338270133654A7412_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_ReturnType_tE77199CFC11B9237A747C8E23BCDA97324CD4769 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_ReturnType_tE77199CFC11B9237A747C8E23BCDA97324CD4769);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_methodID;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = ___1_args;
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		InvokerActionInvoker3< intptr_t, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, L_2, (Il2CppFullySharedGenericAny*)L_4);
		il2cpp_codegen_memcpy(il2cppRetVal, L_4, SizeOf_ReturnType_tE77199CFC11B9237A747C8E23BCDA97324CD4769);
		return;
	}
}
// Method Definition Index: 81714
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Call_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_m584BD08D664D1371FF995E37EDD37FD6AA861406_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_ReturnType_t6A981EFC55AACA8DB2914A6B9C24AF2C1F0D86E3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_ReturnType_t6A981EFC55AACA8DB2914A6B9C24AF2C1F0D86E3);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = ___1_args;
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		InvokerActionInvoker3< String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, L_2, (Il2CppFullySharedGenericAny*)L_4);
		il2cpp_codegen_memcpy(il2cppRetVal, L_4, SizeOf_ReturnType_t6A981EFC55AACA8DB2914A6B9C24AF2C1F0D86E3);
		return;
	}
}
// Method Definition Index: 81720
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE956BC9A30BEC746DE593C53C1B8DB6A685185A6 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		bool L_2;
		L_2 = AndroidJavaObject__CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m41529A6A47CC3B57286AC8F9C6388E54918DF450(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81720
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m6619B03C8DA4F5A66785845A2E5B39DAEF36642A (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		int32_t L_2;
		L_2 = AndroidJavaObject__CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mEE64E87A5A2BC0F38910643A9B91032AF38D882C(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81720
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject_CallStatic_TisIl2CppSharedGenericObject_mF6EA4A6FC668EA46C5309B3E3A5ABB83600F50F6_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		Il2CppSharedGenericObject* L_2;
		L_2 = AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_m8E1513A9554AAA1822CA51D0A816DCF16775AA37(__this, L_0, L_1, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_2;
	}
}
// Method Definition Index: 81721
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_CallStatic_TisIl2CppFullySharedGenericAny_m98714A38A15645388BE9EE530213C7EDB49C34AF_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_ReturnType_t955BFF9AB851C80203957B433FC00E2046696241 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_ReturnType_t955BFF9AB851C80203957B433FC00E2046696241);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_methodID;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		InvokerActionInvoker3< intptr_t, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, L_1, (Il2CppFullySharedGenericAny*)L_2);
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_ReturnType_t955BFF9AB851C80203957B433FC00E2046696241);
		return;
	}
}
// Method Definition Index: 81720
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_CallStatic_TisIl2CppFullySharedGenericAny_m7843D14A0537A1190C739261EF49C05B1FF2ED90_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_ReturnType_t94E13999E45FF70AA5DA5E427955FC4E439412B2 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_ReturnType_t94E13999E45FF70AA5DA5E427955FC4E439412B2);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		InvokerActionInvoker3< String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, L_1, (Il2CppFullySharedGenericAny*)L_2);
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_ReturnType_t94E13999E45FF70AA5DA5E427955FC4E439412B2);
		return;
	}
}
// Method Definition Index: 81700
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_CallStatic_TisIl2CppFullySharedGenericAny_m9E915D63BD1F4FB05B49296EEFE25B86C30EA692_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___1_args, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_methodID;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = ___1_args;
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		AndroidJavaObject__CallStatic_mE917E474DB9801610FB7ABE5BE749DF84CEFD48A(__this, L_0, L_2, NULL);
		return;
	}
}
// Method Definition Index: 81699
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_CallStatic_TisIl2CppFullySharedGenericAny_mB35C0A4F3C829AF99001B27BC0E99CD64736EE1A_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___1_args, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = ___1_args;
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		AndroidJavaObject__CallStatic_mD63902D30CD5626DAEAD1D6484AF7A9ACA85590E(__this, L_0, L_2, NULL);
		return;
	}
}
// Method Definition Index: 81719
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_CallStatic_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_m4C097E550C3D8A103BE65545A8CB9E688DCE341E_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_ReturnType_t3F35C7C5E6E91ECEB1E553CA45292DBBEC6E5501 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_ReturnType_t3F35C7C5E6E91ECEB1E553CA45292DBBEC6E5501);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_methodID;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = ___1_args;
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		InvokerActionInvoker3< intptr_t, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, L_2, (Il2CppFullySharedGenericAny*)L_4);
		il2cpp_codegen_memcpy(il2cppRetVal, L_4, SizeOf_ReturnType_t3F35C7C5E6E91ECEB1E553CA45292DBBEC6E5501);
		return;
	}
}
// Method Definition Index: 81718
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_CallStatic_TisIl2CppFullySharedGenericAny_TisIl2CppFullySharedGenericAny_m9E8D43F22F21BB581F9478C8CF9B3A8059B3B8CD_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_ReturnType_t41223B870DEBD9DC66C7F3F6FDDF2CF6D061E4EB = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_4 = alloca(SizeOf_ReturnType_t41223B870DEBD9DC66C7F3F6FDDF2CF6D061E4EB);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = L_1;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_3 = ___1_args;
		NullCheck(L_2);
		ArrayElementTypeCheck (L_2, L_3);
		(L_2)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_3);
		InvokerActionInvoker3< String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, L_2, (Il2CppFullySharedGenericAny*)L_4);
		il2cpp_codegen_memcpy(il2cppRetVal, L_4, SizeOf_ReturnType_t41223B870DEBD9DC66C7F3F6FDDF2CF6D061E4EB);
		return;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_FromJavaArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mA1CB7EC9473B0674763C89AC566EC664159108E9 (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	bool V_0 = false;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(bool));
		bool L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		bool L_4;
		L_4 = AndroidJNIHelper_ConvertFromJNIArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m0207DDD0AB7EE1136083B85289D4F16FD9A64ECC(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_4;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject_FromJavaArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m18459EC016E866EF307EC4D9442CD86FD9BC28B4 (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	Il2CppChar V_0 = 0x0;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(Il2CppChar));
		Il2CppChar L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		Il2CppChar L_4;
		L_4 = AndroidJNIHelper_ConvertFromJNIArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m51DC292506277213F541434A51F81430E11E7226(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_4;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject_FromJavaArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m3CAD187DEBCBD88D94604015E86A37B8944847BC (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	double V_0 = 0.0;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(double));
		double L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		double L_4;
		L_4 = AndroidJNIHelper_ConvertFromJNIArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_mF6AF89E7365D44A201C5A2C86C49EF49E2D6ADAD(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_4;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject_FromJavaArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m62441E1DA5798B8F585F5EF18386C4FB739BFFC8 (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	int16_t V_0 = 0;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(int16_t));
		int16_t L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		int16_t L_4;
		L_4 = AndroidJNIHelper_ConvertFromJNIArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mF6FCB78A4CF7C2FB0375E88C31C70458F55F422C(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_4;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_FromJavaArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC1390BDE8BF42FAA78215DFE076A25F7AEA674D7 (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(int32_t));
		int32_t L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		int32_t L_4;
		L_4 = AndroidJNIHelper_ConvertFromJNIArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m02707C23958FF03FA0345C2A17F66EE379A1468C(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_4;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject_FromJavaArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m6A8E0C87ADD6FC1CA3F936C0CC0AC4AB379A8422 (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	int64_t V_0 = 0;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(int64_t));
		int64_t L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		int64_t L_4;
		L_4 = AndroidJNIHelper_ConvertFromJNIArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m0CADE835010213386C1C07E63F70E55498142C20(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_4;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject_FromJavaArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mB474FE99696A6DEB269E0F1C3C7DA32CD8854410 (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	int8_t V_0 = 0x0;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(int8_t));
		int8_t L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		int8_t L_4;
		L_4 = AndroidJNIHelper_ConvertFromJNIArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_m2BD2E924FBD117B44A2D5E741144FD7B1FCE918E(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_4;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject_FromJavaArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_mD5A794B10305822230E288BE2D72329A6EE0C8A4 (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	float V_0 = 0.0f;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(float));
		float L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		float L_4;
		L_4 = AndroidJNIHelper_ConvertFromJNIArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m93BCF3156F5C823F8C2B005523A618966B0A6DBB(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_4;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject_FromJavaArray_TisIl2CppSharedGenericObject_m22D9898503E929D77563392822CD11A056613AFF_gshared (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	Il2CppSharedGenericObject* V_0 = NULL;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		Il2CppSharedGenericObject* L_4;
		L_4 = AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_m6F1CD10E4BE61A7211FB87DA1D7D92C7FE85F8E4(L_3, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_4;
	}
}
// Method Definition Index: 81748
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_FromJavaArray_TisIl2CppFullySharedGenericAny_m4D8E7ACD1BBB7E3FEC7BA69AF6C620E70D8BBC3E_fshared (intptr_t ___0_jobject, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC);
	const Il2CppFullySharedGenericAny L_4 = L_2;
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC);
	//<source_info:<no-source>:1>
	Il2CppFullySharedGenericAny V_0 = alloca(SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC);
	memset(V_0, 0, SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC);
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_0, SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC);
		il2cpp_codegen_memcpy(L_2, V_0, SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC);
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC);
		return;
	}

IL_0017:
	{
		intptr_t L_3 = ___0_jobject;
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), NULL, L_3, (Il2CppFullySharedGenericAny*)L_4);
		il2cpp_codegen_box_unbox(L_4, L_5, SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC, il2cpp_rgctx_data(method->rgctx_data, 0), il2cpp_rgctx_data(method->rgctx_data, 0));
		il2cpp_codegen_memcpy(il2cppRetVal, L_5, SizeOf_ReturnType_t2602EED81B2B6E4946933FD7A1734F95B3DD02CC);
		return;
	}
}
// Method Definition Index: 81747
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mC408C8B80896737BA7DE6C18A904D2157E5FD364 (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	bool V_0 = false;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(bool));
		bool L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_002b:
			{
				intptr_t L_3 = ___0_jobject;
				AndroidJNISafe_DeleteLocalRef_m20303564C88A1B90E3D8D7A7D893392E18967094(L_3, NULL);
				return;
			}
		});
		try
		{
			intptr_t L_4 = ___0_jobject;
			bool L_5;
			L_5 = AndroidJNIHelper_ConvertFromJNIArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m0207DDD0AB7EE1136083B85289D4F16FD9A64ECC(L_4, il2cpp_rgctx_method(method->rgctx_data, 1));
			V_0 = L_5;
			goto IL_0032;
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

IL_0032:
	{
		bool L_6 = V_0;
		return L_6;
	}
}
// Method Definition Index: 81747
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m61DD76EF7E812BC3ED41BBA0D0DA1B430EC61256 (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	int32_t V_0 = 0;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(int32_t));
		int32_t L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_002b:
			{
				intptr_t L_3 = ___0_jobject;
				AndroidJNISafe_DeleteLocalRef_m20303564C88A1B90E3D8D7A7D893392E18967094(L_3, NULL);
				return;
			}
		});
		try
		{
			intptr_t L_4 = ___0_jobject;
			int32_t L_5;
			L_5 = AndroidJNIHelper_ConvertFromJNIArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m02707C23958FF03FA0345C2A17F66EE379A1468C(L_4, il2cpp_rgctx_method(method->rgctx_data, 1));
			V_0 = L_5;
			goto IL_0032;
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

IL_0032:
	{
		int32_t L_6 = V_0;
		return L_6;
	}
}
// Method Definition Index: 81747
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisIl2CppSharedGenericObject_mB1CEFE624E92B82D234C1982AA2A7DF6AC5FD284_gshared (intptr_t ___0_jobject, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	Il2CppSharedGenericObject* V_0 = NULL;
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((&V_0), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_2 = V_0;
		return L_2;
	}

IL_0017:
	{
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_002b:
			{
				intptr_t L_3 = ___0_jobject;
				AndroidJNISafe_DeleteLocalRef_m20303564C88A1B90E3D8D7A7D893392E18967094(L_3, NULL);
				return;
			}
		});
		try
		{
			intptr_t L_4 = ___0_jobject;
			Il2CppSharedGenericObject* L_5;
			L_5 = AndroidJNIHelper_ConvertFromJNIArray_TisIl2CppSharedGenericObject_m6F1CD10E4BE61A7211FB87DA1D7D92C7FE85F8E4(L_4, il2cpp_rgctx_method(method->rgctx_data, 1));
			V_0 = L_5;
			goto IL_0032;
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

IL_0032:
	{
		Il2CppSharedGenericObject* L_6 = V_0;
		return L_6;
	}
}
// Method Definition Index: 81747
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisIl2CppFullySharedGenericAny_m5BC2B7B267EBFB3D33E1D15F5F5DFEF99AB48E84_fshared (intptr_t ___0_jobject, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
	const Il2CppFullySharedGenericAny L_5 = L_2;
	const Il2CppFullySharedGenericAny L_7 = L_2;
	const Il2CppFullySharedGenericAny L_6 = alloca(SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
	//<source_info:<no-source>:1>
	Il2CppFullySharedGenericAny V_0 = alloca(SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
	memset(V_0, 0, SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
	{
		intptr_t L_0 = ___0_jobject;
		bool L_1;
		L_1 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_0, 0, NULL);
		if (!L_1)
		{
			goto IL_0017;
		}
	}
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_0, SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
		il2cpp_codegen_memcpy(L_2, V_0, SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
		return;
	}

IL_0017:
	{
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_002b:
			{
				intptr_t L_3 = ___0_jobject;
				AndroidJNISafe_DeleteLocalRef_m20303564C88A1B90E3D8D7A7D893392E18967094(L_3, NULL);
				return;
			}
		});
		try
		{
			intptr_t L_4 = ___0_jobject;
			InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), NULL, L_4, (Il2CppFullySharedGenericAny*)L_5);
			il2cpp_codegen_box_unbox(L_5, L_6, SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9, il2cpp_rgctx_data(method->rgctx_data, 0), il2cpp_rgctx_data(method->rgctx_data, 0));
			il2cpp_codegen_memcpy(V_0, L_6, SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
			goto IL_0032;
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

IL_0032:
	{
		il2cpp_codegen_memcpy(L_7, V_0, SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
		il2cpp_codegen_memcpy(il2cppRetVal, L_7, SizeOf_ReturnType_t7F4379E8D6E3B3545F3D77660B8E3F6DA1DC4DB9);
		return;
	}
}
// Method Definition Index: 81703
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject_Get_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6F4712024377FBA61B40109CE304D4EA5C145016 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_fieldName;
		bool L_1;
		L_1 = AndroidJavaObject__Get_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m68F3AF483142B771BE6BCD51B30F7CED871DB766(__this, L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81703
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_Get_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1C9D9590B9F7212AAD6D9BFFF3F2762BD090BCAB (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_fieldName;
		int32_t L_1;
		L_1 = AndroidJavaObject__Get_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDED207A2CABEE9D78BEC022F1D4338A5091DCC50(__this, L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81703
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject_Get_TisIl2CppSharedGenericObject_m6C4DE572279A608E1FF6AA0F9B421D0BB2870F5C_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_fieldName;
		Il2CppSharedGenericObject* L_1;
		L_1 = AndroidJavaObject__Get_TisIl2CppSharedGenericObject_m3FBD43AC7986C7B2DAE4F575360C5B6A7DBDE4AB(__this, L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81704
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Get_TisIl2CppFullySharedGenericAny_m0921C141B8FD194E4F423401D827FCB547354135_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_t3D88A0B7A2BB1C07E18A9AA7AB32582B0FB37337 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_FieldType_t3D88A0B7A2BB1C07E18A9AA7AB32582B0FB37337);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_fieldID;
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, (Il2CppFullySharedGenericAny*)L_1);
		il2cpp_codegen_memcpy(il2cppRetVal, L_1, SizeOf_FieldType_t3D88A0B7A2BB1C07E18A9AA7AB32582B0FB37337);
		return;
	}
}
// Method Definition Index: 81703
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Get_TisIl2CppFullySharedGenericAny_m390E9396B06E7DB37AB22F9F94EF31B456F01185_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_tE541E61DC1EE486A3DDC10DCA2A2DD9A2A3BADE8 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_FieldType_tE541E61DC1EE486A3DDC10DCA2A2DD9A2A3BADE8);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_fieldName;
		InvokerActionInvoker2< String_t*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, (Il2CppFullySharedGenericAny*)L_1);
		il2cpp_codegen_memcpy(il2cppRetVal, L_1, SizeOf_FieldType_tE541E61DC1EE486A3DDC10DCA2A2DD9A2A3BADE8);
		return;
	}
}
// Method Definition Index: 81707
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject_GetStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m740F3401DEA4A75BADD753EFF71D2328B4147BFC (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_fieldName;
		int32_t L_1;
		L_1 = AndroidJavaObject__GetStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m4289B7D740963C5007AD93A07FF6A0A01857DD28(__this, L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81707
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject_GetStatic_TisIl2CppSharedGenericObject_mB51F8378DA3B0F5FFE061D86A20FA87349F98740_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_fieldName;
		Il2CppSharedGenericObject* L_1;
		L_1 = AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_m177F1A057E8F5EF05D9158BA272E890C6210044B(__this, L_0, il2cpp_rgctx_method(method->rgctx_data, 0));
		return L_1;
	}
}
// Method Definition Index: 81708
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_GetStatic_TisIl2CppFullySharedGenericAny_m7E7CB28FD389C5D15A289C22C10EEBB9E64567B9_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_t4E8B4431DA3B0620A6961D0830B049CE0FA25BB3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_FieldType_t4E8B4431DA3B0620A6961D0830B049CE0FA25BB3);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_fieldID;
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, (Il2CppFullySharedGenericAny*)L_1);
		il2cpp_codegen_memcpy(il2cppRetVal, L_1, SizeOf_FieldType_t4E8B4431DA3B0620A6961D0830B049CE0FA25BB3);
		return;
	}
}
// Method Definition Index: 81707
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_GetStatic_TisIl2CppFullySharedGenericAny_mDD5B83921F6078373B85EB4F803492A68545BA7E_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_tBB418E296327456981AAF34C2BEB510AEC3C4E4D = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_FieldType_tBB418E296327456981AAF34C2BEB510AEC3C4E4D);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_fieldName;
		InvokerActionInvoker2< String_t*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)), il2cpp_rgctx_method(method->rgctx_data, 0), __this, L_0, (Il2CppFullySharedGenericAny*)L_1);
		il2cpp_codegen_memcpy(il2cppRetVal, L_1, SizeOf_FieldType_tBB418E296327456981AAF34C2BEB510AEC3C4E4D);
		return;
	}
}
// Method Definition Index: 81706
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Set_TisIl2CppFullySharedGenericAny_mEB42AA9151144DAA56A324FBF5A564C71A65D35E_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, Il2CppFullySharedGenericAny ___1_val, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_tD083F29D6E32E63E26B83C297746967559FB5289 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_FieldType_tD083F29D6E32E63E26B83C297746967559FB5289);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? ___1_val : &___1_val), SizeOf_FieldType_tD083F29D6E32E63E26B83C297746967559FB5289);
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), __this, L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? L_1: *(void**)L_1));
		return;
	}
}
// Method Definition Index: 81705
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_Set_TisIl2CppFullySharedGenericAny_m7286B3F15DA02D0B999CE939F20412740B22945E_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, Il2CppFullySharedGenericAny ___1_val, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_t5C6F84F3CFFB0874A4DA0D1C58053A25B835551E = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_FieldType_t5C6F84F3CFFB0874A4DA0D1C58053A25B835551E);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_fieldName;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? ___1_val : &___1_val), SizeOf_FieldType_t5C6F84F3CFFB0874A4DA0D1C58053A25B835551E);
		InvokerActionInvoker2< String_t*, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), __this, L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? L_1: *(void**)L_1));
		return;
	}
}
// Method Definition Index: 81710
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_SetStatic_TisIl2CppFullySharedGenericAny_mFB380E32D4FBFB41D3870427D3227A38E72F3D7C_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, Il2CppFullySharedGenericAny ___1_val, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_tD9FB37412CCB11866A06B8D683ACBBD34ED06C6B = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_FieldType_tD9FB37412CCB11866A06B8D683ACBBD34ED06C6B);
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? ___1_val : &___1_val), SizeOf_FieldType_tD9FB37412CCB11866A06B8D683ACBBD34ED06C6B);
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), __this, L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? L_1: *(void**)L_1));
		return;
	}
}
// Method Definition Index: 81709
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject_SetStatic_TisIl2CppFullySharedGenericAny_mF354DBF45CBF7D251BCCAB27BAB86047439CC292_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, Il2CppFullySharedGenericAny ___1_val, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_t6076CC06F19BCC0301AE8C734F26CA429D2DB469 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 0));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_FieldType_t6076CC06F19BCC0301AE8C734F26CA429D2DB469);
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = ___0_fieldName;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? ___1_val : &___1_val), SizeOf_FieldType_t6076CC06F19BCC0301AE8C734F26CA429D2DB469);
		InvokerActionInvoker2< String_t*, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), __this, L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 0)) ? L_1: *(void**)L_1));
		return;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Call_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m73D49A76D8F24AD1815CF94E87F1EF4683A38EA6 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	bool V_3 = false;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	bool V_5 = false;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	bool G_B30_0 = false;
	bool G_B35_0 = false;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jobject;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				bool L_26;
				il2cpp_codegen_box_unbox((&L_25), (&L_26), sizeof(bool), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_26;
				goto IL_03ea;
			}

IL_0090_1:
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jobject;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_28, L_29, L_30, NULL);
				V_3 = L_31;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_32 = __this->___m_jobject;
				intptr_t L_33;
				L_33 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_32, NULL);
				intptr_t L_34 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_35 = V_0;
				int8_t L_36;
				L_36 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_33, L_34, L_35, NULL);
				uint8_t L_37 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_36,NULL));
				uint8_t L_38 = L_37;
				bool L_39;
				il2cpp_codegen_box_unbox((&L_38), (&L_39), sizeof(bool), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_39;
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_40 = __this->___m_jobject;
				intptr_t L_41;
				L_41 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_40, NULL);
				intptr_t L_42 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_43 = V_0;
				int8_t L_44;
				L_44 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_41, L_42, L_43, NULL);
				int8_t L_45 = L_44;
				bool L_46;
				il2cpp_codegen_box_unbox((&L_45), (&L_46), sizeof(bool), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_46;
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_47 = __this->___m_jobject;
				intptr_t L_48;
				L_48 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_47, NULL);
				intptr_t L_49 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_50 = V_0;
				int16_t L_51;
				L_51 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_48, L_49, L_50, NULL);
				int16_t L_52 = L_51;
				bool L_53;
				il2cpp_codegen_box_unbox((&L_52), (&L_53), sizeof(bool), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_53;
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jobject;
				intptr_t L_55;
				L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
				intptr_t L_56 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_57 = V_0;
				int64_t L_58;
				L_58 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_55, L_56, L_57, NULL);
				int64_t L_59 = L_58;
				bool L_60;
				il2cpp_codegen_box_unbox((&L_59), (&L_60), sizeof(bool), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_60;
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_61 = __this->___m_jobject;
				intptr_t L_62;
				L_62 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_61, NULL);
				intptr_t L_63 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_64 = V_0;
				float L_65;
				L_65 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_62, L_63, L_64, NULL);
				float L_66 = L_65;
				bool L_67;
				il2cpp_codegen_box_unbox((&L_66), (&L_67), sizeof(bool), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_67;
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jobject;
				intptr_t L_69;
				L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
				intptr_t L_70 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_71 = V_0;
				double L_72;
				L_72 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_69, L_70, L_71, NULL);
				double L_73 = L_72;
				bool L_74;
				il2cpp_codegen_box_unbox((&L_73), (&L_74), sizeof(bool), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_74;
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jobject;
				intptr_t L_76;
				L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
				intptr_t L_77 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_78 = V_0;
				Il2CppChar L_79;
				L_79 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_76, L_77, L_78, NULL);
				Il2CppChar L_80 = L_79;
				bool L_81;
				il2cpp_codegen_box_unbox((&L_80), (&L_81), sizeof(bool), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jobject;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_83, L_84, L_85, NULL);
				V_3 = ((*(bool*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jobject;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(bool*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(bool));
				bool L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jobject;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(bool*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(bool));
				bool L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jobject;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_113, L_114, L_115, NULL);
				bool L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mA1CB7EC9473B0674763C89AC566EC664159108E9(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(bool));
				bool L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		bool L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Call_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m72E1F5D2CE435E45651E8B970A73B9F5E3E03815 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE3914411BE312CBBA869835158E535A06CD3EEB4(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		bool L_7;
		L_7 = AndroidJavaObject__Call_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m73D49A76D8F24AD1815CF94E87F1EF4683A38EA6(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject__Call_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m8B3C7C06394410B77DB9953832CB9F8C29C28E89 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	Il2CppChar V_3 = 0x0;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	Il2CppChar V_5 = 0x0;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	Il2CppChar G_B30_0 = 0x0;
	Il2CppChar G_B35_0 = 0x0;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jobject;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				Il2CppChar L_26;
				il2cpp_codegen_box_unbox((&L_25), (&L_26), sizeof(Il2CppChar), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_26;
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jobject;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_28, L_29, L_30, NULL);
				bool L_32 = L_31;
				Il2CppChar L_33;
				il2cpp_codegen_box_unbox((&L_32), (&L_33), sizeof(Il2CppChar), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_33;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_34 = __this->___m_jobject;
				intptr_t L_35;
				L_35 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_34, NULL);
				intptr_t L_36 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_37 = V_0;
				int8_t L_38;
				L_38 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_35, L_36, L_37, NULL);
				uint8_t L_39 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_38,NULL));
				uint8_t L_40 = L_39;
				Il2CppChar L_41;
				il2cpp_codegen_box_unbox((&L_40), (&L_41), sizeof(Il2CppChar), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_41;
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_42 = __this->___m_jobject;
				intptr_t L_43;
				L_43 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_42, NULL);
				intptr_t L_44 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_45 = V_0;
				int8_t L_46;
				L_46 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_43, L_44, L_45, NULL);
				int8_t L_47 = L_46;
				Il2CppChar L_48;
				il2cpp_codegen_box_unbox((&L_47), (&L_48), sizeof(Il2CppChar), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_48;
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_49 = __this->___m_jobject;
				intptr_t L_50;
				L_50 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_49, NULL);
				intptr_t L_51 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_52 = V_0;
				int16_t L_53;
				L_53 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_50, L_51, L_52, NULL);
				int16_t L_54 = L_53;
				Il2CppChar L_55;
				il2cpp_codegen_box_unbox((&L_54), (&L_55), sizeof(Il2CppChar), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_55;
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_56 = __this->___m_jobject;
				intptr_t L_57;
				L_57 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_56, NULL);
				intptr_t L_58 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_59 = V_0;
				int64_t L_60;
				L_60 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_57, L_58, L_59, NULL);
				int64_t L_61 = L_60;
				Il2CppChar L_62;
				il2cpp_codegen_box_unbox((&L_61), (&L_62), sizeof(Il2CppChar), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_62;
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_63 = __this->___m_jobject;
				intptr_t L_64;
				L_64 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_63, NULL);
				intptr_t L_65 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_66 = V_0;
				float L_67;
				L_67 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_64, L_65, L_66, NULL);
				float L_68 = L_67;
				Il2CppChar L_69;
				il2cpp_codegen_box_unbox((&L_68), (&L_69), sizeof(Il2CppChar), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_69;
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_70 = __this->___m_jobject;
				intptr_t L_71;
				L_71 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_70, NULL);
				intptr_t L_72 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_73 = V_0;
				double L_74;
				L_74 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_71, L_72, L_73, NULL);
				double L_75 = L_74;
				Il2CppChar L_76;
				il2cpp_codegen_box_unbox((&L_75), (&L_76), sizeof(Il2CppChar), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_76;
				goto IL_03ea;
			}

IL_0246_1:
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_77 = __this->___m_jobject;
				intptr_t L_78;
				L_78 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_77, NULL);
				intptr_t L_79 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_80 = V_0;
				Il2CppChar L_81;
				L_81 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_78, L_79, L_80, NULL);
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jobject;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_83, L_84, L_85, NULL);
				V_3 = ((*(Il2CppChar*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jobject;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(Il2CppChar*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(Il2CppChar));
				Il2CppChar L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jobject;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(Il2CppChar*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(Il2CppChar));
				Il2CppChar L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jobject;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_113, L_114, L_115, NULL);
				Il2CppChar L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m18459EC016E866EF307EC4D9442CD86FD9BC28B4(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(Il2CppChar));
				Il2CppChar L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		Il2CppChar L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar AndroidJavaObject__Call_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m22710813CDD982DC5F88C2A4067FBFE1D57A3065 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m791647AA6EAD3B88467418F51CF53F5755AB38BA(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		Il2CppChar L_7;
		L_7 = AndroidJavaObject__Call_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m8B3C7C06394410B77DB9953832CB9F8C29C28E89(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject__Call_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m0BA90472306DF5C3A1491FC3EB052DD58FA468C3 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	double V_3 = 0.0;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	double V_5 = 0.0;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	double G_B30_0 = 0.0;
	double G_B35_0 = 0.0;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jobject;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				double L_26;
				il2cpp_codegen_box_unbox((&L_25), (&L_26), sizeof(double), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_26;
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jobject;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_28, L_29, L_30, NULL);
				bool L_32 = L_31;
				double L_33;
				il2cpp_codegen_box_unbox((&L_32), (&L_33), sizeof(double), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_33;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_34 = __this->___m_jobject;
				intptr_t L_35;
				L_35 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_34, NULL);
				intptr_t L_36 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_37 = V_0;
				int8_t L_38;
				L_38 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_35, L_36, L_37, NULL);
				uint8_t L_39 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_38,NULL));
				uint8_t L_40 = L_39;
				double L_41;
				il2cpp_codegen_box_unbox((&L_40), (&L_41), sizeof(double), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_41;
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_42 = __this->___m_jobject;
				intptr_t L_43;
				L_43 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_42, NULL);
				intptr_t L_44 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_45 = V_0;
				int8_t L_46;
				L_46 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_43, L_44, L_45, NULL);
				int8_t L_47 = L_46;
				double L_48;
				il2cpp_codegen_box_unbox((&L_47), (&L_48), sizeof(double), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_48;
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_49 = __this->___m_jobject;
				intptr_t L_50;
				L_50 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_49, NULL);
				intptr_t L_51 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_52 = V_0;
				int16_t L_53;
				L_53 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_50, L_51, L_52, NULL);
				int16_t L_54 = L_53;
				double L_55;
				il2cpp_codegen_box_unbox((&L_54), (&L_55), sizeof(double), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_55;
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_56 = __this->___m_jobject;
				intptr_t L_57;
				L_57 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_56, NULL);
				intptr_t L_58 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_59 = V_0;
				int64_t L_60;
				L_60 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_57, L_58, L_59, NULL);
				int64_t L_61 = L_60;
				double L_62;
				il2cpp_codegen_box_unbox((&L_61), (&L_62), sizeof(double), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_62;
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_63 = __this->___m_jobject;
				intptr_t L_64;
				L_64 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_63, NULL);
				intptr_t L_65 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_66 = V_0;
				float L_67;
				L_67 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_64, L_65, L_66, NULL);
				float L_68 = L_67;
				double L_69;
				il2cpp_codegen_box_unbox((&L_68), (&L_69), sizeof(double), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_69;
				goto IL_03ea;
			}

IL_0209_1:
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_70 = __this->___m_jobject;
				intptr_t L_71;
				L_71 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_70, NULL);
				intptr_t L_72 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_73 = V_0;
				double L_74;
				L_74 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_71, L_72, L_73, NULL);
				V_3 = L_74;
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jobject;
				intptr_t L_76;
				L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
				intptr_t L_77 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_78 = V_0;
				Il2CppChar L_79;
				L_79 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_76, L_77, L_78, NULL);
				Il2CppChar L_80 = L_79;
				double L_81;
				il2cpp_codegen_box_unbox((&L_80), (&L_81), sizeof(double), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jobject;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_83, L_84, L_85, NULL);
				V_3 = ((*(double*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jobject;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(double*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(double));
				double L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jobject;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(double*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(double));
				double L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jobject;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_113, L_114, L_115, NULL);
				double L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m3CAD187DEBCBD88D94604015E86A37B8944847BC(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(double));
				double L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		double L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR double AndroidJavaObject__Call_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m946CEC22A94586DE93BE0725E5F25E04589F69DE (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m44C60AAE64EE97BFD5D9D302E7BE5BE20E6862B3(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		double L_7;
		L_7 = AndroidJavaObject__Call_TisDouble_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_m0BA90472306DF5C3A1491FC3EB052DD58FA468C3(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject__Call_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mA616697346ABF4A696B8F63B5F85AA92777EA400 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	int16_t V_3 = 0;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	int16_t V_5 = 0;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	int16_t G_B30_0 = 0;
	int16_t G_B35_0 = 0;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jobject;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				int16_t L_26;
				il2cpp_codegen_box_unbox((&L_25), (&L_26), sizeof(int16_t), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_26;
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jobject;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_28, L_29, L_30, NULL);
				bool L_32 = L_31;
				int16_t L_33;
				il2cpp_codegen_box_unbox((&L_32), (&L_33), sizeof(int16_t), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_33;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_34 = __this->___m_jobject;
				intptr_t L_35;
				L_35 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_34, NULL);
				intptr_t L_36 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_37 = V_0;
				int8_t L_38;
				L_38 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_35, L_36, L_37, NULL);
				uint8_t L_39 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_38,NULL));
				uint8_t L_40 = L_39;
				int16_t L_41;
				il2cpp_codegen_box_unbox((&L_40), (&L_41), sizeof(int16_t), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_41;
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_42 = __this->___m_jobject;
				intptr_t L_43;
				L_43 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_42, NULL);
				intptr_t L_44 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_45 = V_0;
				int8_t L_46;
				L_46 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_43, L_44, L_45, NULL);
				int8_t L_47 = L_46;
				int16_t L_48;
				il2cpp_codegen_box_unbox((&L_47), (&L_48), sizeof(int16_t), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_48;
				goto IL_03ea;
			}

IL_0152_1:
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_49 = __this->___m_jobject;
				intptr_t L_50;
				L_50 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_49, NULL);
				intptr_t L_51 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_52 = V_0;
				int16_t L_53;
				L_53 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_50, L_51, L_52, NULL);
				V_3 = L_53;
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jobject;
				intptr_t L_55;
				L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
				intptr_t L_56 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_57 = V_0;
				int64_t L_58;
				L_58 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_55, L_56, L_57, NULL);
				int64_t L_59 = L_58;
				int16_t L_60;
				il2cpp_codegen_box_unbox((&L_59), (&L_60), sizeof(int16_t), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_60;
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_61 = __this->___m_jobject;
				intptr_t L_62;
				L_62 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_61, NULL);
				intptr_t L_63 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_64 = V_0;
				float L_65;
				L_65 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_62, L_63, L_64, NULL);
				float L_66 = L_65;
				int16_t L_67;
				il2cpp_codegen_box_unbox((&L_66), (&L_67), sizeof(int16_t), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_67;
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jobject;
				intptr_t L_69;
				L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
				intptr_t L_70 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_71 = V_0;
				double L_72;
				L_72 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_69, L_70, L_71, NULL);
				double L_73 = L_72;
				int16_t L_74;
				il2cpp_codegen_box_unbox((&L_73), (&L_74), sizeof(int16_t), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_74;
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jobject;
				intptr_t L_76;
				L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
				intptr_t L_77 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_78 = V_0;
				Il2CppChar L_79;
				L_79 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_76, L_77, L_78, NULL);
				Il2CppChar L_80 = L_79;
				int16_t L_81;
				il2cpp_codegen_box_unbox((&L_80), (&L_81), sizeof(int16_t), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jobject;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_83, L_84, L_85, NULL);
				V_3 = ((*(int16_t*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jobject;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(int16_t*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int16_t));
				int16_t L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jobject;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(int16_t*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int16_t));
				int16_t L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jobject;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_113, L_114, L_115, NULL);
				int16_t L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m62441E1DA5798B8F585F5EF18386C4FB739BFFC8(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int16_t));
				int16_t L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		int16_t L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int16_t AndroidJavaObject__Call_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m08A0CE3C4BDDA756095A0A816C659587814F649E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_m3B57915B048BD436E00741CD146D62AC775E7BA3(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		int16_t L_7;
		L_7 = AndroidJavaObject__Call_TisInt16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_mA616697346ABF4A696B8F63B5F85AA92777EA400(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m0D60BAB951F2C313123D62CAB81EEA263F2F0750 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	int32_t V_3 = 0;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	int32_t V_5 = 0;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	int32_t G_B30_0 = 0;
	int32_t G_B35_0 = 0;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jobject;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_21, L_22, L_23, NULL);
				V_3 = L_24;
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_25 = __this->___m_jobject;
				intptr_t L_26;
				L_26 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_25, NULL);
				intptr_t L_27 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_28 = V_0;
				bool L_29;
				L_29 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_26, L_27, L_28, NULL);
				bool L_30 = L_29;
				int32_t L_31;
				il2cpp_codegen_box_unbox((&L_30), (&L_31), sizeof(int32_t), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_31;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_32 = __this->___m_jobject;
				intptr_t L_33;
				L_33 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_32, NULL);
				intptr_t L_34 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_35 = V_0;
				int8_t L_36;
				L_36 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_33, L_34, L_35, NULL);
				uint8_t L_37 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_36,NULL));
				uint8_t L_38 = L_37;
				int32_t L_39;
				il2cpp_codegen_box_unbox((&L_38), (&L_39), sizeof(int32_t), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_39;
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_40 = __this->___m_jobject;
				intptr_t L_41;
				L_41 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_40, NULL);
				intptr_t L_42 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_43 = V_0;
				int8_t L_44;
				L_44 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_41, L_42, L_43, NULL);
				int8_t L_45 = L_44;
				int32_t L_46;
				il2cpp_codegen_box_unbox((&L_45), (&L_46), sizeof(int32_t), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_46;
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_47 = __this->___m_jobject;
				intptr_t L_48;
				L_48 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_47, NULL);
				intptr_t L_49 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_50 = V_0;
				int16_t L_51;
				L_51 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_48, L_49, L_50, NULL);
				int16_t L_52 = L_51;
				int32_t L_53;
				il2cpp_codegen_box_unbox((&L_52), (&L_53), sizeof(int32_t), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_53;
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jobject;
				intptr_t L_55;
				L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
				intptr_t L_56 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_57 = V_0;
				int64_t L_58;
				L_58 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_55, L_56, L_57, NULL);
				int64_t L_59 = L_58;
				int32_t L_60;
				il2cpp_codegen_box_unbox((&L_59), (&L_60), sizeof(int32_t), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_60;
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_61 = __this->___m_jobject;
				intptr_t L_62;
				L_62 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_61, NULL);
				intptr_t L_63 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_64 = V_0;
				float L_65;
				L_65 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_62, L_63, L_64, NULL);
				float L_66 = L_65;
				int32_t L_67;
				il2cpp_codegen_box_unbox((&L_66), (&L_67), sizeof(int32_t), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_67;
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jobject;
				intptr_t L_69;
				L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
				intptr_t L_70 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_71 = V_0;
				double L_72;
				L_72 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_69, L_70, L_71, NULL);
				double L_73 = L_72;
				int32_t L_74;
				il2cpp_codegen_box_unbox((&L_73), (&L_74), sizeof(int32_t), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_74;
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jobject;
				intptr_t L_76;
				L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
				intptr_t L_77 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_78 = V_0;
				Il2CppChar L_79;
				L_79 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_76, L_77, L_78, NULL);
				Il2CppChar L_80 = L_79;
				int32_t L_81;
				il2cpp_codegen_box_unbox((&L_80), (&L_81), sizeof(int32_t), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jobject;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_83, L_84, L_85, NULL);
				V_3 = ((*(int32_t*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jobject;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(int32_t*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int32_t));
				int32_t L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jobject;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(int32_t*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int32_t));
				int32_t L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jobject;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_113, L_114, L_115, NULL);
				int32_t L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC1390BDE8BF42FAA78215DFE076A25F7AEA674D7(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int32_t));
				int32_t L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		int32_t L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1BFF486C91846170C07AAF27EF84971FF0596B90 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mF87CC238DDBB3C90C35846E626548847CC7E319D(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		int32_t L_7;
		L_7 = AndroidJavaObject__Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m0D60BAB951F2C313123D62CAB81EEA263F2F0750(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject__Call_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m4B925872A7B673BC9981AB72E3A229BBE0E1119E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	int64_t V_3 = 0;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	int64_t V_5 = 0;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	int64_t G_B30_0 = 0;
	int64_t G_B35_0 = 0;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jobject;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				int64_t L_26;
				il2cpp_codegen_box_unbox((&L_25), (&L_26), sizeof(int64_t), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_26;
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jobject;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_28, L_29, L_30, NULL);
				bool L_32 = L_31;
				int64_t L_33;
				il2cpp_codegen_box_unbox((&L_32), (&L_33), sizeof(int64_t), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_33;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_34 = __this->___m_jobject;
				intptr_t L_35;
				L_35 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_34, NULL);
				intptr_t L_36 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_37 = V_0;
				int8_t L_38;
				L_38 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_35, L_36, L_37, NULL);
				uint8_t L_39 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_38,NULL));
				uint8_t L_40 = L_39;
				int64_t L_41;
				il2cpp_codegen_box_unbox((&L_40), (&L_41), sizeof(int64_t), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_41;
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_42 = __this->___m_jobject;
				intptr_t L_43;
				L_43 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_42, NULL);
				intptr_t L_44 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_45 = V_0;
				int8_t L_46;
				L_46 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_43, L_44, L_45, NULL);
				int8_t L_47 = L_46;
				int64_t L_48;
				il2cpp_codegen_box_unbox((&L_47), (&L_48), sizeof(int64_t), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_48;
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_49 = __this->___m_jobject;
				intptr_t L_50;
				L_50 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_49, NULL);
				intptr_t L_51 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_52 = V_0;
				int16_t L_53;
				L_53 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_50, L_51, L_52, NULL);
				int16_t L_54 = L_53;
				int64_t L_55;
				il2cpp_codegen_box_unbox((&L_54), (&L_55), sizeof(int64_t), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_55;
				goto IL_03ea;
			}

IL_018f_1:
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_56 = __this->___m_jobject;
				intptr_t L_57;
				L_57 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_56, NULL);
				intptr_t L_58 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_59 = V_0;
				int64_t L_60;
				L_60 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_57, L_58, L_59, NULL);
				V_3 = L_60;
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_61 = __this->___m_jobject;
				intptr_t L_62;
				L_62 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_61, NULL);
				intptr_t L_63 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_64 = V_0;
				float L_65;
				L_65 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_62, L_63, L_64, NULL);
				float L_66 = L_65;
				int64_t L_67;
				il2cpp_codegen_box_unbox((&L_66), (&L_67), sizeof(int64_t), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_67;
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jobject;
				intptr_t L_69;
				L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
				intptr_t L_70 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_71 = V_0;
				double L_72;
				L_72 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_69, L_70, L_71, NULL);
				double L_73 = L_72;
				int64_t L_74;
				il2cpp_codegen_box_unbox((&L_73), (&L_74), sizeof(int64_t), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_74;
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jobject;
				intptr_t L_76;
				L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
				intptr_t L_77 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_78 = V_0;
				Il2CppChar L_79;
				L_79 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_76, L_77, L_78, NULL);
				Il2CppChar L_80 = L_79;
				int64_t L_81;
				il2cpp_codegen_box_unbox((&L_80), (&L_81), sizeof(int64_t), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jobject;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_83, L_84, L_85, NULL);
				V_3 = ((*(int64_t*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jobject;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(int64_t*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int64_t));
				int64_t L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jobject;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(int64_t*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int64_t));
				int64_t L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jobject;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_113, L_114, L_115, NULL);
				int64_t L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m6A8E0C87ADD6FC1CA3F936C0CC0AC4AB379A8422(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int64_t));
				int64_t L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		int64_t L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int64_t AndroidJavaObject__Call_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_mC6DBECDD44973120E0A56068ABE886D9A7016F5C (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m9665934CC08FD80EB16882C4EA6FDBC5D9C3A175(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		int64_t L_7;
		L_7 = AndroidJavaObject__Call_TisInt64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_m4B925872A7B673BC9981AB72E3A229BBE0E1119E(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject__Call_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mA3300F1D8CC0B110426EAE23F5FB7F27FD8AE76B (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	int8_t V_3 = 0x0;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	int8_t V_5 = 0x0;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	int8_t G_B30_0 = 0x0;
	int8_t G_B35_0 = 0x0;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jobject;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				int8_t L_26;
				il2cpp_codegen_box_unbox((&L_25), (&L_26), sizeof(int8_t), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_26;
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jobject;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_28, L_29, L_30, NULL);
				bool L_32 = L_31;
				int8_t L_33;
				il2cpp_codegen_box_unbox((&L_32), (&L_33), sizeof(int8_t), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_33;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_34 = __this->___m_jobject;
				intptr_t L_35;
				L_35 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_34, NULL);
				intptr_t L_36 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_37 = V_0;
				int8_t L_38;
				L_38 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_35, L_36, L_37, NULL);
				uint8_t L_39 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_38,NULL));
				uint8_t L_40 = L_39;
				int8_t L_41;
				il2cpp_codegen_box_unbox((&L_40), (&L_41), sizeof(int8_t), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_41;
				goto IL_03ea;
			}

IL_0115_1:
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_42 = __this->___m_jobject;
				intptr_t L_43;
				L_43 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_42, NULL);
				intptr_t L_44 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_45 = V_0;
				int8_t L_46;
				L_46 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_43, L_44, L_45, NULL);
				V_3 = L_46;
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_47 = __this->___m_jobject;
				intptr_t L_48;
				L_48 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_47, NULL);
				intptr_t L_49 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_50 = V_0;
				int16_t L_51;
				L_51 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_48, L_49, L_50, NULL);
				int16_t L_52 = L_51;
				int8_t L_53;
				il2cpp_codegen_box_unbox((&L_52), (&L_53), sizeof(int8_t), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_53;
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jobject;
				intptr_t L_55;
				L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
				intptr_t L_56 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_57 = V_0;
				int64_t L_58;
				L_58 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_55, L_56, L_57, NULL);
				int64_t L_59 = L_58;
				int8_t L_60;
				il2cpp_codegen_box_unbox((&L_59), (&L_60), sizeof(int8_t), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_60;
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_61 = __this->___m_jobject;
				intptr_t L_62;
				L_62 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_61, NULL);
				intptr_t L_63 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_64 = V_0;
				float L_65;
				L_65 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_62, L_63, L_64, NULL);
				float L_66 = L_65;
				int8_t L_67;
				il2cpp_codegen_box_unbox((&L_66), (&L_67), sizeof(int8_t), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_67;
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jobject;
				intptr_t L_69;
				L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
				intptr_t L_70 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_71 = V_0;
				double L_72;
				L_72 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_69, L_70, L_71, NULL);
				double L_73 = L_72;
				int8_t L_74;
				il2cpp_codegen_box_unbox((&L_73), (&L_74), sizeof(int8_t), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_74;
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jobject;
				intptr_t L_76;
				L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
				intptr_t L_77 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_78 = V_0;
				Il2CppChar L_79;
				L_79 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_76, L_77, L_78, NULL);
				Il2CppChar L_80 = L_79;
				int8_t L_81;
				il2cpp_codegen_box_unbox((&L_80), (&L_81), sizeof(int8_t), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jobject;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_83, L_84, L_85, NULL);
				V_3 = ((*(int8_t*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jobject;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(int8_t*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int8_t));
				int8_t L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jobject;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(int8_t*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int8_t));
				int8_t L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jobject;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_113, L_114, L_115, NULL);
				int8_t L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mB474FE99696A6DEB269E0F1C3C7DA32CD8854410(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int8_t));
				int8_t L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		int8_t L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int8_t AndroidJavaObject__Call_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mAE2F0D59E0CE54D36BB86C53218704FA72DC0350 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mEF6ACD1C6C5237E0446DB9DBDAC40BA2660849BB(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		int8_t L_7;
		L_7 = AndroidJavaObject__Call_TisSByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_mA3300F1D8CC0B110426EAE23F5FB7F27FD8AE76B(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject__Call_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m8EDEAF89FE2C5B65277B0FC57FAD4FA9253E1140 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	float V_3 = 0.0f;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	float V_5 = 0.0f;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	float G_B30_0 = 0.0f;
	float G_B35_0 = 0.0f;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jobject;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				float L_26;
				il2cpp_codegen_box_unbox((&L_25), (&L_26), sizeof(float), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_26;
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jobject;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_28, L_29, L_30, NULL);
				bool L_32 = L_31;
				float L_33;
				il2cpp_codegen_box_unbox((&L_32), (&L_33), sizeof(float), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_33;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_34 = __this->___m_jobject;
				intptr_t L_35;
				L_35 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_34, NULL);
				intptr_t L_36 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_37 = V_0;
				int8_t L_38;
				L_38 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_35, L_36, L_37, NULL);
				uint8_t L_39 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_38,NULL));
				uint8_t L_40 = L_39;
				float L_41;
				il2cpp_codegen_box_unbox((&L_40), (&L_41), sizeof(float), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_41;
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_42 = __this->___m_jobject;
				intptr_t L_43;
				L_43 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_42, NULL);
				intptr_t L_44 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_45 = V_0;
				int8_t L_46;
				L_46 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_43, L_44, L_45, NULL);
				int8_t L_47 = L_46;
				float L_48;
				il2cpp_codegen_box_unbox((&L_47), (&L_48), sizeof(float), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_48;
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_49 = __this->___m_jobject;
				intptr_t L_50;
				L_50 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_49, NULL);
				intptr_t L_51 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_52 = V_0;
				int16_t L_53;
				L_53 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_50, L_51, L_52, NULL);
				int16_t L_54 = L_53;
				float L_55;
				il2cpp_codegen_box_unbox((&L_54), (&L_55), sizeof(float), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_55;
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_56 = __this->___m_jobject;
				intptr_t L_57;
				L_57 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_56, NULL);
				intptr_t L_58 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_59 = V_0;
				int64_t L_60;
				L_60 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_57, L_58, L_59, NULL);
				int64_t L_61 = L_60;
				float L_62;
				il2cpp_codegen_box_unbox((&L_61), (&L_62), sizeof(float), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_62;
				goto IL_03ea;
			}

IL_01cc_1:
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_63 = __this->___m_jobject;
				intptr_t L_64;
				L_64 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_63, NULL);
				intptr_t L_65 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_66 = V_0;
				float L_67;
				L_67 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_64, L_65, L_66, NULL);
				V_3 = L_67;
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jobject;
				intptr_t L_69;
				L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
				intptr_t L_70 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_71 = V_0;
				double L_72;
				L_72 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_69, L_70, L_71, NULL);
				double L_73 = L_72;
				float L_74;
				il2cpp_codegen_box_unbox((&L_73), (&L_74), sizeof(float), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_74;
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jobject;
				intptr_t L_76;
				L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
				intptr_t L_77 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_78 = V_0;
				Il2CppChar L_79;
				L_79 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_76, L_77, L_78, NULL);
				Il2CppChar L_80 = L_79;
				float L_81;
				il2cpp_codegen_box_unbox((&L_80), (&L_81), sizeof(float), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jobject;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_83, L_84, L_85, NULL);
				V_3 = ((*(float*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jobject;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(float*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(float));
				float L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jobject;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(float*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(float));
				float L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jobject;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_113, L_114, L_115, NULL);
				float L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_mD5A794B10305822230E288BE2D72329A6EE0C8A4(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(float));
				float L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		float L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AndroidJavaObject__Call_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m71549B5031FEE0FBB78A9CE02EDADD14DBFFB56D (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_mA3FFDC6948922BEFC787A297D5483BA277EEFF0C(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		float L_7;
		L_7 = AndroidJavaObject__Call_TisSingle_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_m8EDEAF89FE2C5B65277B0FC57FAD4FA9253E1140(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m34A45129A0AA339AC2E31492EF928112EDECC197_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	Il2CppSharedGenericObject* V_3 = NULL;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	Il2CppSharedGenericObject* V_5 = NULL;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	Il2CppSharedGenericObject* G_B30_0 = NULL;
	Il2CppSharedGenericObject* G_B35_0 = NULL;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jobject;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				RuntimeObject* L_26 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_25);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_26, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jobject;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_28, L_29, L_30, NULL);
				bool L_32 = L_31;
				RuntimeObject* L_33 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_32);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_33, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_34 = __this->___m_jobject;
				intptr_t L_35;
				L_35 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_34, NULL);
				intptr_t L_36 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_37 = V_0;
				int8_t L_38;
				L_38 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_35, L_36, L_37, NULL);
				uint8_t L_39 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_38,NULL));
				uint8_t L_40 = L_39;
				RuntimeObject* L_41 = Box(Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, &L_40);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_41, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_42 = __this->___m_jobject;
				intptr_t L_43;
				L_43 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_42, NULL);
				intptr_t L_44 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_45 = V_0;
				int8_t L_46;
				L_46 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_43, L_44, L_45, NULL);
				int8_t L_47 = L_46;
				RuntimeObject* L_48 = Box(SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, &L_47);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_48, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_49 = __this->___m_jobject;
				intptr_t L_50;
				L_50 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_49, NULL);
				intptr_t L_51 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_52 = V_0;
				int16_t L_53;
				L_53 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_50, L_51, L_52, NULL);
				int16_t L_54 = L_53;
				RuntimeObject* L_55 = Box(Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, &L_54);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_55, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_56 = __this->___m_jobject;
				intptr_t L_57;
				L_57 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_56, NULL);
				intptr_t L_58 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_59 = V_0;
				int64_t L_60;
				L_60 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_57, L_58, L_59, NULL);
				int64_t L_61 = L_60;
				RuntimeObject* L_62 = Box(Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, &L_61);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_62, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_63 = __this->___m_jobject;
				intptr_t L_64;
				L_64 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_63, NULL);
				intptr_t L_65 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_66 = V_0;
				float L_67;
				L_67 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_64, L_65, L_66, NULL);
				float L_68 = L_67;
				RuntimeObject* L_69 = Box(Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, &L_68);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_69, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_70 = __this->___m_jobject;
				intptr_t L_71;
				L_71 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_70, NULL);
				intptr_t L_72 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_73 = V_0;
				double L_74;
				L_74 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_71, L_72, L_73, NULL);
				double L_75 = L_74;
				RuntimeObject* L_76 = Box(Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, &L_75);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_76, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_77 = __this->___m_jobject;
				intptr_t L_78;
				L_78 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_77, NULL);
				intptr_t L_79 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_80 = V_0;
				Il2CppChar L_81;
				L_81 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_78, L_79, L_80, NULL);
				Il2CppChar L_82 = L_81;
				RuntimeObject* L_83 = Box(Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, &L_82);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_83, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0286_1:
			{
				bool L_84 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_84)
				{
					goto IL_02be_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_85 = __this->___m_jobject;
				intptr_t L_86;
				L_86 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_85, NULL);
				intptr_t L_87 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_88 = V_0;
				String_t* L_89;
				L_89 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_86, L_87, L_88, NULL);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_89, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_02be_1:
			{
				bool L_90 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_90)
				{
					goto IL_0319_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_91 = __this->___m_jobject;
				intptr_t L_92;
				L_92 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_91, NULL);
				intptr_t L_93 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_94 = V_0;
				intptr_t L_95;
				L_95 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_92, L_93, L_94, NULL);
				V_4 = L_95;
				intptr_t L_96 = V_4;
				bool L_97;
				L_97 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_96, 0, NULL);
				if (L_97)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_98 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_99 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_99, L_98, NULL);
				G_B30_0 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_99, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(Il2CppSharedGenericObject*));
				Il2CppSharedGenericObject* L_100 = V_5;
				G_B30_0 = L_100;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				bool L_101 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_101)
				{
					goto IL_0371_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_102 = __this->___m_jobject;
				intptr_t L_103;
				L_103 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_102, NULL);
				intptr_t L_104 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_105 = V_0;
				intptr_t L_106;
				L_106 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_103, L_104, L_105, NULL);
				V_6 = L_106;
				intptr_t L_107 = V_6;
				bool L_108;
				L_108 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_107, 0, NULL);
				if (L_108)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_109 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_110 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_110, L_109, NULL);
				G_B35_0 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_110, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(Il2CppSharedGenericObject*));
				Il2CppSharedGenericObject* L_111 = V_5;
				G_B35_0 = L_111;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_112 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_113;
				L_113 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_112, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_114 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_115;
				L_115 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_114, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_116;
				L_116 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_113, L_115, NULL);
				if (!L_116)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_117 = __this->___m_jobject;
				intptr_t L_118;
				L_118 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_117, NULL);
				intptr_t L_119 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_120 = V_0;
				intptr_t L_121;
				L_121 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_118, L_119, L_120, NULL);
				Il2CppSharedGenericObject* L_122;
				L_122 = AndroidJavaObject_FromJavaArray_TisIl2CppSharedGenericObject_m22D9898503E929D77563392822CD11A056613AFF(L_121, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_122;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_123 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_124;
				L_124 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_123, NULL);
				Type_t* L_125 = L_124;
				if (L_125)
				{
					G_B40_0 = L_125;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_125;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_126;
				L_126 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_126;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_127;
				L_127 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_128 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_128, L_127, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_128, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(Il2CppSharedGenericObject*));
				Il2CppSharedGenericObject* L_129 = V_5;
				V_3 = L_129;
				goto IL_03ea;
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

IL_03ea:
	{
		Il2CppSharedGenericObject* L_130 = V_3;
		return L_130;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m39A1E2B88CD5364F83417EFB65F1C296656D2F62_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_m95122CB55DFAF463B14CBBA51C2B0811790DB90B(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		Il2CppSharedGenericObject* L_7;
		L_7 = AndroidJavaObject__Call_TisIl2CppSharedGenericObject_m34A45129A0AA339AC2E31492EF928112EDECC197(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81732
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__Call_TisIl2CppFullySharedGenericAny_m4720E88EC57849809AFC4A771495414E75120D8F_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	const Il2CppFullySharedGenericAny L_27 = alloca(SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
	const Il2CppFullySharedGenericAny L_35 = L_27;
	const Il2CppFullySharedGenericAny L_44 = L_27;
	const Il2CppFullySharedGenericAny L_52 = L_27;
	const Il2CppFullySharedGenericAny L_60 = L_27;
	const Il2CppFullySharedGenericAny L_68 = L_27;
	const Il2CppFullySharedGenericAny L_76 = L_27;
	const Il2CppFullySharedGenericAny L_84 = L_27;
	const Il2CppFullySharedGenericAny L_92 = L_27;
	const Il2CppFullySharedGenericAny L_99 = L_27;
	const Il2CppFullySharedGenericAny L_111 = L_27;
	const Il2CppFullySharedGenericAny L_113 = L_27;
	const Il2CppFullySharedGenericAny L_124 = L_27;
	const Il2CppFullySharedGenericAny L_126 = L_27;
	const Il2CppFullySharedGenericAny L_137 = L_27;
	const Il2CppFullySharedGenericAny L_144 = L_27;
	const Il2CppFullySharedGenericAny L_145 = L_27;
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	Il2CppFullySharedGenericAny V_3 = alloca(SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
	memset(V_3, 0, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	Il2CppFullySharedGenericAny V_5 = alloca(SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
	memset(V_5, 0, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	Il2CppFullySharedGenericAny G_B30_0 = alloca(SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
	memset(G_B30_0, 0, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
	Il2CppFullySharedGenericAny G_B35_0 = alloca(SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
	memset(G_B35_0, 0, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				bool L_20 = (Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_20)
				{
					goto IL_0090_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_21 = __this->___m_jobject;
				intptr_t L_22;
				L_22 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_21, NULL);
				intptr_t L_23 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_24 = V_0;
				int32_t L_25;
				L_25 = AndroidJNISafe_CallIntMethod_m60318205A7EAD0C5CC0643106A7044F1563DCC0E(L_22, L_23, L_24, NULL);
				int32_t L_26 = L_25;
				il2cpp_codegen_box_unbox((&L_26), L_27, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147, Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_27, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_0090_1:
			{
				bool L_28 = (Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_28)
				{
					goto IL_00cd_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_29 = __this->___m_jobject;
				intptr_t L_30;
				L_30 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_29, NULL);
				intptr_t L_31 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_32 = V_0;
				bool L_33;
				L_33 = AndroidJNISafe_CallBooleanMethod_m2F5824C9EA5D1586C7E555F9F8DE01D84757D972(L_30, L_31, L_32, NULL);
				bool L_34 = L_33;
				il2cpp_codegen_box_unbox((&L_34), L_35, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147, Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_35, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_00cd_1:
			{
				bool L_36 = (Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_36)
				{
					goto IL_0115_1;
				}
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_37 = __this->___m_jobject;
				intptr_t L_38;
				L_38 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_37, NULL);
				intptr_t L_39 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_40 = V_0;
				int8_t L_41;
				L_41 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_38, L_39, L_40, NULL);
				uint8_t L_42 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_41,NULL));
				uint8_t L_43 = L_42;
				il2cpp_codegen_box_unbox((&L_43), L_44, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_44, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_0115_1:
			{
				bool L_45 = (SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_45)
				{
					goto IL_0152_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_46 = __this->___m_jobject;
				intptr_t L_47;
				L_47 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_46, NULL);
				intptr_t L_48 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_49 = V_0;
				int8_t L_50;
				L_50 = AndroidJNISafe_CallSByteMethod_m03F9BD1288769A14F5CE8477DACDD62F6D0B77E7(L_47, L_48, L_49, NULL);
				int8_t L_51 = L_50;
				il2cpp_codegen_box_unbox((&L_51), L_52, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147, SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_52, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_0152_1:
			{
				bool L_53 = (Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_53)
				{
					goto IL_018f_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jobject;
				intptr_t L_55;
				L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
				intptr_t L_56 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_57 = V_0;
				int16_t L_58;
				L_58 = AndroidJNISafe_CallShortMethod_m7C82D811B75161D4567651B0D85E5F7A2ED83A97(L_55, L_56, L_57, NULL);
				int16_t L_59 = L_58;
				il2cpp_codegen_box_unbox((&L_59), L_60, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147, Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_60, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_018f_1:
			{
				bool L_61 = (Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_61)
				{
					goto IL_01cc_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_62 = __this->___m_jobject;
				intptr_t L_63;
				L_63 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_62, NULL);
				intptr_t L_64 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_65 = V_0;
				int64_t L_66;
				L_66 = AndroidJNISafe_CallLongMethod_mD04CC840004334A567747BD526F88A813CB833B6(L_63, L_64, L_65, NULL);
				int64_t L_67 = L_66;
				il2cpp_codegen_box_unbox((&L_67), L_68, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147, Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_68, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_01cc_1:
			{
				bool L_69 = (Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_69)
				{
					goto IL_0209_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_70 = __this->___m_jobject;
				intptr_t L_71;
				L_71 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_70, NULL);
				intptr_t L_72 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_73 = V_0;
				float L_74;
				L_74 = AndroidJNISafe_CallFloatMethod_m7437E60E0985885D721F1592E4DACF8246F69BBE(L_71, L_72, L_73, NULL);
				float L_75 = L_74;
				il2cpp_codegen_box_unbox((&L_75), L_76, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147, Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_76, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_0209_1:
			{
				bool L_77 = (Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_77)
				{
					goto IL_0246_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_78 = __this->___m_jobject;
				intptr_t L_79;
				L_79 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_78, NULL);
				intptr_t L_80 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_81 = V_0;
				double L_82;
				L_82 = AndroidJNISafe_CallDoubleMethod_m01B318F7CA4F90C54D689CF0CD84DF312E68CB5E(L_79, L_80, L_81, NULL);
				double L_83 = L_82;
				il2cpp_codegen_box_unbox((&L_83), L_84, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147, Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_84, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_0246_1:
			{
				bool L_85 = (Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_85)
				{
					goto IL_03d1_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_86 = __this->___m_jobject;
				intptr_t L_87;
				L_87 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_86, NULL);
				intptr_t L_88 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_89 = V_0;
				Il2CppChar L_90;
				L_90 = AndroidJNISafe_CallCharMethod_mB777FAF5E9D1BFF480B7EDD5AA5352F30797E1DD(L_87, L_88, L_89, NULL);
				Il2CppChar L_91 = L_90;
				il2cpp_codegen_box_unbox((&L_91), L_92, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147, Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_92, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_0286_1:
			{
				bool L_93 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_93)
				{
					goto IL_02be_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_94 = __this->___m_jobject;
				intptr_t L_95;
				L_95 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_94, NULL);
				intptr_t L_96 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_97 = V_0;
				String_t* L_98;
				L_98 = AndroidJNISafe_CallStringMethod_m4E40DA54A224C0C10A8C600CAC1C2C838B69264C(L_95, L_96, L_97, NULL);
				void* L_100 = UnBox_Any(L_98, il2cpp_rgctx_data(method->rgctx_data, 1), L_99);
				il2cpp_codegen_memcpy(V_3, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_100)), SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_02be_1:
			{
				bool L_101 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_101)
				{
					goto IL_0319_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_102 = __this->___m_jobject;
				intptr_t L_103;
				L_103 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_102, NULL);
				intptr_t L_104 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_105 = V_0;
				intptr_t L_106;
				L_106 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_103, L_104, L_105, NULL);
				V_4 = L_106;
				intptr_t L_107 = V_4;
				bool L_108;
				L_108 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_107, 0, NULL);
				if (L_108)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_109 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_110 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_110, L_109, NULL);
				void* L_112 = UnBox_Any(L_110, il2cpp_rgctx_data(method->rgctx_data, 1), L_111);
				il2cpp_codegen_memcpy(G_B30_0, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_112)), SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_5, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				il2cpp_codegen_memcpy(L_113, V_5, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				il2cpp_codegen_memcpy(G_B30_0, L_113, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
			}

IL_0313_1:
			{
				il2cpp_codegen_memcpy(V_3, G_B30_0, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_0319_1:
			{
				bool L_114 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_114)
				{
					goto IL_0371_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_115 = __this->___m_jobject;
				intptr_t L_116;
				L_116 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_115, NULL);
				intptr_t L_117 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_118 = V_0;
				intptr_t L_119;
				L_119 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_116, L_117, L_118, NULL);
				V_6 = L_119;
				intptr_t L_120 = V_6;
				bool L_121;
				L_121 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_120, 0, NULL);
				if (L_121)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_122 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_123 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_123, L_122, NULL);
				void* L_125 = UnBox_Any(L_123, il2cpp_rgctx_data(method->rgctx_data, 1), L_124);
				il2cpp_codegen_memcpy(G_B35_0, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_125)), SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_5, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				il2cpp_codegen_memcpy(L_126, V_5, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				il2cpp_codegen_memcpy(G_B35_0, L_126, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
			}

IL_036e_1:
			{
				il2cpp_codegen_memcpy(V_3, G_B35_0, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_127 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_128;
				L_128 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_127, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_129 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_130;
				L_130 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_129, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_131;
				L_131 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_128, L_130, NULL);
				if (!L_131)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_132 = __this->___m_jobject;
				intptr_t L_133;
				L_133 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_132, NULL);
				intptr_t L_134 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_135 = V_0;
				intptr_t L_136;
				L_136 = AndroidJNISafe_CallObjectMethod_m12F882542956F2920187AADCD0295D4E32124BEF(L_133, L_134, L_135, NULL);
				InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 2)), il2cpp_rgctx_method(method->rgctx_data, 2), NULL, L_136, (Il2CppFullySharedGenericAny*)L_137);
				il2cpp_codegen_memcpy(V_3, L_137, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_138 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_139;
				L_139 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_138, NULL);
				Type_t* L_140 = L_139;
				if (L_140)
				{
					G_B40_0 = L_140;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_140;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_141;
				L_141 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_141;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_142;
				L_142 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_143 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_143, L_142, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_143, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_5, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				il2cpp_codegen_memcpy(L_144, V_5, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				il2cpp_codegen_memcpy(V_3, L_144, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
				goto IL_03ea;
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

IL_03ea:
	{
		il2cpp_codegen_memcpy(L_145, V_3, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
		il2cpp_codegen_memcpy(il2cppRetVal, L_145, SizeOf_ReturnType_t79ECD6DB4ABC339D39F04EE50C832B65A84F5147);
		return;
	}
}
// Method Definition Index: 81731
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__Call_TisIl2CppFullySharedGenericAny_mA1A866611778FF72A2067F7E30B32913C4EB4904_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_ReturnType_t0FD1385ACD92B5652F803E183304929EDB7632D9 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 2));
	const Il2CppFullySharedGenericAny L_7 = alloca(SizeOf_ReturnType_t0FD1385ACD92B5652F803E183304929EDB7632D9);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = ((  intptr_t (*) (intptr_t, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, bool, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))(L_1, L_2, L_3, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		InvokerActionInvoker3< intptr_t, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), __this, L_5, L_6, (Il2CppFullySharedGenericAny*)L_7);
		il2cpp_codegen_memcpy(il2cppRetVal, L_7, SizeOf_ReturnType_t0FD1385ACD92B5652F803E183304929EDB7632D9);
		return;
	}
}
// Method Definition Index: 81740
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m9EDC162A865064239ADA4058CD2A81B1A2CF6D0F (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	bool V_3 = false;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	bool V_5 = false;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	bool G_B30_0 = false;
	bool G_B35_0 = false;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jclass;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallStaticIntMethod_m915549FA8FD7FB93B57A9708AD759488EA64418C(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				bool L_26;
				il2cpp_codegen_box_unbox((&L_25), (&L_26), sizeof(bool), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_26;
				goto IL_03ea;
			}

IL_0090_1:
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jclass;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallStaticBooleanMethod_m652685AC18F590965249C0F9B107C00C142595BB(L_28, L_29, L_30, NULL);
				V_3 = L_31;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_32 = __this->___m_jclass;
				intptr_t L_33;
				L_33 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_32, NULL);
				intptr_t L_34 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_35 = V_0;
				int8_t L_36;
				L_36 = AndroidJNISafe_CallStaticSByteMethod_m3E1F75978A2D686BC32DBF5A2F1F70F0D746C2B7(L_33, L_34, L_35, NULL);
				uint8_t L_37 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_36,NULL));
				uint8_t L_38 = L_37;
				bool L_39;
				il2cpp_codegen_box_unbox((&L_38), (&L_39), sizeof(bool), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_39;
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_40 = __this->___m_jclass;
				intptr_t L_41;
				L_41 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_40, NULL);
				intptr_t L_42 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_43 = V_0;
				int8_t L_44;
				L_44 = AndroidJNISafe_CallStaticSByteMethod_m3E1F75978A2D686BC32DBF5A2F1F70F0D746C2B7(L_41, L_42, L_43, NULL);
				int8_t L_45 = L_44;
				bool L_46;
				il2cpp_codegen_box_unbox((&L_45), (&L_46), sizeof(bool), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_46;
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_47 = __this->___m_jclass;
				intptr_t L_48;
				L_48 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_47, NULL);
				intptr_t L_49 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_50 = V_0;
				int16_t L_51;
				L_51 = AndroidJNISafe_CallStaticShortMethod_m8330383670ECCD7E24CDD68C419745E486FA6426(L_48, L_49, L_50, NULL);
				int16_t L_52 = L_51;
				bool L_53;
				il2cpp_codegen_box_unbox((&L_52), (&L_53), sizeof(bool), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_53;
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jclass;
				intptr_t L_55;
				L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
				intptr_t L_56 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_57 = V_0;
				int64_t L_58;
				L_58 = AndroidJNISafe_CallStaticLongMethod_mDDE01239BEFCF007ECE05E51A249B3EB5BB61234(L_55, L_56, L_57, NULL);
				int64_t L_59 = L_58;
				bool L_60;
				il2cpp_codegen_box_unbox((&L_59), (&L_60), sizeof(bool), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_60;
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_61 = __this->___m_jclass;
				intptr_t L_62;
				L_62 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_61, NULL);
				intptr_t L_63 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_64 = V_0;
				float L_65;
				L_65 = AndroidJNISafe_CallStaticFloatMethod_m3F5419A10B9DF599352938B2BAD8866F8F112364(L_62, L_63, L_64, NULL);
				float L_66 = L_65;
				bool L_67;
				il2cpp_codegen_box_unbox((&L_66), (&L_67), sizeof(bool), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_67;
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jclass;
				intptr_t L_69;
				L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
				intptr_t L_70 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_71 = V_0;
				double L_72;
				L_72 = AndroidJNISafe_CallStaticDoubleMethod_m73F1D51601D6849EE480389B4E43AED68C42B2B5(L_69, L_70, L_71, NULL);
				double L_73 = L_72;
				bool L_74;
				il2cpp_codegen_box_unbox((&L_73), (&L_74), sizeof(bool), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_74;
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jclass;
				intptr_t L_76;
				L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
				intptr_t L_77 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_78 = V_0;
				Il2CppChar L_79;
				L_79 = AndroidJNISafe_CallStaticCharMethod_mC4B40190CE095728E823AB8B724ECDC8F4B36155(L_76, L_77, L_78, NULL);
				Il2CppChar L_80 = L_79;
				bool L_81;
				il2cpp_codegen_box_unbox((&L_80), (&L_81), sizeof(bool), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jclass;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStaticStringMethod_m4E150E34CC6DBF27A955F8DAEE5941D6E10879C0(L_83, L_84, L_85, NULL);
				V_3 = ((*(bool*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jclass;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(bool*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(bool));
				bool L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jclass;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(bool*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(bool));
				bool L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jclass;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_113, L_114, L_115, NULL);
				bool L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mA1CB7EC9473B0674763C89AC566EC664159108E9(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(bool));
				bool L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		bool L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81739
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m41529A6A47CC3B57286AC8F9C6388E54918DF450 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mE3914411BE312CBBA869835158E535A06CD3EEB4(L_1, L_2, L_3, (bool)1, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		bool L_7;
		L_7 = AndroidJavaObject__CallStatic_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m9EDC162A865064239ADA4058CD2A81B1A2CF6D0F(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81740
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m4F31B91D2F03C3068A4BE95DB368B6DF715CE316 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	int32_t V_3 = 0;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	int32_t V_5 = 0;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	int32_t G_B30_0 = 0;
	int32_t G_B35_0 = 0;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jclass;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallStaticIntMethod_m915549FA8FD7FB93B57A9708AD759488EA64418C(L_21, L_22, L_23, NULL);
				V_3 = L_24;
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_25 = __this->___m_jclass;
				intptr_t L_26;
				L_26 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_25, NULL);
				intptr_t L_27 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_28 = V_0;
				bool L_29;
				L_29 = AndroidJNISafe_CallStaticBooleanMethod_m652685AC18F590965249C0F9B107C00C142595BB(L_26, L_27, L_28, NULL);
				bool L_30 = L_29;
				int32_t L_31;
				il2cpp_codegen_box_unbox((&L_30), (&L_31), sizeof(int32_t), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_31;
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_32 = __this->___m_jclass;
				intptr_t L_33;
				L_33 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_32, NULL);
				intptr_t L_34 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_35 = V_0;
				int8_t L_36;
				L_36 = AndroidJNISafe_CallStaticSByteMethod_m3E1F75978A2D686BC32DBF5A2F1F70F0D746C2B7(L_33, L_34, L_35, NULL);
				uint8_t L_37 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_36,NULL));
				uint8_t L_38 = L_37;
				int32_t L_39;
				il2cpp_codegen_box_unbox((&L_38), (&L_39), sizeof(int32_t), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_39;
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_40 = __this->___m_jclass;
				intptr_t L_41;
				L_41 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_40, NULL);
				intptr_t L_42 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_43 = V_0;
				int8_t L_44;
				L_44 = AndroidJNISafe_CallStaticSByteMethod_m3E1F75978A2D686BC32DBF5A2F1F70F0D746C2B7(L_41, L_42, L_43, NULL);
				int8_t L_45 = L_44;
				int32_t L_46;
				il2cpp_codegen_box_unbox((&L_45), (&L_46), sizeof(int32_t), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_46;
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_47 = __this->___m_jclass;
				intptr_t L_48;
				L_48 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_47, NULL);
				intptr_t L_49 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_50 = V_0;
				int16_t L_51;
				L_51 = AndroidJNISafe_CallStaticShortMethod_m8330383670ECCD7E24CDD68C419745E486FA6426(L_48, L_49, L_50, NULL);
				int16_t L_52 = L_51;
				int32_t L_53;
				il2cpp_codegen_box_unbox((&L_52), (&L_53), sizeof(int32_t), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_53;
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jclass;
				intptr_t L_55;
				L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
				intptr_t L_56 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_57 = V_0;
				int64_t L_58;
				L_58 = AndroidJNISafe_CallStaticLongMethod_mDDE01239BEFCF007ECE05E51A249B3EB5BB61234(L_55, L_56, L_57, NULL);
				int64_t L_59 = L_58;
				int32_t L_60;
				il2cpp_codegen_box_unbox((&L_59), (&L_60), sizeof(int32_t), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_60;
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_61 = __this->___m_jclass;
				intptr_t L_62;
				L_62 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_61, NULL);
				intptr_t L_63 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_64 = V_0;
				float L_65;
				L_65 = AndroidJNISafe_CallStaticFloatMethod_m3F5419A10B9DF599352938B2BAD8866F8F112364(L_62, L_63, L_64, NULL);
				float L_66 = L_65;
				int32_t L_67;
				il2cpp_codegen_box_unbox((&L_66), (&L_67), sizeof(int32_t), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_67;
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jclass;
				intptr_t L_69;
				L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
				intptr_t L_70 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_71 = V_0;
				double L_72;
				L_72 = AndroidJNISafe_CallStaticDoubleMethod_m73F1D51601D6849EE480389B4E43AED68C42B2B5(L_69, L_70, L_71, NULL);
				double L_73 = L_72;
				int32_t L_74;
				il2cpp_codegen_box_unbox((&L_73), (&L_74), sizeof(int32_t), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_74;
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jclass;
				intptr_t L_76;
				L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
				intptr_t L_77 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_78 = V_0;
				Il2CppChar L_79;
				L_79 = AndroidJNISafe_CallStaticCharMethod_mC4B40190CE095728E823AB8B724ECDC8F4B36155(L_76, L_77, L_78, NULL);
				Il2CppChar L_80 = L_79;
				int32_t L_81;
				il2cpp_codegen_box_unbox((&L_80), (&L_81), sizeof(int32_t), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				V_3 = L_81;
				goto IL_03ea;
			}

IL_0286_1:
			{
				goto IL_02be_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = __this->___m_jclass;
				intptr_t L_83;
				L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
				intptr_t L_84 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_85 = V_0;
				String_t* L_86;
				L_86 = AndroidJNISafe_CallStaticStringMethod_m4E150E34CC6DBF27A955F8DAEE5941D6E10879C0(L_83, L_84, L_85, NULL);
				V_3 = ((*(int32_t*)UnBox(L_86, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_03ea;
			}

IL_02be_1:
			{
				goto IL_0319_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jclass;
				intptr_t L_88;
				L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
				intptr_t L_89 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_90 = V_0;
				intptr_t L_91;
				L_91 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_88, L_89, L_90, NULL);
				V_4 = L_91;
				intptr_t L_92 = V_4;
				bool L_93;
				L_93 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_92, 0, NULL);
				if (L_93)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_94 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_95 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_95, L_94, NULL);
				G_B30_0 = ((*(int32_t*)UnBox(L_95, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int32_t));
				int32_t L_96 = V_5;
				G_B30_0 = L_96;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				goto IL_0371_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_97 = __this->___m_jclass;
				intptr_t L_98;
				L_98 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_97, NULL);
				intptr_t L_99 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_100 = V_0;
				intptr_t L_101;
				L_101 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_98, L_99, L_100, NULL);
				V_6 = L_101;
				intptr_t L_102 = V_6;
				bool L_103;
				L_103 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_102, 0, NULL);
				if (L_103)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_104 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_105 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_105, L_104, NULL);
				G_B35_0 = ((*(int32_t*)UnBox(L_105, il2cpp_rgctx_data(method->rgctx_data, 1))));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int32_t));
				int32_t L_106 = V_5;
				G_B35_0 = L_106;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_107 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_108;
				L_108 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_107, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_110;
				L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_111;
				L_111 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_108, L_110, NULL);
				if (!L_111)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_112 = __this->___m_jclass;
				intptr_t L_113;
				L_113 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_112, NULL);
				intptr_t L_114 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_115 = V_0;
				intptr_t L_116;
				L_116 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_113, L_114, L_115, NULL);
				int32_t L_117;
				L_117 = AndroidJavaObject_FromJavaArray_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mC1390BDE8BF42FAA78215DFE076A25F7AEA674D7(L_116, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_117;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_118 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_119;
				L_119 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_118, NULL);
				Type_t* L_120 = L_119;
				if (L_120)
				{
					G_B40_0 = L_120;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_120;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_121;
				L_121 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_121;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_122;
				L_122 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_123 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_123, L_122, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_123, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(int32_t));
				int32_t L_124 = V_5;
				V_3 = L_124;
				goto IL_03ea;
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

IL_03ea:
	{
		int32_t L_125 = V_3;
		return L_125;
	}
}
// Method Definition Index: 81739
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mEE64E87A5A2BC0F38910643A9B91032AF38D882C (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mF87CC238DDBB3C90C35846E626548847CC7E319D(L_1, L_2, L_3, (bool)1, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		int32_t L_7;
		L_7 = AndroidJavaObject__CallStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m4F31B91D2F03C3068A4BE95DB368B6DF715CE316(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81740
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_mF9D318147E29742DAA4B5C033BFC68D29CC1123B_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	Il2CppSharedGenericObject* V_3 = NULL;
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	Il2CppSharedGenericObject* V_5 = NULL;
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	Il2CppSharedGenericObject* G_B30_0 = NULL;
	Il2CppSharedGenericObject* G_B35_0 = NULL;
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				goto IL_0090_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_20 = __this->___m_jclass;
				intptr_t L_21;
				L_21 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_20, NULL);
				intptr_t L_22 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_23 = V_0;
				int32_t L_24;
				L_24 = AndroidJNISafe_CallStaticIntMethod_m915549FA8FD7FB93B57A9708AD759488EA64418C(L_21, L_22, L_23, NULL);
				int32_t L_25 = L_24;
				RuntimeObject* L_26 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_25);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_26, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0090_1:
			{
				goto IL_00cd_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_27 = __this->___m_jclass;
				intptr_t L_28;
				L_28 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_27, NULL);
				intptr_t L_29 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_30 = V_0;
				bool L_31;
				L_31 = AndroidJNISafe_CallStaticBooleanMethod_m652685AC18F590965249C0F9B107C00C142595BB(L_28, L_29, L_30, NULL);
				bool L_32 = L_31;
				RuntimeObject* L_33 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_32);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_33, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_00cd_1:
			{
				goto IL_0115_1;
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_34 = __this->___m_jclass;
				intptr_t L_35;
				L_35 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_34, NULL);
				intptr_t L_36 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_37 = V_0;
				int8_t L_38;
				L_38 = AndroidJNISafe_CallStaticSByteMethod_m3E1F75978A2D686BC32DBF5A2F1F70F0D746C2B7(L_35, L_36, L_37, NULL);
				uint8_t L_39 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_38,NULL));
				uint8_t L_40 = L_39;
				RuntimeObject* L_41 = Box(Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, &L_40);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_41, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0115_1:
			{
				goto IL_0152_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_42 = __this->___m_jclass;
				intptr_t L_43;
				L_43 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_42, NULL);
				intptr_t L_44 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_45 = V_0;
				int8_t L_46;
				L_46 = AndroidJNISafe_CallStaticSByteMethod_m3E1F75978A2D686BC32DBF5A2F1F70F0D746C2B7(L_43, L_44, L_45, NULL);
				int8_t L_47 = L_46;
				RuntimeObject* L_48 = Box(SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, &L_47);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_48, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0152_1:
			{
				goto IL_018f_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_49 = __this->___m_jclass;
				intptr_t L_50;
				L_50 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_49, NULL);
				intptr_t L_51 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_52 = V_0;
				int16_t L_53;
				L_53 = AndroidJNISafe_CallStaticShortMethod_m8330383670ECCD7E24CDD68C419745E486FA6426(L_50, L_51, L_52, NULL);
				int16_t L_54 = L_53;
				RuntimeObject* L_55 = Box(Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, &L_54);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_55, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_018f_1:
			{
				goto IL_01cc_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_56 = __this->___m_jclass;
				intptr_t L_57;
				L_57 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_56, NULL);
				intptr_t L_58 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_59 = V_0;
				int64_t L_60;
				L_60 = AndroidJNISafe_CallStaticLongMethod_mDDE01239BEFCF007ECE05E51A249B3EB5BB61234(L_57, L_58, L_59, NULL);
				int64_t L_61 = L_60;
				RuntimeObject* L_62 = Box(Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, &L_61);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_62, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_01cc_1:
			{
				goto IL_0209_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_63 = __this->___m_jclass;
				intptr_t L_64;
				L_64 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_63, NULL);
				intptr_t L_65 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_66 = V_0;
				float L_67;
				L_67 = AndroidJNISafe_CallStaticFloatMethod_m3F5419A10B9DF599352938B2BAD8866F8F112364(L_64, L_65, L_66, NULL);
				float L_68 = L_67;
				RuntimeObject* L_69 = Box(Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, &L_68);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_69, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0209_1:
			{
				goto IL_0246_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_70 = __this->___m_jclass;
				intptr_t L_71;
				L_71 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_70, NULL);
				intptr_t L_72 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_73 = V_0;
				double L_74;
				L_74 = AndroidJNISafe_CallStaticDoubleMethod_m73F1D51601D6849EE480389B4E43AED68C42B2B5(L_71, L_72, L_73, NULL);
				double L_75 = L_74;
				RuntimeObject* L_76 = Box(Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, &L_75);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_76, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0246_1:
			{
				goto IL_03d1_1;
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_77 = __this->___m_jclass;
				intptr_t L_78;
				L_78 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_77, NULL);
				intptr_t L_79 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_80 = V_0;
				Il2CppChar L_81;
				L_81 = AndroidJNISafe_CallStaticCharMethod_mC4B40190CE095728E823AB8B724ECDC8F4B36155(L_78, L_79, L_80, NULL);
				Il2CppChar L_82 = L_81;
				RuntimeObject* L_83 = Box(Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, &L_82);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_83, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_0286_1:
			{
				bool L_84 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_84)
				{
					goto IL_02be_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_85 = __this->___m_jclass;
				intptr_t L_86;
				L_86 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_85, NULL);
				intptr_t L_87 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_88 = V_0;
				String_t* L_89;
				L_89 = AndroidJNISafe_CallStaticStringMethod_m4E150E34CC6DBF27A955F8DAEE5941D6E10879C0(L_86, L_87, L_88, NULL);
				V_3 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_89, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_03ea;
			}

IL_02be_1:
			{
				bool L_90 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_90)
				{
					goto IL_0319_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_91 = __this->___m_jclass;
				intptr_t L_92;
				L_92 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_91, NULL);
				intptr_t L_93 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_94 = V_0;
				intptr_t L_95;
				L_95 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_92, L_93, L_94, NULL);
				V_4 = L_95;
				intptr_t L_96 = V_4;
				bool L_97;
				L_97 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_96, 0, NULL);
				if (L_97)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_98 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_99 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_99, L_98, NULL);
				G_B30_0 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_99, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(Il2CppSharedGenericObject*));
				Il2CppSharedGenericObject* L_100 = V_5;
				G_B30_0 = L_100;
			}

IL_0313_1:
			{
				V_3 = G_B30_0;
				goto IL_03ea;
			}

IL_0319_1:
			{
				bool L_101 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_101)
				{
					goto IL_0371_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_102 = __this->___m_jclass;
				intptr_t L_103;
				L_103 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_102, NULL);
				intptr_t L_104 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_105 = V_0;
				intptr_t L_106;
				L_106 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_103, L_104, L_105, NULL);
				V_6 = L_106;
				intptr_t L_107 = V_6;
				bool L_108;
				L_108 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_107, 0, NULL);
				if (L_108)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_109 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_110 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_110, L_109, NULL);
				G_B35_0 = ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_110, il2cpp_rgctx_data(method->rgctx_data, 1)));
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(Il2CppSharedGenericObject*));
				Il2CppSharedGenericObject* L_111 = V_5;
				G_B35_0 = L_111;
			}

IL_036e_1:
			{
				V_3 = G_B35_0;
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_112 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_113;
				L_113 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_112, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_114 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_115;
				L_115 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_114, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_116;
				L_116 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_113, L_115, NULL);
				if (!L_116)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_117 = __this->___m_jclass;
				intptr_t L_118;
				L_118 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_117, NULL);
				intptr_t L_119 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_120 = V_0;
				intptr_t L_121;
				L_121 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_118, L_119, L_120, NULL);
				Il2CppSharedGenericObject* L_122;
				L_122 = AndroidJavaObject_FromJavaArray_TisIl2CppSharedGenericObject_m22D9898503E929D77563392822CD11A056613AFF(L_121, il2cpp_rgctx_method(method->rgctx_data, 2));
				V_3 = L_122;
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_123 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_124;
				L_124 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_123, NULL);
				Type_t* L_125 = L_124;
				if (L_125)
				{
					G_B40_0 = L_125;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_125;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_126;
				L_126 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_126;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_127;
				L_127 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_128 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_128, L_127, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_128, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((&V_5), sizeof(Il2CppSharedGenericObject*));
				Il2CppSharedGenericObject* L_129 = V_5;
				V_3 = L_129;
				goto IL_03ea;
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

IL_03ea:
	{
		Il2CppSharedGenericObject* L_130 = V_3;
		return L_130;
	}
}
// Method Definition Index: 81739
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_m8E1513A9554AAA1822CA51D0A816DCF16775AA37_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = AndroidJNIHelper_GetMethodID_TisIl2CppSharedGenericObject_m95122CB55DFAF463B14CBBA51C2B0811790DB90B(L_1, L_2, L_3, (bool)1, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		Il2CppSharedGenericObject* L_7;
		L_7 = AndroidJavaObject__CallStatic_TisIl2CppSharedGenericObject_mF9D318147E29742DAA4B5C033BFC68D29CC1123B(__this, L_5, L_6, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_7;
	}
}
// Method Definition Index: 81740
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__CallStatic_TisIl2CppFullySharedGenericAny_m8D1EB1F6B7C8D0B7D54EF55F42BCE6B7576E0953_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_methodID, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	const Il2CppFullySharedGenericAny L_27 = alloca(SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
	const Il2CppFullySharedGenericAny L_35 = L_27;
	const Il2CppFullySharedGenericAny L_44 = L_27;
	const Il2CppFullySharedGenericAny L_52 = L_27;
	const Il2CppFullySharedGenericAny L_60 = L_27;
	const Il2CppFullySharedGenericAny L_68 = L_27;
	const Il2CppFullySharedGenericAny L_76 = L_27;
	const Il2CppFullySharedGenericAny L_84 = L_27;
	const Il2CppFullySharedGenericAny L_92 = L_27;
	const Il2CppFullySharedGenericAny L_99 = L_27;
	const Il2CppFullySharedGenericAny L_111 = L_27;
	const Il2CppFullySharedGenericAny L_113 = L_27;
	const Il2CppFullySharedGenericAny L_124 = L_27;
	const Il2CppFullySharedGenericAny L_126 = L_27;
	const Il2CppFullySharedGenericAny L_137 = L_27;
	const Il2CppFullySharedGenericAny L_144 = L_27;
	const Il2CppFullySharedGenericAny L_145 = L_27;
	//<source_info:<no-source>:1>
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 V_1;
	memset((&V_1), 0, sizeof(V_1));
	int32_t V_2 = 0;
	Il2CppFullySharedGenericAny V_3 = alloca(SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
	memset(V_3, 0, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
	intptr_t V_4;
	memset((&V_4), 0, sizeof(V_4));
	Il2CppFullySharedGenericAny V_5 = alloca(SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
	memset(V_5, 0, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
	intptr_t V_6;
	memset((&V_6), 0, sizeof(V_6));
	Il2CppFullySharedGenericAny G_B30_0 = alloca(SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
	memset(G_B30_0, 0, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
	Il2CppFullySharedGenericAny G_B35_0 = alloca(SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
	memset(G_B35_0, 0, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_0 = ___1_args;
		if (!L_0)
		{
			goto IL_0007;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = ___1_args;
		NullCheck(L_1);
		if ((((RuntimeArray*)L_1)->max_length))
		{
			goto IL_0011;
		}
	}

IL_0007:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7));
		goto IL_0027;
	}

IL_0011:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_2 = ___1_args;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		V_2 = L_3;
		int32_t L_4 = V_2;
		uintptr_t L_5 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(L_4,NULL));
		uint32_t L_6 = sizeof(jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225);
		if ((uintptr_t)L_5 * (uintptr_t)L_6 > (uintptr_t)kIl2CppUIntPtrMax)
			IL2CPP_RAISE_MANAGED_EXCEPTION(il2cpp_codegen_get_overflow_exception(), method);
		intptr_t L_7 = ((intptr_t)il2cpp_codegen_multiply((intptr_t)L_5, (int32_t)L_6));
		int8_t* L_8;
		if (L_7 == 0)
		{
			L_8 = NULL;
		}
		else
		{
			L_8 = (int8_t*)alloca(L_7);
			memset(L_8, 0, L_7);
		}
		int32_t L_9 = V_2;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline((&L_10), (void*)L_8, L_9, NULL);
		V_1 = L_10;
	}

IL_0027:
	{
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_11 = V_1;
		V_0 = L_11;
		int32_t L_12;
		L_12 = Span_1_get_Length_m9C7E8DECAA7368617C319A866C6A9E960F140BF7_inline((&V_0), NULL);
		int32_t L_13;
		L_13 = AndroidJNI_PushLocalFrame_m2D8050A3799AEBB4A7E506E6790839EB66932E10(((int32_t)il2cpp_codegen_add(L_12, 1)), NULL);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = ___1_args;
		Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_15 = V_0;
		AndroidJNIHelper_CreateJNIArgArray_mD8E0CA2404E31F155EDE1A028EC686C17B17730F(L_14, L_15, NULL);
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_03de:
			{
				intptr_t L_16;
				L_16 = AndroidJNI_PopLocalFrame_m32AF6F9065F09D80BFDD3F573B62C782F392E609(0, NULL);
				return;
			}
		});
		try
		{
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_18;
				L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_19;
				L_19 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_18, NULL);
				if (!L_19)
				{
					goto IL_0286_1;
				}
			}
			{
				bool L_20 = (Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_20)
				{
					goto IL_0090_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_21 = __this->___m_jclass;
				intptr_t L_22;
				L_22 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_21, NULL);
				intptr_t L_23 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_24 = V_0;
				int32_t L_25;
				L_25 = AndroidJNISafe_CallStaticIntMethod_m915549FA8FD7FB93B57A9708AD759488EA64418C(L_22, L_23, L_24, NULL);
				int32_t L_26 = L_25;
				il2cpp_codegen_box_unbox((&L_26), L_27, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B, Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_27, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_0090_1:
			{
				bool L_28 = (Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_28)
				{
					goto IL_00cd_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_29 = __this->___m_jclass;
				intptr_t L_30;
				L_30 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_29, NULL);
				intptr_t L_31 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_32 = V_0;
				bool L_33;
				L_33 = AndroidJNISafe_CallStaticBooleanMethod_m652685AC18F590965249C0F9B107C00C142595BB(L_30, L_31, L_32, NULL);
				bool L_34 = L_33;
				il2cpp_codegen_box_unbox((&L_34), L_35, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B, Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_35, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_00cd_1:
			{
				bool L_36 = (Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_36)
				{
					goto IL_0115_1;
				}
			}
			{
				CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteralBBA88D18E0EF50828EC393CF2F0ADD9E557A1A9E, NULL);
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_37 = __this->___m_jclass;
				intptr_t L_38;
				L_38 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_37, NULL);
				intptr_t L_39 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_40 = V_0;
				int8_t L_41;
				L_41 = AndroidJNISafe_CallStaticSByteMethod_m3E1F75978A2D686BC32DBF5A2F1F70F0D746C2B7(L_38, L_39, L_40, NULL);
				uint8_t L_42 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_41,NULL));
				uint8_t L_43 = L_42;
				il2cpp_codegen_box_unbox((&L_43), L_44, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_44, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_0115_1:
			{
				bool L_45 = (SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_45)
				{
					goto IL_0152_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_46 = __this->___m_jclass;
				intptr_t L_47;
				L_47 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_46, NULL);
				intptr_t L_48 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_49 = V_0;
				int8_t L_50;
				L_50 = AndroidJNISafe_CallStaticSByteMethod_m3E1F75978A2D686BC32DBF5A2F1F70F0D746C2B7(L_47, L_48, L_49, NULL);
				int8_t L_51 = L_50;
				il2cpp_codegen_box_unbox((&L_51), L_52, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B, SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_52, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_0152_1:
			{
				bool L_53 = (Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_53)
				{
					goto IL_018f_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jclass;
				intptr_t L_55;
				L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
				intptr_t L_56 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_57 = V_0;
				int16_t L_58;
				L_58 = AndroidJNISafe_CallStaticShortMethod_m8330383670ECCD7E24CDD68C419745E486FA6426(L_55, L_56, L_57, NULL);
				int16_t L_59 = L_58;
				il2cpp_codegen_box_unbox((&L_59), L_60, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B, Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_60, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_018f_1:
			{
				bool L_61 = (Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_61)
				{
					goto IL_01cc_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_62 = __this->___m_jclass;
				intptr_t L_63;
				L_63 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_62, NULL);
				intptr_t L_64 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_65 = V_0;
				int64_t L_66;
				L_66 = AndroidJNISafe_CallStaticLongMethod_mDDE01239BEFCF007ECE05E51A249B3EB5BB61234(L_63, L_64, L_65, NULL);
				int64_t L_67 = L_66;
				il2cpp_codegen_box_unbox((&L_67), L_68, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B, Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_68, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_01cc_1:
			{
				bool L_69 = (Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_69)
				{
					goto IL_0209_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_70 = __this->___m_jclass;
				intptr_t L_71;
				L_71 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_70, NULL);
				intptr_t L_72 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_73 = V_0;
				float L_74;
				L_74 = AndroidJNISafe_CallStaticFloatMethod_m3F5419A10B9DF599352938B2BAD8866F8F112364(L_71, L_72, L_73, NULL);
				float L_75 = L_74;
				il2cpp_codegen_box_unbox((&L_75), L_76, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B, Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_76, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_0209_1:
			{
				bool L_77 = (Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_77)
				{
					goto IL_0246_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_78 = __this->___m_jclass;
				intptr_t L_79;
				L_79 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_78, NULL);
				intptr_t L_80 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_81 = V_0;
				double L_82;
				L_82 = AndroidJNISafe_CallStaticDoubleMethod_m73F1D51601D6849EE480389B4E43AED68C42B2B5(L_79, L_80, L_81, NULL);
				double L_83 = L_82;
				il2cpp_codegen_box_unbox((&L_83), L_84, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B, Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_84, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_0246_1:
			{
				bool L_85 = (Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_85)
				{
					goto IL_03d1_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_86 = __this->___m_jclass;
				intptr_t L_87;
				L_87 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_86, NULL);
				intptr_t L_88 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_89 = V_0;
				Il2CppChar L_90;
				L_90 = AndroidJNISafe_CallStaticCharMethod_mC4B40190CE095728E823AB8B724ECDC8F4B36155(L_87, L_88, L_89, NULL);
				Il2CppChar L_91 = L_90;
				il2cpp_codegen_box_unbox((&L_91), L_92, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B, Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
				il2cpp_codegen_memcpy(V_3, L_92, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_0286_1:
			{
				bool L_93 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_93)
				{
					goto IL_02be_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_94 = __this->___m_jclass;
				intptr_t L_95;
				L_95 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_94, NULL);
				intptr_t L_96 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_97 = V_0;
				String_t* L_98;
				L_98 = AndroidJNISafe_CallStaticStringMethod_m4E150E34CC6DBF27A955F8DAEE5941D6E10879C0(L_95, L_96, L_97, NULL);
				void* L_100 = UnBox_Any(L_98, il2cpp_rgctx_data(method->rgctx_data, 1), L_99);
				il2cpp_codegen_memcpy(V_3, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_100)), SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_02be_1:
			{
				bool L_101 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_101)
				{
					goto IL_0319_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_102 = __this->___m_jclass;
				intptr_t L_103;
				L_103 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_102, NULL);
				intptr_t L_104 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_105 = V_0;
				intptr_t L_106;
				L_106 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_103, L_104, L_105, NULL);
				V_4 = L_106;
				intptr_t L_107 = V_4;
				bool L_108;
				L_108 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_107, 0, NULL);
				if (L_108)
				{
					goto IL_0309_1;
				}
			}
			{
				intptr_t L_109 = V_4;
				AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_110 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)il2cpp_codegen_object_new(AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
				AndroidJavaClass__ctor_mB206D3CB990755BD56E308F61CD43BB9EA4421D0(L_110, L_109, NULL);
				void* L_112 = UnBox_Any(L_110, il2cpp_rgctx_data(method->rgctx_data, 1), L_111);
				il2cpp_codegen_memcpy(G_B30_0, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_112)), SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_0313_1;
			}

IL_0309_1:
			{
				il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_5, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				il2cpp_codegen_memcpy(L_113, V_5, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				il2cpp_codegen_memcpy(G_B30_0, L_113, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
			}

IL_0313_1:
			{
				il2cpp_codegen_memcpy(V_3, G_B30_0, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_0319_1:
			{
				bool L_114 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
				if (!L_114)
				{
					goto IL_0371_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_115 = __this->___m_jclass;
				intptr_t L_116;
				L_116 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_115, NULL);
				intptr_t L_117 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_118 = V_0;
				intptr_t L_119;
				L_119 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_116, L_117, L_118, NULL);
				V_6 = L_119;
				intptr_t L_120 = V_6;
				bool L_121;
				L_121 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_120, 0, NULL);
				if (L_121)
				{
					goto IL_0364_1;
				}
			}
			{
				intptr_t L_122 = V_6;
				AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_123 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)il2cpp_codegen_object_new(AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
				AndroidJavaObject__ctor_m0CEE7D570807333CE2C193A82AB3AB8D4F873A6B(L_123, L_122, NULL);
				void* L_125 = UnBox_Any(L_123, il2cpp_rgctx_data(method->rgctx_data, 1), L_124);
				il2cpp_codegen_memcpy(G_B35_0, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_125)), SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_036e_1;
			}

IL_0364_1:
			{
				il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_5, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				il2cpp_codegen_memcpy(L_126, V_5, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				il2cpp_codegen_memcpy(G_B35_0, L_126, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
			}

IL_036e_1:
			{
				il2cpp_codegen_memcpy(V_3, G_B35_0, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_0371_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_127 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_128;
				L_128 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_127, NULL);
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_129 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				Type_t* L_130;
				L_130 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_129, NULL);
				CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				bool L_131;
				L_131 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_128, L_130, NULL);
				if (!L_131)
				{
					goto IL_03a6_1;
				}
			}
			{
				GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_132 = __this->___m_jclass;
				intptr_t L_133;
				L_133 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_132, NULL);
				intptr_t L_134 = ___0_methodID;
				Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7 L_135 = V_0;
				intptr_t L_136;
				L_136 = AndroidJNISafe_CallStaticObjectMethod_m3171BFAEF780EEF400AD592B6F040E7BE87C2387(L_133, L_134, L_135, NULL);
				InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 2)), il2cpp_rgctx_method(method->rgctx_data, 2), NULL, L_136, (Il2CppFullySharedGenericAny*)L_137);
				il2cpp_codegen_memcpy(V_3, L_137, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
			}

IL_03a6_1:
			{
				RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_138 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
				CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
				Type_t* L_139;
				L_139 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_138, NULL);
				Type_t* L_140 = L_139;
				if (L_140)
				{
					G_B40_0 = L_140;
					G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
					goto IL_03bc_1;
				}
				G_B39_0 = L_140;
				G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralD89F6478390C0E72E54E8EB2729A4BBC7009F43D));
			}
			{
				G_B41_0 = ((String_t*)(NULL));
				G_B41_1 = G_B39_1;
				goto IL_03c1_1;
			}

IL_03bc_1:
			{
				NullCheck(G_B40_0);
				String_t* L_141;
				L_141 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
				G_B41_0 = L_141;
				G_B41_1 = G_B40_1;
			}

IL_03c1_1:
			{
				String_t* L_142;
				L_142 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
				Exception_t* L_143 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
				Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_143, L_142, NULL);
				IL2CPP_RAISE_MANAGED_EXCEPTION(L_143, method);
			}

IL_03d1_1:
			{
				il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_5, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				il2cpp_codegen_memcpy(L_144, V_5, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				il2cpp_codegen_memcpy(V_3, L_144, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
				goto IL_03ea;
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

IL_03ea:
	{
		il2cpp_codegen_memcpy(L_145, V_3, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
		il2cpp_codegen_memcpy(il2cppRetVal, L_145, SizeOf_ReturnType_tB840B385B71FF8E350FF799B5A7DF58E03C1A69B);
		return;
	}
}
// Method Definition Index: 81739
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__CallStatic_TisIl2CppFullySharedGenericAny_mA069934E49E8EFEB980CA5EED2B1DE810C150A68_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_methodName, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_ReturnType_t2DAB0875DF34A21B532F695F9B7329A0B5BBCB27 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 2));
	const Il2CppFullySharedGenericAny L_7 = alloca(SizeOf_ReturnType_t2DAB0875DF34A21B532F695F9B7329A0B5BBCB27);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_methodName;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = ___1_args;
		intptr_t L_4;
		L_4 = ((  intptr_t (*) (intptr_t, String_t*, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, bool, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))(L_1, L_2, L_3, (bool)1, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_4;
		intptr_t L_5 = V_0;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = ___1_args;
		InvokerActionInvoker3< intptr_t, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), __this, L_5, L_6, (Il2CppFullySharedGenericAny*)L_7);
		il2cpp_codegen_memcpy(il2cppRetVal, L_7, SizeOf_ReturnType_t2DAB0875DF34A21B532F695F9B7329A0B5BBCB27);
		return;
	}
}
// Method Definition Index: 81734
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Get_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mBEAFA1EB54999ED8C620F9B4F0E2D3CA2530CECE (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	bool V_1 = false;
	intptr_t V_2;
	memset((&V_2), 0, sizeof(V_2));
	Type_t* G_B33_0 = NULL;
	String_t* G_B33_1 = NULL;
	Type_t* G_B32_0 = NULL;
	String_t* G_B32_1 = NULL;
	String_t* G_B34_0 = NULL;
	String_t* G_B34_1 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_2;
		L_2 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_1, NULL);
		if (!L_2)
		{
			goto IL_0211;
		}
	}
	{
		goto IL_004b;
	}

IL_004b:
	{
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_3 = __this->___m_jobject;
		intptr_t L_4;
		L_4 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_3, NULL);
		intptr_t L_5 = ___0_fieldID;
		bool L_6;
		L_6 = AndroidJNISafe_GetBooleanField_m34F37B560A6AEC81B9061FB3B72698C84720435D(L_4, L_5, NULL);
		return L_6;
	}

IL_0211:
	{
		goto IL_0243;
	}

IL_0243:
	{
		goto IL_0293;
	}

IL_0293:
	{
		goto IL_02e3;
	}

IL_02e3:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_7 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_8;
		L_8 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_7, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_9 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_10;
		L_10 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_9, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_11;
		L_11 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_8, L_10, NULL);
		if (!L_11)
		{
			goto IL_0315;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_12 = __this->___m_jobject;
		intptr_t L_13;
		L_13 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_12, NULL);
		intptr_t L_14 = ___0_fieldID;
		intptr_t L_15;
		L_15 = AndroidJNISafe_GetObjectField_mCF3BB1C38718D6F55081126BC7F6C286B382B275(L_13, L_14, NULL);
		bool L_16;
		L_16 = AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mC408C8B80896737BA7DE6C18A904D2157E5FD364(L_15, il2cpp_rgctx_method(method->rgctx_data, 2));
		return L_16;
	}

IL_0315:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_18;
		L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
		Type_t* L_19 = L_18;
		if (L_19)
		{
			G_B33_0 = L_19;
			G_B33_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
			goto IL_032b;
		}
		G_B32_0 = L_19;
		G_B32_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
	}
	{
		G_B34_0 = ((String_t*)(NULL));
		G_B34_1 = G_B32_1;
		goto IL_0330;
	}

IL_032b:
	{
		NullCheck(G_B33_0);
		String_t* L_20;
		L_20 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B33_0);
		G_B34_0 = L_20;
		G_B34_1 = G_B33_1;
	}

IL_0330:
	{
		String_t* L_21;
		L_21 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B34_1, G_B34_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		Exception_t* L_22 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_22, L_21, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_22, method);
	}
	IL2CPP_UNREACHABLE;
	bool ret = false;
	return ret;
}
// Method Definition Index: 81733
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AndroidJavaObject__Get_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m68F3AF483142B771BE6BCD51B30F7CED871DB766 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_fieldName;
		intptr_t L_3;
		L_3 = AndroidJNIHelper_GetFieldID_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6F960B6375B61125FD05A03EBE8CEC790A986044(L_1, L_2, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_3;
		intptr_t L_4 = V_0;
		bool L_5;
		L_5 = AndroidJavaObject__Get_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mBEAFA1EB54999ED8C620F9B4F0E2D3CA2530CECE(__this, L_4, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_5;
	}
}
// Method Definition Index: 81734
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Get_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mB506BB319D4A193AE597F3C3166DDB165593D4D6 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	int32_t V_1 = 0;
	intptr_t V_2;
	memset((&V_2), 0, sizeof(V_2));
	Type_t* G_B33_0 = NULL;
	String_t* G_B33_1 = NULL;
	Type_t* G_B32_0 = NULL;
	String_t* G_B32_1 = NULL;
	String_t* G_B34_0 = NULL;
	String_t* G_B34_1 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_2;
		L_2 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_1, NULL);
		if (!L_2)
		{
			goto IL_0211;
		}
	}
	{
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_3 = __this->___m_jobject;
		intptr_t L_4;
		L_4 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_3, NULL);
		intptr_t L_5 = ___0_fieldID;
		int32_t L_6;
		L_6 = AndroidJNISafe_GetIntField_mBD983688B73063DE5C55D320F60F266443FAC97C(L_4, L_5, NULL);
		return L_6;
	}

IL_0211:
	{
		goto IL_0243;
	}

IL_0243:
	{
		goto IL_0293;
	}

IL_0293:
	{
		goto IL_02e3;
	}

IL_02e3:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_7 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_8;
		L_8 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_7, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_9 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_10;
		L_10 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_9, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_11;
		L_11 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_8, L_10, NULL);
		if (!L_11)
		{
			goto IL_0315;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_12 = __this->___m_jobject;
		intptr_t L_13;
		L_13 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_12, NULL);
		intptr_t L_14 = ___0_fieldID;
		intptr_t L_15;
		L_15 = AndroidJNISafe_GetObjectField_mCF3BB1C38718D6F55081126BC7F6C286B382B275(L_13, L_14, NULL);
		int32_t L_16;
		L_16 = AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m61DD76EF7E812BC3ED41BBA0D0DA1B430EC61256(L_15, il2cpp_rgctx_method(method->rgctx_data, 2));
		return L_16;
	}

IL_0315:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_18;
		L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
		Type_t* L_19 = L_18;
		if (L_19)
		{
			G_B33_0 = L_19;
			G_B33_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
			goto IL_032b;
		}
		G_B32_0 = L_19;
		G_B32_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
	}
	{
		G_B34_0 = ((String_t*)(NULL));
		G_B34_1 = G_B32_1;
		goto IL_0330;
	}

IL_032b:
	{
		NullCheck(G_B33_0);
		String_t* L_20;
		L_20 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B33_0);
		G_B34_0 = L_20;
		G_B34_1 = G_B33_1;
	}

IL_0330:
	{
		String_t* L_21;
		L_21 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B34_1, G_B34_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		Exception_t* L_22 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_22, L_21, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_22, method);
	}
	IL2CPP_UNREACHABLE;
	int32_t ret = 0;
	return ret;
}
// Method Definition Index: 81733
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__Get_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDED207A2CABEE9D78BEC022F1D4338A5091DCC50 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_fieldName;
		intptr_t L_3;
		L_3 = AndroidJNIHelper_GetFieldID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1B394DCE5ED40DFA09EB18874BA2FFC3B5E50B2E(L_1, L_2, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_3;
		intptr_t L_4 = V_0;
		int32_t L_5;
		L_5 = AndroidJavaObject__Get_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mB506BB319D4A193AE597F3C3166DDB165593D4D6(__this, L_4, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_5;
	}
}
// Method Definition Index: 81734
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__Get_TisIl2CppSharedGenericObject_mD7317316E4D5BBE57FE9495B6E117EEFFA6F149E_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	Il2CppSharedGenericObject* V_1 = NULL;
	intptr_t V_2;
	memset((&V_2), 0, sizeof(V_2));
	Type_t* G_B33_0 = NULL;
	String_t* G_B33_1 = NULL;
	Type_t* G_B32_0 = NULL;
	String_t* G_B32_1 = NULL;
	String_t* G_B34_0 = NULL;
	String_t* G_B34_1 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_2;
		L_2 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_1, NULL);
		if (!L_2)
		{
			goto IL_0211;
		}
	}
	{
		goto IL_004b;
	}

IL_004b:
	{
		goto IL_0082;
	}

IL_0082:
	{
		goto IL_00c4;
	}

IL_00c4:
	{
		goto IL_00fb;
	}

IL_00fb:
	{
		goto IL_0132;
	}

IL_0132:
	{
		goto IL_0169;
	}

IL_0169:
	{
		goto IL_01a0;
	}

IL_01a0:
	{
		goto IL_01d7;
	}

IL_01d7:
	{
		goto IL_0340;
	}

IL_0211:
	{
		bool L_3 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_3)
		{
			goto IL_0243;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_4 = __this->___m_jobject;
		intptr_t L_5;
		L_5 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_4, NULL);
		intptr_t L_6 = ___0_fieldID;
		String_t* L_7;
		L_7 = AndroidJNISafe_GetStringField_mADFCA05D6DE790600B57E90B20F2E75AFC036B0F(L_5, L_6, NULL);
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_7, il2cpp_rgctx_data(method->rgctx_data, 1)));
	}

IL_0243:
	{
		bool L_8 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_8)
		{
			goto IL_0293;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_9 = __this->___m_jobject;
		intptr_t L_10;
		L_10 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_9, NULL);
		intptr_t L_11 = ___0_fieldID;
		intptr_t L_12;
		L_12 = AndroidJNISafe_GetObjectField_mCF3BB1C38718D6F55081126BC7F6C286B382B275(L_10, L_11, NULL);
		V_0 = L_12;
		intptr_t L_13 = V_0;
		bool L_14;
		L_14 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_13, 0, NULL);
		if (L_14)
		{
			goto IL_0289;
		}
	}
	{
		intptr_t L_15 = V_0;
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_16;
		L_16 = AndroidJavaObject_AndroidJavaClassDeleteLocalRef_m56C84D7516BCB51A84E8AFDB3FCA46BAF494548F(L_15, NULL);
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_16, il2cpp_rgctx_data(method->rgctx_data, 1)));
	}

IL_0289:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_17 = V_1;
		return L_17;
	}

IL_0293:
	{
		bool L_18 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_18)
		{
			goto IL_02e3;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_19 = __this->___m_jobject;
		intptr_t L_20;
		L_20 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_19, NULL);
		intptr_t L_21 = ___0_fieldID;
		intptr_t L_22;
		L_22 = AndroidJNISafe_GetObjectField_mCF3BB1C38718D6F55081126BC7F6C286B382B275(L_20, L_21, NULL);
		V_2 = L_22;
		intptr_t L_23 = V_2;
		bool L_24;
		L_24 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_23, 0, NULL);
		if (L_24)
		{
			goto IL_02d9;
		}
	}
	{
		intptr_t L_25 = V_2;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_26;
		L_26 = AndroidJavaObject_AndroidJavaObjectDeleteLocalRef_m2ECEEAF6389ABB9D6B963B8A98568ECD9413DF3C(L_25, NULL);
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_26, il2cpp_rgctx_data(method->rgctx_data, 1)));
	}

IL_02d9:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_27 = V_1;
		return L_27;
	}

IL_02e3:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_28 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_29;
		L_29 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_28, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_30 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_31;
		L_31 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_30, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_32;
		L_32 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_29, L_31, NULL);
		if (!L_32)
		{
			goto IL_0315;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_33 = __this->___m_jobject;
		intptr_t L_34;
		L_34 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_33, NULL);
		intptr_t L_35 = ___0_fieldID;
		intptr_t L_36;
		L_36 = AndroidJNISafe_GetObjectField_mCF3BB1C38718D6F55081126BC7F6C286B382B275(L_34, L_35, NULL);
		Il2CppSharedGenericObject* L_37;
		L_37 = AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisIl2CppSharedGenericObject_mB1CEFE624E92B82D234C1982AA2A7DF6AC5FD284(L_36, il2cpp_rgctx_method(method->rgctx_data, 2));
		return L_37;
	}

IL_0315:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_38 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_39;
		L_39 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_38, NULL);
		Type_t* L_40 = L_39;
		if (L_40)
		{
			G_B33_0 = L_40;
			G_B33_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
			goto IL_032b;
		}
		G_B32_0 = L_40;
		G_B32_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
	}
	{
		G_B34_0 = ((String_t*)(NULL));
		G_B34_1 = G_B32_1;
		goto IL_0330;
	}

IL_032b:
	{
		NullCheck(G_B33_0);
		String_t* L_41;
		L_41 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B33_0);
		G_B34_0 = L_41;
		G_B34_1 = G_B33_1;
	}

IL_0330:
	{
		String_t* L_42;
		L_42 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B34_1, G_B34_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		Exception_t* L_43 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_43, L_42, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_43, method);
	}

IL_0340:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_44 = V_1;
		return L_44;
	}
}
// Method Definition Index: 81733
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__Get_TisIl2CppSharedGenericObject_m3FBD43AC7986C7B2DAE4F575360C5B6A7DBDE4AB_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_fieldName;
		intptr_t L_3;
		L_3 = AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mDFF491584A820BE0629A47ED3B790A199050D589(L_1, L_2, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_3;
		intptr_t L_4 = V_0;
		Il2CppSharedGenericObject* L_5;
		L_5 = AndroidJavaObject__Get_TisIl2CppSharedGenericObject_mD7317316E4D5BBE57FE9495B6E117EEFFA6F149E(__this, L_4, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_5;
	}
}
// Method Definition Index: 81734
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__Get_TisIl2CppFullySharedGenericAny_m16AE597DA478C33ABF978BB8131126B5C7360D09_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral50E02893F5B6939534A49C535241D6B7BAFCF599);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	const Il2CppFullySharedGenericAny L_9 = alloca(SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
	const Il2CppFullySharedGenericAny L_16 = L_9;
	const Il2CppFullySharedGenericAny L_24 = L_9;
	const Il2CppFullySharedGenericAny L_31 = L_9;
	const Il2CppFullySharedGenericAny L_38 = L_9;
	const Il2CppFullySharedGenericAny L_45 = L_9;
	const Il2CppFullySharedGenericAny L_52 = L_9;
	const Il2CppFullySharedGenericAny L_59 = L_9;
	const Il2CppFullySharedGenericAny L_66 = L_9;
	const Il2CppFullySharedGenericAny L_72 = L_9;
	const Il2CppFullySharedGenericAny L_83 = L_9;
	const Il2CppFullySharedGenericAny L_85 = L_9;
	const Il2CppFullySharedGenericAny L_95 = L_9;
	const Il2CppFullySharedGenericAny L_97 = L_9;
	const Il2CppFullySharedGenericAny L_107 = L_9;
	const Il2CppFullySharedGenericAny L_114 = L_9;
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	Il2CppFullySharedGenericAny V_1 = alloca(SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
	memset(V_1, 0, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
	intptr_t V_2;
	memset((&V_2), 0, sizeof(V_2));
	Type_t* G_B33_0 = NULL;
	String_t* G_B33_1 = NULL;
	Type_t* G_B32_0 = NULL;
	String_t* G_B32_1 = NULL;
	String_t* G_B34_0 = NULL;
	String_t* G_B34_1 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_2;
		L_2 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_1, NULL);
		if (!L_2)
		{
			goto IL_0211;
		}
	}
	{
		bool L_3 = (Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_3)
		{
			goto IL_004b;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_4 = __this->___m_jobject;
		intptr_t L_5;
		L_5 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_4, NULL);
		intptr_t L_6 = ___0_fieldID;
		int32_t L_7;
		L_7 = AndroidJNISafe_GetIntField_mBD983688B73063DE5C55D320F60F266443FAC97C(L_5, L_6, NULL);
		int32_t L_8 = L_7;
		il2cpp_codegen_box_unbox((&L_8), L_9, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29, Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_9, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_004b:
	{
		bool L_10 = (Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_10)
		{
			goto IL_0082;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_11 = __this->___m_jobject;
		intptr_t L_12;
		L_12 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_11, NULL);
		intptr_t L_13 = ___0_fieldID;
		bool L_14;
		L_14 = AndroidJNISafe_GetBooleanField_m34F37B560A6AEC81B9061FB3B72698C84720435D(L_12, L_13, NULL);
		bool L_15 = L_14;
		il2cpp_codegen_box_unbox((&L_15), L_16, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29, Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_16, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_0082:
	{
		bool L_17 = (Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_17)
		{
			goto IL_00c4;
		}
	}
	{
		CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral50E02893F5B6939534A49C535241D6B7BAFCF599, NULL);
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_18 = __this->___m_jobject;
		intptr_t L_19;
		L_19 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_18, NULL);
		intptr_t L_20 = ___0_fieldID;
		int8_t L_21;
		L_21 = AndroidJNISafe_GetSByteField_mAD3B08AA8A97F77CAE17DD25B0F389AFAC2023B1(L_19, L_20, NULL);
		uint8_t L_22 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_21,NULL));
		uint8_t L_23 = L_22;
		il2cpp_codegen_box_unbox((&L_23), L_24, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_24, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_00c4:
	{
		bool L_25 = (SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_25)
		{
			goto IL_00fb;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_26 = __this->___m_jobject;
		intptr_t L_27;
		L_27 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_26, NULL);
		intptr_t L_28 = ___0_fieldID;
		int8_t L_29;
		L_29 = AndroidJNISafe_GetSByteField_mAD3B08AA8A97F77CAE17DD25B0F389AFAC2023B1(L_27, L_28, NULL);
		int8_t L_30 = L_29;
		il2cpp_codegen_box_unbox((&L_30), L_31, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29, SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_31, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_00fb:
	{
		bool L_32 = (Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_32)
		{
			goto IL_0132;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_33 = __this->___m_jobject;
		intptr_t L_34;
		L_34 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_33, NULL);
		intptr_t L_35 = ___0_fieldID;
		int16_t L_36;
		L_36 = AndroidJNISafe_GetShortField_m5D21E87061C1DAC89DF58671C53432D0361F0C6E(L_34, L_35, NULL);
		int16_t L_37 = L_36;
		il2cpp_codegen_box_unbox((&L_37), L_38, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29, Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_38, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_0132:
	{
		bool L_39 = (Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_39)
		{
			goto IL_0169;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_40 = __this->___m_jobject;
		intptr_t L_41;
		L_41 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_40, NULL);
		intptr_t L_42 = ___0_fieldID;
		int64_t L_43;
		L_43 = AndroidJNISafe_GetLongField_m7DD751358D10BB276D8A95D413B9DFB1E8EE81D8(L_41, L_42, NULL);
		int64_t L_44 = L_43;
		il2cpp_codegen_box_unbox((&L_44), L_45, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29, Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_45, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_0169:
	{
		bool L_46 = (Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_46)
		{
			goto IL_01a0;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_47 = __this->___m_jobject;
		intptr_t L_48;
		L_48 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_47, NULL);
		intptr_t L_49 = ___0_fieldID;
		float L_50;
		L_50 = AndroidJNISafe_GetFloatField_m1EAA1ED33002BBA28CA2B630521D6BF1B7D3A2E7(L_48, L_49, NULL);
		float L_51 = L_50;
		il2cpp_codegen_box_unbox((&L_51), L_52, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29, Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_52, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_01a0:
	{
		bool L_53 = (Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_53)
		{
			goto IL_01d7;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jobject;
		intptr_t L_55;
		L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
		intptr_t L_56 = ___0_fieldID;
		double L_57;
		L_57 = AndroidJNISafe_GetDoubleField_mBCBD5E80223EDECC06FA783F34149E3625219074(L_55, L_56, NULL);
		double L_58 = L_57;
		il2cpp_codegen_box_unbox((&L_58), L_59, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29, Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_59, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_01d7:
	{
		bool L_60 = (Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_60)
		{
			goto IL_0340;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_61 = __this->___m_jobject;
		intptr_t L_62;
		L_62 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_61, NULL);
		intptr_t L_63 = ___0_fieldID;
		Il2CppChar L_64;
		L_64 = AndroidJNISafe_GetCharField_m8301FA96B40E27C032590FE3F8E84A777A4739C3(L_62, L_63, NULL);
		Il2CppChar L_65 = L_64;
		il2cpp_codegen_box_unbox((&L_65), L_66, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29, Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_66, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_0211:
	{
		bool L_67 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_67)
		{
			goto IL_0243;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jobject;
		intptr_t L_69;
		L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
		intptr_t L_70 = ___0_fieldID;
		String_t* L_71;
		L_71 = AndroidJNISafe_GetStringField_mADFCA05D6DE790600B57E90B20F2E75AFC036B0F(L_69, L_70, NULL);
		void* L_73 = UnBox_Any(L_71, il2cpp_rgctx_data(method->rgctx_data, 1), L_72);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_73)), SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_0243:
	{
		bool L_74 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_74)
		{
			goto IL_0293;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jobject;
		intptr_t L_76;
		L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
		intptr_t L_77 = ___0_fieldID;
		intptr_t L_78;
		L_78 = AndroidJNISafe_GetObjectField_mCF3BB1C38718D6F55081126BC7F6C286B382B275(L_76, L_77, NULL);
		V_0 = L_78;
		intptr_t L_79 = V_0;
		bool L_80;
		L_80 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_79, 0, NULL);
		if (L_80)
		{
			goto IL_0289;
		}
	}
	{
		intptr_t L_81 = V_0;
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_82;
		L_82 = AndroidJavaObject_AndroidJavaClassDeleteLocalRef_m56C84D7516BCB51A84E8AFDB3FCA46BAF494548F(L_81, NULL);
		void* L_84 = UnBox_Any(L_82, il2cpp_rgctx_data(method->rgctx_data, 1), L_83);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_84)), SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_0289:
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_1, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		il2cpp_codegen_memcpy(L_85, V_1, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		il2cpp_codegen_memcpy(il2cppRetVal, L_85, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_0293:
	{
		bool L_86 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_86)
		{
			goto IL_02e3;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jobject;
		intptr_t L_88;
		L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
		intptr_t L_89 = ___0_fieldID;
		intptr_t L_90;
		L_90 = AndroidJNISafe_GetObjectField_mCF3BB1C38718D6F55081126BC7F6C286B382B275(L_88, L_89, NULL);
		V_2 = L_90;
		intptr_t L_91 = V_2;
		bool L_92;
		L_92 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_91, 0, NULL);
		if (L_92)
		{
			goto IL_02d9;
		}
	}
	{
		intptr_t L_93 = V_2;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_94;
		L_94 = AndroidJavaObject_AndroidJavaObjectDeleteLocalRef_m2ECEEAF6389ABB9D6B963B8A98568ECD9413DF3C(L_93, NULL);
		void* L_96 = UnBox_Any(L_94, il2cpp_rgctx_data(method->rgctx_data, 1), L_95);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_96)), SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_02d9:
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_1, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		il2cpp_codegen_memcpy(L_97, V_1, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		il2cpp_codegen_memcpy(il2cppRetVal, L_97, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_02e3:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_98 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_99;
		L_99 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_98, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_100 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_101;
		L_101 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_100, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_102;
		L_102 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_99, L_101, NULL);
		if (!L_102)
		{
			goto IL_0315;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_103 = __this->___m_jobject;
		intptr_t L_104;
		L_104 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_103, NULL);
		intptr_t L_105 = ___0_fieldID;
		intptr_t L_106;
		L_106 = AndroidJNISafe_GetObjectField_mCF3BB1C38718D6F55081126BC7F6C286B382B275(L_104, L_105, NULL);
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 2)), il2cpp_rgctx_method(method->rgctx_data, 2), NULL, L_106, (Il2CppFullySharedGenericAny*)L_107);
		il2cpp_codegen_memcpy(il2cppRetVal, L_107, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}

IL_0315:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_108 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_109;
		L_109 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_108, NULL);
		Type_t* L_110 = L_109;
		if (L_110)
		{
			G_B33_0 = L_110;
			G_B33_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
			goto IL_032b;
		}
		G_B32_0 = L_110;
		G_B32_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
	}
	{
		G_B34_0 = ((String_t*)(NULL));
		G_B34_1 = G_B32_1;
		goto IL_0330;
	}

IL_032b:
	{
		NullCheck(G_B33_0);
		String_t* L_111;
		L_111 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B33_0);
		G_B34_0 = L_111;
		G_B34_1 = G_B33_1;
	}

IL_0330:
	{
		String_t* L_112;
		L_112 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B34_1, G_B34_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		Exception_t* L_113 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_113, L_112, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_113, method);
	}

IL_0340:
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_1, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		il2cpp_codegen_memcpy(L_114, V_1, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		il2cpp_codegen_memcpy(il2cppRetVal, L_114, SizeOf_FieldType_t1266B1AF802837275B234C725395054CAC857D29);
		return;
	}
}
// Method Definition Index: 81733
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__Get_TisIl2CppFullySharedGenericAny_m6AA7C8A5BE069836640026CB6EDA5D93B3246683_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_tBD81DD51AB3076BC0134BD255EBBAC34E341C535 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 2));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_FieldType_tBD81DD51AB3076BC0134BD255EBBAC34E341C535);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_fieldName;
		intptr_t L_3;
		L_3 = ((  intptr_t (*) (intptr_t, String_t*, bool, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))(L_1, L_2, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_3;
		intptr_t L_4 = V_0;
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), __this, L_4, (Il2CppFullySharedGenericAny*)L_5);
		il2cpp_codegen_memcpy(il2cppRetVal, L_5, SizeOf_FieldType_tBD81DD51AB3076BC0134BD255EBBAC34E341C535);
		return;
	}
}
// Method Definition Index: 81742
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__GetStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m86131C9F9AC1A6082963096E96DDDDDD7366189E (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	int32_t V_1 = 0;
	intptr_t V_2;
	memset((&V_2), 0, sizeof(V_2));
	Type_t* G_B33_0 = NULL;
	String_t* G_B33_1 = NULL;
	Type_t* G_B32_0 = NULL;
	String_t* G_B32_1 = NULL;
	String_t* G_B34_0 = NULL;
	String_t* G_B34_1 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_2;
		L_2 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_1, NULL);
		if (!L_2)
		{
			goto IL_0211;
		}
	}
	{
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_3 = __this->___m_jclass;
		intptr_t L_4;
		L_4 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_3, NULL);
		intptr_t L_5 = ___0_fieldID;
		int32_t L_6;
		L_6 = AndroidJNISafe_GetStaticIntField_m0698D50C44E490A009E8388C7321630DED5973BD(L_4, L_5, NULL);
		return L_6;
	}

IL_0211:
	{
		goto IL_0243;
	}

IL_0243:
	{
		goto IL_0293;
	}

IL_0293:
	{
		goto IL_02e3;
	}

IL_02e3:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_7 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_8;
		L_8 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_7, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_9 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_10;
		L_10 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_9, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_11;
		L_11 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_8, L_10, NULL);
		if (!L_11)
		{
			goto IL_0315;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_12 = __this->___m_jclass;
		intptr_t L_13;
		L_13 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_12, NULL);
		intptr_t L_14 = ___0_fieldID;
		intptr_t L_15;
		L_15 = AndroidJNISafe_GetStaticObjectField_mB6B9A9EB2619DFDF1DA56300BF9FEC19BF883867(L_13, L_14, NULL);
		int32_t L_16;
		L_16 = AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m61DD76EF7E812BC3ED41BBA0D0DA1B430EC61256(L_15, il2cpp_rgctx_method(method->rgctx_data, 2));
		return L_16;
	}

IL_0315:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_17 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_18;
		L_18 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_17, NULL);
		Type_t* L_19 = L_18;
		if (L_19)
		{
			G_B33_0 = L_19;
			G_B33_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
			goto IL_032b;
		}
		G_B32_0 = L_19;
		G_B32_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
	}
	{
		G_B34_0 = ((String_t*)(NULL));
		G_B34_1 = G_B32_1;
		goto IL_0330;
	}

IL_032b:
	{
		NullCheck(G_B33_0);
		String_t* L_20;
		L_20 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B33_0);
		G_B34_0 = L_20;
		G_B34_1 = G_B33_1;
	}

IL_0330:
	{
		String_t* L_21;
		L_21 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B34_1, G_B34_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		Exception_t* L_22 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_22, L_21, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_22, method);
	}
	IL2CPP_UNREACHABLE;
	int32_t ret = 0;
	return ret;
}
// Method Definition Index: 81741
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AndroidJavaObject__GetStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m4289B7D740963C5007AD93A07FF6A0A01857DD28 (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_fieldName;
		intptr_t L_3;
		L_3 = AndroidJNIHelper_GetFieldID_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m1B394DCE5ED40DFA09EB18874BA2FFC3B5E50B2E(L_1, L_2, (bool)1, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_3;
		intptr_t L_4 = V_0;
		int32_t L_5;
		L_5 = AndroidJavaObject__GetStatic_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_m86131C9F9AC1A6082963096E96DDDDDD7366189E(__this, L_4, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_5;
	}
}
// Method Definition Index: 81742
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_mF12B3A136CBDB98DF0876DF1FC47EA4A14D98A17_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	Il2CppSharedGenericObject* V_1 = NULL;
	intptr_t V_2;
	memset((&V_2), 0, sizeof(V_2));
	Type_t* G_B33_0 = NULL;
	String_t* G_B33_1 = NULL;
	Type_t* G_B32_0 = NULL;
	String_t* G_B32_1 = NULL;
	String_t* G_B34_0 = NULL;
	String_t* G_B34_1 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_2;
		L_2 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_1, NULL);
		if (!L_2)
		{
			goto IL_0211;
		}
	}
	{
		goto IL_004b;
	}

IL_004b:
	{
		goto IL_0082;
	}

IL_0082:
	{
		goto IL_00c4;
	}

IL_00c4:
	{
		goto IL_00fb;
	}

IL_00fb:
	{
		goto IL_0132;
	}

IL_0132:
	{
		goto IL_0169;
	}

IL_0169:
	{
		goto IL_01a0;
	}

IL_01a0:
	{
		goto IL_01d7;
	}

IL_01d7:
	{
		goto IL_0340;
	}

IL_0211:
	{
		bool L_3 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_3)
		{
			goto IL_0243;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_4 = __this->___m_jclass;
		intptr_t L_5;
		L_5 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_4, NULL);
		intptr_t L_6 = ___0_fieldID;
		String_t* L_7;
		L_7 = AndroidJNISafe_GetStaticStringField_mB3D1325B08A38C7DAF1FA3E6CB52F6D8E0A2CB47(L_5, L_6, NULL);
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_7, il2cpp_rgctx_data(method->rgctx_data, 1)));
	}

IL_0243:
	{
		bool L_8 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_8)
		{
			goto IL_0293;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_9 = __this->___m_jclass;
		intptr_t L_10;
		L_10 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_9, NULL);
		intptr_t L_11 = ___0_fieldID;
		intptr_t L_12;
		L_12 = AndroidJNISafe_GetStaticObjectField_mB6B9A9EB2619DFDF1DA56300BF9FEC19BF883867(L_10, L_11, NULL);
		V_0 = L_12;
		intptr_t L_13 = V_0;
		bool L_14;
		L_14 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_13, 0, NULL);
		if (L_14)
		{
			goto IL_0289;
		}
	}
	{
		intptr_t L_15 = V_0;
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_16;
		L_16 = AndroidJavaObject_AndroidJavaClassDeleteLocalRef_m56C84D7516BCB51A84E8AFDB3FCA46BAF494548F(L_15, NULL);
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_16, il2cpp_rgctx_data(method->rgctx_data, 1)));
	}

IL_0289:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_17 = V_1;
		return L_17;
	}

IL_0293:
	{
		bool L_18 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_18)
		{
			goto IL_02e3;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_19 = __this->___m_jclass;
		intptr_t L_20;
		L_20 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_19, NULL);
		intptr_t L_21 = ___0_fieldID;
		intptr_t L_22;
		L_22 = AndroidJNISafe_GetStaticObjectField_mB6B9A9EB2619DFDF1DA56300BF9FEC19BF883867(L_20, L_21, NULL);
		V_2 = L_22;
		intptr_t L_23 = V_2;
		bool L_24;
		L_24 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_23, 0, NULL);
		if (L_24)
		{
			goto IL_02d9;
		}
	}
	{
		intptr_t L_25 = V_2;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_26;
		L_26 = AndroidJavaObject_AndroidJavaObjectDeleteLocalRef_m2ECEEAF6389ABB9D6B963B8A98568ECD9413DF3C(L_25, NULL);
		return ((Il2CppSharedGenericObject*)Castclass((RuntimeObject*)L_26, il2cpp_rgctx_data(method->rgctx_data, 1)));
	}

IL_02d9:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_27 = V_1;
		return L_27;
	}

IL_02e3:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_28 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_29;
		L_29 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_28, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_30 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_31;
		L_31 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_30, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_32;
		L_32 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_29, L_31, NULL);
		if (!L_32)
		{
			goto IL_0315;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_33 = __this->___m_jclass;
		intptr_t L_34;
		L_34 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_33, NULL);
		intptr_t L_35 = ___0_fieldID;
		intptr_t L_36;
		L_36 = AndroidJNISafe_GetStaticObjectField_mB6B9A9EB2619DFDF1DA56300BF9FEC19BF883867(L_34, L_35, NULL);
		Il2CppSharedGenericObject* L_37;
		L_37 = AndroidJavaObject_FromJavaArrayDeleteLocalRef_TisIl2CppSharedGenericObject_mB1CEFE624E92B82D234C1982AA2A7DF6AC5FD284(L_36, il2cpp_rgctx_method(method->rgctx_data, 2));
		return L_37;
	}

IL_0315:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_38 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_39;
		L_39 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_38, NULL);
		Type_t* L_40 = L_39;
		if (L_40)
		{
			G_B33_0 = L_40;
			G_B33_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
			goto IL_032b;
		}
		G_B32_0 = L_40;
		G_B32_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
	}
	{
		G_B34_0 = ((String_t*)(NULL));
		G_B34_1 = G_B32_1;
		goto IL_0330;
	}

IL_032b:
	{
		NullCheck(G_B33_0);
		String_t* L_41;
		L_41 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B33_0);
		G_B34_0 = L_41;
		G_B34_1 = G_B33_1;
	}

IL_0330:
	{
		String_t* L_42;
		L_42 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B34_1, G_B34_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		Exception_t* L_43 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_43, L_42, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_43, method);
	}

IL_0340:
	{
		il2cpp_codegen_initobj((&V_1), sizeof(Il2CppSharedGenericObject*));
		Il2CppSharedGenericObject* L_44 = V_1;
		return L_44;
	}
}
// Method Definition Index: 81741
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_m177F1A057E8F5EF05D9158BA272E890C6210044B_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_fieldName;
		intptr_t L_3;
		L_3 = AndroidJNIHelper_GetFieldID_TisIl2CppSharedGenericObject_mDFF491584A820BE0629A47ED3B790A199050D589(L_1, L_2, (bool)1, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_3;
		intptr_t L_4 = V_0;
		Il2CppSharedGenericObject* L_5;
		L_5 = AndroidJavaObject__GetStatic_TisIl2CppSharedGenericObject_mF12B3A136CBDB98DF0876DF1FC47EA4A14D98A17(__this, L_4, il2cpp_rgctx_method(method->rgctx_data, 1));
		return L_5;
	}
}
// Method Definition Index: 81742
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__GetStatic_TisIl2CppFullySharedGenericAny_m265EB43F18C8FB22DCD571C5E50A76642369551D_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral50E02893F5B6939534A49C535241D6B7BAFCF599);
		il2cpp_rgctx_method_init(method);
	}
	const uint32_t SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	const Il2CppFullySharedGenericAny L_9 = alloca(SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
	const Il2CppFullySharedGenericAny L_16 = L_9;
	const Il2CppFullySharedGenericAny L_24 = L_9;
	const Il2CppFullySharedGenericAny L_31 = L_9;
	const Il2CppFullySharedGenericAny L_38 = L_9;
	const Il2CppFullySharedGenericAny L_45 = L_9;
	const Il2CppFullySharedGenericAny L_52 = L_9;
	const Il2CppFullySharedGenericAny L_59 = L_9;
	const Il2CppFullySharedGenericAny L_66 = L_9;
	const Il2CppFullySharedGenericAny L_72 = L_9;
	const Il2CppFullySharedGenericAny L_83 = L_9;
	const Il2CppFullySharedGenericAny L_85 = L_9;
	const Il2CppFullySharedGenericAny L_95 = L_9;
	const Il2CppFullySharedGenericAny L_97 = L_9;
	const Il2CppFullySharedGenericAny L_107 = L_9;
	const Il2CppFullySharedGenericAny L_114 = L_9;
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	Il2CppFullySharedGenericAny V_1 = alloca(SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
	memset(V_1, 0, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
	intptr_t V_2;
	memset((&V_2), 0, sizeof(V_2));
	Type_t* G_B33_0 = NULL;
	String_t* G_B33_1 = NULL;
	Type_t* G_B32_0 = NULL;
	String_t* G_B32_1 = NULL;
	String_t* G_B34_0 = NULL;
	String_t* G_B34_1 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_2;
		L_2 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_1, NULL);
		if (!L_2)
		{
			goto IL_0211;
		}
	}
	{
		bool L_3 = (Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_3)
		{
			goto IL_004b;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_4 = __this->___m_jclass;
		intptr_t L_5;
		L_5 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_4, NULL);
		intptr_t L_6 = ___0_fieldID;
		int32_t L_7;
		L_7 = AndroidJNISafe_GetStaticIntField_m0698D50C44E490A009E8388C7321630DED5973BD(L_5, L_6, NULL);
		int32_t L_8 = L_7;
		il2cpp_codegen_box_unbox((&L_8), L_9, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3, Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_9, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_004b:
	{
		bool L_10 = (Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_10)
		{
			goto IL_0082;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_11 = __this->___m_jclass;
		intptr_t L_12;
		L_12 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_11, NULL);
		intptr_t L_13 = ___0_fieldID;
		bool L_14;
		L_14 = AndroidJNISafe_GetStaticBooleanField_m172BEAA3F0AB6754EA5F1AD30C36DAA0D3D7C666(L_12, L_13, NULL);
		bool L_15 = L_14;
		il2cpp_codegen_box_unbox((&L_15), L_16, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3, Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_16, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_0082:
	{
		bool L_17 = (Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_17)
		{
			goto IL_00c4;
		}
	}
	{
		CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral50E02893F5B6939534A49C535241D6B7BAFCF599, NULL);
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_18 = __this->___m_jclass;
		intptr_t L_19;
		L_19 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_18, NULL);
		intptr_t L_20 = ___0_fieldID;
		int8_t L_21;
		L_21 = AndroidJNISafe_GetStaticSByteField_m77596E5B1AE58DAFF39268AC954CAD53974A688D(L_19, L_20, NULL);
		uint8_t L_22 = (il2cpp_codegen_conv<uint8_t,int8_t,int32_t,false,false>(L_21,NULL));
		uint8_t L_23 = L_22;
		il2cpp_codegen_box_unbox((&L_23), L_24, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_24, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_00c4:
	{
		bool L_25 = (SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_25)
		{
			goto IL_00fb;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_26 = __this->___m_jclass;
		intptr_t L_27;
		L_27 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_26, NULL);
		intptr_t L_28 = ___0_fieldID;
		int8_t L_29;
		L_29 = AndroidJNISafe_GetStaticSByteField_m77596E5B1AE58DAFF39268AC954CAD53974A688D(L_27, L_28, NULL);
		int8_t L_30 = L_29;
		il2cpp_codegen_box_unbox((&L_30), L_31, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3, SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_31, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_00fb:
	{
		bool L_32 = (Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_32)
		{
			goto IL_0132;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_33 = __this->___m_jclass;
		intptr_t L_34;
		L_34 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_33, NULL);
		intptr_t L_35 = ___0_fieldID;
		int16_t L_36;
		L_36 = AndroidJNISafe_GetStaticShortField_m83716D4D85B30F26803F866AC47D5C04AAB5D320(L_34, L_35, NULL);
		int16_t L_37 = L_36;
		il2cpp_codegen_box_unbox((&L_37), L_38, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3, Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_38, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_0132:
	{
		bool L_39 = (Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_39)
		{
			goto IL_0169;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_40 = __this->___m_jclass;
		intptr_t L_41;
		L_41 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_40, NULL);
		intptr_t L_42 = ___0_fieldID;
		int64_t L_43;
		L_43 = AndroidJNISafe_GetStaticLongField_mABC2B933CEB757E3FAF1FD6C60AA0C4D38E9C49D(L_41, L_42, NULL);
		int64_t L_44 = L_43;
		il2cpp_codegen_box_unbox((&L_44), L_45, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3, Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_45, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_0169:
	{
		bool L_46 = (Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_46)
		{
			goto IL_01a0;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_47 = __this->___m_jclass;
		intptr_t L_48;
		L_48 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_47, NULL);
		intptr_t L_49 = ___0_fieldID;
		float L_50;
		L_50 = AndroidJNISafe_GetStaticFloatField_mD1456B729026959309A839C2647279C0B6541356(L_48, L_49, NULL);
		float L_51 = L_50;
		il2cpp_codegen_box_unbox((&L_51), L_52, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3, Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_52, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_01a0:
	{
		bool L_53 = (Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_53)
		{
			goto IL_01d7;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_54 = __this->___m_jclass;
		intptr_t L_55;
		L_55 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_54, NULL);
		intptr_t L_56 = ___0_fieldID;
		double L_57;
		L_57 = AndroidJNISafe_GetStaticDoubleField_mEB86F2CE1F3879AAA9DEDA4B496F882C0E1DCBC2(L_55, L_56, NULL);
		double L_58 = L_57;
		il2cpp_codegen_box_unbox((&L_58), L_59, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3, Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_59, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_01d7:
	{
		bool L_60 = (Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_60)
		{
			goto IL_0340;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_61 = __this->___m_jclass;
		intptr_t L_62;
		L_62 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_61, NULL);
		intptr_t L_63 = ___0_fieldID;
		Il2CppChar L_64;
		L_64 = AndroidJNISafe_GetStaticCharField_mF70F6D197261364AF2A9E875D84DDDA35BD0ED96(L_62, L_63, NULL);
		Il2CppChar L_65 = L_64;
		il2cpp_codegen_box_unbox((&L_65), L_66, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3, Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var, il2cpp_rgctx_data(method->rgctx_data, 1));
		il2cpp_codegen_memcpy(il2cppRetVal, L_66, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_0211:
	{
		bool L_67 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_67)
		{
			goto IL_0243;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_68 = __this->___m_jclass;
		intptr_t L_69;
		L_69 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_68, NULL);
		intptr_t L_70 = ___0_fieldID;
		String_t* L_71;
		L_71 = AndroidJNISafe_GetStaticStringField_mB3D1325B08A38C7DAF1FA3E6CB52F6D8E0A2CB47(L_69, L_70, NULL);
		void* L_73 = UnBox_Any(L_71, il2cpp_rgctx_data(method->rgctx_data, 1), L_72);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_73)), SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_0243:
	{
		bool L_74 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_74)
		{
			goto IL_0293;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jclass;
		intptr_t L_76;
		L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
		intptr_t L_77 = ___0_fieldID;
		intptr_t L_78;
		L_78 = AndroidJNISafe_GetStaticObjectField_mB6B9A9EB2619DFDF1DA56300BF9FEC19BF883867(L_76, L_77, NULL);
		V_0 = L_78;
		intptr_t L_79 = V_0;
		bool L_80;
		L_80 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_79, 0, NULL);
		if (L_80)
		{
			goto IL_0289;
		}
	}
	{
		intptr_t L_81 = V_0;
		AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03* L_82;
		L_82 = AndroidJavaObject_AndroidJavaClassDeleteLocalRef_m56C84D7516BCB51A84E8AFDB3FCA46BAF494548F(L_81, NULL);
		void* L_84 = UnBox_Any(L_82, il2cpp_rgctx_data(method->rgctx_data, 1), L_83);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_84)), SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_0289:
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_1, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		il2cpp_codegen_memcpy(L_85, V_1, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		il2cpp_codegen_memcpy(il2cppRetVal, L_85, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_0293:
	{
		bool L_86 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_86)
		{
			goto IL_02e3;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_87 = __this->___m_jclass;
		intptr_t L_88;
		L_88 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_87, NULL);
		intptr_t L_89 = ___0_fieldID;
		intptr_t L_90;
		L_90 = AndroidJNISafe_GetStaticObjectField_mB6B9A9EB2619DFDF1DA56300BF9FEC19BF883867(L_88, L_89, NULL);
		V_2 = L_90;
		intptr_t L_91 = V_2;
		bool L_92;
		L_92 = IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline(L_91, 0, NULL);
		if (L_92)
		{
			goto IL_02d9;
		}
	}
	{
		intptr_t L_93 = V_2;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_94;
		L_94 = AndroidJavaObject_AndroidJavaObjectDeleteLocalRef_m2ECEEAF6389ABB9D6B963B8A98568ECD9413DF3C(L_93, NULL);
		void* L_96 = UnBox_Any(L_94, il2cpp_rgctx_data(method->rgctx_data, 1), L_95);
		il2cpp_codegen_memcpy(il2cppRetVal, (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_96)), SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_02d9:
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_1, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		il2cpp_codegen_memcpy(L_97, V_1, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		il2cpp_codegen_memcpy(il2cppRetVal, L_97, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_02e3:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_98 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_99;
		L_99 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_98, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_100 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_101;
		L_101 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_100, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_102;
		L_102 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_99, L_101, NULL);
		if (!L_102)
		{
			goto IL_0315;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_103 = __this->___m_jclass;
		intptr_t L_104;
		L_104 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_103, NULL);
		intptr_t L_105 = ___0_fieldID;
		intptr_t L_106;
		L_106 = AndroidJNISafe_GetStaticObjectField_mB6B9A9EB2619DFDF1DA56300BF9FEC19BF883867(L_104, L_105, NULL);
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 2)), il2cpp_rgctx_method(method->rgctx_data, 2), NULL, L_106, (Il2CppFullySharedGenericAny*)L_107);
		il2cpp_codegen_memcpy(il2cppRetVal, L_107, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}

IL_0315:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_108 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_109;
		L_109 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_108, NULL);
		Type_t* L_110 = L_109;
		if (L_110)
		{
			G_B33_0 = L_110;
			G_B33_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
			goto IL_032b;
		}
		G_B32_0 = L_110;
		G_B32_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
	}
	{
		G_B34_0 = ((String_t*)(NULL));
		G_B34_1 = G_B32_1;
		goto IL_0330;
	}

IL_032b:
	{
		NullCheck(G_B33_0);
		String_t* L_111;
		L_111 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B33_0);
		G_B34_0 = L_111;
		G_B34_1 = G_B33_1;
	}

IL_0330:
	{
		String_t* L_112;
		L_112 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B34_1, G_B34_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		Exception_t* L_113 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_113, L_112, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_113, method);
	}

IL_0340:
	{
		il2cpp_codegen_initobj((Il2CppFullySharedGenericAny*)V_1, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		il2cpp_codegen_memcpy(L_114, V_1, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		il2cpp_codegen_memcpy(il2cppRetVal, L_114, SizeOf_FieldType_t4130D50F8DF6CBFB2A17DDF9CA62DF84750A5FF3);
		return;
	}
}
// Method Definition Index: 81741
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__GetStatic_TisIl2CppFullySharedGenericAny_m5FA6F86B221B6AD76DDD71818F721187D59BFC38_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_t31FF3BC94504224433BABB9DAD6CE4032EA80634 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 2));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_FieldType_t31FF3BC94504224433BABB9DAD6CE4032EA80634);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_fieldName;
		intptr_t L_3;
		L_3 = ((  intptr_t (*) (intptr_t, String_t*, bool, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))(L_1, L_2, (bool)1, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_3;
		intptr_t L_4 = V_0;
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)), il2cpp_rgctx_method(method->rgctx_data, 1), __this, L_4, (Il2CppFullySharedGenericAny*)L_5);
		il2cpp_codegen_memcpy(il2cppRetVal, L_5, SizeOf_FieldType_t31FF3BC94504224433BABB9DAD6CE4032EA80634);
		return;
	}
}
// Method Definition Index: 81736
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__Set_TisIl2CppFullySharedGenericAny_m84E47B5A5FF416288BF85C7F79F287D6FD90356F_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, Il2CppFullySharedGenericAny ___1_val, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1D86DE1FC0445C55F712ED0F6D3E32FED0A1E724);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	const uint32_t SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	const Il2CppFullySharedGenericAny L_7 = alloca(SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
	const Il2CppFullySharedGenericAny L_13 = L_7;
	const Il2CppFullySharedGenericAny L_19 = L_7;
	const Il2CppFullySharedGenericAny L_26 = L_7;
	const Il2CppFullySharedGenericAny L_32 = L_7;
	const Il2CppFullySharedGenericAny L_38 = L_7;
	const Il2CppFullySharedGenericAny L_44 = L_7;
	const Il2CppFullySharedGenericAny L_50 = L_7;
	const Il2CppFullySharedGenericAny L_56 = L_7;
	const Il2CppFullySharedGenericAny L_62 = L_7;
	const Il2CppFullySharedGenericAny L_68 = L_7;
	const Il2CppFullySharedGenericAny L_70 = L_7;
	const Il2CppFullySharedGenericAny L_78 = L_7;
	const Il2CppFullySharedGenericAny L_80 = L_7;
	const Il2CppFullySharedGenericAny L_92 = L_7;
	const Il2CppFullySharedGenericAny L_94 = L_7;
	const Il2CppFullySharedGenericAny L_102 = L_7;
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	intptr_t G_B24_0;
	memset((&G_B24_0), 0, sizeof(G_B24_0));
	intptr_t G_B24_1;
	memset((&G_B24_1), 0, sizeof(G_B24_1));
	intptr_t G_B23_0;
	memset((&G_B23_0), 0, sizeof(G_B23_0));
	intptr_t G_B23_1;
	memset((&G_B23_1), 0, sizeof(G_B23_1));
	intptr_t G_B25_0;
	memset((&G_B25_0), 0, sizeof(G_B25_0));
	intptr_t G_B25_1;
	memset((&G_B25_1), 0, sizeof(G_B25_1));
	intptr_t G_B25_2;
	memset((&G_B25_2), 0, sizeof(G_B25_2));
	intptr_t G_B29_0;
	memset((&G_B29_0), 0, sizeof(G_B29_0));
	intptr_t G_B29_1;
	memset((&G_B29_1), 0, sizeof(G_B29_1));
	intptr_t G_B28_0;
	memset((&G_B28_0), 0, sizeof(G_B28_0));
	intptr_t G_B28_1;
	memset((&G_B28_1), 0, sizeof(G_B28_1));
	intptr_t G_B30_0;
	memset((&G_B30_0), 0, sizeof(G_B30_0));
	intptr_t G_B30_1;
	memset((&G_B30_1), 0, sizeof(G_B30_1));
	intptr_t G_B30_2;
	memset((&G_B30_2), 0, sizeof(G_B30_2));
	intptr_t G_B34_0;
	memset((&G_B34_0), 0, sizeof(G_B34_0));
	intptr_t G_B34_1;
	memset((&G_B34_1), 0, sizeof(G_B34_1));
	intptr_t G_B33_0;
	memset((&G_B33_0), 0, sizeof(G_B33_0));
	intptr_t G_B33_1;
	memset((&G_B33_1), 0, sizeof(G_B33_1));
	intptr_t G_B35_0;
	memset((&G_B35_0), 0, sizeof(G_B35_0));
	intptr_t G_B35_1;
	memset((&G_B35_1), 0, sizeof(G_B35_1));
	intptr_t G_B35_2;
	memset((&G_B35_2), 0, sizeof(G_B35_2));
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_2;
		L_2 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_1, NULL);
		if (!L_2)
		{
			goto IL_021a;
		}
	}
	{
		bool L_3 = (Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_3)
		{
			goto IL_004c;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_4 = __this->___m_jobject;
		intptr_t L_5;
		L_5 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_4, NULL);
		intptr_t L_6 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_7, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		int32_t L_8;
		il2cpp_codegen_box_unbox(L_7, (&L_8), sizeof(int32_t), il2cpp_rgctx_data(method->rgctx_data, 1), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetIntField_mD238DA37BA1B3D7693484237951A6EFEA9C62120(L_5, L_6, L_8, NULL);
		return;
	}

IL_004c:
	{
		bool L_9 = (Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_9)
		{
			goto IL_0084;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_10 = __this->___m_jobject;
		intptr_t L_11;
		L_11 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_10, NULL);
		intptr_t L_12 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_13, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		bool L_14;
		il2cpp_codegen_box_unbox(L_13, (&L_14), sizeof(bool), il2cpp_rgctx_data(method->rgctx_data, 1), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetBooleanField_m5279EA41B214699E79733DC6C93259CC9DCA1D9E(L_11, L_12, L_14, NULL);
		return;
	}

IL_0084:
	{
		bool L_15 = (Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_15)
		{
			goto IL_00c7;
		}
	}
	{
		CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral1D86DE1FC0445C55F712ED0F6D3E32FED0A1E724, NULL);
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_16 = __this->___m_jobject;
		intptr_t L_17;
		L_17 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_16, NULL);
		intptr_t L_18 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_19, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		uint8_t L_20;
		il2cpp_codegen_box_unbox(L_19, (&L_20), sizeof(uint8_t), il2cpp_rgctx_data(method->rgctx_data, 1), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var);
		int8_t L_21 = (il2cpp_codegen_conv<int8_t,uint8_t,int32_t,false,false>(L_20,NULL));
		AndroidJNISafe_SetSByteField_mB021168746571E7CAA8C0EAD7AA7F02C18B5EE33(L_17, L_18, L_21, NULL);
		return;
	}

IL_00c7:
	{
		bool L_22 = (SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_22)
		{
			goto IL_00ff;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_23 = __this->___m_jobject;
		intptr_t L_24;
		L_24 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_23, NULL);
		intptr_t L_25 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_26, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		int8_t L_27;
		il2cpp_codegen_box_unbox(L_26, (&L_27), sizeof(int8_t), il2cpp_rgctx_data(method->rgctx_data, 1), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetSByteField_mB021168746571E7CAA8C0EAD7AA7F02C18B5EE33(L_24, L_25, L_27, NULL);
		return;
	}

IL_00ff:
	{
		bool L_28 = (Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_28)
		{
			goto IL_0137;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_29 = __this->___m_jobject;
		intptr_t L_30;
		L_30 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_29, NULL);
		intptr_t L_31 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_32, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		int16_t L_33;
		il2cpp_codegen_box_unbox(L_32, (&L_33), sizeof(int16_t), il2cpp_rgctx_data(method->rgctx_data, 1), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetShortField_mF95E569C142DEDD604CE8BA7617328B3EDDD2F0D(L_30, L_31, L_33, NULL);
		return;
	}

IL_0137:
	{
		bool L_34 = (Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_34)
		{
			goto IL_016f;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_35 = __this->___m_jobject;
		intptr_t L_36;
		L_36 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_35, NULL);
		intptr_t L_37 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_38, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		int64_t L_39;
		il2cpp_codegen_box_unbox(L_38, (&L_39), sizeof(int64_t), il2cpp_rgctx_data(method->rgctx_data, 1), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetLongField_m13905547F5CDC7E01AB0D8C787BF98DC2870EC35(L_36, L_37, L_39, NULL);
		return;
	}

IL_016f:
	{
		bool L_40 = (Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_40)
		{
			goto IL_01a7;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_41 = __this->___m_jobject;
		intptr_t L_42;
		L_42 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_41, NULL);
		intptr_t L_43 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_44, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		float L_45;
		il2cpp_codegen_box_unbox(L_44, (&L_45), sizeof(float), il2cpp_rgctx_data(method->rgctx_data, 1), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetFloatField_m589CA6B8DD2BFD4515C5AEAE3772782B293F02C3(L_42, L_43, L_45, NULL);
		return;
	}

IL_01a7:
	{
		bool L_46 = (Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_46)
		{
			goto IL_01df;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_47 = __this->___m_jobject;
		intptr_t L_48;
		L_48 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_47, NULL);
		intptr_t L_49 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_50, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		double L_51;
		il2cpp_codegen_box_unbox(L_50, (&L_51), sizeof(double), il2cpp_rgctx_data(method->rgctx_data, 1), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetDoubleField_mE93D0C5EC2019A1B657BD32970FE6EFC9B005A58(L_48, L_49, L_51, NULL);
		return;
	}

IL_01df:
	{
		bool L_52 = (Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_52)
		{
			goto IL_03aa;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_53 = __this->___m_jobject;
		intptr_t L_54;
		L_54 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_53, NULL);
		intptr_t L_55 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_56, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		Il2CppChar L_57;
		il2cpp_codegen_box_unbox(L_56, (&L_57), sizeof(Il2CppChar), il2cpp_rgctx_data(method->rgctx_data, 1), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetCharField_m69D09A6A2CEA55D84B240FE32D90300AAB1334F9(L_54, L_55, L_57, NULL);
		return;
	}

IL_021a:
	{
		bool L_58 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_58)
		{
			goto IL_0252;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_59 = __this->___m_jobject;
		intptr_t L_60;
		L_60 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_59, NULL);
		intptr_t L_61 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_62, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		RuntimeObject* L_63 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_62);
		AndroidJNISafe_SetStringField_m649363D4E87763D6A9760359EAFB29802E90B409(L_60, L_61, ((String_t*)CastclassSealed((RuntimeObject*)L_63, String_t_il2cpp_TypeInfo_var)), NULL);
		return;
	}

IL_0252:
	{
		bool L_64 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_64)
		{
			goto IL_02a3;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_65 = __this->___m_jobject;
		intptr_t L_66;
		L_66 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_65, NULL);
		intptr_t L_67 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_68, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		bool L_69 = il2cpp_codegen_would_box_to_non_null(il2cpp_rgctx_data(method->rgctx_data, 1), L_68);
		if (!L_69)
		{
			G_B24_0 = L_67;
			G_B24_1 = L_66;
			goto IL_0298;
		}
		G_B23_0 = L_67;
		G_B23_1 = L_66;
	}
	{
		il2cpp_codegen_memcpy(L_70, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		RuntimeObject* L_71 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_70);
		NullCheck(((AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)CastclassClass((RuntimeObject*)L_71, AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var)));
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_72 = ((AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)((AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)CastclassClass((RuntimeObject*)L_71, AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var)))->___m_jclass;
		intptr_t L_73;
		L_73 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_72, NULL);
		G_B25_0 = L_73;
		G_B25_1 = G_B23_0;
		G_B25_2 = G_B23_1;
		goto IL_029d;
	}

IL_0298:
	{
		G_B25_0 = 0;
		G_B25_1 = G_B24_0;
		G_B25_2 = G_B24_1;
	}

IL_029d:
	{
		AndroidJNISafe_SetObjectField_mFE500926F9C963FF106E8AA30A16F4C671BAA8CA(G_B25_2, G_B25_1, G_B25_0, NULL);
		return;
	}

IL_02a3:
	{
		bool L_74 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_74)
		{
			goto IL_02f4;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jobject;
		intptr_t L_76;
		L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
		intptr_t L_77 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_78, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		bool L_79 = il2cpp_codegen_would_box_to_non_null(il2cpp_rgctx_data(method->rgctx_data, 1), L_78);
		if (!L_79)
		{
			G_B29_0 = L_77;
			G_B29_1 = L_76;
			goto IL_02e9;
		}
		G_B28_0 = L_77;
		G_B28_1 = L_76;
	}
	{
		il2cpp_codegen_memcpy(L_80, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		RuntimeObject* L_81 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_80);
		NullCheck(((AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)CastclassClass((RuntimeObject*)L_81, AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var)));
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = ((AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)CastclassClass((RuntimeObject*)L_81, AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var))->___m_jobject;
		intptr_t L_83;
		L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
		G_B30_0 = L_83;
		G_B30_1 = G_B28_0;
		G_B30_2 = G_B28_1;
		goto IL_02ee;
	}

IL_02e9:
	{
		G_B30_0 = 0;
		G_B30_1 = G_B29_0;
		G_B30_2 = G_B29_1;
	}

IL_02ee:
	{
		AndroidJNISafe_SetObjectField_mFE500926F9C963FF106E8AA30A16F4C671BAA8CA(G_B30_2, G_B30_1, G_B30_0, NULL);
		return;
	}

IL_02f4:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_84 = { reinterpret_cast<intptr_t> (AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_85;
		L_85 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_84, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_86 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_87;
		L_87 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_86, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_88;
		L_88 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_85, L_87, NULL);
		if (!L_88)
		{
			goto IL_0340;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_89 = __this->___m_jobject;
		intptr_t L_90;
		L_90 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_89, NULL);
		intptr_t L_91 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_92, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		bool L_93 = il2cpp_codegen_would_box_to_non_null(il2cpp_rgctx_data(method->rgctx_data, 1), L_92);
		if (!L_93)
		{
			G_B34_0 = L_91;
			G_B34_1 = L_90;
			goto IL_0335;
		}
		G_B33_0 = L_91;
		G_B33_1 = L_90;
	}
	{
		il2cpp_codegen_memcpy(L_94, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		RuntimeObject* L_95 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_94);
		NullCheck(((AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D*)CastclassClass((RuntimeObject*)L_95, AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var)));
		intptr_t L_96;
		L_96 = AndroidJavaProxy_GetRawProxy_m685E066A4D378B596CD88385B954AE90CBF328A9(((AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D*)CastclassClass((RuntimeObject*)L_95, AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var)), NULL);
		G_B35_0 = L_96;
		G_B35_1 = G_B33_0;
		G_B35_2 = G_B33_1;
		goto IL_033a;
	}

IL_0335:
	{
		G_B35_0 = 0;
		G_B35_1 = G_B34_0;
		G_B35_2 = G_B34_1;
	}

IL_033a:
	{
		AndroidJNISafe_SetObjectField_mFE500926F9C963FF106E8AA30A16F4C671BAA8CA(G_B35_2, G_B35_1, G_B35_0, NULL);
		return;
	}

IL_0340:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_97 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_98;
		L_98 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_97, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_99 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_100;
		L_100 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_99, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_101;
		L_101 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_98, L_100, NULL);
		if (!L_101)
		{
			goto IL_037f;
		}
	}
	{
		il2cpp_codegen_memcpy(L_102, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tC72607BAF758371FA7A7DDA2D0873E433EE11D19);
		RuntimeObject* L_103 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_102);
		intptr_t L_104;
		L_104 = AndroidJNIHelper_ConvertToJNIArray_mBEAE4605FF297D19AFB8CE4E8443C9C0F87E9A13(((RuntimeArray*)CastclassClass((RuntimeObject*)L_103, RuntimeArray_il2cpp_TypeInfo_var)), NULL);
		V_0 = L_104;
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_105 = __this->___m_jobject;
		intptr_t L_106;
		L_106 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_105, NULL);
		intptr_t L_107 = ___0_fieldID;
		intptr_t L_108 = V_0;
		AndroidJNISafe_SetObjectField_mFE500926F9C963FF106E8AA30A16F4C671BAA8CA(L_106, L_107, L_108, NULL);
		return;
	}

IL_037f:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_110;
		L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
		Type_t* L_111 = L_110;
		if (L_111)
		{
			G_B40_0 = L_111;
			G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
			goto IL_0395;
		}
		G_B39_0 = L_111;
		G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
	}
	{
		G_B41_0 = ((String_t*)(NULL));
		G_B41_1 = G_B39_1;
		goto IL_039a;
	}

IL_0395:
	{
		NullCheck(G_B40_0);
		String_t* L_112;
		L_112 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
		G_B41_0 = L_112;
		G_B41_1 = G_B40_1;
	}

IL_039a:
	{
		String_t* L_113;
		L_113 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		Exception_t* L_114 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_114, L_113, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_114, method);
	}

IL_03aa:
	{
		return;
	}
}
// Method Definition Index: 81735
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__Set_TisIl2CppFullySharedGenericAny_m1AF6344C2EF172B8799D007F4BAC3ECDC182BC22_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, Il2CppFullySharedGenericAny ___1_val, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_t2F905C7C598CE04C35E1297452F5AE8BFD208DAE = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_FieldType_t2F905C7C598CE04C35E1297452F5AE8BFD208DAE);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_fieldName;
		intptr_t L_3;
		L_3 = ((  intptr_t (*) (intptr_t, String_t*, bool, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))(L_1, L_2, (bool)0, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_3;
		intptr_t L_4 = V_0;
		il2cpp_codegen_memcpy(L_5, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_t2F905C7C598CE04C35E1297452F5AE8BFD208DAE);
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 2)), il2cpp_rgctx_method(method->rgctx_data, 2), __this, L_4, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? L_5: *(void**)L_5));
		return;
	}
}
// Method Definition Index: 81744
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__SetStatic_TisIl2CppFullySharedGenericAny_m07564225CF639F8502927F2D783809867A34367A_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, intptr_t ___0_fieldID, Il2CppFullySharedGenericAny ___1_val, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral1D86DE1FC0445C55F712ED0F6D3E32FED0A1E724);
		il2cpp_rgctx_method_init(method);
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	CHECKED_LOCAL(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit);
	const uint32_t SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	CHECKED_LOCAL(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit);
	const Il2CppFullySharedGenericAny L_7 = alloca(SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
	const Il2CppFullySharedGenericAny L_13 = L_7;
	const Il2CppFullySharedGenericAny L_19 = L_7;
	const Il2CppFullySharedGenericAny L_26 = L_7;
	const Il2CppFullySharedGenericAny L_32 = L_7;
	const Il2CppFullySharedGenericAny L_38 = L_7;
	const Il2CppFullySharedGenericAny L_44 = L_7;
	const Il2CppFullySharedGenericAny L_50 = L_7;
	const Il2CppFullySharedGenericAny L_56 = L_7;
	const Il2CppFullySharedGenericAny L_62 = L_7;
	const Il2CppFullySharedGenericAny L_68 = L_7;
	const Il2CppFullySharedGenericAny L_70 = L_7;
	const Il2CppFullySharedGenericAny L_78 = L_7;
	const Il2CppFullySharedGenericAny L_80 = L_7;
	const Il2CppFullySharedGenericAny L_92 = L_7;
	const Il2CppFullySharedGenericAny L_94 = L_7;
	const Il2CppFullySharedGenericAny L_102 = L_7;
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	intptr_t G_B24_0;
	memset((&G_B24_0), 0, sizeof(G_B24_0));
	intptr_t G_B24_1;
	memset((&G_B24_1), 0, sizeof(G_B24_1));
	intptr_t G_B23_0;
	memset((&G_B23_0), 0, sizeof(G_B23_0));
	intptr_t G_B23_1;
	memset((&G_B23_1), 0, sizeof(G_B23_1));
	intptr_t G_B25_0;
	memset((&G_B25_0), 0, sizeof(G_B25_0));
	intptr_t G_B25_1;
	memset((&G_B25_1), 0, sizeof(G_B25_1));
	intptr_t G_B25_2;
	memset((&G_B25_2), 0, sizeof(G_B25_2));
	intptr_t G_B29_0;
	memset((&G_B29_0), 0, sizeof(G_B29_0));
	intptr_t G_B29_1;
	memset((&G_B29_1), 0, sizeof(G_B29_1));
	intptr_t G_B28_0;
	memset((&G_B28_0), 0, sizeof(G_B28_0));
	intptr_t G_B28_1;
	memset((&G_B28_1), 0, sizeof(G_B28_1));
	intptr_t G_B30_0;
	memset((&G_B30_0), 0, sizeof(G_B30_0));
	intptr_t G_B30_1;
	memset((&G_B30_1), 0, sizeof(G_B30_1));
	intptr_t G_B30_2;
	memset((&G_B30_2), 0, sizeof(G_B30_2));
	intptr_t G_B34_0;
	memset((&G_B34_0), 0, sizeof(G_B34_0));
	intptr_t G_B34_1;
	memset((&G_B34_1), 0, sizeof(G_B34_1));
	intptr_t G_B33_0;
	memset((&G_B33_0), 0, sizeof(G_B33_0));
	intptr_t G_B33_1;
	memset((&G_B33_1), 0, sizeof(G_B33_1));
	intptr_t G_B35_0;
	memset((&G_B35_0), 0, sizeof(G_B35_0));
	intptr_t G_B35_1;
	memset((&G_B35_1), 0, sizeof(G_B35_1));
	intptr_t G_B35_2;
	memset((&G_B35_2), 0, sizeof(G_B35_2));
	Type_t* G_B40_0 = NULL;
	String_t* G_B40_1 = NULL;
	Type_t* G_B39_0 = NULL;
	String_t* G_B39_1 = NULL;
	String_t* G_B41_0 = NULL;
	String_t* G_B41_1 = NULL;
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_2;
		L_2 = AndroidReflection_IsPrimitive_mA41A9ECECE3D73679C79DC8B0FDD32B59570DF25(L_1, NULL);
		if (!L_2)
		{
			goto IL_021a;
		}
	}
	{
		bool L_3 = (Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_3)
		{
			goto IL_004c;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_4 = __this->___m_jclass;
		intptr_t L_5;
		L_5 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_4, NULL);
		intptr_t L_6 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_7, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		int32_t L_8;
		il2cpp_codegen_box_unbox(L_7, (&L_8), sizeof(int32_t), il2cpp_rgctx_data(method->rgctx_data, 1), Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetStaticIntField_m1E20F6C72260CAFBF73207DCEC1816B2816EEBE1(L_5, L_6, L_8, NULL);
		return;
	}

IL_004c:
	{
		bool L_9 = (Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_9)
		{
			goto IL_0084;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_10 = __this->___m_jclass;
		intptr_t L_11;
		L_11 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_10, NULL);
		intptr_t L_12 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_13, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		bool L_14;
		il2cpp_codegen_box_unbox(L_13, (&L_14), sizeof(bool), il2cpp_rgctx_data(method->rgctx_data, 1), Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetStaticBooleanField_mBE4E40DA1B07A29D356AEEE6CB9519F2B3621AC9(L_11, L_12, L_14, NULL);
		return;
	}

IL_0084:
	{
		bool L_15 = (Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_15)
		{
			goto IL_00c7;
		}
	}
	{
		CHECKED_LOCAL_INIT(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_StaticInit,(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Debug_LogWarning_m33EF1B897E0C7C6FF538989610BFAFFEF4628CA9(_stringLiteral1D86DE1FC0445C55F712ED0F6D3E32FED0A1E724, NULL);
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_16 = __this->___m_jclass;
		intptr_t L_17;
		L_17 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_16, NULL);
		intptr_t L_18 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_19, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		uint8_t L_20;
		il2cpp_codegen_box_unbox(L_19, (&L_20), sizeof(uint8_t), il2cpp_rgctx_data(method->rgctx_data, 1), Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var);
		int8_t L_21 = (il2cpp_codegen_conv<int8_t,uint8_t,int32_t,false,false>(L_20,NULL));
		AndroidJNISafe_SetStaticSByteField_m242120982A9227E1E8344FFE9F06FD74986D15E9(L_17, L_18, L_21, NULL);
		return;
	}

IL_00c7:
	{
		bool L_22 = (SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_22)
		{
			goto IL_00ff;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_23 = __this->___m_jclass;
		intptr_t L_24;
		L_24 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_23, NULL);
		intptr_t L_25 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_26, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		int8_t L_27;
		il2cpp_codegen_box_unbox(L_26, (&L_27), sizeof(int8_t), il2cpp_rgctx_data(method->rgctx_data, 1), SByte_tFEFFEF5D2FEBF5207950AE6FAC150FC53B668DB5_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetStaticSByteField_m242120982A9227E1E8344FFE9F06FD74986D15E9(L_24, L_25, L_27, NULL);
		return;
	}

IL_00ff:
	{
		bool L_28 = (Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_28)
		{
			goto IL_0137;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_29 = __this->___m_jclass;
		intptr_t L_30;
		L_30 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_29, NULL);
		intptr_t L_31 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_32, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		int16_t L_33;
		il2cpp_codegen_box_unbox(L_32, (&L_33), sizeof(int16_t), il2cpp_rgctx_data(method->rgctx_data, 1), Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetStaticShortField_m92534AAA86D7E1055E12936C8A7BD6B865B7DB81(L_30, L_31, L_33, NULL);
		return;
	}

IL_0137:
	{
		bool L_34 = (Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_34)
		{
			goto IL_016f;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_35 = __this->___m_jclass;
		intptr_t L_36;
		L_36 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_35, NULL);
		intptr_t L_37 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_38, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		int64_t L_39;
		il2cpp_codegen_box_unbox(L_38, (&L_39), sizeof(int64_t), il2cpp_rgctx_data(method->rgctx_data, 1), Int64_t092CFB123BE63C28ACDAF65C68F21A526050DBA3_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetStaticLongField_m299AAC2DE8B6747B0B5E109BABB2F3A4FC1F486E(L_36, L_37, L_39, NULL);
		return;
	}

IL_016f:
	{
		bool L_40 = (Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_40)
		{
			goto IL_01a7;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_41 = __this->___m_jclass;
		intptr_t L_42;
		L_42 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_41, NULL);
		intptr_t L_43 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_44, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		float L_45;
		il2cpp_codegen_box_unbox(L_44, (&L_45), sizeof(float), il2cpp_rgctx_data(method->rgctx_data, 1), Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetStaticFloatField_mB2EDDE632AB2088CD12F1FD12174FB86990BCBEE(L_42, L_43, L_45, NULL);
		return;
	}

IL_01a7:
	{
		bool L_46 = (Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_46)
		{
			goto IL_01df;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_47 = __this->___m_jclass;
		intptr_t L_48;
		L_48 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_47, NULL);
		intptr_t L_49 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_50, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		double L_51;
		il2cpp_codegen_box_unbox(L_50, (&L_51), sizeof(double), il2cpp_rgctx_data(method->rgctx_data, 1), Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetStaticDoubleField_mA0253927D476917D2158A9CE29F1BF535485B956(L_48, L_49, L_51, NULL);
		return;
	}

IL_01df:
	{
		bool L_52 = (Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_52)
		{
			goto IL_03aa;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_53 = __this->___m_jclass;
		intptr_t L_54;
		L_54 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_53, NULL);
		intptr_t L_55 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_56, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		Il2CppChar L_57;
		il2cpp_codegen_box_unbox(L_56, (&L_57), sizeof(Il2CppChar), il2cpp_rgctx_data(method->rgctx_data, 1), Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var);
		AndroidJNISafe_SetStaticCharField_m2B8245275C36525798C869B7B1088B25BA663613(L_54, L_55, L_57, NULL);
		return;
	}

IL_021a:
	{
		bool L_58 = (String_t_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_58)
		{
			goto IL_0252;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_59 = __this->___m_jclass;
		intptr_t L_60;
		L_60 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_59, NULL);
		intptr_t L_61 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_62, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		RuntimeObject* L_63 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_62);
		AndroidJNISafe_SetStaticStringField_m445D977B2374056C6E4607FAEDB7E99A1353E2EE(L_60, L_61, ((String_t*)CastclassSealed((RuntimeObject*)L_63, String_t_il2cpp_TypeInfo_var)), NULL);
		return;
	}

IL_0252:
	{
		bool L_64 = (AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_64)
		{
			goto IL_02a3;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_65 = __this->___m_jclass;
		intptr_t L_66;
		L_66 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_65, NULL);
		intptr_t L_67 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_68, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		bool L_69 = il2cpp_codegen_would_box_to_non_null(il2cpp_rgctx_data(method->rgctx_data, 1), L_68);
		if (!L_69)
		{
			G_B24_0 = L_67;
			G_B24_1 = L_66;
			goto IL_0298;
		}
		G_B23_0 = L_67;
		G_B23_1 = L_66;
	}
	{
		il2cpp_codegen_memcpy(L_70, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		RuntimeObject* L_71 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_70);
		NullCheck(((AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)CastclassClass((RuntimeObject*)L_71, AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var)));
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_72 = ((AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)((AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03*)CastclassClass((RuntimeObject*)L_71, AndroidJavaClass_tE6296B30CC4BF84434A9B765267F3FD0DD8DDB03_il2cpp_TypeInfo_var)))->___m_jclass;
		intptr_t L_73;
		L_73 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_72, NULL);
		G_B25_0 = L_73;
		G_B25_1 = G_B23_0;
		G_B25_2 = G_B23_1;
		goto IL_029d;
	}

IL_0298:
	{
		G_B25_0 = 0;
		G_B25_1 = G_B24_0;
		G_B25_2 = G_B24_1;
	}

IL_029d:
	{
		AndroidJNISafe_SetStaticObjectField_m7757F7E30F8122DAF89F138A8AE727CB896BC721(G_B25_2, G_B25_1, G_B25_0, NULL);
		return;
	}

IL_02a3:
	{
		bool L_74 = (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(method->rgctx_data, 1));
		if (!L_74)
		{
			goto IL_02f4;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_75 = __this->___m_jclass;
		intptr_t L_76;
		L_76 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_75, NULL);
		intptr_t L_77 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_78, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		bool L_79 = il2cpp_codegen_would_box_to_non_null(il2cpp_rgctx_data(method->rgctx_data, 1), L_78);
		if (!L_79)
		{
			G_B29_0 = L_77;
			G_B29_1 = L_76;
			goto IL_02e9;
		}
		G_B28_0 = L_77;
		G_B28_1 = L_76;
	}
	{
		il2cpp_codegen_memcpy(L_80, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		RuntimeObject* L_81 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_80);
		NullCheck(((AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)CastclassClass((RuntimeObject*)L_81, AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var)));
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_82 = ((AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0*)CastclassClass((RuntimeObject*)L_81, AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0_il2cpp_TypeInfo_var))->___m_jobject;
		intptr_t L_83;
		L_83 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_82, NULL);
		G_B30_0 = L_83;
		G_B30_1 = G_B28_0;
		G_B30_2 = G_B28_1;
		goto IL_02ee;
	}

IL_02e9:
	{
		G_B30_0 = 0;
		G_B30_1 = G_B29_0;
		G_B30_2 = G_B29_1;
	}

IL_02ee:
	{
		AndroidJNISafe_SetStaticObjectField_m7757F7E30F8122DAF89F138A8AE727CB896BC721(G_B30_2, G_B30_1, G_B30_0, NULL);
		return;
	}

IL_02f4:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_84 = { reinterpret_cast<intptr_t> (AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_85;
		L_85 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_84, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_86 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_87;
		L_87 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_86, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_88;
		L_88 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_85, L_87, NULL);
		if (!L_88)
		{
			goto IL_0340;
		}
	}
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_89 = __this->___m_jclass;
		intptr_t L_90;
		L_90 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_89, NULL);
		intptr_t L_91 = ___0_fieldID;
		il2cpp_codegen_memcpy(L_92, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		bool L_93 = il2cpp_codegen_would_box_to_non_null(il2cpp_rgctx_data(method->rgctx_data, 1), L_92);
		if (!L_93)
		{
			G_B34_0 = L_91;
			G_B34_1 = L_90;
			goto IL_0335;
		}
		G_B33_0 = L_91;
		G_B33_1 = L_90;
	}
	{
		il2cpp_codegen_memcpy(L_94, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		RuntimeObject* L_95 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_94);
		NullCheck(((AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D*)CastclassClass((RuntimeObject*)L_95, AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var)));
		intptr_t L_96;
		L_96 = AndroidJavaProxy_GetRawProxy_m685E066A4D378B596CD88385B954AE90CBF328A9(((AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D*)CastclassClass((RuntimeObject*)L_95, AndroidJavaProxy_tE5521F9761F7B95444B9C39FB15FDFC23F80A78D_il2cpp_TypeInfo_var)), NULL);
		G_B35_0 = L_96;
		G_B35_1 = G_B33_0;
		G_B35_2 = G_B33_1;
		goto IL_033a;
	}

IL_0335:
	{
		G_B35_0 = 0;
		G_B35_1 = G_B34_0;
		G_B35_2 = G_B34_1;
	}

IL_033a:
	{
		AndroidJNISafe_SetStaticObjectField_m7757F7E30F8122DAF89F138A8AE727CB896BC721(G_B35_2, G_B35_1, G_B35_0, NULL);
		return;
	}

IL_0340:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_97 = { reinterpret_cast<intptr_t> (RuntimeArray_0_0_0_var) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_98;
		L_98 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_97, NULL);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_99 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		Type_t* L_100;
		L_100 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_99, NULL);
		CHECKED_LOCAL_INIT(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_StaticInit,(AndroidReflection_tD59014B286F902906DBB75DA3473897D35684908_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_101;
		L_101 = AndroidReflection_IsAssignableFrom_mBAE0D5121AD208959B89E9BDAF62F0E0BAB917C6(L_98, L_100, NULL);
		if (!L_101)
		{
			goto IL_037f;
		}
	}
	{
		il2cpp_codegen_memcpy(L_102, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_tCE4037CFD91325CEB0ECC8FFA61A181595DF67F7);
		RuntimeObject* L_103 = Box(il2cpp_rgctx_data(method->rgctx_data, 1), L_102);
		intptr_t L_104;
		L_104 = AndroidJNIHelper_ConvertToJNIArray_mBEAE4605FF297D19AFB8CE4E8443C9C0F87E9A13(((RuntimeArray*)CastclassClass((RuntimeObject*)L_103, RuntimeArray_il2cpp_TypeInfo_var)), NULL);
		V_0 = L_104;
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_105 = __this->___m_jclass;
		intptr_t L_106;
		L_106 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_105, NULL);
		intptr_t L_107 = ___0_fieldID;
		intptr_t L_108 = V_0;
		AndroidJNISafe_SetStaticObjectField_m7757F7E30F8122DAF89F138A8AE727CB896BC721(L_106, L_107, L_108, NULL);
		return;
	}

IL_037f:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_109 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(method->rgctx_data, 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_110;
		L_110 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_109, NULL);
		Type_t* L_111 = L_110;
		if (L_111)
		{
			G_B40_0 = L_111;
			G_B40_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
			goto IL_0395;
		}
		G_B39_0 = L_111;
		G_B39_1 = ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral45BE536C9F5116AD2AF9F35AA7EF84EC0980CA92));
	}
	{
		G_B41_0 = ((String_t*)(NULL));
		G_B41_1 = G_B39_1;
		goto IL_039a;
	}

IL_0395:
	{
		NullCheck(G_B40_0);
		String_t* L_112;
		L_112 = VirtualFuncInvoker0< String_t* >::Invoke(3, G_B40_0);
		G_B41_0 = L_112;
		G_B41_1 = G_B40_1;
	}

IL_039a:
	{
		String_t* L_113;
		L_113 = String_Concat_m8855A6DE10F84DA7F4EC113CADDB59873A25573B(G_B41_1, G_B41_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral42646B33B50B6AA15E22733C8900716F0FE19E1D)), NULL);
		Exception_t* L_114 = (Exception_t*)il2cpp_codegen_object_new(Exception_t_il2cpp_TypeInfo_var);
		Exception__ctor_m9B2BD92CD68916245A75109105D9071C9D430E7F(L_114, L_113, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_114, method);
	}

IL_03aa:
	{
		return;
	}
}
// Method Definition Index: 81743
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AndroidJavaObject__SetStatic_TisIl2CppFullySharedGenericAny_mA923EF05918D13946E2452B81B496229D8B952FB_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* __this, String_t* ___0_fieldName, Il2CppFullySharedGenericAny ___1_val, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	const uint32_t SizeOf_FieldType_t0374C27CAE86F7DA1E32463BA4A51CE6B68D02E1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(method->rgctx_data, 1));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_FieldType_t0374C27CAE86F7DA1E32463BA4A51CE6B68D02E1);
	//<source_info:<no-source>:1>
	intptr_t V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = __this->___m_jclass;
		intptr_t L_1;
		L_1 = GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline(L_0, NULL);
		String_t* L_2 = ___0_fieldName;
		intptr_t L_3;
		L_3 = ((  intptr_t (*) (intptr_t, String_t*, bool, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 0)))(L_1, L_2, (bool)1, il2cpp_rgctx_method(method->rgctx_data, 0));
		V_0 = L_3;
		intptr_t L_4 = V_0;
		il2cpp_codegen_memcpy(L_5, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? ___1_val : &___1_val), SizeOf_FieldType_t0374C27CAE86F7DA1E32463BA4A51CE6B68D02E1);
		InvokerActionInvoker2< intptr_t, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 2)), il2cpp_rgctx_method(method->rgctx_data, 2), __this, L_4, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(method->rgctx_data, 1)) ? L_5: *(void**)L_5));
		return;
	}
}
// Method Definition Index: 76585
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AndroidJavaObjectExtensions_Enumerate_TisIl2CppSharedGenericObject_m57683981D3696BEDAAE2737CCDBE03B01A3EA0A1_gshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* ___0_androidJavaList, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3EE5A4671A4E5AEBD31CA5F013A7773DC18ED22B);
		il2cpp_rgctx_method_init(method);
	}
	//<source_info:<no-source>:1>
	U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6* V_0 = NULL;
	int32_t V_1 = 0;
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* G_B2_0 = NULL;
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* G_B1_0 = NULL;
	int32_t G_B3_0 = 0;
	{
		U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6* L_0 = (U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->rgctx_data, 0));
		U3CU3Ec__DisplayClass0_0_1__ctor_mF0054910492777A7D64055D8F895832306C7FF6B(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6* L_1 = V_0;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_2 = ___0_androidJavaList;
		NullCheck(L_1);
		L_1->___androidJavaList = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___androidJavaList), (void*)L_2);
		U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6* L_3 = V_0;
		NullCheck(L_3);
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_4 = L_3->___androidJavaList;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_5 = L_4;
		if (L_5)
		{
			G_B2_0 = L_5;
			goto IL_001a;
		}
		G_B1_0 = L_5;
	}
	{
		G_B3_0 = 0;
		goto IL_0029;
	}

IL_001a:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6;
		L_6 = Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_inline(Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var);
		NullCheck(G_B2_0);
		int32_t L_7;
		L_7 = AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC(G_B2_0, _stringLiteral3EE5A4671A4E5AEBD31CA5F013A7773DC18ED22B, L_6, AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC_RuntimeMethod_var);
		G_B3_0 = L_7;
	}

IL_0029:
	{
		V_1 = G_B3_0;
		int32_t L_8 = V_1;
		RuntimeObject* L_9;
		L_9 = Enumerable_Range_m4FB9BBBA09BEF5177C13506DB385CF4467C15FD8(0, L_8, NULL);
		U3CU3Ec__DisplayClass0_0_1_t5D0A4A305FD42EFB3743B0C3BF642A856CC2E2A6* L_10 = V_0;
		Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A* L_11 = (Func_2_t8D1CDC1953F408CBE801D1954A3A00249853A94A*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->rgctx_data, 3));
		Func_2__ctor_m266186AD54086DE797B83FC2FF930C212522BB9A(L_11, (RuntimeObject*)L_10, (intptr_t)((void*)il2cpp_rgctx_method(method->rgctx_data, 2)), NULL);
		RuntimeObject* L_12;
		L_12 = Enumerable_Select_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_TisIl2CppSharedGenericObject_m9A1A985DC3BC579FA94A34322A551416C1A99804(L_9, L_11, il2cpp_rgctx_method(method->rgctx_data, 5));
		List_1_t6959D78D53022948E65A4FDA6291D7F38FEFA02E* L_13;
		L_13 = Enumerable_ToList_TisIl2CppSharedGenericObject_mFF96E16527D742B009531AEA8672FF89A8A379E2(L_12, il2cpp_rgctx_method(method->rgctx_data, 6));
		return (RuntimeObject*)L_13;
	}
}
// Method Definition Index: 76585
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AndroidJavaObjectExtensions_Enumerate_TisIl2CppFullySharedGenericAny_mABD6D48B9BACFE0C3AEA2BB2A0A5E042FEA4D0CA_fshared (AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* ___0_androidJavaList, const RuntimeMethod* method) 
{
	if (!il2cpp_rgctx_is_initialized(method))
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3EE5A4671A4E5AEBD31CA5F013A7773DC18ED22B);
		il2cpp_rgctx_method_init(method);
	}
	//<source_info:<no-source>:1>
	U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5* V_0 = NULL;
	int32_t V_1 = 0;
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* G_B2_0 = NULL;
	AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* G_B1_0 = NULL;
	int32_t G_B3_0 = 0;
	{
		U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5* L_0 = (U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->rgctx_data, 0));
		((  void (*) (U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 1)))(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5* L_1 = V_0;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_2 = ___0_androidJavaList;
		NullCheck(L_1);
		L_1->___androidJavaList = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___androidJavaList), (void*)L_2);
		U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5* L_3 = V_0;
		NullCheck(L_3);
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_4 = L_3->___androidJavaList;
		AndroidJavaObject_t8FFB930F335C1178405B82AC2BF512BB1EEF9EB0* L_5 = L_4;
		if (L_5)
		{
			G_B2_0 = L_5;
			goto IL_001a;
		}
		G_B1_0 = L_5;
	}
	{
		G_B3_0 = 0;
		goto IL_0029;
	}

IL_001a:
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6;
		L_6 = Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_inline(Array_Empty_TisRuntimeObject_mFB8A63D602BB6974D31E20300D9EB89C6FE7C278_RuntimeMethod_var);
		NullCheck(G_B2_0);
		int32_t L_7;
		L_7 = AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC(G_B2_0, _stringLiteral3EE5A4671A4E5AEBD31CA5F013A7773DC18ED22B, L_6, AndroidJavaObject_Call_TisInt32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_mDC5FD095AFC55DFE596907E5B055B5774DA5B5AC_RuntimeMethod_var);
		G_B3_0 = L_7;
	}

IL_0029:
	{
		V_1 = G_B3_0;
		int32_t L_8 = V_1;
		RuntimeObject* L_9;
		L_9 = Enumerable_Range_m4FB9BBBA09BEF5177C13506DB385CF4467C15FD8(0, L_8, NULL);
		U3CU3Ec__DisplayClass0_0_1_t96EB6435AF4BFC4AD00B521F74032B501E9BECA5* L_10 = V_0;
		Func_2_t8C8978DB83B81CEAC25C00451C27A47420AA8F84* L_11 = (Func_2_t8C8978DB83B81CEAC25C00451C27A47420AA8F84*)il2cpp_codegen_object_new(il2cpp_rgctx_data(method->rgctx_data, 3));
		((  void (*) (Func_2_t8C8978DB83B81CEAC25C00451C27A47420AA8F84*, RuntimeObject*, intptr_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 4)))(L_11, (RuntimeObject*)L_10, (intptr_t)((void*)il2cpp_rgctx_method(method->rgctx_data, 2)), NULL);
		RuntimeObject* L_12;
		L_12 = ((  RuntimeObject* (*) (RuntimeObject*, Func_2_t8C8978DB83B81CEAC25C00451C27A47420AA8F84*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 5)))(L_9, L_11, il2cpp_rgctx_method(method->rgctx_data, 5));
		List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* L_13;
		L_13 = ((  List_1_tDBA89B0E21BAC58CFBD3C1F76E4668E3B562761A* (*) (RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(method->rgctx_data, 6)))(L_12, il2cpp_rgctx_method(method->rgctx_data, 6));
		return (RuntimeObject*)L_13;
	}
}
// Method Definition Index: 84253
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnimationPlayableExtensions_SetAnimatedProperties_TisPlayable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_m513792C14506E4E0171D0CBFB99F439319D73802 (Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F ___0_playable, AnimationClip_t00BD2F131D308A4AD2C6B0BF66644FC25FECE712* ___1_clip, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_StaticInit);
	//<source_info:<no-source>:1>
	PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		CHECKED_LOCAL_INIT(Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_StaticInit,(Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0;
		L_0 = Playable_GetHandle_m39356D23E849DC5428B262092657662C064E04F8_inline((&___0_playable), NULL);
		V_0 = L_0;
		AnimationClip_t00BD2F131D308A4AD2C6B0BF66644FC25FECE712* L_1 = ___1_clip;
		AnimationPlayableExtensions_SetAnimatedPropertiesInternal_m9038247416A38E252EF0DCBCCBFE990589F4C51F((&V_0), L_1, NULL);
		return;
	}
}
// Method Definition Index: 83262
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Type_t* LifecycleScopeKey_get_Type_mEC585239B2BC794EDFDE68B89A6533971E12C752_inline (LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Type_t* L_0 = __this->____scopeType;
		return L_0;
	}
}
// Method Definition Index: 83128
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* ScopeTransitionRequestBase_get_Scope_mEB7F256D6FF4393EB627213D2E7343BD35BDCB50_inline (ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_0 = __this->___U3CScopeU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 83130
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t ScopeTransitionRequestBase_get_TransitionType_m64E9CBFD2036C2727F62B4276B07594B1AD0244B_inline (ScopeTransitionRequestBase_t9E5F91D84A64F389496153A6DA14A6686E4E3C16* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->___U3CTransitionTypeU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 83239
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* LifecycleScopeBase_get_Name_m9A358255FEBBEA5F94FCB82DC2B1AF1F8AB288BB_inline (LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		String_t* L_0 = __this->___U3CNameU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 83081
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* ActiveLifecycleScopeBase_get_Scope_m711F6C219BF9AED00688EB5C251A74C6780E0780_inline (ActiveLifecycleScopeBase_t0F0C7F8D06A577773F8D661F8CCE4F9FA9999A8C* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		LifecycleScopeBase_t7CC30D9721A69BA0A9E955EAED505A32007C0755* L_0 = __this->___U3CScopeU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 83180
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool DebugLifecycle_get_LoggingEnabled_mADB463016DF27BC61A483A646E5B999ABB3A8D94_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticInit,(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		bool L_0 = ((DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_StaticFields*)il2cpp_codegen_static_fields_for(DebugLifecycle_tDEBE1FE9649A80BE545B4DD634205A3880487E11_il2cpp_TypeInfo_var))->___loggingEnabled;
		return L_0;
	}
}
// Method Definition Index: 3915
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool IntPtr_op_Equality_m7D9CDCDE9DC2A0C2C614633F4921E90187FAB271_inline (intptr_t ___0_value1, intptr_t ___1_value2, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_value1;
		intptr_t L_1 = ___1_value2;
		return (bool)((((intptr_t)L_0) == ((intptr_t)L_1))? 1 : 0);
	}
}
// Method Definition Index: 81669
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR intptr_t GlobalJavaObjectRef_op_Implicit_m444B263750F9B778C87C30EA918CDC0B62271F24_inline (GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		GlobalJavaObjectRef_t20D8E5AAFC2EB2518FCABBF40465855E797FF0D8* L_0 = ___0_obj;
		NullCheck(L_0);
		intptr_t L_1 = L_0->___m_jobject;
		return L_1;
	}
}
// Method Definition Index: 57350
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 Playable_GetHandle_m39356D23E849DC5428B262092657662C064E04F8_inline (Playable_t95C6B795846BA0C7D96E4DA14897CCCF2554334F* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		PlayableHandle_t5D6A01EF94382EFEDC047202F71DF882769654D4 L_0 = __this->___m_Handle;
		return L_0;
	}
}
// Method Definition Index: 11280
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D Enumerator_get_Current_m9C78D88F0F2E50F8F748978518338E28736E4522_gshared_inline (Enumerator_t1A261715170E73393537C60CCC568DE6296C8F99* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		LifecycleScopeKey_t786C4635697BF09DC1C3BA0331E34017B8BA961D L_0 = __this->____currentKey;
		return L_0;
	}
}
// Method Definition Index: 83272
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* LifecycleScopeWithContext_1_get_Context_m71715E88B71B9A07D4F70E53A5142B8F5383AC27_gshared_inline (LifecycleScopeWithContext_1_t35373E7691E4D4045644241660D92D3E94032599* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Il2CppSharedGenericObject* L_0 = __this->___U3CContextU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 11300
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* Enumerator_get_Current_mFB92CF0C22891AA06481368B806933878E96848C_gshared_inline (Enumerator_tADBE53A4D403A692FEE79405C385A6A1DC739F7D* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Il2CppSharedGenericObject* L_0 = __this->____currentValue;
		return L_0;
	}
}
// Method Definition Index: 11410
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Il2CppSharedGenericObject* Enumerator_get_Current_mA50CED82C4671CC4E1D82333FAC2587F700565D0_gshared_inline (Enumerator_tC367FBE981D257FF6A6357382526F6EC9FF3B2F9* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Il2CppSharedGenericObject* L_0 = __this->____current;
		return L_0;
	}
}
// Method Definition Index: 53935
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool NativeArray_1_get_IsCreated_m527A2C3B75C25BFF29D1D9EA88C81172FF4F5A5A_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		void* L_0 = __this->___m_Buffer;
		uintptr_t L_1 = (il2cpp_codegen_conv<uintptr_t,int32_t,int32_t,false,false>(0,NULL));
		return (bool)((((int32_t)((((intptr_t)L_0) == ((intptr_t)L_1))? 1 : 0)) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 54020
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void* NativeArrayUnsafeUtility_GetUnsafePtr_TisIl2CppFullySharedGenericStruct_m4AB802D5B1B296D0F976C1E7631699B0C4A4D00F_fshared_inline (NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 ___0_nativeArray, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NativeArray_1_tDB8B8DC66CC8E16ED6D9A8C75D2C1AFC80AC1E18 L_0 = ___0_nativeArray;
		void* L_1 = L_0.___m_Buffer;
		return L_1;
	}
}
// Method Definition Index: 2479
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Span_1__ctor_mA426411A0F5450BCD564C621BB663B1273773B99_inline (Span_1_t968992D6F95715A2C7F64EDDA83CD37C8C7CBCD7* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		goto IL_0016;
	}

IL_0016:
	{
		int32_t L_0 = ___1_length;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_001f;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_001f:
	{
		void* L_1 = ___0_pointer;
		jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225* L_2;
		L_2 = il2cpp_unsafe_as_ref<jvalue_t1756CE401EE222450C9AD0B98CB30E213D4A3225>((uint8_t*)L_1);
		ByReference_1_tE0748F88701C64E729013351521FC3EED28DE1DC L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2493
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline (Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____length;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR __CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* Array_Empty_TisIl2CppSharedGenericObject_m688909407CF299AE69145CC33ACB1E90C775C653_gshared_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		__CanonU5BU5D_tFF96AE6C231BB36A6CEE54CEEB72ED8E90201979* L_0 = ((EmptyArray_1_tF2F83411E5DC963DBFFD1F75B10B58A1ED224DD5_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
