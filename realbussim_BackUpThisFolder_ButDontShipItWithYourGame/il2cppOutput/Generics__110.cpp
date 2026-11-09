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
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*,T1,const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj,p1,invokeData.method);
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
template <typename T1, typename T2>
struct InterfaceActionInvoker2Invoker;
template <typename T1, typename T2>
struct InterfaceActionInvoker2Invoker<T1*, T2*>
{
	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1* p1, T2* p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		void* params[2] = { p1, p2 };
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, params, params[1]);
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
template <typename R, typename T1>
struct InterfaceFuncInvoker1
{
	typedef R (*Func)(void*,T1,const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj,p1,invokeData.method);
	}
};
template <typename R, typename T1>
struct InterfaceFuncInvoker1Invoker;
template <typename R, typename T1>
struct InterfaceFuncInvoker1Invoker<R, T1*>
{
	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1* p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		R ret;
		void* params[1] = { p1 };
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, params, &ret);
		return ret;
	}
};
template <typename R, typename T1, typename T2>
struct InterfaceFuncInvoker2Invoker;
template <typename R, typename T1, typename T2>
struct InterfaceFuncInvoker2Invoker<R, T1*, T2*>
{
	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1* p1, T2* p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		R ret;
		void* params[2] = { p1, p2 };
		invokeData.method->invoker_method(il2cpp_codegen_get_method_pointer(invokeData.method), invokeData.method, obj, params, &ret);
		return ret;
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
struct InvokerActionInvoker2<T1*, T2*>
{
	static inline void Invoke (Il2CppMethodPointer methodPtr, const RuntimeMethod* method, void* obj, T1* p1, T2* p2)
	{
		void* params[2] = { p1, p2 };
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

struct ICollection_1_tB388ED908E2D241F56264CA971F25D9ADC1ADEC3;
struct IDictionary_2_t4D3B65115D85F2C21975A2BBF4A23860F8BCD02F;
struct IEnumerable_1_t29E7244AE33B71FA0981E50D5BC73B7938F35C66;
struct IEnumerator_1_tFABD3B897F1296469E9A2DB9BCF6C89439049208;
struct IEnumerator_1_t039FD1F79231873CA31A318A129D0F6366AE3729;
struct IEnumerator_1_tCF3C4D7636A086A62025E9F59CD37C3A0F57BEE5;
struct IEnumerator_1_t05AB4B707B5C100437510D3CD21B098CF9B1B3E2;
struct IEnumerator_1_t15BDEA63728C9199E0531C862F5C850A3D0C29B8;
struct IList_1_t7DC7A22F42EEFA1E8D796516A9F22282DF4EB5FA;
struct IList_1_t8AC59FFD0F90EC2DAD4C3FA39B2A5851F9D2987A;
struct IList_1_t45147F5693AC2CD8E1FC02E6C69D23EC082D597B;
struct IList_1_t22C1E3F54860102CFEA7369D35F4FD35972105B6;
struct IList_1_t167843A5F05138E45B11C0C0C05948EC417AFA03;
struct IList_1_tFAAC62CDDF5DFAD071729740E8797D4AC890A2CE;
struct KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221;
struct ObservableCollection_1_tE47F2087FEB0CBCB2B73E995AF74710B883ABC62;
struct ObservableCollection_1_t88F7DA7F7087023443CFAD5538FB4B4B561318A8;
struct ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263;
struct ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8;
struct ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235;
struct ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730;
struct ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3;
struct ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9;
struct ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734;
struct ReadOnlyObservableCollection_1_tDDB997B5588C61C7ACD439AF6BF94358E788B352;
struct ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6;
struct SimpleMonitor_t27F1F10F194FAC35A771426BFE1E3D9142688A76;
struct SimpleMonitor_tB9FB1EDE482325676967121CD31A0F99524EEF7F;
struct ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849;
struct KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57;
struct SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06;
struct ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F;
struct AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A;
struct BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80;
struct BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB;
struct BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213;
struct BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4;
struct BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389;
struct Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259;
struct CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093;
struct ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A;
struct ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38;
struct ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2;
struct CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5;
struct CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533;
struct DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0;
struct DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB;
struct EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49;
struct EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1;
struct EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9;
struct FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58;
struct FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27;
struct FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643;
struct GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
struct EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B;
struct ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498;
struct ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73;
struct RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316;
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263;
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129;
struct ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F;
struct BatchDrawCommand_t9DAB9B739DA9EBC6B1BB0D2DD51AF65B12F66BF0;
struct BatchDrawCommandIndirect_t06F3B5DC4AF7D4B115CEECDB0A1E4E92065BC705;
struct BatchDrawCommandProcedural_t5780BD7745B5772AA54BA6E6021292F6A6553B86;
struct BatchDrawCommandProceduralIndirect_t8417CDF3495237CD8C9204B3DADC6BA4A1488B50;
struct BatchDrawRange_t736D745E918A629658B80A0F7D2D17DAFBA6172F;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct Delegate_t;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8;
struct Entry_t475ED76E31923970E7F7A6522E570E7577C487B9;
struct ICollection_t37E7B9DC5B4EF41D190D607F92835BF1171C0E8E;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IDictionaryEnumerator_tE129D608FCDB7207E0F0ECE33473CC950A83AD16;
struct IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA;
struct IList_t1C522956D79B7DC92B5B01053DF1AC058C8B598D;
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct MethodInfo_t;
struct NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A;
struct NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA;
struct NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF;
struct PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321;
struct PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA;
struct RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C;
struct String_t;
struct Type_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ICollection_t37E7B9DC5B4EF41D190D607F92835BF1171C0E8E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerable_t6331596D5DD37C462B1B8D49CF6B319B00AB7131_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* INotifyCollectionChanged_tD2E4219A01AB2BD93859A30D924324BE793C2806_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* INotifyPropertyChanged_tF956261A15128123778A0D86196231401847CF8C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RuntimeObject_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* String_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral2B6D6F48C27C60C3B55391AB377D9DC8F5639AA1;
IL2CPP_EXTERN_C String_t* _stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D;
IL2CPP_EXTERN_C String_t* _stringLiteral38E3DBC7FC353425EF3A98DC8DAC6689AF5FD1BE;
IL2CPP_EXTERN_C String_t* _stringLiteral3ECE023333DCF45DE7B1FEAFFE30E295210DDD9B;
IL2CPP_EXTERN_C String_t* _stringLiteral6195D7DA68D16D4985AD1A1B4FD2841A43CDDE70;
IL2CPP_EXTERN_C String_t* _stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D;
IL2CPP_EXTERN_C String_t* _stringLiteral958BF5561D89C9A90653416FCB65BBE72DADDE1E;
IL2CPP_EXTERN_C String_t* _stringLiteral967D403A541A1026A83D548E5AD5CA800AD4EFB5;
IL2CPP_EXTERN_C String_t* _stringLiteralB829404B947F7E1629A30B5E953A49EB21CCD2ED;
IL2CPP_EXTERN_C String_t* _stringLiteralBD0381A992FDF4F7DA60E5D83689FE7FF6309CB8;
IL2CPP_EXTERN_C String_t* _stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB;
IL2CPP_EXTERN_C String_t* _stringLiteralE7D028CCE3B6E7B61AE2C752D7AE970DA04AB7C6;
IL2CPP_EXTERN_C String_t* _stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57;
struct SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06;
struct ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F;
struct AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A;
struct BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80;
struct BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB;
struct BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213;
struct BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4;
struct BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389;
struct Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259;
struct CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093;
struct ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A;
struct ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38;
struct ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2;
struct CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5;
struct CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61;
struct DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533;
struct DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0;
struct DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB;
struct EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49;
struct EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1;
struct EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9;
struct FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58;
struct FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27;
struct FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643;
struct GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC;
struct EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B;
struct ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498;
struct ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73;
struct RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct Collection_1_t6C2C233A06E9C99D480D378D80FC0DDC0DE67F37  : public RuntimeObject
{
	RuntimeObject* ___items;
};
struct Collection_1_t64F5801B7817C08B013BC2F8613C49D0A43A562E  : public RuntimeObject
{
	RuntimeObject* ___items;
};
struct EmptyArray_1_t0542F0EB5C43D54C8F4ADCAB1E94F18AEA151867  : public RuntimeObject
{
};
struct EmptyArray_1_t910BE047C1A6E9E334831BFBF9773D3C3852ED95  : public RuntimeObject
{
};
struct EmptyArray_1_t5B5242ED623249C0903BF0800195DB10397B3BCD  : public RuntimeObject
{
};
struct EmptyArray_1_tA6E339CF983886C190886835190061C2E3B679F2  : public RuntimeObject
{
};
struct EmptyArray_1_tC8FD7AE710007FB0A9059B5125949067B761A8E5  : public RuntimeObject
{
};
struct EmptyArray_1_t234239C1AFE29F273AE44FBF753D3916E498AC5C  : public RuntimeObject
{
};
struct EmptyArray_1_t7902087DD0C0221C9C7362ABAB7CB57D6D519A99  : public RuntimeObject
{
};
struct EmptyArray_1_t42D5099F7FCE2834141926DB372392B8C75ADBEC  : public RuntimeObject
{
};
struct EmptyArray_1_t7187E746F328254739F076CFBCAABB28D4B4554C  : public RuntimeObject
{
};
struct EmptyArray_1_t7BBC8CED754F364A777871A238BBBE3F94FFDDE1  : public RuntimeObject
{
};
struct EmptyArray_1_t9FC4EAC7DDBFA164B697CFEBFF39A52B321CC8C9  : public RuntimeObject
{
};
struct EmptyArray_1_tC72AE83DCB4E240E4B2AC2595448148BB03F9498  : public RuntimeObject
{
};
struct EmptyArray_1_tF7ADA3AE49000F6D4933A7D7B32DDF6230630DC0  : public RuntimeObject
{
};
struct EmptyArray_1_tF01C5B6CDD56CDA122E6B85402777F31EE92A685  : public RuntimeObject
{
};
struct EmptyArray_1_tCE52674ADF71EC22E199D7DF299141790948751A  : public RuntimeObject
{
};
struct EmptyArray_1_tB029523A05EA2FFE1EB90490D66A384F7A0D4574  : public RuntimeObject
{
};
struct EmptyArray_1_t0605DD263F295CE065C4B24BC0263FB5067617EC  : public RuntimeObject
{
};
struct EmptyArray_1_t27401338DAB1F4C8F9B8E695AB18ABBBF6573E17  : public RuntimeObject
{
};
struct EmptyArray_1_tD918BE070C4FAED797D16A5616D8A88DC319BF09  : public RuntimeObject
{
};
struct EmptyArray_1_t640F34C5C1351444FCE089711FFEF74161372D64  : public RuntimeObject
{
};
struct EmptyArray_1_t7B0255D5BEE03D8E592C7F4DB26CEE268619FD74  : public RuntimeObject
{
};
struct EmptyArray_1_t662659478531CA2736931D12958F36C14566158B  : public RuntimeObject
{
};
struct EmptyArray_1_t9D02D6D1FF8E81441083A08AA0F8970E511F0900  : public RuntimeObject
{
};
struct EmptyArray_1_t2AA231E0C904F8398D206EDF41018BEF22BD2325  : public RuntimeObject
{
};
struct EmptyArray_1_t2D4836FD2685619474AF41CBDA7A6813DB13F5CA  : public RuntimeObject
{
};
struct EmptyArray_1_t00CB7A66F677EBAF03269ABB2C35F4597F8AFA77  : public RuntimeObject
{
};
struct EmptyArray_1_t3264D01575877F752602C31CBF848A455D557C15  : public RuntimeObject
{
};
struct KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221  : public RuntimeObject
{
	RuntimeObject* ____collection;
	RuntimeObject* ____syncRoot;
};
struct MemoryManager_1_tB90442C8E0A1B9C0F8A3B603FD50501A1BADAC6E  : public RuntimeObject
{
};
struct MemoryManager_1_t9E713A8E4F8473E685FE4A73C98D29C9DB1EE583  : public RuntimeObject
{
};
struct MemoryManager_1_tED55276A18D77758B15A7A09B9C0D4DB79480487  : public RuntimeObject
{
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
struct ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9  : public RuntimeObject
{
	RuntimeObject* ___list;
	RuntimeObject* ____syncRoot;
};
struct ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734  : public RuntimeObject
{
	RuntimeObject* ___m_dictionary;
	RuntimeObject* ____syncRoot;
	KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* ____keys;
	ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* ____values;
};
struct ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849  : public RuntimeObject
{
	RuntimeObject* ____collection;
	RuntimeObject* ____syncRoot;
};
struct EventArgs_t37273F03EAC87217701DD431B190FBD84AD7C377  : public RuntimeObject
{
};
struct MemberInfo_t  : public RuntimeObject
{
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
struct DictionaryEnumerator_tA07B53C91CCF243B3DDF48825CD4F71A6835CD77 
{
	RuntimeObject* ____dictionary;
	RuntimeObject* ____enumerator;
};
typedef Il2CppFullySharedGenericStruct KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669;
struct Memory_1_tB7CEF4416F5014E364267478CEF016A4AC5C0036 
{
	RuntimeObject* ____object;
	int32_t ____index;
	int32_t ____length;
};
#ifndef Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_pinvoke_define
#define Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_pinvoke_define
struct Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_pinvoke
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
#ifndef Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_com_define
#define Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_com_define
struct Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_com
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
struct Memory_1_t48D1B1093D181D6F37155EB644659FD7D1DF616E 
{
	RuntimeObject* ____object;
	int32_t ____index;
	int32_t ____length;
};
#ifndef Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_pinvoke_define
#define Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_pinvoke_define
struct Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_pinvoke
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
#ifndef Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_com_define
#define Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_com_define
struct Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_com
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
struct Memory_1_t762D61B435521D1706EEDC25CA8C8EA1AE19AFEA 
{
	RuntimeObject* ____object;
	int32_t ____index;
	int32_t ____length;
};
#ifndef Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_pinvoke_define
#define Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_pinvoke_define
struct Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_pinvoke
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
#ifndef Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_com_define
#define Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_com_define
struct Memory_1_t56F63672B8E752B13E0BBBBD034BA3C1F6CFDC17_marshaled_com
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
struct ObservableCollection_1_tE47F2087FEB0CBCB2B73E995AF74710B883ABC62  : public Collection_1_t6C2C233A06E9C99D480D378D80FC0DDC0DE67F37
{
	SimpleMonitor_t27F1F10F194FAC35A771426BFE1E3D9142688A76* ____monitor;
	int32_t ____blockReentrancyCount;
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* ___CollectionChanged;
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* ___PropertyChanged;
};
struct ObservableCollection_1_t88F7DA7F7087023443CFAD5538FB4B4B561318A8  : public Collection_1_t64F5801B7817C08B013BC2F8613C49D0A43A562E
{
	SimpleMonitor_tB9FB1EDE482325676967121CD31A0F99524EEF7F* ____monitor;
	int32_t ____blockReentrancyCount;
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* ___CollectionChanged;
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* ___PropertyChanged;
};
struct ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399 
{
	RuntimeObject* ____object;
	int32_t ____index;
	int32_t ____length;
};
#ifndef ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_pinvoke_define
#define ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_pinvoke_define
struct ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_pinvoke
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
#ifndef ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_com_define
#define ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_com_define
struct ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_com
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
struct ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6 
{
	RuntimeObject* ____object;
	int32_t ____index;
	int32_t ____length;
};
#ifndef ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_pinvoke_define
#define ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_pinvoke_define
struct ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_pinvoke
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
#ifndef ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_com_define
#define ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_com_define
struct ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_com
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
struct ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 
{
	RuntimeObject* ____object;
	int32_t ____index;
	int32_t ____length;
};
#ifndef ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_pinvoke_define
#define ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_pinvoke_define
struct ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_pinvoke
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
#ifndef ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_com_define
#define ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_com_define
struct ReadOnlyMemory_1_t766DD3EE24B08138FB23CBC5B315D83C6E1272F5_marshaled_com
{
	Il2CppIUnknown* ____object;
	int32_t ____index;
	int32_t ____length;
};
#endif
struct ReadOnlyObservableCollection_1_tDDB997B5588C61C7ACD439AF6BF94358E788B352  : public ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263
{
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* ___CollectionChanged;
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* ___PropertyChanged;
};
struct ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6  : public ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8
{
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* ___CollectionChanged;
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* ___PropertyChanged;
};
struct BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D 
{
	BatchDrawCommand_t9DAB9B739DA9EBC6B1BB0D2DD51AF65B12F66BF0* ___drawCommands;
	BatchDrawCommandIndirect_t06F3B5DC4AF7D4B115CEECDB0A1E4E92065BC705* ___indirectDrawCommands;
	BatchDrawCommandProcedural_t5780BD7745B5772AA54BA6E6021292F6A6553B86* ___proceduralDrawCommands;
	BatchDrawCommandProceduralIndirect_t8417CDF3495237CD8C9204B3DADC6BA4A1488B50* ___proceduralIndirectDrawCommands;
	int32_t* ___visibleInstances;
	BatchDrawRange_t736D745E918A629658B80A0F7D2D17DAFBA6172F* ___drawRanges;
	float* ___instanceSortingPositions;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* ___drawCommandPickingEntityIds;
	int32_t ___drawCommandCount;
	int32_t ___indirectDrawCommandCount;
	int32_t ___proceduralDrawCommandCount;
	int32_t ___proceduralIndirectDrawCommandCount;
	int32_t ___visibleInstanceCount;
	int32_t ___drawRangeCount;
	int32_t ___instanceSortingPositionFloatCount;
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
struct Color_tD001788D726C3A7F1379BEED0260B9591F440C1F 
{
	float ___r;
	float ___g;
	float ___b;
	float ___a;
};
struct Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B 
{
	union
	{
		#pragma pack(push, tp, 1)
		struct
		{
			int32_t ___rgba;
		};
		#pragma pack(pop, tp)
		struct
		{
			int32_t ___rgba_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			uint8_t ___r;
		};
		#pragma pack(pop, tp)
		struct
		{
			uint8_t ___r_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___g_OffsetPadding[1];
			uint8_t ___g;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___g_OffsetPadding_forAlignmentOnly[1];
			uint8_t ___g_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___b_OffsetPadding[2];
			uint8_t ___b;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___b_OffsetPadding_forAlignmentOnly[2];
			uint8_t ___b_forAlignmentOnly;
		};
		#pragma pack(push, tp, 1)
		struct
		{
			char ___a_OffsetPadding[3];
			uint8_t ___a;
		};
		#pragma pack(pop, tp)
		struct
		{
			char ___a_OffsetPadding_forAlignmentOnly[3];
			uint8_t ___a_forAlignmentOnly;
		};
	};
};
struct DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB 
{
	RuntimeObject* ____key;
	RuntimeObject* ____value;
};
struct DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB_marshaled_pinvoke
{
	Il2CppIUnknown* ____key;
	Il2CppIUnknown* ____value;
};
struct DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB_marshaled_com
{
	Il2CppIUnknown* ____key;
	Il2CppIUnknown* ____value;
};
struct DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8 
{
	float ___Distance;
	float ___T;
};
struct DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5 
{
	int32_t ___firstIndex;
	int32_t ___indexCount;
	int32_t ___minIndexVal;
	int32_t ___vertsReferenced;
};
struct EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 
{
	union
	{
		struct
		{
			uint64_t ___m_rawData;
		};
		uint8_t EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8__padding[8];
	};
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
struct Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 
{
	float ___m00;
	float ___m10;
	float ___m20;
	float ___m30;
	float ___m01;
	float ___m11;
	float ___m21;
	float ___m31;
	float ___m02;
	float ___m12;
	float ___m22;
	float ___m32;
	float ___m03;
	float ___m13;
	float ___m23;
	float ___m33;
};
struct PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321  : public EventArgs_t37273F03EAC87217701DD431B190FBD84AD7C377
{
	String_t* ____propertyName;
};
struct Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
};
struct Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D 
{
	float ___m_XMin;
	float ___m_YMin;
	float ___m_Width;
	float ___m_Height;
};
struct UInt64_t8F12534CC8FC4B5860F2A2CD1EE79D322E7A41AF 
{
	uint64_t ___m_value;
};
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 
{
	float ___x;
	float ___y;
};
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	float ___x;
	float ___y;
	float ___z;
};
struct Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
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
struct float4_t545A994996126766890C1F28B43EA823F2410190 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
};
struct U3Cm_FadeTransitionWidthsU3Ee__FixedBuffer_tC44EA0D00BAF5D4021A10861FE9C59C8804B21E4 
{
	union
	{
		struct
		{
			uint16_t ___FixedElementField;
		};
		uint8_t U3Cm_FadeTransitionWidthsU3Ee__FixedBuffer_tC44EA0D00BAF5D4021A10861FE9C59C8804B21E4__padding[16];
	};
};
struct U3Cm_RendererCountsU3Ee__FixedBuffer_t877AF3B14647FADCA74D2F1A1BBB59648DC5AE67 
{
	union
	{
		struct
		{
			uint8_t ___FixedElementField;
		};
		uint8_t U3Cm_RendererCountsU3Ee__FixedBuffer_t877AF3B14647FADCA74D2F1A1BBB59648DC5AE67__padding[8];
	};
};
struct U3Cm_ScreenRelativeTransitionHeightsU3Ee__FixedBuffer_t2C41FA0F591BD145EEED3C0E09D6F04CD521B804 
{
	union
	{
		struct
		{
			uint16_t ___FixedElementField;
		};
		uint8_t U3Cm_ScreenRelativeTransitionHeightsU3Ee__FixedBuffer_t2C41FA0F591BD145EEED3C0E09D6F04CD521B804__padding[16];
	};
};
struct LodSelectionCurve_t5E3F99BC782C22E828074538B8B43051FF859713 
{
	float ___m_LodSlope;
	float ___m_LodBias;
};
struct Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 
{
	int32_t ___node;
	float ___priority;
};
struct ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F 
{
	intptr_t ____value;
};
struct ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 
{
	intptr_t ____value;
};
struct ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C 
{
	intptr_t ____value;
};
struct ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 
{
	intptr_t ____value;
};
struct ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD 
{
	intptr_t ____value;
};
struct ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 
{
	intptr_t ____value;
};
struct ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 
{
	intptr_t ____value;
};
struct ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B 
{
	intptr_t ____value;
};
struct ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC 
{
	intptr_t ____value;
};
struct ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 
{
	intptr_t ____value;
};
struct ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 
{
	intptr_t ____value;
};
struct ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 
{
	intptr_t ____value;
};
struct ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B 
{
	intptr_t ____value;
};
struct ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE 
{
	intptr_t ____value;
};
struct ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 
{
	intptr_t ____value;
};
struct ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 
{
	intptr_t ____value;
};
struct ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB 
{
	intptr_t ____value;
};
struct ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 
{
	intptr_t ____value;
};
struct ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B 
{
	intptr_t ____value;
};
struct ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 
{
	intptr_t ____value;
};
struct ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA 
{
	intptr_t ____value;
};
struct ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 
{
	intptr_t ____value;
};
struct ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF 
{
	intptr_t ____value;
};
struct ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB 
{
	intptr_t ____value;
};
struct ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 
{
	intptr_t ____value;
};
struct ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 
{
	intptr_t ____value;
};
struct ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 
{
	intptr_t ____value;
};
struct ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 
{
	intptr_t ____value;
};
struct ReadOnlyNativeVector_1_t0DE13C6BA89CEDB2D828D3F45AA93F7EC7AD4A0E 
{
	intptr_t ___m_Ptr;
	int32_t ___m_Count;
};
struct ReadOnlyNativeVector_1_t53A6814B9ECC2D21CA860DE85D9AEB86D11F6F17 
{
	intptr_t ___m_Ptr;
	int32_t ___m_Count;
};
struct ReadOnlyNativeVector_1_tE45C179B8DFEAD70E1F59D744D5C6999873C0FFC 
{
	intptr_t ___m_Ptr;
	int32_t ___m_Count;
};
struct ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E 
{
	intptr_t ___m_TextElementInfosPtr;
	int32_t ___m_TextElementCount;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___textAssetId;
};
struct AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03 
{
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_ClipEntityId;
	float ___m_Weight;
};
struct BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988 
{
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___P0;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___P1;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___P2;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___P3;
};
struct Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Center;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_Extents;
};
struct CollisionPairHeaderFlags_tBA40F470BC1564B84C3093CEA5761804FA0650C3 
{
	uint16_t ___value__;
};
struct CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE 
{
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_MeshInstanceID;
	int32_t ___m_SubMeshIndex;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___m_Transform;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___m_LightmapScaleOffset;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___m_RealtimeLightmapScaleOffset;
};
struct ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C 
{
	intptr_t ___texCoord1Src;
	int32_t ___texCoord1SrcStride;
	int32_t ___texCoord1DstOffset;
	intptr_t ___texCoord2Src;
	int32_t ___texCoord2SrcStride;
	int32_t ___texCoord2DstOffset;
	intptr_t ___texCoord3Src;
	int32_t ___texCoord3SrcStride;
	int32_t ___texCoord3DstOffset;
	intptr_t ___normalSrc;
	int32_t ___normalSrcStride;
	int32_t ___normalDstOffset;
	intptr_t ___tangentSrc;
	int32_t ___tangentSrcStride;
	int32_t ___tangentDstOffset;
};
struct CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8 
{
	intptr_t ___vertSrc;
	intptr_t ___vertDst;
	int32_t ___vertCount;
	int32_t ___vertStride;
	intptr_t ___indexSrc;
	intptr_t ___indexDst;
	int32_t ___indexCount;
	int32_t ___indexOffset;
};
struct CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___sphereCenter;
	float ___sphereRadius;
	int32_t ___cullingPlaneOffset;
	int32_t ___cullingPlaneCount;
	float ___cascadeBlendCullingFactor;
	float ___nearPlane;
	Matrix4x4_tDB70CF134A14BA38190C59AA700BCE10E2AED3E6 ___cullingMatrix;
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
struct EasingMode_tEF87477B9B9EB2524525550AE5ABEBC00FC7B0DF 
{
	int32_t ___value__;
};
struct EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B 
{
	U3Cm_ScreenRelativeTransitionHeightsU3Ee__FixedBuffer_t2C41FA0F591BD145EEED3C0E09D6F04CD521B804 ___m_ScreenRelativeTransitionHeights;
	U3Cm_FadeTransitionWidthsU3Ee__FixedBuffer_tC44EA0D00BAF5D4021A10861FE9C59C8804B21E4 ___m_FadeTransitionWidths;
	U3Cm_RendererCountsU3Ee__FixedBuffer_t877AF3B14647FADCA74D2F1A1BBB59648DC5AE67 ___m_RendererCounts;
	uint8_t ___m_Length;
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
struct FilterParameterType_t7250262A78079140235AAFF6F4FA3CCB2F99E390 
{
	int32_t ___value__;
};
struct GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32 
{
	int32_t ___subMeshCount;
	int32_t ___meshLodCount;
	LodSelectionCurve_t5E3F99BC782C22E828074538B8B43051FF859713 ___meshLodSelectionCurve;
};
struct NotifyCollectionChangedAction_tA580EA64F38D1FB2B1470FDD8266E5F32666D9FF 
{
	int32_t ___value__;
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct SelectorAccelerationTableType_t29E86C4FCF992B838F757327FF6002927A251B9A 
{
	int32_t ___value__;
};
struct StyleSelectorRelationship_tAABCDC80BF87B347ACE1C64B32C85B05268A7F9E 
{
	int32_t ___value__;
};
struct StyleSelectorType_t425962DE6D175F785FA2B5554D793B71D39430A3 
{
	int32_t ___value__;
};
struct TextCoreSettingsMode_t335FCFC221A752C14314BF46F1043A24FB2C8568 
{
	int32_t ___value__;
};
struct VertexFlags_t4332E863C191870E63009EBE5F6E77D6D176C4FF 
{
	uint16_t ___value__;
};
struct quaternion_t9C8C2916B7359C174B215BBB41F290ABCBEA8AEA 
{
	float4_t545A994996126766890C1F28B43EA823F2410190 ___value;
};
struct RawData_t37CAF2D3F74B7723974ED7CEEE9B297D8FA64ED0  : public RuntimeObject
{
	intptr_t ___Bounds;
	intptr_t ___Count;
	uint8_t ___Data;
};
struct RawData_t37CAF2D3F74B7723974ED7CEEE9B297D8FA64ED0_marshaled_pinvoke
{
	intptr_t ___Bounds;
	intptr_t ___Count;
	uint8_t ___Data;
};
struct RawData_t37CAF2D3F74B7723974ED7CEEE9B297D8FA64ED0_marshaled_com
{
	intptr_t ___Bounds;
	intptr_t ___Count;
	uint8_t ___Data;
};
struct VisualsProcessingType_t19983E0076886C15ED2761057B9992989A710ECC 
{
	int32_t ___value__;
};
struct Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 
{
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___Pos;
	Quaternion_tDA59F214EF07D7700B26E40E562F267AF7306974 ___Rot;
};
struct ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 
{
	ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 
{
	ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 
{
	ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 
{
	ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E 
{
	ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F 
{
	ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 
{
	ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 
{
	ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D 
{
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 
{
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 
{
	ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC 
{
	ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A 
{
	ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B 
{
	ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 
{
	ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A 
{
	ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA 
{
	ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF 
{
	ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A 
{
	ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B 
{
	ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 
{
	ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 
{
	ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 
{
	ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 
{
	ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A 
{
	ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D 
{
	ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 
{
	ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 ____pointer;
	int32_t ____length;
};
struct ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC 
{
	ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 ____pointer;
	int32_t ____length;
};
struct SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474 
{
	int32_t ___type;
	int32_t ___uniqueStringId;
};
struct Span_1_t291EAC5144D1F469ED3426F74052EFC6F1E9BCB8 
{
	ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F ____pointer;
	int32_t ____length;
};
struct Span_1_t14EC2E77DBAA48290810774B64FB790F124E6948 
{
	ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 ____pointer;
	int32_t ____length;
};
struct Span_1_t00FA2E5225B268183BB8AEC08EFAA8532A0B403D 
{
	ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C ____pointer;
	int32_t ____length;
};
struct Span_1_t36B7E942A80FDA89A0043D5EDED0559212667428 
{
	ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 ____pointer;
	int32_t ____length;
};
struct Span_1_t403EC894E0C29DB4AEFFFACE95188C989E44E524 
{
	ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD ____pointer;
	int32_t ____length;
};
struct Span_1_tEA5727F0F276960BF4E47192A9109344388B48E1 
{
	ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 ____pointer;
	int32_t ____length;
};
struct Span_1_t087F0E3724EBFD3A74A84E3F9E3F027249F37B51 
{
	ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 ____pointer;
	int32_t ____length;
};
struct Span_1_tF1D09AE3581D2E6304B9FC753705C3FC5DF90634 
{
	ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B ____pointer;
	int32_t ____length;
};
struct Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 
{
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC ____pointer;
	int32_t ____length;
};
struct Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D 
{
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 ____pointer;
	int32_t ____length;
};
struct Span_1_t162CEB1BEB175B131BEFB9D4647CEB2D48558018 
{
	ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 ____pointer;
	int32_t ____length;
};
struct Span_1_tCF27B3DC55C4AD13268A1F154352D6782B237F7D 
{
	ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 ____pointer;
	int32_t ____length;
};
struct Span_1_t417FB548242C07F6973D324BB1024F79C8D74EAD 
{
	ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B ____pointer;
	int32_t ____length;
};
struct Span_1_tBBAFA836A153BEF6FDEF8E7DE7F4E0F6DC6B0907 
{
	ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE ____pointer;
	int32_t ____length;
};
struct Span_1_tFD51B8D0B2DA78C0F911056FC60AB6B732AD4F7C 
{
	ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 ____pointer;
	int32_t ____length;
};
struct Span_1_tA69488C008961B8AE09141F37FDAE8A0F575CE48 
{
	ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 ____pointer;
	int32_t ____length;
};
struct Span_1_t0F901C7F7248B37E2D8773703E8E8790204A0A30 
{
	ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB ____pointer;
	int32_t ____length;
};
struct Span_1_tC85B5D30CDD48BE32D9DF528934ED5AEC5BFC116 
{
	ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 ____pointer;
	int32_t ____length;
};
struct Span_1_tAD7D80F571B6C63E604288FC6952D4E8B93EB381 
{
	ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B ____pointer;
	int32_t ____length;
};
struct Span_1_tFCA7D5D0BF7401ABF1CDA871A9259927808E7F73 
{
	ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 ____pointer;
	int32_t ____length;
};
struct Span_1_t43234050271CE7DE614C13A0021A344861FB5593 
{
	ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA ____pointer;
	int32_t ____length;
};
struct Span_1_t6834048668549DB2E233F0E7E8E71F1E3AFDE712 
{
	ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 ____pointer;
	int32_t ____length;
};
struct Span_1_t5D7E7A039DDB174888A2AC5552AF592E3B641F6A 
{
	ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF ____pointer;
	int32_t ____length;
};
struct Span_1_t420A830887305E0E02A744003AACBACED16172EB 
{
	ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB ____pointer;
	int32_t ____length;
};
struct Span_1_tD4A141E2CAE7DFB331921ECC10AB8884452E2784 
{
	ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 ____pointer;
	int32_t ____length;
};
struct Span_1_tF25812E0445523D140232909284012C24E110428 
{
	ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 ____pointer;
	int32_t ____length;
};
struct Span_1_tBFB0B8ED6E219789174070F50BFF3D1D12DDADA4 
{
	ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 ____pointer;
	int32_t ____length;
};
struct Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54 
{
	ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 ____pointer;
	int32_t ____length;
};
struct BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3 
{
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___Position;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___TangentIn;
	float3_t7600B73F092B37F484B12910A5269F30C778D31E ___TangentOut;
	quaternion_t9C8C2916B7359C174B215BBB41F290ABCBEA8AEA ___Rotation;
};
struct ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B 
{
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_BodyID;
	EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 ___m_OtherBodyID;
	intptr_t ___m_StartPtr;
	uint32_t ___m_NbPairs;
	uint16_t ___m_Flags;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_ThisBodyLinearVelocity;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_ThisBodyAngularVelocity;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_OtherBodyLinearVelocity;
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___m_OtherBodyAngularVelocity;
};
struct ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1 
{
	intptr_t ___vertSrc;
	intptr_t ___vertDst;
	int32_t ___vertCount;
	int32_t ___vertStride;
	uint16_t ___clipRectId;
	uint16_t ___elementId;
	uint16_t ___dynamicColorOrTextCoreId;
	uint16_t ___flags;
	uint16_t ___textureId;
	uint16_t ___gradientSettingsIndexOffset;
	int32_t ___usesTextCoreSettings;
	intptr_t ___indexSrc;
	intptr_t ___indexDst;
	int32_t ___indexCount;
	int32_t ___indexOffset;
	int32_t ___flipIndices;
	int32_t ___forceZ;
	float ___positionZ;
	int32_t ___remapUVs;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___atlasRect;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___layoutSize;
	intptr_t ___extras;
};
struct EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4 
{
	int32_t ___m_Mode;
};
struct FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060 
{
	int32_t ___m_Type;
	float ___m_FloatValue;
	Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___m_ColorValue;
};
struct FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9 
{
	int32_t ___partsStart;
	int32_t ___pseudoStateMask;
	int32_t ___negatedPseudoStateMask;
	int32_t ___previousRelationship;
	uint16_t ___partCount;
};
struct FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986 
{
	int32_t ___uniqueStringId;
	int32_t ___type;
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
struct NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA  : public EventArgs_t37273F03EAC87217701DD431B190FBD84AD7C377
{
	int32_t ____action;
	RuntimeObject* ____newItems;
	RuntimeObject* ____oldItems;
	int32_t ____newStartingIndex;
	int32_t ____oldStartingIndex;
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 
{
	RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2* ___renderData;
	int32_t ___type;
	Entry_t475ED76E31923970E7F7A6522E570E7577C487B9* ___rootEntry;
};
struct EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289_marshaled_pinvoke
{
	RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2* ___renderData;
	int32_t ___type;
	Entry_t475ED76E31923970E7F7A6522E570E7577C487B9* ___rootEntry;
};
struct EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289_marshaled_com
{
	RenderData_t1ABE116B2B5E0409AC699E195922516606531DC2* ___renderData;
	int32_t ___type;
	Entry_t475ED76E31923970E7F7A6522E570E7577C487B9* ___rootEntry;
};
struct RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B 
{
	float ___Time;
	bool ___IsCut;
	Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ___Item;
};
struct RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B_marshaled_pinvoke
{
	float ___Time;
	int32_t ___IsCut;
	Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ___Item;
};
struct RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B_marshaled_com
{
	float ___Time;
	int32_t ___IsCut;
	Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ___Item;
};
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	String_t* ____paramName;
};
struct ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF  : public MulticastDelegate_t
{
};
struct PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA  : public MulticastDelegate_t
{
};
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
};
struct ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
	RuntimeObject* ____actualValue;
};
struct EmptyArray_1_t0542F0EB5C43D54C8F4ADCAB1E94F18AEA151867_StaticFields
{
	SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* ___Value;
};
struct EmptyArray_1_t910BE047C1A6E9E334831BFBF9773D3C3852ED95_StaticFields
{
	ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* ___Value;
};
struct EmptyArray_1_t5B5242ED623249C0903BF0800195DB10397B3BCD_StaticFields
{
	AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* ___Value;
};
struct EmptyArray_1_tA6E339CF983886C190886835190061C2E3B679F2_StaticFields
{
	BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* ___Value;
};
struct EmptyArray_1_tC8FD7AE710007FB0A9059B5125949067B761A8E5_StaticFields
{
	BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* ___Value;
};
struct EmptyArray_1_t234239C1AFE29F273AE44FBF753D3916E498AC5C_StaticFields
{
	BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* ___Value;
};
struct EmptyArray_1_t7902087DD0C0221C9C7362ABAB7CB57D6D519A99_StaticFields
{
	BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* ___Value;
};
struct EmptyArray_1_t42D5099F7FCE2834141926DB372392B8C75ADBEC_StaticFields
{
	BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* ___Value;
};
struct EmptyArray_1_t7187E746F328254739F076CFBCAABB28D4B4554C_StaticFields
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___Value;
};
struct EmptyArray_1_t7BBC8CED754F364A777871A238BBBE3F94FFDDE1_StaticFields
{
	CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___Value;
};
struct EmptyArray_1_t9FC4EAC7DDBFA164B697CFEBFF39A52B321CC8C9_StaticFields
{
	ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* ___Value;
};
struct EmptyArray_1_tC72AE83DCB4E240E4B2AC2595448148BB03F9498_StaticFields
{
	Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* ___Value;
};
struct EmptyArray_1_tF7ADA3AE49000F6D4933A7D7B32DDF6230630DC0_StaticFields
{
	CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* ___Value;
};
struct EmptyArray_1_tF01C5B6CDD56CDA122E6B85402777F31EE92A685_StaticFields
{
	ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* ___Value;
};
struct EmptyArray_1_tCE52674ADF71EC22E199D7DF299141790948751A_StaticFields
{
	ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* ___Value;
};
struct EmptyArray_1_tB029523A05EA2FFE1EB90490D66A384F7A0D4574_StaticFields
{
	ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* ___Value;
};
struct EmptyArray_1_t0605DD263F295CE065C4B24BC0263FB5067617EC_StaticFields
{
	CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* ___Value;
};
struct EmptyArray_1_t27401338DAB1F4C8F9B8E695AB18ABBBF6573E17_StaticFields
{
	CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* ___Value;
};
struct EmptyArray_1_tD918BE070C4FAED797D16A5616D8A88DC319BF09_StaticFields
{
	DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* ___Value;
};
struct EmptyArray_1_t640F34C5C1351444FCE089711FFEF74161372D64_StaticFields
{
	DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* ___Value;
};
struct EmptyArray_1_t7B0255D5BEE03D8E592C7F4DB26CEE268619FD74_StaticFields
{
	EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* ___Value;
};
struct EmptyArray_1_t662659478531CA2736931D12958F36C14566158B_StaticFields
{
	EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* ___Value;
};
struct EmptyArray_1_t9D02D6D1FF8E81441083A08AA0F8970E511F0900_StaticFields
{
	EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* ___Value;
};
struct EmptyArray_1_t2AA231E0C904F8398D206EDF41018BEF22BD2325_StaticFields
{
	FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* ___Value;
};
struct EmptyArray_1_t2D4836FD2685619474AF41CBDA7A6813DB13F5CA_StaticFields
{
	FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* ___Value;
};
struct EmptyArray_1_t00CB7A66F677EBAF03269ABB2C35F4597F8AFA77_StaticFields
{
	FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* ___Value;
};
struct EmptyArray_1_t3264D01575877F752602C31CBF848A455D557C15_StaticFields
{
	GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* ___Value;
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
struct DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_StaticFields
{
	DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8 ___Invalid;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
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
struct EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B  : public RuntimeArray
{
	ALIGN_FIELD (8) EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 m_Items[1];

	inline EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___renderData), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___rootEntry), (void*)NULL);
		#endif
	}
	inline EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___renderData), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->___rootEntry), (void*)NULL);
		#endif
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
struct ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498  : public RuntimeArray
{
	ALIGN_FIELD (8) Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 m_Items[1];

	inline Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 value)
	{
		m_Items[index] = value;
	}
};
struct ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73  : public RuntimeArray
{
	ALIGN_FIELD (8) Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 m_Items[1];

	inline Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 value)
	{
		m_Items[index] = value;
	}
};
struct RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316  : public RuntimeArray
{
	ALIGN_FIELD (8) RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B m_Items[1];

	inline RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B value)
	{
		m_Items[index] = value;
	}
};
struct KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57  : public RuntimeArray
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
struct DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533  : public RuntimeArray
{
	ALIGN_FIELD (8) DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB m_Items[1];

	inline DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->____key), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->____value), (void*)NULL);
		#endif
	}
	inline DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->____key), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&((m_Items + index)->____value), (void*)NULL);
		#endif
	}
};
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
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB  : public RuntimeArray
{
	ALIGN_FIELD (8) Il2CppChar m_Items[1];

	inline Il2CppChar GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Il2CppChar* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Il2CppChar value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Il2CppChar GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Il2CppChar* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Il2CppChar value)
	{
		m_Items[index] = value;
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
struct SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06  : public RuntimeArray
{
	ALIGN_FIELD (8) SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474 m_Items[1];

	inline SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474 value)
	{
		m_Items[index] = value;
	}
};
struct ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F  : public RuntimeArray
{
	ALIGN_FIELD (8) ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E m_Items[1];

	inline ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E value)
	{
		m_Items[index] = value;
	}
};
struct AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A  : public RuntimeArray
{
	ALIGN_FIELD (8) AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03 m_Items[1];

	inline AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03 value)
	{
		m_Items[index] = value;
	}
};
struct BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80  : public RuntimeArray
{
	ALIGN_FIELD (8) BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D m_Items[1];

	inline BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D value)
	{
		m_Items[index] = value;
	}
};
struct BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB  : public RuntimeArray
{
	ALIGN_FIELD (8) BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988 m_Items[1];

	inline BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988 value)
	{
		m_Items[index] = value;
	}
};
struct BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213  : public RuntimeArray
{
	ALIGN_FIELD (8) BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3 m_Items[1];

	inline BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3 value)
	{
		m_Items[index] = value;
	}
};
struct BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4  : public RuntimeArray
{
	ALIGN_FIELD (8) bool m_Items[1];

	inline bool GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline bool* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, bool value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline bool GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline bool* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, bool value)
	{
		m_Items[index] = value;
	}
};
struct BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5  : public RuntimeArray
{
	ALIGN_FIELD (8) Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 m_Items[1];

	inline Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3 value)
	{
		m_Items[index] = value;
	}
};
struct ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389  : public RuntimeArray
{
	ALIGN_FIELD (8) Color_tD001788D726C3A7F1379BEED0260B9591F440C1F m_Items[1];

	inline Color_tD001788D726C3A7F1379BEED0260B9591F440C1F GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Color_tD001788D726C3A7F1379BEED0260B9591F440C1F GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F value)
	{
		m_Items[index] = value;
	}
};
struct Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259  : public RuntimeArray
{
	ALIGN_FIELD (8) Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B m_Items[1];

	inline Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B value)
	{
		m_Items[index] = value;
	}
};
struct CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093  : public RuntimeArray
{
	ALIGN_FIELD (8) CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE m_Items[1];

	inline CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE value)
	{
		m_Items[index] = value;
	}
};
struct ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A  : public RuntimeArray
{
	ALIGN_FIELD (8) ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B m_Items[1];

	inline ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B value)
	{
		m_Items[index] = value;
	}
};
struct ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38  : public RuntimeArray
{
	ALIGN_FIELD (8) ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C m_Items[1];

	inline ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C value)
	{
		m_Items[index] = value;
	}
};
struct ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2  : public RuntimeArray
{
	ALIGN_FIELD (8) ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1 m_Items[1];

	inline ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1 value)
	{
		m_Items[index] = value;
	}
};
struct CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5  : public RuntimeArray
{
	ALIGN_FIELD (8) CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8 m_Items[1];

	inline CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8 value)
	{
		m_Items[index] = value;
	}
};
struct CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61  : public RuntimeArray
{
	ALIGN_FIELD (8) CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0 m_Items[1];

	inline CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0 value)
	{
		m_Items[index] = value;
	}
};
struct DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0  : public RuntimeArray
{
	ALIGN_FIELD (8) DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8 m_Items[1];

	inline DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8 value)
	{
		m_Items[index] = value;
	}
};
struct DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB  : public RuntimeArray
{
	ALIGN_FIELD (8) DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5 m_Items[1];

	inline DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5 value)
	{
		m_Items[index] = value;
	}
};
struct EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49  : public RuntimeArray
{
	ALIGN_FIELD (8) EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4 m_Items[1];

	inline EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4 value)
	{
		m_Items[index] = value;
	}
};
struct EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1  : public RuntimeArray
{
	ALIGN_FIELD (8) EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B m_Items[1];

	inline EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B value)
	{
		m_Items[index] = value;
	}
};
struct EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9  : public RuntimeArray
{
	ALIGN_FIELD (8) EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 m_Items[1];

	inline EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8 value)
	{
		m_Items[index] = value;
	}
};
struct FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58  : public RuntimeArray
{
	ALIGN_FIELD (8) FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060 m_Items[1];

	inline FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060 value)
	{
		m_Items[index] = value;
	}
};
struct FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27  : public RuntimeArray
{
	ALIGN_FIELD (8) FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9 m_Items[1];

	inline FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9 value)
	{
		m_Items[index] = value;
	}
};
struct FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643  : public RuntimeArray
{
	ALIGN_FIELD (8) FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986 m_Items[1];

	inline FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986 value)
	{
		m_Items[index] = value;
	}
};
struct GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE  : public RuntimeArray
{
	ALIGN_FIELD (8) GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32 m_Items[1];

	inline GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32 value)
	{
		m_Items[index] = value;
	}
};


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_m8B2AEA509E07E389B797D3576C0DDF9A5551E3A4 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_mC9A90AF46B6E7EC8BC710283B87EB110801FAB1B (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_mD750C5DD43B49E3369FF1CC3778B43D83F58636E (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_mDA0A610D1FE508DC17DABD5A4D5D861295857416 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_mF0276FF4CFBDD56B7D357DF165F97430B0F8D31F (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_m94BEB48405CC3157014AA7165FF29E33FB86873D (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_m6D1B999B20EB8B812887BC95465F6111CC658D31 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_mBFBFA5087A22447FCFE83273238A7F4CA47337A9 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_mB93E2CFB5B2969AC39C4A63A510317CFDEE6CFF9 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_m989053EC4E5C8CEDAE6D100C0507BCE7CF6BE8A7 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_mD3065D7A926DDD6807774F528EF0D74C95B73147 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_m26DD8D621F4B82DD8AF033C7ABF8ABC7475D7ADB (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_mA5768C536204B5BD1FAF1DA63A182D90F8E99A00 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_m1614379187C193FBC11C8EA68980A7A00B472377 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_m75DEDE8EC817491CD64CE8D09BCFBA73BDB72656 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_mD092C8821B7FC54D44F94C4F3B282D4588DED068 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DictionaryEnumerator__ctor_mA1A62AF5ED8AB1B514BBAAAEA9BB3D081C3AA4ED_fshared (DictionaryEnumerator_tA07B53C91CCF243B3DDF48825CD4F71A6835CD77* __this, RuntimeObject* ___0_dictionary, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void KeyValuePair_2__ctor_mD82E516936D2BDE6D46C8C45270250647986231E_fshared (KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669* __this, Il2CppFullySharedGenericAny ___0_key, Il2CppFullySharedGenericAny ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlyMemory_1_ToString_m0C4DF727B39C613F84AC56D6FDAF7ACFF9E39720 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 Span_1_Slice_m9D8BA8245B8DC9BFB4A4164759CBAAEAD1318CD6_inline (Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC Span_1_op_Implicit_m704A5B9FD25EEA23756181A8EB40B875387A6C01_fshared (Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54 ___0_span, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m7B5C2765879EA5E8D1617D834CC465A39540A913_inline (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_mF7DA75997B4FFDFB2D6F953E8DC580EC3C941A61_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 ___0_other, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 Memory_1_op_Implicit_m7216145178505C2CB2BA0E21CB95773BE1E6A7AF_fshared (Memory_1_t762D61B435521D1706EEDC25CA8C8EA1AE19AFEA ___0_memory, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_mD0C4947B91FD3864C476B6CA8BA929A6F5B01175 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_CombineHashCodes_mC20EA69C2706DE824529A0506BD45837E26C6980 (int32_t ___0_h1, int32_t ___1_h2, int32_t ___2_h3, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_GetHashCode_mE47597BBDBFFBC9185DE65261A7388207854ADE6 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_CombineHashCodes_m74D5DE9AF0CCB1FF7B88058328C835DBD981C568_fshared (int32_t ___0_left, int32_t ___1_right, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ReadOnlyMemory_1_get_Span_m21E4F67DBB43CC122008ADE132AB9213723588EA_inline (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mB13A913EF211B14B719EE62133C15C298642E34D (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlyMemory_1_ToString_m4676D3AFBC120B54C2B53171F9C7DE3B4E753F68 (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D Span_1_Slice_mEFBC3C78FD443FFE23F9E841D43B7B0271622843_inline (Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, Il2CppFullySharedGenericAny* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ReadOnlySpan_1_Slice_mDEEA4C304B13C8F7A63BC3D60B62FF17BBEE282B_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mDADE08E6B4594775C6675B389078E5FE98C745D5_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_mE4EA5DC4ACF51E07F85C4A55A3281E580903506F (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_CombineHashCodes_m90089D22A29124B5DFB7253B5795FBBBA2E17BC1 (int32_t ___0_h1, int32_t ___1_h2, int32_t ___2_h3, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_GetHashCode_m7D885665B2C6E9B5B157129A6CCAFC4938400D0D (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m04AD05E39D10A41E67653C41A22108A810CA9857_fshared (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlyMemory_1_ToString_m1AB8B8725FFC7144748E59C73875E2C6822E2321_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mC9869776ABBFE9D2520512EEB39ABD1CFFE7F7B9_fshared_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_m2C16DF2AE78D8E7182CFAE8E3E07C3B20A777888_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_GetHashCode_m96535F6C6512A16E6DBC2A8341F8FECDF2E47F54_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared (ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8* __this, RuntimeObject* ___0_list, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_get_Items_m0C20F5911B7A7A3EBAE35C0C5DC6F8E796FAD090_fshared_inline (ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline (Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisSelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474_m8B225EBD79F98163072E7428252E4FDAFE5A3E1A (SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* ___0_destination, SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* Array_Empty_TisSelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474_m4433599FD227186796BAB47DC5BED2AF106ED67E_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC ___0_left, ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC ___1_right, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E_mF7B64A7A3AED5F75779174F1164B6D60E51A6215 (ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* ___0_destination, ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* Array_Empty_TisATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E_m5293579F263F9A3750FDFA1AA45718B28873BFBB_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisAnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03_mA05FD1200E24DDB998528FC769FF4219BC53EE43 (AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* ___0_destination, AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* Array_Empty_TisAnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03_mC9DEB54C94662C9FD5779A4BFFAB7AD0469A27C8_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D_m9074142CF2F0828232AFEBB926F490D33E285B9B (BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* ___0_destination, BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* Array_Empty_TisBatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D_m652D1F78F0DEF31A60A1D09589A89DE06F12C3E4_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988_m54E83FBDC4755CF4FADD61A5C99841D951AAEF78 (BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* ___0_destination, BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* Array_Empty_TisBezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988_m89904E98E5508C2B001AC9AB5EE2E1CCA80CEEF3_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3_mD278D7E5E9C662C8F05E45162C4D4E540419CC20 (BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* ___0_destination, BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* Array_Empty_TisBezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3_m77719C87E49330E8DA7EA4CD03C4AFDCE18C19C0_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6C1A997DCFEFC3BA96EEB5E75FB8B54DF2D21198 (bool* ___0_destination, bool* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* Array_Empty_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mAB215C445888719BD89809D99C3DBD3135C2B1E7_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3_m4925E1D88272BE2E628F28AFFEC4FF09CC2EC647 (Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* ___0_destination, Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* Array_Empty_TisBounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3_mB6CFBB5D8AF33F0BAE72154209AB29B8D52FBCDA_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_mB1465EEEBE0A608FA51B29BC3F145F287AD04190 (uint8_t* ___0_destination, uint8_t* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* Array_Empty_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m6080CA526758F4FA182A066B2780D1761CD36ED5_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m62AF071D7F91DFC9A4D8B847D6A4472B820B5446 (Il2CppChar* ___0_destination, Il2CppChar* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* Array_Empty_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_mD1C1362CB74B91496D984B006ADC79B688D9B50D_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisColor_tD001788D726C3A7F1379BEED0260B9591F440C1F_m3B15D4D61932A340BAAA5BE6E8D419605D3692E6 (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ___0_destination, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* Array_Empty_TisColor_tD001788D726C3A7F1379BEED0260B9591F440C1F_m7E922E24AAEBD664256383832D53DDF17E1F3052_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisColor32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B_mA65CAF282F03FD4DB46666E2C44C22E3D0035A68 (Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* ___0_destination, Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* Array_Empty_TisColor32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B_m5B415C4745E47108DD9258EBCCB422EFD6B8A0EB_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisCombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE_m3C4417F1F221D78FCCCF0E2157B78FE672A7E86F (CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* ___0_destination, CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* Array_Empty_TisCombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE_mC39B4BC80AD9C1F917509BB8BE77C4C73CB471F9_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B_m7B231E78B8FBBF3128AC7D5D9AA1EF250AB8BBB7 (ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* ___0_destination, ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* Array_Empty_TisContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B_m2AE4E8C460D733DF5362B246ED865C5CE0C4AA60_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C_m25EF20C7583AB5F948F033EAAAD3726E13608167 (ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* ___0_destination, ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* Array_Empty_TisConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C_m4779104F7674EACD211A240162D4414087C431E8_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1_m747B7D7DC0AA466783F69D3676EC5EE854D08D52 (ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* ___0_destination, ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* Array_Empty_TisConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1_m164EDF2519D24430C10F8BB106ADB252BF225C26_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisCopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8_m8A5B65BF262A2B1C988311D13733E08E197C8A03 (CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* ___0_destination, CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* Array_Empty_TisCopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8_m636107AFEDC2888AAD9A190C5FCB115D4483F86F_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisCullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0_m9A4B5780CB8BC1ACD63C193EC68D5E217CDBAC80 (CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* ___0_destination, CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* Array_Empty_TisCullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0_m46787E1D6E481E4790F1F9E63000A067C9AF33CD_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisDistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_mDC37E2EA7AF7BC4F29B1BBEC90A762EB26B760F5 (DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* ___0_destination, DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* Array_Empty_TisDistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_m7807FD2713C36285071ABDD0A617C0D77C6C1EB7_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisDrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5_mDF1CDB2973A50B932ED2E72CC7A57E75C7FCAB54 (DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* ___0_destination, DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* Array_Empty_TisDrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5_m05A4DA3A4ACD6D784909779870EE466DF7778EC7_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisEasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4_m0343B8FCB3868B60552322D0BFD9C87585A924B1 (EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* ___0_destination, EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* Array_Empty_TisEasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4_m5BB1B3F942E717EB0EEA932DE146F79E347604BA_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisEmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B_mDFAF68DAF7C667016B41A5C085293991850D1D8C (EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* ___0_destination, EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* Array_Empty_TisEmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B_m87CA399E64D82A02BCE81DF2F8F52AD65AC9BC25_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisEntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8_m8D3BF3FF1317BAF44B55A14301469F3AB0E60900 (EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* ___0_destination, EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* Array_Empty_TisEntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8_m8EF4F258FBF5AEC804ED891772BFFC84A01DB5D9_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisFilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060_mDD7300E8E4A6CFCCD3B437359FAAA469263822D0 (FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* ___0_destination, FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* Array_Empty_TisFilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060_mA4E39A2097C6E2511B93A1BF0C9221165B65938C_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisFlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9_m6C3DAA8091937F2A2CBA773FD3BAA3DCD1C9B2EF (FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* ___0_destination, FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* Array_Empty_TisFlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9_m7E0303803800EFD09E46C64192C01A6D011D4260_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisFlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986_m19C1CA978BDC4B53F03AF962E7E6296C9E5A5A33 (FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* ___0_destination, FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* Array_Empty_TisFlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986_m907AAB5CF7AEF4C510C711BBF98ABA5EA468D2B1_inline (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisGPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32_m83F98C10F75BBF5CBB456D86857C192E9B2D06B6 (GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* ___0_destination, GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* Array_Empty_TisGPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32_m0C324743704C230D82B1D3F4A8A72DAECCD01F62_inline (const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Span_1__ctor_mBA868F06359701D9950DEB1B10F52F848E9FF6DA_fshared_inline (Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54* __this, Il2CppFullySharedGenericAny* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC (int32_t ___0_argument, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA (int32_t ___0_resource, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Array_GetLowerBound_m4FB0601E2E8A6304A42E3FC400576DF7B0F084BC (RuntimeArray* __this, int32_t ___0_dimension, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_m8B2AEA509E07E389B797D3576C0DDF9A5551E3A4 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_mC9A90AF46B6E7EC8BC710283B87EB110801FAB1B (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_mD750C5DD43B49E3369FF1CC3778B43D83F58636E (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_mDA0A610D1FE508DC17DABD5A4D5D861295857416 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_mF0276FF4CFBDD56B7D357DF165F97430B0F8D31F (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_m94BEB48405CC3157014AA7165FF29E33FB86873D (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_m6D1B999B20EB8B812887BC95465F6111CC658D31 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_mBFBFA5087A22447FCFE83273238A7F4CA47337A9 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_mB93E2CFB5B2969AC39C4A63A510317CFDEE6CFF9 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_m989053EC4E5C8CEDAE6D100C0507BCE7CF6BE8A7 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_mD3065D7A926DDD6807774F528EF0D74C95B73147 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_m26DD8D621F4B82DD8AF033C7ABF8ABC7475D7ADB (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_mA5768C536204B5BD1FAF1DA63A182D90F8E99A00 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_m1614379187C193FBC11C8EA68980A7A00B472377 (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_m75DEDE8EC817491CD64CE8D09BCFBA73BDB72656 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_mD092C8821B7FC54D44F94C4F3B282D4588DED068 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* __this, String_t* ___0_paramName, const RuntimeMethod* method) ;
inline void DictionaryEnumerator__ctor_mA1A62AF5ED8AB1B514BBAAAEA9BB3D081C3AA4ED (DictionaryEnumerator_tA07B53C91CCF243B3DDF48825CD4F71A6835CD77* __this, RuntimeObject* ___0_dictionary, const RuntimeMethod* method)
{
	((  void (*) (DictionaryEnumerator_tA07B53C91CCF243B3DDF48825CD4F71A6835CD77*, RuntimeObject*, const RuntimeMethod*))DictionaryEnumerator__ctor_mA1A62AF5ED8AB1B514BBAAAEA9BB3D081C3AA4ED_fshared)(__this, ___0_dictionary, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465 (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* __this, String_t* ___0_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentOutOfRangeException__ctor_mE5B2755F0BEA043CACF915D5CE140859EE58FA66 (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* __this, String_t* ___0_paramName, String_t* ___1_message, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DictionaryEntry__ctor_m2768353E53A75C4860E34B37DAF1342120C5D1EA (DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB* __this, RuntimeObject* ___0_key, RuntimeObject* ___1_value, const RuntimeMethod* method) ;
inline void KeyValuePair_2__ctor_mD82E516936D2BDE6D46C8C45270250647986231E (KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669* __this, Il2CppFullySharedGenericAny ___0_key, Il2CppFullySharedGenericAny ___1_value, const RuntimeMethod* method)
{
	((  void (*) (KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669*, Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny, const RuntimeMethod*))KeyValuePair_2__ctor_mD82E516936D2BDE6D46C8C45270250647986231E_fshared)((KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669*)__this, ___0_key, ___1_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987 (String_t* ___0_format, RuntimeObject* ___1_arg0, RuntimeObject* ___2_arg1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlyMemory_1_ToString_m0C4DF727B39C613F84AC56D6FDAF7ACFF9E39720 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 Span_1_Slice_m9D8BA8245B8DC9BFB4A4164759CBAAEAD1318CD6_inline (Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) ;
inline ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D Span_1_op_Implicit_mD249188242C0C9D3192A31E9F7FA74C683F05B84 (Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 ___0_span, const RuntimeMethod* method)
{
	return ((  ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D (*) (Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305, const RuntimeMethod*))Span_1_op_Implicit_m704A5B9FD25EEA23756181A8EB40B875387A6C01_fshared)(___0_span, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m7B5C2765879EA5E8D1617D834CC465A39540A913_inline (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) ;
inline bool ReadOnlyMemory_1_Equals_mC3969DE0744A246E0576FEDE51191A0D3F5F69A9 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399 ___0_other, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399*, ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399, const RuntimeMethod*))ReadOnlyMemory_1_Equals_mF7DA75997B4FFDFB2D6F953E8DC580EC3C941A61_fshared)(__this, ___0_other, method);
}
inline ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399 Memory_1_op_Implicit_m7B80A71310A3EA9F99CB1774CAFED254811599CD (Memory_1_tB7CEF4416F5014E364267478CEF016A4AC5C0036 ___0_memory, const RuntimeMethod* method)
{
	return ((  ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399 (*) (Memory_1_tB7CEF4416F5014E364267478CEF016A4AC5C0036, const RuntimeMethod*))Memory_1_op_Implicit_m7216145178505C2CB2BA0E21CB95773BE1E6A7AF_fshared)(___0_memory, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_mD0C4947B91FD3864C476B6CA8BA929A6F5B01175 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295 (int32_t* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_CombineHashCodes_mC20EA69C2706DE824529A0506BD45837E26C6980 (int32_t ___0_h1, int32_t ___1_h2, int32_t ___2_h3, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_GetHashCode_mE47597BBDBFFBC9185DE65261A7388207854ADE6 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, const RuntimeMethod* method) ;
inline int32_t ReadOnlyMemory_1_CombineHashCodes_mAD75C6BDDB231C42D8A846F50E2B081E244108DA (int32_t ___0_left, int32_t ___1_right, const RuntimeMethod* method)
{
	return ((  int32_t (*) (int32_t, int32_t, const RuntimeMethod*))ReadOnlyMemory_1_CombineHashCodes_m74D5DE9AF0CCB1FF7B88058328C835DBD981C568_fshared)(___0_left, ___1_right, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ReadOnlyMemory_1_get_Span_m21E4F67DBB43CC122008ADE132AB9213723588EA_inline (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mB13A913EF211B14B719EE62133C15C298642E34D (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Substring_mB1D94F47935D22E130FF2C01DBB6A4135FBB76CE (String_t* __this, int32_t ___0_startIndex, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlyMemory_1_ToString_m4676D3AFBC120B54C2B53171F9C7DE3B4E753F68 (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D Span_1_Slice_mEFBC3C78FD443FFE23F9E841D43B7B0271622843_inline (Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) ;
inline ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 Span_1_op_Implicit_m03D0CEDC1BC95844236105D1DE24A702B956BFE4 (Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D ___0_span, const RuntimeMethod* method)
{
	return ((  ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 (*) (Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D, const RuntimeMethod*))Span_1_op_Implicit_m704A5B9FD25EEA23756181A8EB40B875387A6C01_fshared)(___0_span, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar* String_GetRawStringData_m87BC50B7B314C055E27A28032D1003D42FDE411D (String_t* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, Il2CppChar* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1*, Il2CppChar*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ReadOnlySpan_1_Slice_mDEEA4C304B13C8F7A63BC3D60B62FF17BBEE282B_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mDADE08E6B4594775C6675B389078E5FE98C745D5_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) ;
inline bool ReadOnlyMemory_1_Equals_mC4A7615E6A8C3467783EB6D9FBDA06A1B9745117 (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6 ___0_other, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6*, ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6, const RuntimeMethod*))ReadOnlyMemory_1_Equals_mF7DA75997B4FFDFB2D6F953E8DC580EC3C941A61_fshared)(__this, ___0_other, method);
}
inline ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6 Memory_1_op_Implicit_m2A354E866CDB4F77671ADADD234CFF9A31DAB8FB (Memory_1_t48D1B1093D181D6F37155EB644659FD7D1DF616E ___0_memory, const RuntimeMethod* method)
{
	return ((  ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6 (*) (Memory_1_t48D1B1093D181D6F37155EB644659FD7D1DF616E, const RuntimeMethod*))Memory_1_op_Implicit_m7216145178505C2CB2BA0E21CB95773BE1E6A7AF_fshared)(___0_memory, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_mE4EA5DC4ACF51E07F85C4A55A3281E580903506F (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_CombineHashCodes_m90089D22A29124B5DFB7253B5795FBBBA2E17BC1 (int32_t ___0_h1, int32_t ___1_h2, int32_t ___2_h3, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_GetHashCode_m7D885665B2C6E9B5B157129A6CCAFC4938400D0D (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) ;
inline int32_t ReadOnlyMemory_1_CombineHashCodes_mE36B3B07DCBC22779CDDB94D35E0D00AD8568028 (int32_t ___0_left, int32_t ___1_right, const RuntimeMethod* method)
{
	return ((  int32_t (*) (int32_t, int32_t, const RuntimeMethod*))ReadOnlyMemory_1_CombineHashCodes_m74D5DE9AF0CCB1FF7B88058328C835DBD981C568_fshared)(___0_left, ___1_right, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56 (const RuntimeMethod* method) ;
inline String_t* ReadOnlySpan_1_ToString_m04AD05E39D10A41E67653C41A22108A810CA9857 (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, const RuntimeMethod* method)
{
	return ((  String_t* (*) (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC*, const RuntimeMethod*))ReadOnlySpan_1_ToString_m04AD05E39D10A41E67653C41A22108A810CA9857_fshared)(__this, method);
}
inline String_t* ReadOnlyMemory_1_ToString_m1AB8B8725FFC7144748E59C73875E2C6822E2321 (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, const RuntimeMethod* method)
{
	return ((  String_t* (*) (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4*, const RuntimeMethod*))ReadOnlyMemory_1_ToString_m1AB8B8725FFC7144748E59C73875E2C6822E2321_fshared)(__this, method);
}
inline void ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, Il2CppFullySharedGenericAny* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC*, Il2CppFullySharedGenericAny*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
inline void ReadOnlySpan_1__ctor_mC9869776ABBFE9D2520512EEB39ABD1CFFE7F7B9_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC*, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*, int32_t, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mC9869776ABBFE9D2520512EEB39ABD1CFFE7F7B9_fshared_inline)(__this, ___0_array, ___1_start, ___2_length, method);
}
inline bool ReadOnlyMemory_1_Equals_m2C16DF2AE78D8E7182CFAE8E3E07C3B20A777888 (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4*, RuntimeObject*, const RuntimeMethod*))ReadOnlyMemory_1_Equals_m2C16DF2AE78D8E7182CFAE8E3E07C3B20A777888_fshared)(__this, ___0_obj, method);
}
inline bool ReadOnlyMemory_1_Equals_mF7DA75997B4FFDFB2D6F953E8DC580EC3C941A61 (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 ___0_other, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4*, ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4, const RuntimeMethod*))ReadOnlyMemory_1_Equals_mF7DA75997B4FFDFB2D6F953E8DC580EC3C941A61_fshared)(__this, ___0_other, method);
}
inline int32_t ReadOnlyMemory_1_GetHashCode_m96535F6C6512A16E6DBC2A8341F8FECDF2E47F54 (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4*, const RuntimeMethod*))ReadOnlyMemory_1_GetHashCode_m96535F6C6512A16E6DBC2A8341F8FECDF2E47F54_fshared)(__this, method);
}
inline void ReadOnlyCollection_1__ctor_mC1890FAC00703F47A655C35CBCB613C74A811580 (ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263* __this, RuntimeObject* ___0_list, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263*, RuntimeObject*, const RuntimeMethod*))ReadOnlyCollection_1__ctor_m903CF4492D6E4F6EE0D865BB7AA36A0DDB888355_fshared)(__this, ___0_list, method);
}
inline RuntimeObject* ReadOnlyCollection_1_get_Items_m9959886AAEACF554130E90E83847131E07819CC0_inline (ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263*, const RuntimeMethod*))ReadOnlyCollection_1_get_Items_m0C20F5911B7A7A3EBAE35C0C5DC6F8E796FAD090_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NotifyCollectionChangedEventHandler__ctor_m4DAF76BAC5E44532E6E784CD15A0C4493156E05D (NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PropertyChangedEventHandler__ctor_m4015A035EDCA8077D1EA9A8EAB5FE1CCD06A5B82 (PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00 (Delegate_t* ___0_a, Delegate_t* ___1_b, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3 (Delegate_t* ___0_source, Delegate_t* ___1_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void NotifyCollectionChangedEventHandler_Invoke_m8DED5705E365E59E2F6BDCBC6D6D762DA1ECA194_inline (NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* __this, RuntimeObject* ___0_sender, NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA* ___1_e, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PropertyChangedEventHandler_Invoke_m040B3056C279FDDC4EC678519D5D415771905298_inline (PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* __this, RuntimeObject* ___0_sender, PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321* ___1_e, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR uint8_t* Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline (RuntimeArray* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5 (const RuntimeMethod* method) ;
inline int32_t Span_1_get_Length_mBA6FA6140EADE76BD14920BDC6A3E87EDB72FB76_inline (Span_1_t291EAC5144D1F469ED3426F74052EFC6F1E9BCB8* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t291EAC5144D1F469ED3426F74052EFC6F1E9BCB8*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisSelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474_m8B225EBD79F98163072E7428252E4FDAFE5A3E1A (SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* ___0_destination, SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D (const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m68B11A5CBD341B71C3B49A184D9A4A0F68B2BC1B_inline (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84*, SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* Array_Empty_TisSelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474_m4433599FD227186796BAB47DC5BED2AF106ED67E_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m273AE2A699A7739E89673154DA9E3BE6B08A180D (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 ___0_left, ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84, ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mAB8B8F731C3753B681689724D8C10D0F6706324A_inline (Span_1_t14EC2E77DBAA48290810774B64FB790F124E6948* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t14EC2E77DBAA48290810774B64FB790F124E6948*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E_mF7B64A7A3AED5F75779174F1164B6D60E51A6215 (ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* ___0_destination, ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m61974A48F0249F9BF95E2B30196D22127E7AD9CB_inline (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7*, ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* Array_Empty_TisATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E_m5293579F263F9A3750FDFA1AA45718B28873BFBB_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mEDC9BE8DCA0B1A3E86A7A3BB531829C06CFA5034 (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 ___0_left, ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7, ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m1BA243F694B76E06057D36CAD8FE40AF6F9F10B0_inline (Span_1_t00FA2E5225B268183BB8AEC08EFAA8532A0B403D* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t00FA2E5225B268183BB8AEC08EFAA8532A0B403D*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisAnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03_mA05FD1200E24DDB998528FC769FF4219BC53EE43 (AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* ___0_destination, AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_mDBF6374C7B6653E0696EFB86B12AB11D9D4EA4C5_inline (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029*, AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* Array_Empty_TisAnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03_mC9DEB54C94662C9FD5779A4BFFAB7AD0469A27C8_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mA1563EEFB0CA3654CCED4F7716C0C745D5E04930 (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 ___0_left, ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029, ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m739A39BB426B19B7583557F308B2BDA52E2C3DD9_inline (Span_1_t36B7E942A80FDA89A0043D5EDED0559212667428* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t36B7E942A80FDA89A0043D5EDED0559212667428*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D_m9074142CF2F0828232AFEBB926F490D33E285B9B (BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* ___0_destination, BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m729E93C65964D6955F3A0794056B0A8FA3E1FB44_inline (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7*, BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* Array_Empty_TisBatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D_m652D1F78F0DEF31A60A1D09589A89DE06F12C3E4_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mF6DC606C39C6583C5F7B5BD9E2C131F7250B04E2 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 ___0_left, ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7, ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mD73C9E60D4C33EBDFB279AF8A4AB7B17EA1D4F3A_inline (Span_1_t403EC894E0C29DB4AEFFFACE95188C989E44E524* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t403EC894E0C29DB4AEFFFACE95188C989E44E524*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988_m54E83FBDC4755CF4FADD61A5C99841D951AAEF78 (BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* ___0_destination, BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m872DD525AC862F1722D3C2CC755B72C6E857038D_inline (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E*, BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* Array_Empty_TisBezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988_m89904E98E5508C2B001AC9AB5EE2E1CCA80CEEF3_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m06E929F8878539CC28D9D036C88463DB777E290A (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E ___0_left, ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E, ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m7A0384ECCF4D6B5955A86B1A3AAA14F426708FE9_inline (Span_1_tEA5727F0F276960BF4E47192A9109344388B48E1* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tEA5727F0F276960BF4E47192A9109344388B48E1*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3_mD278D7E5E9C662C8F05E45162C4D4E540419CC20 (BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* ___0_destination, BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m5F9A39BA66796C6F498F90EC36259B20AAC259F8_inline (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F*, BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* Array_Empty_TisBezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3_m77719C87E49330E8DA7EA4CD03C4AFDCE18C19C0_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mF59FE9D28EA17747D9710D454C4D0A9F68FFE7D4 (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F ___0_left, ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F, ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mED1253429B93CB6D2928015A22105A16FF64C86B_inline (Span_1_t087F0E3724EBFD3A74A84E3F9E3F027249F37B51* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t087F0E3724EBFD3A74A84E3F9E3F027249F37B51*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6C1A997DCFEFC3BA96EEB5E75FB8B54DF2D21198 (bool* ___0_destination, bool* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m5818A0EC9B5A3628E69F90A3521BDE96E1FDC74F_inline (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, bool* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2*, bool*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* Array_Empty_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mAB215C445888719BD89809D99C3DBD3135C2B1E7_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m0D13DF1A2B584A204679D1E948943A8B7173B812 (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 ___0_left, ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2, ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mF487895F53310FF6F399B7BD07038C49D29A6EAB_inline (Span_1_tF1D09AE3581D2E6304B9FC753705C3FC5DF90634* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tF1D09AE3581D2E6304B9FC753705C3FC5DF90634*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisBounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3_m4925E1D88272BE2E628F28AFFEC4FF09CC2EC647 (Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* ___0_destination, Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_mB60BC31800C0877424E21575E45AE6C8AA50ADA6_inline (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99*, Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* Array_Empty_TisBounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3_mB6CFBB5D8AF33F0BAE72154209AB29B8D52FBCDA_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m23D991AB6012F2C9896FC8EF47BC84CF63CF86CD (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 ___0_left, ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99, ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m8E944E4954E037877A25B9FF6B901F1F901D4769_inline (Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_mB1465EEEBE0A608FA51B29BC3F145F287AD04190 (uint8_t* ___0_destination, uint8_t* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m0FC0B92549C2968E80B5F75A85F28B96DBFCFD63_inline (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, uint8_t* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D*, uint8_t*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* Array_Empty_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m6080CA526758F4FA182A066B2780D1761CD36ED5_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mB932864AE19818E61EF5ADBBCB02A87BD771CDF7 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D ___0_left, ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D, ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mB79622153F80AD55A805C005842AF045F4FCF992_inline (Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m62AF071D7F91DFC9A4D8B847D6A4472B820B5446 (Il2CppChar* ___0_destination, Il2CppChar* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_CreateString_m3F8794FEB452558B8A68C65E1F0B603B3D94E0E2 (String_t* __this, Il2CppChar* ___0_value, int32_t ___1_startIndex, int32_t ___2_length, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* Array_Empty_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_mD1C1362CB74B91496D984B006ADC79B688D9B50D_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m3ADF20EB2963614D95DD3C4605667A643990414A (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ___0_left, ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1, ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m83147EFB62B7FFBA49EEBEBE975F7348D665BAD0_inline (Span_1_t162CEB1BEB175B131BEFB9D4647CEB2D48558018* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t162CEB1BEB175B131BEFB9D4647CEB2D48558018*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisColor_tD001788D726C3A7F1379BEED0260B9591F440C1F_m3B15D4D61932A340BAAA5BE6E8D419605D3692E6 (Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ___0_destination, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_mD46BD07C5320D1B1A225E53FAF466E153FFC1679_inline (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816*, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* Array_Empty_TisColor_tD001788D726C3A7F1379BEED0260B9591F440C1F_m7E922E24AAEBD664256383832D53DDF17E1F3052_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mB25172459C8906252EAB2A17B1BD28318B8A9AF2 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 ___0_left, ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816, ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m413F20ECFE3F523163B62ED5BAB7DFA22F2FCCD4_inline (Span_1_tCF27B3DC55C4AD13268A1F154352D6782B237F7D* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tCF27B3DC55C4AD13268A1F154352D6782B237F7D*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisColor32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B_mA65CAF282F03FD4DB46666E2C44C22E3D0035A68 (Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* ___0_destination, Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m841651F5E0DEC77984A109EF01090F3F776571FB_inline (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC*, Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* Array_Empty_TisColor32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B_m5B415C4745E47108DD9258EBCCB422EFD6B8A0EB_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mB7E64928FFEDF69D6B4302B599C9461869E152C7 (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC ___0_left, ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC, ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m0B73281BD3E653B71D0308FCD7C435EFD8D176E6_inline (Span_1_t417FB548242C07F6973D324BB1024F79C8D74EAD* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t417FB548242C07F6973D324BB1024F79C8D74EAD*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisCombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE_m3C4417F1F221D78FCCCF0E2157B78FE672A7E86F (CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* ___0_destination, CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_mD840B6A68992D74BFDFF1A7A19DE2840B62C80CC_inline (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A*, CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* Array_Empty_TisCombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE_mC39B4BC80AD9C1F917509BB8BE77C4C73CB471F9_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m7666D2A6469776FB525177C22CFBEDDB5A0C3EEB (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A ___0_left, ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A, ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mFC8114ED22DABAF53250748121F627D7199AFE7C_inline (Span_1_tBBAFA836A153BEF6FDEF8E7DE7F4E0F6DC6B0907* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tBBAFA836A153BEF6FDEF8E7DE7F4E0F6DC6B0907*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B_m7B231E78B8FBBF3128AC7D5D9AA1EF250AB8BBB7 (ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* ___0_destination, ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m3263AAE2466F33D0150BF7B8223568108EB82C0B_inline (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B*, ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* Array_Empty_TisContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B_m2AE4E8C460D733DF5362B246ED865C5CE0C4AA60_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mCFF3D8D583CDF24902773BD33205A6488B81C7B3 (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B ___0_left, ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B, ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mBAB1E51297560C637528EE41B5EB5658D2EFE5D9_inline (Span_1_tFD51B8D0B2DA78C0F911056FC60AB6B732AD4F7C* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tFD51B8D0B2DA78C0F911056FC60AB6B732AD4F7C*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C_m25EF20C7583AB5F948F033EAAAD3726E13608167 (ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* ___0_destination, ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m137BDE40C94EA6785C970709ABF68088E145D866_inline (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962*, ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* Array_Empty_TisConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C_m4779104F7674EACD211A240162D4414087C431E8_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mE5D73B81C2027EABC9DF61DD832743B960E6C20B (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 ___0_left, ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962, ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m3B50FB2CF29A0E7C0C8B69E4E5A72D75883EBC4B_inline (Span_1_tA69488C008961B8AE09141F37FDAE8A0F575CE48* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tA69488C008961B8AE09141F37FDAE8A0F575CE48*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1_m747B7D7DC0AA466783F69D3676EC5EE854D08D52 (ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* ___0_destination, ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_mDB9E4DFF0687E0A5405FF4F6387379524B1B07AB_inline (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A*, ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* Array_Empty_TisConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1_m164EDF2519D24430C10F8BB106ADB252BF225C26_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m0615A4A62B9BB81921E873791EA6E0973505024A (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A ___0_left, ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A, ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mD8B694DDD50485878B197BD97C4848FA0E770DCD_inline (Span_1_t0F901C7F7248B37E2D8773703E8E8790204A0A30* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t0F901C7F7248B37E2D8773703E8E8790204A0A30*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisCopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8_m8A5B65BF262A2B1C988311D13733E08E197C8A03 (CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* ___0_destination, CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m75A7463F5588DB7F45AC0CE204FFB188419EC066_inline (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA*, CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* Array_Empty_TisCopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8_m636107AFEDC2888AAD9A190C5FCB115D4483F86F_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m80E264A2D14E2F539A355539BA0FCA443D47DF41 (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA ___0_left, ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA, ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m385F87A001B432F97C5381EF1F01B485642ED27D_inline (Span_1_tC85B5D30CDD48BE32D9DF528934ED5AEC5BFC116* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tC85B5D30CDD48BE32D9DF528934ED5AEC5BFC116*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisCullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0_m9A4B5780CB8BC1ACD63C193EC68D5E217CDBAC80 (CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* ___0_destination, CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m7515BCC9EAB305B335A968E684B597A1FC9A5F24_inline (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF*, CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* Array_Empty_TisCullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0_m46787E1D6E481E4790F1F9E63000A067C9AF33CD_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m26E754ACC25871CADAADB0E10FEFBC9C47C4B94B (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF ___0_left, ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF, ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m9D56AB26DA029B854B802F9CC7B8E6E02EA21313_inline (Span_1_tAD7D80F571B6C63E604288FC6952D4E8B93EB381* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tAD7D80F571B6C63E604288FC6952D4E8B93EB381*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisDistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_mDC37E2EA7AF7BC4F29B1BBEC90A762EB26B760F5 (DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* ___0_destination, DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m3171966ED88C89D4F7ED5107E056F3CBED6E8194_inline (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A*, DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* Array_Empty_TisDistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_m7807FD2713C36285071ABDD0A617C0D77C6C1EB7_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m11E5D65C3EBFEECBEED20DAF22DE8F003EEAF13B (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A ___0_left, ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A, ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mFF4419EF107B2F1CD3364357F9723EAC96433F03_inline (Span_1_tFCA7D5D0BF7401ABF1CDA871A9259927808E7F73* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tFCA7D5D0BF7401ABF1CDA871A9259927808E7F73*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisDrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5_mDF1CDB2973A50B932ED2E72CC7A57E75C7FCAB54 (DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* ___0_destination, DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_mB6153B16E29314D6497048CA9F509CF21C277F71_inline (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B*, DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* Array_Empty_TisDrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5_m05A4DA3A4ACD6D784909779870EE466DF7778EC7_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mE3EC2F33B96AEACAFC2FAF883F633DF80BC23419 (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B ___0_left, ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B, ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m43418E7D1C7802E4F31E08DAE3AD5C88BAACB75F_inline (Span_1_t43234050271CE7DE614C13A0021A344861FB5593* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t43234050271CE7DE614C13A0021A344861FB5593*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisEasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4_m0343B8FCB3868B60552322D0BFD9C87585A924B1 (EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* ___0_destination, EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_mF80BB7536BFA47CBB0B535E725155286821D1B34_inline (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7*, EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* Array_Empty_TisEasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4_m5BB1B3F942E717EB0EEA932DE146F79E347604BA_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m049B1C57C88411BB8B33BBA73450C898DD25013F (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 ___0_left, ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7, ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m7DA42A78AA51399F6E2624EC951C4C5675A83DF4_inline (Span_1_t6834048668549DB2E233F0E7E8E71F1E3AFDE712* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t6834048668549DB2E233F0E7E8E71F1E3AFDE712*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisEmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B_mDFAF68DAF7C667016B41A5C085293991850D1D8C (EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* ___0_destination, EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m47891B0EBECA0C6CBF5505295F2B0E603027A014_inline (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744*, EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* Array_Empty_TisEmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B_m87CA399E64D82A02BCE81DF2F8F52AD65AC9BC25_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m039886C6A427B52C591AA65AEA91EED22AB2F907 (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 ___0_left, ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744, ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mF40B0389DD242EC35D44F25C7490012314A18568_inline (Span_1_t5D7E7A039DDB174888A2AC5552AF592E3B641F6A* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t5D7E7A039DDB174888A2AC5552AF592E3B641F6A*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisEntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8_m8D3BF3FF1317BAF44B55A14301469F3AB0E60900 (EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* ___0_destination, EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m1F035A6038B22EE2B7A2E6000536EFB888AC4043_inline (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686*, EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* Array_Empty_TisEntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8_m8EF4F258FBF5AEC804ED891772BFFC84A01DB5D9_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m0064C72F0B13F04420687FB7276738A0B0DFFB3A (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 ___0_left, ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686, ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m89645090890B982B6625634865741039212A2D7D_inline (Span_1_t420A830887305E0E02A744003AACBACED16172EB* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_t420A830887305E0E02A744003AACBACED16172EB*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisFilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060_mDD7300E8E4A6CFCCD3B437359FAAA469263822D0 (FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* ___0_destination, FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m25FED3B4C89777BD3B6B73F981D9CF573F5C3435_inline (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27*, FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* Array_Empty_TisFilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060_mA4E39A2097C6E2511B93A1BF0C9221165B65938C_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mFCC221F4B2884F75CC518EB825129A4DEE06CE76 (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 ___0_left, ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27, ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m3A8F09C0012040ACB55950D35820310E9F94B6E6_inline (Span_1_tD4A141E2CAE7DFB331921ECC10AB8884452E2784* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tD4A141E2CAE7DFB331921ECC10AB8884452E2784*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisFlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9_m6C3DAA8091937F2A2CBA773FD3BAA3DCD1C9B2EF (FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* ___0_destination, FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_mBBF4ED998EFF74C1D72698CA0007CD8300A88835_inline (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A*, FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* Array_Empty_TisFlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9_m7E0303803800EFD09E46C64192C01A6D011D4260_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m377EF66B9E4E4BD87483BC46AF730DD83D4917D1 (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A ___0_left, ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A, ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_m8CC502CE0D66420E8B9B92D8F233B020CF67E932_inline (Span_1_tF25812E0445523D140232909284012C24E110428* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tF25812E0445523D140232909284012C24E110428*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisFlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986_m19C1CA978BDC4B53F03AF962E7E6296C9E5A5A33 (FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* ___0_destination, FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_mAD01EEA7AC8462087E2A66DC8852371D56D2B4DD_inline (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D*, FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* Array_Empty_TisFlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986_m907AAB5CF7AEF4C510C711BBF98ABA5EA468D2B1_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_mEC6D213E2DE3F5E82725CD7FFB7EE813023463C0 (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D ___0_left, ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D, ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline int32_t Span_1_get_Length_mE30066A0C4D7876DED68538384FA5CC160C69D64_inline (Span_1_tBFB0B8ED6E219789174070F50BFF3D1D12DDADA4* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Span_1_tBFB0B8ED6E219789174070F50BFF3D1D12DDADA4*, const RuntimeMethod*))Span_1_get_Length_m4CED98A19744D579382FDCEDEF16DBC11586BE1C_fshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Buffer_Memmove_TisGPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32_m83F98C10F75BBF5CBB456D86857C192E9B2D06B6 (GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* ___0_destination, GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* ___1_source, uint64_t ___2_elementCount, const RuntimeMethod* method) ;
inline void ReadOnlySpan_1__ctor_m10A374622B0CBE7DF98D6204B69DB5AB8EBEDC48_inline (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60*, GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32*, int32_t, const RuntimeMethod*))ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* Array_Empty_TisGPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32_m0C324743704C230D82B1D3F4A8A72DAECCD01F62_inline (const RuntimeMethod* method) ;
inline bool ReadOnlySpan_1_op_Equality_m1D1955D11B345171E9FF68FAD8B91958FB6F0653 (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 ___0_left, ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 ___1_right, const RuntimeMethod* method)
{
	return ((  bool (*) (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60, ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60, const RuntimeMethod*))ReadOnlySpan_1_op_Equality_mCB01F92371372FA04CD22F603093A600A977916A_fshared)(___0_left, ___1_right, method);
}
inline void Span_1__ctor_m947BF95D54571BF3897F96822B7A8FDA5853497B_inline (Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305* __this, uint8_t* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305*, uint8_t*, int32_t, const RuntimeMethod*))Span_1__ctor_mBA868F06359701D9950DEB1B10F52F848E9FF6DA_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
inline void Span_1__ctor_mC9BE2938B716B46BB6B9070B94DBE5CE814BC0E2_inline (Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D* __this, Il2CppChar* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method)
{
	((  void (*) (Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D*, Il2CppChar*, int32_t, const RuntimeMethod*))Span_1__ctor_mBA868F06359701D9950DEB1B10F52F848E9FF6DA_fshared_inline)(__this, ___0_ptr, ___1_length, method);
}
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 11078
// Method Definition Index: 11079
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_m8B2AEA509E07E389B797D3576C0DDF9A5551E3A4 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0);
		return L_1;
	}
}
// Method Definition Index: 11080
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 ReadOnlyCollection_1_get_Item_m586F777658B145682E70A3FCE202B5FEE78F8F18 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 L_2;
		L_2 = InterfaceFuncInvoker1< EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11081
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_mD750C5DD43B49E3369FF1CC3778B43D83F58636E (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 L_1 = ___0_value;
		NullCheck(L_0);
		bool L_2;
		L_2 = InterfaceFuncInvoker1< bool, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 >::Invoke(4, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11082
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1_CopyTo_mF318B9F02EA7AE7C2734AC064B64ED94C16F8798 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B* ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B* L_1 = ___0_array;
		int32_t L_2 = ___1_index;
		NullCheck(L_0);
		InterfaceActionInvoker2< EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, L_1, L_2);
		return;
	}
}
// Method Definition Index: 11083
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_GetEnumerator_m7C08CE459BEEDC1DF2952F054F69A3482C2433F6 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 7), L_0);
		return L_1;
	}
}
// Method Definition Index: 11084
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_mDA0A610D1FE508DC17DABD5A4D5D861295857416 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 L_1 = ___0_value;
		NullCheck(L_0);
		int32_t L_2;
		L_2 = InterfaceFuncInvoker1< int32_t, EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 >::Invoke(2, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11085
// Method Definition Index: 11086
// Method Definition Index: 11087
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 ReadOnlyCollection_1_System_Collections_Generic_IListU3CTU3E_get_Item_m5D48D4159D5115984C187FE5B07D9140556FDA49 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 L_2;
		L_2 = InterfaceFuncInvoker1< EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11088
// Method Definition Index: 11089
// Method Definition Index: 11090
// Method Definition Index: 11091
// Method Definition Index: 11092
// Method Definition Index: 11093
// Method Definition Index: 11094
// Method Definition Index: 11095
// Method Definition Index: 11096
// Method Definition Index: 11097
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1_System_Collections_ICollection_CopyTo_m8958097E6E660BACA1709F0424DE966BF89439FD (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, RuntimeArray* ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B* V_0 = NULL;
	Type_t* V_1 = NULL;
	Type_t* V_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_3 = NULL;
	int32_t V_4 = 0;
	int32_t V_5 = 0;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeArray* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(3, NULL);
	}

IL_0009:
	{
		RuntimeArray* L_1 = ___0_array;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = il2cpp_codegen_array_get_rank(L_1);
		if ((((int32_t)L_2) == ((int32_t)1)))
		{
			goto IL_0018;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(7, NULL);
	}

IL_0018:
	{
		RuntimeArray* L_3 = ___0_array;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = Array_GetLowerBound_m4FB0601E2E8A6304A42E3FC400576DF7B0F084BC(L_3, 0, NULL);
		if (!L_4)
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(6, NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_index;
		if ((((int32_t)L_5) >= ((int32_t)0)))
		{
			goto IL_0030;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0030:
	{
		RuntimeArray* L_6 = ___0_array;
		NullCheck(L_6);
		int32_t L_7;
		L_7 = il2cpp_codegen_array_get_length(L_6);
		int32_t L_8 = ___1_index;
		int32_t L_9;
		L_9 = ReadOnlyCollection_1_get_Count_m8B2AEA509E07E389B797D3576C0DDF9A5551E3A4(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 10));
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_7, L_8))) >= ((int32_t)L_9)))
		{
			goto IL_0046;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(5, NULL);
	}

IL_0046:
	{
		RuntimeArray* L_10 = ___0_array;
		V_0 = ((EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B*)IsInstSealed((RuntimeObject*)L_10, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 11)));
		EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B* L_11 = V_0;
		if (!L_11)
		{
			goto IL_005e;
		}
	}
	{
		RuntimeObject* L_12 = __this->___list;
		EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B* L_13 = V_0;
		int32_t L_14 = ___1_index;
		NullCheck(L_12);
		InterfaceActionInvoker2< EntryProcessingInfoU5BU5D_tAB0CBF4A4379F39BA667FBBFCB22E4174C4D319B*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_12, L_13, L_14);
		return;
	}

IL_005e:
	{
		RuntimeArray* L_15 = ___0_array;
		NullCheck(L_15);
		Type_t* L_16;
		L_16 = il2cpp_codegen_object_get_type(L_15);
		NullCheck(L_16);
		Type_t* L_17;
		L_17 = VirtualFuncInvoker0< Type_t* >::Invoke(48, L_16);
		V_1 = L_17;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_18 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 12)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_19;
		L_19 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_18, NULL);
		V_2 = L_19;
		Type_t* L_20 = V_1;
		Type_t* L_21 = V_2;
		NullCheck(L_20);
		bool L_22;
		L_22 = VirtualFuncInvoker1< bool, Type_t* >::Invoke(24, L_20, L_21);
		if (L_22)
		{
			goto IL_008c;
		}
	}
	{
		Type_t* L_23 = V_2;
		Type_t* L_24 = V_1;
		NullCheck(L_23);
		bool L_25;
		L_25 = VirtualFuncInvoker1< bool, Type_t* >::Invoke(24, L_23, L_24);
		if (L_25)
		{
			goto IL_008c;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
	}

IL_008c:
	{
		RuntimeArray* L_26 = ___0_array;
		V_3 = ((ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)IsInst((RuntimeObject*)L_26, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_27 = V_3;
		if (L_27)
		{
			goto IL_009b;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
	}

IL_009b:
	{
		RuntimeObject* L_28 = __this->___list;
		NullCheck(L_28);
		int32_t L_29;
		L_29 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_28);
		V_4 = L_29;
	}
	try
	{
		{
			V_5 = 0;
			goto IL_00cd_1;
		}

IL_00ad_1:
		{
			ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_30 = V_3;
			int32_t L_31 = ___1_index;
			int32_t L_32 = L_31;
			___1_index = ((int32_t)il2cpp_codegen_add(L_32, 1));
			RuntimeObject* L_33 = __this->___list;
			int32_t L_34 = V_5;
			NullCheck(L_33);
			EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 L_35;
			L_35 = InterfaceFuncInvoker1< EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_33, L_34);
			EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 L_36 = L_35;
			RuntimeObject* L_37 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4), &L_36);
			NullCheck(L_30);
			ArrayElementTypeCheck (L_30, L_37);
			(L_30)->SetAt(static_cast<il2cpp_array_size_t>(L_32), (RuntimeObject*)L_37);
			int32_t L_38 = V_5;
			V_5 = ((int32_t)il2cpp_codegen_add(L_38, 1));
		}

IL_00cd_1:
		{
			int32_t L_39 = V_5;
			int32_t L_40 = V_4;
			if ((((int32_t)L_39) < ((int32_t)L_40)))
			{
				goto IL_00ad_1;
			}
		}
		{
			goto IL_00dd;
		}
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_00d5;
		}
		throw e;
	}

CATCH_00d5:
	{
		ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1* L_41 = ((ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*)IL2CPP_GET_ACTIVE_EXCEPTION(ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*));;
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_00dd;
	}

IL_00dd:
	{
		return;
	}
}
// Method Definition Index: 11098
// Method Definition Index: 11099
// Method Definition Index: 11100
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_System_Collections_IList_get_Item_mAC7741CB7BECB294DA1E38E3745C58853F0A197A (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 L_2;
		L_2 = InterfaceFuncInvoker1< EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 L_3 = L_2;
		RuntimeObject* L_4 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4), &L_3);
		return L_4;
	}
}
// Method Definition Index: 11101
// Method Definition Index: 11102
// Method Definition Index: 11103
// Method Definition Index: 11104
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_mC9A90AF46B6E7EC8BC710283B87EB110801FAB1B (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))))
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
		il2cpp_codegen_initobj((&V_0), sizeof(EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289));
		EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289 L_2 = V_0;
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
// Method Definition Index: 11105
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_System_Collections_IList_Contains_mC6029EF4ADA5CF37C75A5D189C30F6EEF696996C (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_value;
		bool L_1;
		L_1 = ReadOnlyCollection_1_IsCompatibleObject_mC9A90AF46B6E7EC8BC710283B87EB110801FAB1B(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_value;
		bool L_3;
		L_3 = ReadOnlyCollection_1_Contains_mD750C5DD43B49E3369FF1CC3778B43D83F58636E(__this, ((*(EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11106
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_System_Collections_IList_IndexOf_mF634A5AA11D90AB73EE09FC2DA892D8C98BD76E1 (ReadOnlyCollection_1_t537840A047E12157FC8F008F8986460D6E1ED235* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_value;
		bool L_1;
		L_1 = ReadOnlyCollection_1_IsCompatibleObject_mC9A90AF46B6E7EC8BC710283B87EB110801FAB1B(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_value;
		int32_t L_3;
		L_3 = ReadOnlyCollection_1_IndexOf_mDA0A610D1FE508DC17DABD5A4D5D861295857416(__this, ((*(EntryProcessingInfo_tE4803801829E2739380552B9DE313B056A9C9289*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11107
// Method Definition Index: 11108
// Method Definition Index: 11109
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 11078
// Method Definition Index: 11079
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_mF0276FF4CFBDD56B7D357DF165F97430B0F8D31F (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0);
		return L_1;
	}
}
// Method Definition Index: 11080
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 ReadOnlyCollection_1_get_Item_mB7CE812B86FFEACB024FE8ED7F7099E3E9E68599 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 L_2;
		L_2 = InterfaceFuncInvoker1< Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11081
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_m6D1B999B20EB8B812887BC95465F6111CC658D31 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 L_1 = ___0_value;
		NullCheck(L_0);
		bool L_2;
		L_2 = InterfaceFuncInvoker1< bool, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 >::Invoke(4, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11082
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1_CopyTo_m0153A349FF630AADC76BD14890C4F6F1C4E927C3 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498* ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498* L_1 = ___0_array;
		int32_t L_2 = ___1_index;
		NullCheck(L_0);
		InterfaceActionInvoker2< ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, L_1, L_2);
		return;
	}
}
// Method Definition Index: 11083
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_GetEnumerator_m266B100EBFFD667F19E085024FFAD72D6D853EA9 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 7), L_0);
		return L_1;
	}
}
// Method Definition Index: 11084
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_mBFBFA5087A22447FCFE83273238A7F4CA47337A9 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 L_1 = ___0_value;
		NullCheck(L_0);
		int32_t L_2;
		L_2 = InterfaceFuncInvoker1< int32_t, Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 >::Invoke(2, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11085
// Method Definition Index: 11086
// Method Definition Index: 11087
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 ReadOnlyCollection_1_System_Collections_Generic_IListU3CTU3E_get_Item_m7C2B35027D240FE3ABD82BEB6D262746999D6F14 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 L_2;
		L_2 = InterfaceFuncInvoker1< Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11088
// Method Definition Index: 11089
// Method Definition Index: 11090
// Method Definition Index: 11091
// Method Definition Index: 11092
// Method Definition Index: 11093
// Method Definition Index: 11094
// Method Definition Index: 11095
// Method Definition Index: 11096
// Method Definition Index: 11097
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1_System_Collections_ICollection_CopyTo_m2C7C0A61FFE98B0C84B37B15D64842BDCE0B4222 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, RuntimeArray* ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498* V_0 = NULL;
	Type_t* V_1 = NULL;
	Type_t* V_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_3 = NULL;
	int32_t V_4 = 0;
	int32_t V_5 = 0;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeArray* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(3, NULL);
	}

IL_0009:
	{
		RuntimeArray* L_1 = ___0_array;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = il2cpp_codegen_array_get_rank(L_1);
		if ((((int32_t)L_2) == ((int32_t)1)))
		{
			goto IL_0018;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(7, NULL);
	}

IL_0018:
	{
		RuntimeArray* L_3 = ___0_array;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = Array_GetLowerBound_m4FB0601E2E8A6304A42E3FC400576DF7B0F084BC(L_3, 0, NULL);
		if (!L_4)
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(6, NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_index;
		if ((((int32_t)L_5) >= ((int32_t)0)))
		{
			goto IL_0030;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0030:
	{
		RuntimeArray* L_6 = ___0_array;
		NullCheck(L_6);
		int32_t L_7;
		L_7 = il2cpp_codegen_array_get_length(L_6);
		int32_t L_8 = ___1_index;
		int32_t L_9;
		L_9 = ReadOnlyCollection_1_get_Count_mF0276FF4CFBDD56B7D357DF165F97430B0F8D31F(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 10));
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_7, L_8))) >= ((int32_t)L_9)))
		{
			goto IL_0046;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(5, NULL);
	}

IL_0046:
	{
		RuntimeArray* L_10 = ___0_array;
		V_0 = ((ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498*)IsInstSealed((RuntimeObject*)L_10, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 11)));
		ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498* L_11 = V_0;
		if (!L_11)
		{
			goto IL_005e;
		}
	}
	{
		RuntimeObject* L_12 = __this->___list;
		ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498* L_13 = V_0;
		int32_t L_14 = ___1_index;
		NullCheck(L_12);
		InterfaceActionInvoker2< ItemU5BU5D_t9EF4F4542E9108F5AB0A8361AD74FF1A14DE2498*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_12, L_13, L_14);
		return;
	}

IL_005e:
	{
		RuntimeArray* L_15 = ___0_array;
		NullCheck(L_15);
		Type_t* L_16;
		L_16 = il2cpp_codegen_object_get_type(L_15);
		NullCheck(L_16);
		Type_t* L_17;
		L_17 = VirtualFuncInvoker0< Type_t* >::Invoke(48, L_16);
		V_1 = L_17;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_18 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 12)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_19;
		L_19 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_18, NULL);
		V_2 = L_19;
		Type_t* L_20 = V_1;
		Type_t* L_21 = V_2;
		NullCheck(L_20);
		bool L_22;
		L_22 = VirtualFuncInvoker1< bool, Type_t* >::Invoke(24, L_20, L_21);
		if (L_22)
		{
			goto IL_008c;
		}
	}
	{
		Type_t* L_23 = V_2;
		Type_t* L_24 = V_1;
		NullCheck(L_23);
		bool L_25;
		L_25 = VirtualFuncInvoker1< bool, Type_t* >::Invoke(24, L_23, L_24);
		if (L_25)
		{
			goto IL_008c;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
	}

IL_008c:
	{
		RuntimeArray* L_26 = ___0_array;
		V_3 = ((ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)IsInst((RuntimeObject*)L_26, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_27 = V_3;
		if (L_27)
		{
			goto IL_009b;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
	}

IL_009b:
	{
		RuntimeObject* L_28 = __this->___list;
		NullCheck(L_28);
		int32_t L_29;
		L_29 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_28);
		V_4 = L_29;
	}
	try
	{
		{
			V_5 = 0;
			goto IL_00cd_1;
		}

IL_00ad_1:
		{
			ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_30 = V_3;
			int32_t L_31 = ___1_index;
			int32_t L_32 = L_31;
			___1_index = ((int32_t)il2cpp_codegen_add(L_32, 1));
			RuntimeObject* L_33 = __this->___list;
			int32_t L_34 = V_5;
			NullCheck(L_33);
			Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 L_35;
			L_35 = InterfaceFuncInvoker1< Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_33, L_34);
			Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 L_36 = L_35;
			RuntimeObject* L_37 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4), &L_36);
			NullCheck(L_30);
			ArrayElementTypeCheck (L_30, L_37);
			(L_30)->SetAt(static_cast<il2cpp_array_size_t>(L_32), (RuntimeObject*)L_37);
			int32_t L_38 = V_5;
			V_5 = ((int32_t)il2cpp_codegen_add(L_38, 1));
		}

IL_00cd_1:
		{
			int32_t L_39 = V_5;
			int32_t L_40 = V_4;
			if ((((int32_t)L_39) < ((int32_t)L_40)))
			{
				goto IL_00ad_1;
			}
		}
		{
			goto IL_00dd;
		}
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_00d5;
		}
		throw e;
	}

CATCH_00d5:
	{
		ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1* L_41 = ((ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*)IL2CPP_GET_ACTIVE_EXCEPTION(ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*));;
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_00dd;
	}

IL_00dd:
	{
		return;
	}
}
// Method Definition Index: 11098
// Method Definition Index: 11099
// Method Definition Index: 11100
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_System_Collections_IList_get_Item_m74B65BACD023E998D48CBC35FE2F75E3BB709737 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 L_2;
		L_2 = InterfaceFuncInvoker1< Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 L_3 = L_2;
		RuntimeObject* L_4 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4), &L_3);
		return L_4;
	}
}
// Method Definition Index: 11101
// Method Definition Index: 11102
// Method Definition Index: 11103
// Method Definition Index: 11104
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_m94BEB48405CC3157014AA7165FF29E33FB86873D (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))))
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
		il2cpp_codegen_initobj((&V_0), sizeof(Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346));
		Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346 L_2 = V_0;
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
// Method Definition Index: 11105
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_System_Collections_IList_Contains_m05DB73DD04F5B6505938C164518BB64491F8CEDE (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_value;
		bool L_1;
		L_1 = ReadOnlyCollection_1_IsCompatibleObject_m94BEB48405CC3157014AA7165FF29E33FB86873D(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_value;
		bool L_3;
		L_3 = ReadOnlyCollection_1_Contains_m6D1B999B20EB8B812887BC95465F6111CC658D31(__this, ((*(Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11106
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_System_Collections_IList_IndexOf_m077AF1541DC89A83DAE137727FA68DA5BD3CE9C2 (ReadOnlyCollection_1_t2B35133435FCFBF6E312870171066215A2CE1730* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_value;
		bool L_1;
		L_1 = ReadOnlyCollection_1_IsCompatibleObject_m94BEB48405CC3157014AA7165FF29E33FB86873D(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_value;
		int32_t L_3;
		L_3 = ReadOnlyCollection_1_IndexOf_mBFBFA5087A22447FCFE83273238A7F4CA47337A9(__this, ((*(Item_t0AEAE00CD3B7D53E287DE0FB4F994C1CDA433346*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11107
// Method Definition Index: 11108
// Method Definition Index: 11109
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 11078
// Method Definition Index: 11079
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_mB93E2CFB5B2969AC39C4A63A510317CFDEE6CFF9 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0);
		return L_1;
	}
}
// Method Definition Index: 11080
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ReadOnlyCollection_1_get_Item_m2873EE6951A9D52360B2983D71A24CBBB412E669 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 L_2;
		L_2 = InterfaceFuncInvoker1< Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11081
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_mD3065D7A926DDD6807774F528EF0D74C95B73147 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 L_1 = ___0_value;
		NullCheck(L_0);
		bool L_2;
		L_2 = InterfaceFuncInvoker1< bool, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 >::Invoke(4, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11082
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1_CopyTo_m518D0DA542FB03A5EC3D28A2BA16ECAC2D01DD25 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73* ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73* L_1 = ___0_array;
		int32_t L_2 = ___1_index;
		NullCheck(L_0);
		InterfaceActionInvoker2< ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, L_1, L_2);
		return;
	}
}
// Method Definition Index: 11083
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_GetEnumerator_mC29C91AEC09530EFA2EC4F7CDB86C1F7E2973140 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 7), L_0);
		return L_1;
	}
}
// Method Definition Index: 11084
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_m26DD8D621F4B82DD8AF033C7ABF8ABC7475D7ADB (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 L_1 = ___0_value;
		NullCheck(L_0);
		int32_t L_2;
		L_2 = InterfaceFuncInvoker1< int32_t, Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 >::Invoke(2, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11085
// Method Definition Index: 11086
// Method Definition Index: 11087
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 ReadOnlyCollection_1_System_Collections_Generic_IListU3CTU3E_get_Item_m8E4C2C760903C7EA67FE6FF09C09A49877F3CFE5 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 L_2;
		L_2 = InterfaceFuncInvoker1< Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11088
// Method Definition Index: 11089
// Method Definition Index: 11090
// Method Definition Index: 11091
// Method Definition Index: 11092
// Method Definition Index: 11093
// Method Definition Index: 11094
// Method Definition Index: 11095
// Method Definition Index: 11096
// Method Definition Index: 11097
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1_System_Collections_ICollection_CopyTo_m069159C372D25365EDAC10B39048134700316192 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, RuntimeArray* ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73* V_0 = NULL;
	Type_t* V_1 = NULL;
	Type_t* V_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_3 = NULL;
	int32_t V_4 = 0;
	int32_t V_5 = 0;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeArray* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(3, NULL);
	}

IL_0009:
	{
		RuntimeArray* L_1 = ___0_array;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = il2cpp_codegen_array_get_rank(L_1);
		if ((((int32_t)L_2) == ((int32_t)1)))
		{
			goto IL_0018;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(7, NULL);
	}

IL_0018:
	{
		RuntimeArray* L_3 = ___0_array;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = Array_GetLowerBound_m4FB0601E2E8A6304A42E3FC400576DF7B0F084BC(L_3, 0, NULL);
		if (!L_4)
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(6, NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_index;
		if ((((int32_t)L_5) >= ((int32_t)0)))
		{
			goto IL_0030;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0030:
	{
		RuntimeArray* L_6 = ___0_array;
		NullCheck(L_6);
		int32_t L_7;
		L_7 = il2cpp_codegen_array_get_length(L_6);
		int32_t L_8 = ___1_index;
		int32_t L_9;
		L_9 = ReadOnlyCollection_1_get_Count_mB93E2CFB5B2969AC39C4A63A510317CFDEE6CFF9(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 10));
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_7, L_8))) >= ((int32_t)L_9)))
		{
			goto IL_0046;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(5, NULL);
	}

IL_0046:
	{
		RuntimeArray* L_10 = ___0_array;
		V_0 = ((ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73*)IsInstSealed((RuntimeObject*)L_10, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 11)));
		ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73* L_11 = V_0;
		if (!L_11)
		{
			goto IL_005e;
		}
	}
	{
		RuntimeObject* L_12 = __this->___list;
		ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73* L_13 = V_0;
		int32_t L_14 = ___1_index;
		NullCheck(L_12);
		InterfaceActionInvoker2< ItemU5BU5D_t1BE09B3DC30555BDE6DCCB3E47B0CAD060E38B73*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_12, L_13, L_14);
		return;
	}

IL_005e:
	{
		RuntimeArray* L_15 = ___0_array;
		NullCheck(L_15);
		Type_t* L_16;
		L_16 = il2cpp_codegen_object_get_type(L_15);
		NullCheck(L_16);
		Type_t* L_17;
		L_17 = VirtualFuncInvoker0< Type_t* >::Invoke(48, L_16);
		V_1 = L_17;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_18 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 12)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_19;
		L_19 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_18, NULL);
		V_2 = L_19;
		Type_t* L_20 = V_1;
		Type_t* L_21 = V_2;
		NullCheck(L_20);
		bool L_22;
		L_22 = VirtualFuncInvoker1< bool, Type_t* >::Invoke(24, L_20, L_21);
		if (L_22)
		{
			goto IL_008c;
		}
	}
	{
		Type_t* L_23 = V_2;
		Type_t* L_24 = V_1;
		NullCheck(L_23);
		bool L_25;
		L_25 = VirtualFuncInvoker1< bool, Type_t* >::Invoke(24, L_23, L_24);
		if (L_25)
		{
			goto IL_008c;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
	}

IL_008c:
	{
		RuntimeArray* L_26 = ___0_array;
		V_3 = ((ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)IsInst((RuntimeObject*)L_26, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_27 = V_3;
		if (L_27)
		{
			goto IL_009b;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
	}

IL_009b:
	{
		RuntimeObject* L_28 = __this->___list;
		NullCheck(L_28);
		int32_t L_29;
		L_29 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_28);
		V_4 = L_29;
	}
	try
	{
		{
			V_5 = 0;
			goto IL_00cd_1;
		}

IL_00ad_1:
		{
			ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_30 = V_3;
			int32_t L_31 = ___1_index;
			int32_t L_32 = L_31;
			___1_index = ((int32_t)il2cpp_codegen_add(L_32, 1));
			RuntimeObject* L_33 = __this->___list;
			int32_t L_34 = V_5;
			NullCheck(L_33);
			Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 L_35;
			L_35 = InterfaceFuncInvoker1< Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_33, L_34);
			Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 L_36 = L_35;
			RuntimeObject* L_37 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4), &L_36);
			NullCheck(L_30);
			ArrayElementTypeCheck (L_30, L_37);
			(L_30)->SetAt(static_cast<il2cpp_array_size_t>(L_32), (RuntimeObject*)L_37);
			int32_t L_38 = V_5;
			V_5 = ((int32_t)il2cpp_codegen_add(L_38, 1));
		}

IL_00cd_1:
		{
			int32_t L_39 = V_5;
			int32_t L_40 = V_4;
			if ((((int32_t)L_39) < ((int32_t)L_40)))
			{
				goto IL_00ad_1;
			}
		}
		{
			goto IL_00dd;
		}
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_00d5;
		}
		throw e;
	}

CATCH_00d5:
	{
		ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1* L_41 = ((ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*)IL2CPP_GET_ACTIVE_EXCEPTION(ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*));;
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_00dd;
	}

IL_00dd:
	{
		return;
	}
}
// Method Definition Index: 11098
// Method Definition Index: 11099
// Method Definition Index: 11100
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_System_Collections_IList_get_Item_mBE287B5E2719FB5CA2D4CBB5BC113174C63D77DE (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 L_2;
		L_2 = InterfaceFuncInvoker1< Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 L_3 = L_2;
		RuntimeObject* L_4 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4), &L_3);
		return L_4;
	}
}
// Method Definition Index: 11101
// Method Definition Index: 11102
// Method Definition Index: 11103
// Method Definition Index: 11104
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_m989053EC4E5C8CEDAE6D100C0507BCE7CF6BE8A7 (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))))
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
		il2cpp_codegen_initobj((&V_0), sizeof(Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9));
		Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9 L_2 = V_0;
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
// Method Definition Index: 11105
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_System_Collections_IList_Contains_mA660C38FD0787017894FDFDC334E44CFD2612386 (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_value;
		bool L_1;
		L_1 = ReadOnlyCollection_1_IsCompatibleObject_m989053EC4E5C8CEDAE6D100C0507BCE7CF6BE8A7(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_value;
		bool L_3;
		L_3 = ReadOnlyCollection_1_Contains_mD3065D7A926DDD6807774F528EF0D74C95B73147(__this, ((*(Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11106
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_System_Collections_IList_IndexOf_m7158231C15FFBB9BFA6D6C619F34E574BDCC76CF (ReadOnlyCollection_1_t46D95FFFC4790800955B456576222EF3ACAD2DC3* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_value;
		bool L_1;
		L_1 = ReadOnlyCollection_1_IsCompatibleObject_m989053EC4E5C8CEDAE6D100C0507BCE7CF6BE8A7(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_value;
		int32_t L_3;
		L_3 = ReadOnlyCollection_1_IndexOf_m26DD8D621F4B82DD8AF033C7ABF8ABC7475D7ADB(__this, ((*(Item_t451B0F5ACBE74A12F984548E7F04A85270912DA9*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11107
// Method Definition Index: 11108
// Method Definition Index: 11109
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 11078
// Method Definition Index: 11079
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_get_Count_mA5768C536204B5BD1FAF1DA63A182D90F8E99A00 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0);
		return L_1;
	}
}
// Method Definition Index: 11080
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B ReadOnlyCollection_1_get_Item_m629A53464FB7B4FBBB8B011A4E5ABDA2FB236B6C (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B L_2;
		L_2 = InterfaceFuncInvoker1< RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11081
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_Contains_m75DEDE8EC817491CD64CE8D09BCFBA73BDB72656 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B L_1 = ___0_value;
		NullCheck(L_0);
		bool L_2;
		L_2 = InterfaceFuncInvoker1< bool, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B >::Invoke(4, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11082
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1_CopyTo_m6D1B02DA65790B10D6AC649CB5BFB02D47B70D95 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316* ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316* L_1 = ___0_array;
		int32_t L_2 = ___1_index;
		NullCheck(L_0);
		InterfaceActionInvoker2< RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, L_1, L_2);
		return;
	}
}
// Method Definition Index: 11083
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_GetEnumerator_m12E9F80E8FBC3968C432691E7BC1C495FB1E362C (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 7), L_0);
		return L_1;
	}
}
// Method Definition Index: 11084
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_IndexOf_mD092C8821B7FC54D44F94C4F3B282D4588DED068 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B L_1 = ___0_value;
		NullCheck(L_0);
		int32_t L_2;
		L_2 = InterfaceFuncInvoker1< int32_t, RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B >::Invoke(2, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11085
// Method Definition Index: 11086
// Method Definition Index: 11087
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B ReadOnlyCollection_1_System_Collections_Generic_IListU3CTU3E_get_Item_m1D5EB6438C4221D078C4F7C47FD49C17F44D49E0 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B L_2;
		L_2 = InterfaceFuncInvoker1< RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11088
// Method Definition Index: 11089
// Method Definition Index: 11090
// Method Definition Index: 11091
// Method Definition Index: 11092
// Method Definition Index: 11093
// Method Definition Index: 11094
// Method Definition Index: 11095
// Method Definition Index: 11096
// Method Definition Index: 11097
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyCollection_1_System_Collections_ICollection_CopyTo_m077B6DC74DFC374D67ED0A2123019815AD604BC3 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RuntimeArray* ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316* V_0 = NULL;
	Type_t* V_1 = NULL;
	Type_t* V_2 = NULL;
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_3 = NULL;
	int32_t V_4 = 0;
	int32_t V_5 = 0;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeArray* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		ThrowHelper_ThrowArgumentNullException_m05B7DB75576C421D7CA84FA73F84D7E114974CEC(3, NULL);
	}

IL_0009:
	{
		RuntimeArray* L_1 = ___0_array;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = il2cpp_codegen_array_get_rank(L_1);
		if ((((int32_t)L_2) == ((int32_t)1)))
		{
			goto IL_0018;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(7, NULL);
	}

IL_0018:
	{
		RuntimeArray* L_3 = ___0_array;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = Array_GetLowerBound_m4FB0601E2E8A6304A42E3FC400576DF7B0F084BC(L_3, 0, NULL);
		if (!L_4)
		{
			goto IL_0027;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(6, NULL);
	}

IL_0027:
	{
		int32_t L_5 = ___1_index;
		if ((((int32_t)L_5) >= ((int32_t)0)))
		{
			goto IL_0030;
		}
	}
	{
		ThrowHelper_ThrowIndexArgumentOutOfRange_NeedNonNegNumException_m57AAB1E093F20BFC64BDDBD90FB5B592F582B82F(NULL);
	}

IL_0030:
	{
		RuntimeArray* L_6 = ___0_array;
		NullCheck(L_6);
		int32_t L_7;
		L_7 = il2cpp_codegen_array_get_length(L_6);
		int32_t L_8 = ___1_index;
		int32_t L_9;
		L_9 = ReadOnlyCollection_1_get_Count_mA5768C536204B5BD1FAF1DA63A182D90F8E99A00(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 10));
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_7, L_8))) >= ((int32_t)L_9)))
		{
			goto IL_0046;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_m698044D4F664D7D0DDB88124EEEE2D052AF628BA(5, NULL);
	}

IL_0046:
	{
		RuntimeArray* L_10 = ___0_array;
		V_0 = ((RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316*)IsInstSealed((RuntimeObject*)L_10, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 11)));
		RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316* L_11 = V_0;
		if (!L_11)
		{
			goto IL_005e;
		}
	}
	{
		RuntimeObject* L_12 = __this->___list;
		RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316* L_13 = V_0;
		int32_t L_14 = ___1_index;
		NullCheck(L_12);
		InterfaceActionInvoker2< RecordingItemU5BU5D_t228311297F412A340877DF3E2E283CF129D86316*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_12, L_13, L_14);
		return;
	}

IL_005e:
	{
		RuntimeArray* L_15 = ___0_array;
		NullCheck(L_15);
		Type_t* L_16;
		L_16 = il2cpp_codegen_object_get_type(L_15);
		NullCheck(L_16);
		Type_t* L_17;
		L_17 = VirtualFuncInvoker0< Type_t* >::Invoke(48, L_16);
		V_1 = L_17;
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_18 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(il2cpp_codegen_method_rgctx(method), 12)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_19;
		L_19 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_18, NULL);
		V_2 = L_19;
		Type_t* L_20 = V_1;
		Type_t* L_21 = V_2;
		NullCheck(L_20);
		bool L_22;
		L_22 = VirtualFuncInvoker1< bool, Type_t* >::Invoke(24, L_20, L_21);
		if (L_22)
		{
			goto IL_008c;
		}
	}
	{
		Type_t* L_23 = V_2;
		Type_t* L_24 = V_1;
		NullCheck(L_23);
		bool L_25;
		L_25 = VirtualFuncInvoker1< bool, Type_t* >::Invoke(24, L_23, L_24);
		if (L_25)
		{
			goto IL_008c;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
	}

IL_008c:
	{
		RuntimeArray* L_26 = ___0_array;
		V_3 = ((ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)IsInst((RuntimeObject*)L_26, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_27 = V_3;
		if (L_27)
		{
			goto IL_009b;
		}
	}
	{
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
	}

IL_009b:
	{
		RuntimeObject* L_28 = __this->___list;
		NullCheck(L_28);
		int32_t L_29;
		L_29 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_28);
		V_4 = L_29;
	}
	try
	{
		{
			V_5 = 0;
			goto IL_00cd_1;
		}

IL_00ad_1:
		{
			ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_30 = V_3;
			int32_t L_31 = ___1_index;
			int32_t L_32 = L_31;
			___1_index = ((int32_t)il2cpp_codegen_add(L_32, 1));
			RuntimeObject* L_33 = __this->___list;
			int32_t L_34 = V_5;
			NullCheck(L_33);
			RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B L_35;
			L_35 = InterfaceFuncInvoker1< RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_33, L_34);
			RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B L_36 = L_35;
			RuntimeObject* L_37 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4), &L_36);
			NullCheck(L_30);
			ArrayElementTypeCheck (L_30, L_37);
			(L_30)->SetAt(static_cast<il2cpp_array_size_t>(L_32), (RuntimeObject*)L_37);
			int32_t L_38 = V_5;
			V_5 = ((int32_t)il2cpp_codegen_add(L_38, 1));
		}

IL_00cd_1:
		{
			int32_t L_39 = V_5;
			int32_t L_40 = V_4;
			if ((((int32_t)L_39) < ((int32_t)L_40)))
			{
				goto IL_00ad_1;
			}
		}
		{
			goto IL_00dd;
		}
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_00d5;
		}
		throw e;
	}

CATCH_00d5:
	{
		ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1* L_41 = ((ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*)IL2CPP_GET_ACTIVE_EXCEPTION(ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*));;
		ThrowHelper_ThrowArgumentException_Argument_InvalidArrayType_m469A6A5731A0F1E94D8B609ED9D001C3A1652A58(NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_00dd;
	}

IL_00dd:
	{
		return;
	}
}
// Method Definition Index: 11098
// Method Definition Index: 11099
// Method Definition Index: 11100
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_System_Collections_IList_get_Item_mA0918F854D8301692ABD5FC09118B33565AB5510 (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		int32_t L_1 = ___0_index;
		NullCheck(L_0);
		RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B L_2;
		L_2 = InterfaceFuncInvoker1< RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B, int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 2), L_0, L_1);
		RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B L_3 = L_2;
		RuntimeObject* L_4 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4), &L_3);
		return L_4;
	}
}
// Method Definition Index: 11101
// Method Definition Index: 11102
// Method Definition Index: 11103
// Method Definition Index: 11104
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_IsCompatibleObject_m1614379187C193FBC11C8EA68980A7A00B472377 (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		RuntimeObject* L_0 = ___0_value;
		if (((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))))
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
		il2cpp_codegen_initobj((&V_0), sizeof(RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B));
		RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B L_2 = V_0;
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
// Method Definition Index: 11105
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyCollection_1_System_Collections_IList_Contains_m03F5113A82A515CBF9586737F10A9A7F2F0F6B9C (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_value;
		bool L_1;
		L_1 = ReadOnlyCollection_1_IsCompatibleObject_m1614379187C193FBC11C8EA68980A7A00B472377(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_value;
		bool L_3;
		L_3 = ReadOnlyCollection_1_Contains_m75DEDE8EC817491CD64CE8D09BCFBA73BDB72656(__this, ((*(RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 14));
		return L_3;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11106
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyCollection_1_System_Collections_IList_IndexOf_m7CDCD9BDBD24662DDD49C70C631294B140E295AE (ReadOnlyCollection_1_t0CB4C481EACBE4CE57BE9DDE62444E73D8C242E9* __this, RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_value;
		bool L_1;
		L_1 = ReadOnlyCollection_1_IsCompatibleObject_m1614379187C193FBC11C8EA68980A7A00B472377(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 13));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_value;
		int32_t L_3;
		L_3 = ReadOnlyCollection_1_IndexOf_mD092C8821B7FC54D44F94C4F3B282D4588DED068(__this, ((*(RecordingItem_t93E6DE178E1A6CD7D286976816F71B561800900B*)UnBox(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 4)))), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 15));
		return L_3;
	}

IL_0015:
	{
		return (-1);
	}
}
// Method Definition Index: 11107
// Method Definition Index: 11108
// Method Definition Index: 11109
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 11126
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* ReadOnlyDictionary_2_get_Keys_m3D38C4849A0CBA72A3BEC65E1F8DBBEA252E34F0_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* L_0 = __this->____keys;
		if (L_0)
		{
			goto IL_001e;
		}
	}
	{
		RuntimeObject* L_1 = __this->___m_dictionary;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(2, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_1);
		KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* L_3 = (KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 2));
		((  void (*) (KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 3)))(L_3, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 3));
		__this->____keys = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____keys), (void*)L_3);
	}

IL_001e:
	{
		KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* L_4 = __this->____keys;
		return L_4;
	}
}
// Method Definition Index: 11127
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* ReadOnlyDictionary_2_get_Values_m8896F49C0945D79E3B09562D2CDC7582F2FD7328_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* L_0 = __this->____values;
		if (L_0)
		{
			goto IL_001e;
		}
	}
	{
		RuntimeObject* L_1 = __this->___m_dictionary;
		NullCheck(L_1);
		RuntimeObject* L_2;
		L_2 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(3, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_1);
		ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* L_3 = (ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849*)il2cpp_codegen_object_new(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 5));
		((  void (*) (ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 6)))(L_3, L_2, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 6));
		__this->____values = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____values), (void*)L_3);
	}

IL_001e:
	{
		ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* L_4 = __this->____values;
		return L_4;
	}
}
// Method Definition Index: 11128
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_ContainsKey_mE23701A23F87A60B1BCF98D9292D94BB14ADE64C_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, Il2CppFullySharedGenericAny ___0_key, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? ___0_key : &___0_key), SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
		NullCheck(L_0);
		bool L_2;
		L_2 = InterfaceFuncInvoker1Invoker< bool, Il2CppFullySharedGenericAny >::Invoke(4, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? L_1: *(void**)L_1));
		return L_2;
	}
}
// Method Definition Index: 11129
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_Generic_IDictionaryU3CTKeyU2CTValueU3E_get_Keys_mBEBC84C1F5E1B11D0450837253B41C389D74FABF_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* L_0;
		L_0 = ((  KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* (*) (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 9)))(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 9));
		return (RuntimeObject*)L_0;
	}
}
// Method Definition Index: 11130
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_TryGetValue_m1E53DEAAC0A4FA6EA9A62FD6CAE08C28094D206B_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, Il2CppFullySharedGenericAny ___0_key, Il2CppFullySharedGenericAny* ___1_value, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? ___0_key : &___0_key), SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
		Il2CppFullySharedGenericAny* L_2 = ___1_value;
		NullCheck(L_0);
		bool L_3;
		L_3 = InterfaceFuncInvoker2Invoker< bool, Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny* >::Invoke(7, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? L_1: *(void**)L_1), L_2);
		return L_3;
	}
}
// Method Definition Index: 11131
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_Generic_IDictionaryU3CTKeyU2CTValueU3E_get_Values_m48CD0FA2868BA87C697AA1271790FCFD0D38ED69_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* L_0;
		L_0 = ((  ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* (*) (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11)))(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return (RuntimeObject*)L_0;
	}
}
// Method Definition Index: 11132
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_get_Item_m7C1B738861802341266E872538431A816B1507A2_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, Il2CppFullySharedGenericAny ___0_key, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7));
	const uint32_t SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 13));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? ___0_key : &___0_key), SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
		NullCheck(L_0);
		InterfaceActionInvoker2Invoker< Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? L_1: *(void**)L_1), (Il2CppFullySharedGenericAny*)L_2);
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1);
		return;
	}
}
// Method Definition Index: 11133
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_Generic_IDictionaryU3CTKeyU2CTValueU3E_Add_m760232769BCF4D542ADC3675CDFB72C49999896D_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, Il2CppFullySharedGenericAny ___0_key, Il2CppFullySharedGenericAny ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11134
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_System_Collections_Generic_IDictionaryU3CTKeyU2CTValueU3E_Remove_mD8EBB6AFBE0B504354FA2AD3C925191E6609F731_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, Il2CppFullySharedGenericAny ___0_key, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11135
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_Generic_IDictionaryU3CTKeyU2CTValueU3E_get_Item_m9574ECF6ECDBFB0B78285A7C8AFF03FD2B4013D6_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, Il2CppFullySharedGenericAny ___0_key, Il2CppFullySharedGenericAny* il2cppRetVal, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7));
	const uint32_t SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 13));
	const Il2CppFullySharedGenericAny L_1 = alloca(SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
	const Il2CppFullySharedGenericAny L_2 = alloca(SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		il2cpp_codegen_memcpy(L_1, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? ___0_key : &___0_key), SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
		NullCheck(L_0);
		InterfaceActionInvoker2Invoker< Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 0), L_0, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? L_1: *(void**)L_1), (Il2CppFullySharedGenericAny*)L_2);
		il2cpp_codegen_memcpy(il2cppRetVal, L_2, SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1);
		return;
	}
}
// Method Definition Index: 11136
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_Generic_IDictionaryU3CTKeyU2CTValueU3E_set_Item_m04BE92540AEBDD07DF1A30D6736F68317A8086B1_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, Il2CppFullySharedGenericAny ___0_key, Il2CppFullySharedGenericAny ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11137
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyDictionary_2_get_Count_m570BC218FBF8EE3D7E5551E874DF20638F397CA7_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		NullCheck((RuntimeObject*)L_0);
		int32_t L_1;
		L_1 = InterfaceFuncInvoker0< int32_t >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 14), (RuntimeObject*)L_0);
		return L_1;
	}
}
// Method Definition Index: 11138
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_System_Collections_Generic_ICollectionU3CSystem_Collections_Generic_KeyValuePairU3CTKeyU2CTValueU3EU3E_Contains_m1911E89A7546D2C7F1DD67B61404C8B3D00A36EE_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 ___0_item, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
	const KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 L_1 = alloca(SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		il2cpp_codegen_memcpy(L_1, ___0_item, SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
		NullCheck((RuntimeObject*)L_0);
		bool L_2;
		L_2 = InterfaceFuncInvoker1Invoker< bool, KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 >::Invoke(4, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 14), (RuntimeObject*)L_0, L_1);
		return L_2;
	}
}
// Method Definition Index: 11139
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_Generic_ICollectionU3CSystem_Collections_Generic_KeyValuePairU3CTKeyU2CTValueU3EU3E_CopyTo_mF57A320EF60929F01E8B81CEA0B003EFF819CB79_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57* ___0_array, int32_t ___1_arrayIndex, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57* L_1 = ___0_array;
		int32_t L_2 = ___1_arrayIndex;
		NullCheck((RuntimeObject*)L_0);
		InterfaceActionInvoker2< KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 14), (RuntimeObject*)L_0, L_1, L_2);
		return;
	}
}
// Method Definition Index: 11140
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_System_Collections_Generic_ICollectionU3CSystem_Collections_Generic_KeyValuePairU3CTKeyU2CTValueU3EU3E_get_IsReadOnly_m95064FB8DE29991E11D317B1112868234B687EB6_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		return (bool)1;
	}
}
// Method Definition Index: 11141
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_Generic_ICollectionU3CSystem_Collections_Generic_KeyValuePairU3CTKeyU2CTValueU3EU3E_Add_mE36881BC770002B4BFB810CDD84B88FE8500A55F_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11142
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_Generic_ICollectionU3CSystem_Collections_Generic_KeyValuePairU3CTKeyU2CTValueU3EU3E_Clear_mFB7C727CCCA77F742773A4537B99A5042C48DBF3_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11143
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_System_Collections_Generic_ICollectionU3CSystem_Collections_Generic_KeyValuePairU3CTKeyU2CTValueU3EU3E_Remove_mD21C9806294C21C66C48BBCC7124FF9F72753A08_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 ___0_item, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11144
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_GetEnumerator_m3FFC97D7546675C2514644991A7E196D5039E5AB_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		NullCheck((RuntimeObject*)L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 19), (RuntimeObject*)L_0);
		return L_1;
	}
}
// Method Definition Index: 11145
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_IEnumerable_GetEnumerator_mE28E564A8649570EA9A1CE7107DF7E5FE7A21EFD_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_t6331596D5DD37C462B1B8D49CF6B319B00AB7131_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		NullCheck((RuntimeObject*)L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, IEnumerable_t6331596D5DD37C462B1B8D49CF6B319B00AB7131_il2cpp_TypeInfo_var, (RuntimeObject*)L_0);
		return L_1;
	}
}
// Method Definition Index: 11146
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_IsCompatibleKey_m4D74774D18B06ACA450E3D355EF8398353B677EE_fshared (RuntimeObject* ___0_key, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_key;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7D028CCE3B6E7B61AE2C752D7AE970DA04AB7C6)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		RuntimeObject* L_2 = ___0_key;
		return (bool)((!(((RuntimeObject*)(RuntimeObject*)((RuntimeObject*)IsInst((RuntimeObject*)L_2, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7)))) <= ((RuntimeObject*)(RuntimeObject*)NULL)))? 1 : 0);
	}
}
// Method Definition Index: 11147
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_IDictionary_Add_mFD7ED5C86355FB8D05C1209AFBB8A79B6E0BA797_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, RuntimeObject* ___0_key, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11148
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_IDictionary_Clear_mFAE5622F79BFAADCDA0C21C758D1B6AA49D32017_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11149
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_System_Collections_IDictionary_Contains_m24123A7F88C2C800C9037836C4773B3A18A54275_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, RuntimeObject* ___0_key, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_key;
		bool L_1;
		L_1 = ((  bool (*) (RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21)))(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2 = ___0_key;
		void* L_4 = UnBox_Any(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7), L_3);
		bool L_5;
		L_5 = InvokerFuncInvoker1< bool, Il2CppFullySharedGenericAny >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 22), __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4)): *(void**)(((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4))));
		return L_5;
	}

IL_0015:
	{
		return (bool)0;
	}
}
// Method Definition Index: 11150
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_IDictionary_GetEnumerator_m0285CEE9474EAA483097CF2480942BC24E6E3BF7_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	{
		RuntimeObject* L_0 = __this->___m_dictionary;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_0, IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var));
		RuntimeObject* L_1 = V_0;
		if (!L_1)
		{
			goto IL_0016;
		}
	}
	{
		RuntimeObject* L_2 = V_0;
		NullCheck(L_2);
		RuntimeObject* L_3;
		L_3 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(9, IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220_il2cpp_TypeInfo_var, L_2);
		return L_3;
	}

IL_0016:
	{
		RuntimeObject* L_4 = __this->___m_dictionary;
		DictionaryEnumerator_tA07B53C91CCF243B3DDF48825CD4F71A6835CD77 L_5;
		memset((&L_5), 0, sizeof(L_5));
		DictionaryEnumerator__ctor_mA1A62AF5ED8AB1B514BBAAAEA9BB3D081C3AA4ED((&L_5), L_4, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 23));
		DictionaryEnumerator_tA07B53C91CCF243B3DDF48825CD4F71A6835CD77 L_6 = L_5;
		RuntimeObject* L_7 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 24), &L_6);
		return (RuntimeObject*)L_7;
	}
}
// Method Definition Index: 11151
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_System_Collections_IDictionary_get_IsFixedSize_mADA2A7D8798B25E2D5AA76D0DB7B68F18BB4FF70_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		return (bool)1;
	}
}
// Method Definition Index: 11152
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_System_Collections_IDictionary_get_IsReadOnly_mCADBA490FFA90323B8FEDC38CD78141C54CC10DF_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		return (bool)1;
	}
}
// Method Definition Index: 11153
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_IDictionary_get_Keys_m203F3078EE234F53D90604EFDC9609D2B90B219B_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* L_0;
		L_0 = ((  KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* (*) (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 9)))(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 9));
		return (RuntimeObject*)L_0;
	}
}
// Method Definition Index: 11154
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_IDictionary_Remove_mCC94C51FD44D29ACE9EBBA8BF55A9BED1EE617D8_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, RuntimeObject* ___0_key, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11155
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_IDictionary_get_Values_m4592B94799170DC13E0F5BDD221769C548BCD680_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* L_0;
		L_0 = ((  ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* (*) (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11)))(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return (RuntimeObject*)L_0;
	}
}
// Method Definition Index: 11156
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_IDictionary_get_Item_m5EB78E7B173088D4B73585597F8C1AA9A61E2720_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, RuntimeObject* ___0_key, const RuntimeMethod* method) 
{
	const uint32_t SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7));
	const Il2CppFullySharedGenericAny L_3 = alloca(SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
	const uint32_t SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 13));
	const Il2CppFullySharedGenericAny L_5 = alloca(SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = ___0_key;
		bool L_1;
		L_1 = ((  bool (*) (RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21)))(L_0, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 21));
		if (!L_1)
		{
			goto IL_001a;
		}
	}
	{
		RuntimeObject* L_2 = ___0_key;
		void* L_4 = UnBox_Any(L_2, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7), L_3);
		InvokerActionInvoker2< Il2CppFullySharedGenericAny, Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 25), __this, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? (((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4)): *(void**)(((Il2CppFullySharedGenericAny)(Il2CppFullySharedGenericAny*)L_4))), (Il2CppFullySharedGenericAny*)L_5);
		RuntimeObject* L_6 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 13), L_5);
		return L_6;
	}

IL_001a:
	{
		return NULL;
	}
}
// Method Definition Index: 11157
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_IDictionary_set_Item_mCFEB082FB514C3C58C6C908B833CC7FF5B5A6D6A_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, RuntimeObject* ___0_key, RuntimeObject* ___1_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7AF8BCE7F42000322C784E007AAB43CA3E3D9AB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 11158
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyDictionary_2_System_Collections_ICollection_CopyTo_mB747FA5969557062AD3E00EF54BC5185C08504E7_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, RuntimeArray* ___0_array, int32_t ___1_index, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	const uint32_t SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16));
	const uint32_t SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7));
	const uint32_t SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 13));
	const Il2CppFullySharedGenericAny L_34 = alloca(SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
	const Il2CppFullySharedGenericAny L_53 = L_34;
	const Il2CppFullySharedGenericAny L_55 = alloca(SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3);
	const Il2CppFullySharedGenericAny L_36 = alloca(SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1);
	const Il2CppFullySharedGenericAny L_54 = L_36;
	const Il2CppFullySharedGenericAny L_56 = alloca(SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1);
	const KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 L_30 = alloca(SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
	const KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 L_49 = L_30;
	const KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 L_57 = alloca(SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
	//<source_info:<no-source>:1>
	KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57* V_0 = NULL;
	DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533* V_1 = NULL;
	RuntimeObject* V_2 = NULL;
	KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 V_3 = alloca(SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
	memset(V_3, 0, SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_4 = NULL;
	KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669 V_5 = alloca(SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
	memset(V_5, 0, SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		RuntimeArray* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralB829404B947F7E1629A30B5E953A49EB21CCD2ED)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, method);
	}

IL_000e:
	{
		RuntimeArray* L_2 = ___0_array;
		NullCheck(L_2);
		int32_t L_3;
		L_3 = il2cpp_codegen_array_get_rank(L_2);
		if ((((int32_t)L_3) == ((int32_t)1)))
		{
			goto IL_0022;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_4 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral967D403A541A1026A83D548E5AD5CA800AD4EFB5)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_4, method);
	}

IL_0022:
	{
		RuntimeArray* L_5 = ___0_array;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = Array_GetLowerBound_m4FB0601E2E8A6304A42E3FC400576DF7B0F084BC(L_5, 0, NULL);
		if (!L_6)
		{
			goto IL_0036;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_7 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_7, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral6195D7DA68D16D4985AD1A1B4FD2841A43CDDE70)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_7, method);
	}

IL_0036:
	{
		int32_t L_8 = ___1_index;
		if ((((int32_t)L_8) < ((int32_t)0)))
		{
			goto IL_0043;
		}
	}
	{
		int32_t L_9 = ___1_index;
		RuntimeArray* L_10 = ___0_array;
		NullCheck(L_10);
		int32_t L_11;
		L_11 = il2cpp_codegen_array_get_length(L_10);
		if ((((int32_t)L_9) <= ((int32_t)L_11)))
		{
			goto IL_0053;
		}
	}

IL_0043:
	{
		ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F* L_12 = (ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentOutOfRangeException_tEA2822DAF62B10EEED00E0E3A341D4BAF78CF85F_il2cpp_TypeInfo_var)));
		ArgumentOutOfRangeException__ctor_mE5B2755F0BEA043CACF915D5CE140859EE58FA66(L_12, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral2B6D6F48C27C60C3B55391AB377D9DC8F5639AA1)), ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral38E3DBC7FC353425EF3A98DC8DAC6689AF5FD1BE)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_12, method);
	}

IL_0053:
	{
		RuntimeArray* L_13 = ___0_array;
		NullCheck(L_13);
		int32_t L_14;
		L_14 = il2cpp_codegen_array_get_length(L_13);
		int32_t L_15 = ___1_index;
		int32_t L_16;
		L_16 = ((  int32_t (*) (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 26)))(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 26));
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_14, L_15))) >= ((int32_t)L_16)))
		{
			goto IL_006e;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_17 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_17, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral3ECE023333DCF45DE7B1FEAFFE30E295210DDD9B)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_17, method);
	}

IL_006e:
	{
		RuntimeArray* L_18 = ___0_array;
		V_0 = ((KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57*)IsInstSealed((RuntimeObject*)L_18, il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 27)));
		KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57* L_19 = V_0;
		if (!L_19)
		{
			goto IL_0086;
		}
	}
	{
		RuntimeObject* L_20 = __this->___m_dictionary;
		KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57* L_21 = V_0;
		int32_t L_22 = ___1_index;
		NullCheck((RuntimeObject*)L_20);
		InterfaceActionInvoker2< KeyValuePair_2U5BU5D_t885F2E060B0261B18E97D336746D53BA61338F57*, int32_t >::Invoke(5, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 14), (RuntimeObject*)L_20, L_21, L_22);
		return;
	}

IL_0086:
	{
		RuntimeArray* L_23 = ___0_array;
		V_1 = ((DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533*)IsInstSealed((RuntimeObject*)L_23, DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533_il2cpp_TypeInfo_var));
		DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533* L_24 = V_1;
		if (!L_24)
		{
			goto IL_00e2;
		}
	}
	{
		RuntimeObject* L_25 = __this->___m_dictionary;
		NullCheck((RuntimeObject*)L_25);
		RuntimeObject* L_26;
		L_26 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 19), (RuntimeObject*)L_25);
		V_2 = L_26;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_00d8:
			{
				{
					RuntimeObject* L_27 = V_2;
					if (!L_27)
					{
						goto IL_00e1;
					}
				}
				{
					RuntimeObject* L_28 = V_2;
					NullCheck((RuntimeObject*)L_28);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_28);
				}

IL_00e1:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_00ce_1;
			}

IL_009e_1:
			{
				RuntimeObject* L_29 = V_2;
				NullCheck(L_29);
				InterfaceActionInvoker1Invoker< KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 28), L_29, (KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669*)L_30);
				il2cpp_codegen_memcpy(V_3, L_30, SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
				DictionaryEntryU5BU5D_t410156653E754D17B5E1161CC6CF565103B63533* L_31 = V_1;
				int32_t L_32 = ___1_index;
				int32_t L_33 = L_32;
				___1_index = ((int32_t)il2cpp_codegen_add(L_33, 1));
				InvokerActionInvoker1< Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 30)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 30), (KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669*)V_3, (Il2CppFullySharedGenericAny*)L_34);
				RuntimeObject* L_35 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7), L_34);
				InvokerActionInvoker1< Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 31)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 31), (KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669*)V_3, (Il2CppFullySharedGenericAny*)L_36);
				RuntimeObject* L_37 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 13), L_36);
				DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB L_38;
				memset((&L_38), 0, sizeof(L_38));
				DictionaryEntry__ctor_m2768353E53A75C4860E34B37DAF1342120C5D1EA((&L_38), L_35, L_37, NULL);
				NullCheck(L_31);
				(L_31)->SetAt(static_cast<il2cpp_array_size_t>(L_33), (DictionaryEntry_t171080F37B311C25AA9E75888F9C9D703FA721BB)L_38);
			}

IL_00ce_1:
			{
				RuntimeObject* L_39 = V_2;
				NullCheck((RuntimeObject*)L_39);
				bool L_40;
				L_40 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_39);
				if (L_40)
				{
					goto IL_009e_1;
				}
			}
			{
				goto IL_0153;
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

IL_00e2:
	{
		RuntimeArray* L_41 = ___0_array;
		V_4 = ((ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)IsInst((RuntimeObject*)L_41, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_42 = V_4;
		if (L_42)
		{
			goto IL_00f9;
		}
	}
	{
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_43 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_43, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralBD0381A992FDF4F7DA60E5D83689FE7FF6309CB8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_43, method);
	}

IL_00f9:
	{
	}
	try
	{
		{
			RuntimeObject* L_44 = __this->___m_dictionary;
			NullCheck((RuntimeObject*)L_44);
			RuntimeObject* L_45;
			L_45 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 19), (RuntimeObject*)L_44);
			V_2 = L_45;
		}
		{
			auto __finallyBlock = il2cpp::utils::Finally([&]
			{

FINALLY_013b_1:
				{
					{
						RuntimeObject* L_46 = V_2;
						if (!L_46)
						{
							goto IL_0144_1;
						}
					}
					{
						RuntimeObject* L_47 = V_2;
						NullCheck((RuntimeObject*)L_47);
						InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, (RuntimeObject*)L_47);
					}

IL_0144_1:
					{
						return;
					}
				}
			});
			try
			{
				{
					goto IL_0131_2;
				}

IL_0108_2:
				{
					RuntimeObject* L_48 = V_2;
					NullCheck(L_48);
					InterfaceActionInvoker1Invoker< KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669* >::Invoke(0, il2cpp_rgctx_data_init(il2cpp_codegen_method_rgctx(method), 28), L_48, (KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669*)L_49);
					il2cpp_codegen_memcpy(V_5, L_49, SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
					ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_50 = V_4;
					int32_t L_51 = ___1_index;
					int32_t L_52 = L_51;
					___1_index = ((int32_t)il2cpp_codegen_add(L_52, 1));
					InvokerActionInvoker1< Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 30)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 30), (KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669*)V_5, (Il2CppFullySharedGenericAny*)L_53);
					InvokerActionInvoker1< Il2CppFullySharedGenericAny* >::Invoke(il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 31)), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 31), (KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669*)V_5, (Il2CppFullySharedGenericAny*)L_54);
					memset(L_57, 0, SizeOf_KeyValuePair_2_t0239F3881F4C8BE84CFABF0F90DFD19A583B6C95);
					KeyValuePair_2__ctor_mD82E516936D2BDE6D46C8C45270250647986231E((KeyValuePair_2_t28EF90BF7804CE5D7F99A364266351E7DC652669*)L_57, (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 7)) ? il2cpp_codegen_memcpy(L_55, L_53, SizeOf_TKey_t6CF24411882409C9C594E33E1DFE77DBD6BBA1B3): *(void**)L_53), (il2cpp_codegen_class_is_value_type(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 13)) ? il2cpp_codegen_memcpy(L_56, L_54, SizeOf_TValue_t43EDFC56FE2495E1ED9690FFC4BE75C460E759B1): *(void**)L_54), il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 32));
					RuntimeObject* L_58 = Box(il2cpp_rgctx_data(il2cpp_codegen_method_rgctx(method), 16), L_57);
					NullCheck(L_50);
					ArrayElementTypeCheck (L_50, L_58);
					(L_50)->SetAt(static_cast<il2cpp_array_size_t>(L_52), (RuntimeObject*)L_58);
				}

IL_0131_2:
				{
					RuntimeObject* L_59 = V_2;
					NullCheck((RuntimeObject*)L_59);
					bool L_60;
					L_60 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, (RuntimeObject*)L_59);
					if (L_60)
					{
						goto IL_0108_2;
					}
				}
				{
					goto IL_0145_1;
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

IL_0145_1:
		{
			goto IL_0153;
		}
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_0147;
		}
		throw e;
	}

CATCH_0147:
	{
		ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1* L_61 = ((ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*)IL2CPP_GET_ACTIVE_EXCEPTION(ArrayTypeMismatchException_t95F1723A5A166E62D3FBEF9734DEFBF61594F8F1*));;
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_62 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		ArgumentException__ctor_m026938A67AF9D36BB7ED27F80425D7194B514465(L_62, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralBD0381A992FDF4F7DA60E5D83689FE7FF6309CB8)), NULL);
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_62, method);
	}

IL_0153:
	{
		return;
	}
}
// Method Definition Index: 11159
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyDictionary_2_System_Collections_ICollection_get_IsSynchronized_m775918D51697D5404B898ACB89208A1498A46CB3_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		return (bool)0;
	}
}
// Method Definition Index: 11160
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_ICollection_get_SyncRoot_mE06D92F6EEC7E9C3245A31B7B75DE348212F59F7_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ICollection_t37E7B9DC5B4EF41D190D607F92835BF1171C0E8E_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	RuntimeObject* V_0 = NULL;
	{
		RuntimeObject* L_0 = __this->____syncRoot;
		if (L_0)
		{
			goto IL_0037;
		}
	}
	{
		RuntimeObject* L_1 = __this->___m_dictionary;
		V_0 = ((RuntimeObject*)IsInst((RuntimeObject*)L_1, ICollection_t37E7B9DC5B4EF41D190D607F92835BF1171C0E8E_il2cpp_TypeInfo_var));
		RuntimeObject* L_2 = V_0;
		if (!L_2)
		{
			goto IL_0025;
		}
	}
	{
		RuntimeObject* L_3 = V_0;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(2, ICollection_t37E7B9DC5B4EF41D190D607F92835BF1171C0E8E_il2cpp_TypeInfo_var, L_3);
		__this->____syncRoot = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____syncRoot), (void*)L_4);
		goto IL_0037;
	}

IL_0025:
	{
		RuntimeObject** L_5 = (RuntimeObject**)(&__this->____syncRoot);
		RuntimeObject* L_6 = (RuntimeObject*)il2cpp_codegen_object_new(RuntimeObject_il2cpp_TypeInfo_var);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(L_6, NULL);
		RuntimeObject* L_7;
		L_7 = il2cpp_intrinsic_interlocked_compare_exchange(L_5, L_6, NULL);
	}

IL_0037:
	{
		RuntimeObject* L_8 = __this->____syncRoot;
		return L_8;
	}
}
// Method Definition Index: 11161
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_Generic_IReadOnlyDictionaryU3CTKeyU2CTValueU3E_get_Keys_m5517F5B43EFB878E6A1DB24A9DA5C2B0A6C2FB65_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* L_0;
		L_0 = ((  KeyCollection_t77347D5E6080A99487985EFA49C429DAC62A4221* (*) (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 9)))(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 9));
		return (RuntimeObject*)L_0;
	}
}
// Method Definition Index: 11162
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyDictionary_2_System_Collections_Generic_IReadOnlyDictionaryU3CTKeyU2CTValueU3E_get_Values_mFD4DE52E42982BE1C72AB6314736968DEA611E37_fshared (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* L_0;
		L_0 = ((  ValueCollection_tBD303C20E55147B0A354B6AD8C7779ABBC77D849* (*) (ReadOnlyDictionary_2_tC043257AB947D9A94D4AF6916DD67B7C30D3F734*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11)))(__this, il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 11));
		return (RuntimeObject*)L_0;
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
// Method Definition Index: 2373
// Method Definition Index: 2374
// Method Definition Index: 2375
// Method Definition Index: 2376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlyMemory_1_ToString_m0C4DF727B39C613F84AC56D6FDAF7ACFF9E39720 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral958BF5561D89C9A90653416FCB65BBE72DADDE1E);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	String_t* V_0 = NULL;
	ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_0058;
	}

IL_0058:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = ((int32_t)(L_3&((int32_t)2147483647LL)));
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral958BF5561D89C9A90653416FCB65BBE72DADDE1E, L_2, L_5, NULL);
		return L_6;
	}
}
IL2CPP_EXTERN_C  String_t* ReadOnlyMemory_1_ToString_m0C4DF727B39C613F84AC56D6FDAF7ACFF9E39720_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399>(__this);
	String_t* _returnValue;
	_returnValue = ReadOnlyMemory_1_ToString_m0C4DF727B39C613F84AC56D6FDAF7ACFF9E39720(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 2377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D ReadOnlyMemory_1_get_Span_mA087C8160638E6581A03C1BDAF2803AC88834762 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 V_0;
	memset((&V_0), 0, sizeof(V_0));
	String_t* V_1 = NULL;
	ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		int32_t L_0 = __this->____index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0039;
		}
	}
	{
		RuntimeObject* L_1 = __this->____object;
		NullCheck(((MemoryManager_1_tB90442C8E0A1B9C0F8A3B603FD50501A1BADAC6E*)CastclassClass((RuntimeObject*)L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))));
		Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 L_2;
		L_2 = VirtualFuncInvoker0< Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 >::Invoke(4, ((MemoryManager_1_tB90442C8E0A1B9C0F8A3B603FD50501A1BADAC6E*)CastclassClass((RuntimeObject*)L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))));
		V_0 = L_2;
		int32_t L_3 = __this->____index;
		int32_t L_4 = __this->____length;
		Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 L_5;
		L_5 = Span_1_Slice_m9D8BA8245B8DC9BFB4A4164759CBAAEAD1318CD6_inline((&V_0), ((int32_t)(L_3&((int32_t)2147483647LL))), L_4, NULL);
		ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D L_6;
		L_6 = Span_1_op_Implicit_mD249188242C0C9D3192A31E9F7FA74C683F05B84(L_5, NULL);
		return L_6;
	}

IL_0039:
	{
		goto IL_008e;
	}

IL_008e:
	{
		RuntimeObject* L_7 = __this->____object;
		if (!L_7)
		{
			goto IL_00b9;
		}
	}
	{
		RuntimeObject* L_8 = __this->____object;
		int32_t L_9 = __this->____index;
		int32_t L_10 = __this->____length;
		ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D L_11;
		memset((&L_11), 0, sizeof(L_11));
		ReadOnlySpan_1__ctor_m7B5C2765879EA5E8D1617D834CC465A39540A913_inline((&L_11), ((ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)Castclass((RuntimeObject*)L_8, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 9))), L_9, ((int32_t)(L_10&((int32_t)2147483647LL))), NULL);
		return L_11;
	}

IL_00b9:
	{
		il2cpp_codegen_initobj((&V_2), sizeof(ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D));
		ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D L_12 = V_2;
		return L_12;
	}
}
// Method Definition Index: 2378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_mD0C4947B91FD3864C476B6CA8BA929A6F5B01175 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Memory_1_tB7CEF4416F5014E364267478CEF016A4AC5C0036 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		RuntimeObject* L_0 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 11))))
		{
			goto IL_0017;
		}
	}
	{
		RuntimeObject* L_1 = ___0_obj;
		V_0 = ((*(ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399*)UnBox(L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 11))));
		ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399 L_2 = V_0;
		bool L_3;
		L_3 = ReadOnlyMemory_1_Equals_mC3969DE0744A246E0576FEDE51191A0D3F5F69A9(__this, L_2, NULL);
		return L_3;
	}

IL_0017:
	{
		RuntimeObject* L_4 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_4, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 13))))
		{
			goto IL_0033;
		}
	}
	{
		RuntimeObject* L_5 = ___0_obj;
		V_1 = ((*(Memory_1_tB7CEF4416F5014E364267478CEF016A4AC5C0036*)UnBox(L_5, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 13))));
		Memory_1_tB7CEF4416F5014E364267478CEF016A4AC5C0036 L_6 = V_1;
		ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399 L_7;
		L_7 = Memory_1_op_Implicit_m7B80A71310A3EA9F99CB1774CAFED254811599CD(L_6, NULL);
		bool L_8;
		L_8 = ReadOnlyMemory_1_Equals_mC3969DE0744A246E0576FEDE51191A0D3F5F69A9(__this, L_7, NULL);
		return L_8;
	}

IL_0033:
	{
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool ReadOnlyMemory_1_Equals_mD0C4947B91FD3864C476B6CA8BA929A6F5B01175_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399>(__this);
	bool _returnValue;
	_returnValue = ReadOnlyMemory_1_Equals_mD0C4947B91FD3864C476B6CA8BA929A6F5B01175(_thisAdjusted, ___0_obj, method);
	return _returnValue;
}
// Method Definition Index: 2379
// Method Definition Index: 2380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_GetHashCode_mE47597BBDBFFBC9185DE65261A7388207854ADE6 (ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->____object;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		return 0;
	}

IL_000a:
	{
		RuntimeObject* L_1 = __this->____object;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = VirtualFuncInvoker0< int32_t >::Invoke(2, L_1);
		int32_t* L_3 = (int32_t*)(&__this->____index);
		int32_t L_4;
		L_4 = Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295(L_3, NULL);
		int32_t* L_5 = (int32_t*)(&__this->____length);
		int32_t L_6;
		L_6 = Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295(L_5, NULL);
		int32_t L_7;
		L_7 = ReadOnlyMemory_1_CombineHashCodes_mC20EA69C2706DE824529A0506BD45837E26C6980(L_2, L_4, L_6, NULL);
		return L_7;
	}
}
IL2CPP_EXTERN_C  int32_t ReadOnlyMemory_1_GetHashCode_mE47597BBDBFFBC9185DE65261A7388207854ADE6_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t63F301BF893B0AB689953D86A641168CA66D2399>(__this);
	int32_t _returnValue;
	_returnValue = ReadOnlyMemory_1_GetHashCode_mE47597BBDBFFBC9185DE65261A7388207854ADE6(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 2381
// Method Definition Index: 2382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_CombineHashCodes_mC20EA69C2706DE824529A0506BD45837E26C6980 (int32_t ___0_h1, int32_t ___1_h2, int32_t ___2_h3, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_h1;
		int32_t L_1 = ___1_h2;
		int32_t L_2;
		L_2 = ReadOnlyMemory_1_CombineHashCodes_mAD75C6BDDB231C42D8A846F50E2B081E244108DA(L_0, L_1, NULL);
		int32_t L_3 = ___2_h3;
		int32_t L_4;
		L_4 = ReadOnlyMemory_1_CombineHashCodes_mAD75C6BDDB231C42D8A846F50E2B081E244108DA(L_2, L_3, NULL);
		return L_4;
	}
}
// Method Definition Index: 2383
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2373
// Method Definition Index: 2374
// Method Definition Index: 2375
// Method Definition Index: 2376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlyMemory_1_ToString_m4676D3AFBC120B54C2B53171F9C7DE3B4E753F68 (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	String_t* V_0 = NULL;
	ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
	}
	{
		RuntimeObject* L_0 = __this->____object;
		V_0 = ((String_t*)IsInstSealed((RuntimeObject*)L_0, String_t_il2cpp_TypeInfo_var));
		String_t* L_1 = V_0;
		if (L_1)
		{
			goto IL_003f;
		}
	}
	{
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_2;
		L_2 = ReadOnlyMemory_1_get_Span_m21E4F67DBB43CC122008ADE132AB9213723588EA_inline(__this, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		V_1 = L_2;
		String_t* L_3;
		L_3 = ReadOnlySpan_1_ToString_mB13A913EF211B14B719EE62133C15C298642E34D((&V_1), NULL);
		return L_3;
	}

IL_003f:
	{
		String_t* L_4 = V_0;
		int32_t L_5 = __this->____index;
		int32_t L_6 = __this->____length;
		NullCheck(L_4);
		String_t* L_7;
		L_7 = String_Substring_mB1D94F47935D22E130FF2C01DBB6A4135FBB76CE(L_4, L_5, ((int32_t)(L_6&((int32_t)2147483647LL))), NULL);
		return L_7;
	}
}
IL2CPP_EXTERN_C  String_t* ReadOnlyMemory_1_ToString_m4676D3AFBC120B54C2B53171F9C7DE3B4E753F68_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6>(__this);
	String_t* _returnValue;
	_returnValue = ReadOnlyMemory_1_ToString_m4676D3AFBC120B54C2B53171F9C7DE3B4E753F68(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 2377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ReadOnlyMemory_1_get_Span_m21E4F67DBB43CC122008ADE132AB9213723588EA (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D V_0;
	memset((&V_0), 0, sizeof(V_0));
	String_t* V_1 = NULL;
	ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		int32_t L_0 = __this->____index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0039;
		}
	}
	{
		RuntimeObject* L_1 = __this->____object;
		NullCheck(((MemoryManager_1_t9E713A8E4F8473E685FE4A73C98D29C9DB1EE583*)CastclassClass((RuntimeObject*)L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))));
		Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D L_2;
		L_2 = VirtualFuncInvoker0< Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D >::Invoke(4, ((MemoryManager_1_t9E713A8E4F8473E685FE4A73C98D29C9DB1EE583*)CastclassClass((RuntimeObject*)L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))));
		V_0 = L_2;
		int32_t L_3 = __this->____index;
		int32_t L_4 = __this->____length;
		Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D L_5;
		L_5 = Span_1_Slice_mEFBC3C78FD443FFE23F9E841D43B7B0271622843_inline((&V_0), ((int32_t)(L_3&((int32_t)2147483647LL))), L_4, NULL);
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_6;
		L_6 = Span_1_op_Implicit_m03D0CEDC1BC95844236105D1DE24A702B956BFE4(L_5, NULL);
		return L_6;
	}

IL_0039:
	{
	}
	{
		RuntimeObject* L_7 = __this->____object;
		V_1 = ((String_t*)IsInstSealed((RuntimeObject*)L_7, String_t_il2cpp_TypeInfo_var));
		String_t* L_8 = V_1;
		if (!L_8)
		{
			goto IL_008e;
		}
	}
	{
		String_t* L_9 = V_1;
		NullCheck(L_9);
		Il2CppChar* L_10;
		L_10 = String_GetRawStringData_m87BC50B7B314C055E27A28032D1003D42FDE411D(L_9, NULL);
		Il2CppChar* L_11;
		L_11 = il2cpp_unsafe_as_ref<Il2CppChar>(L_10);
		String_t* L_12 = V_1;
		NullCheck(L_12);
		int32_t L_13;
		L_13 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_12, NULL);
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_14;
		memset((&L_14), 0, sizeof(L_14));
		ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_inline((&L_14), L_11, L_13, NULL);
		V_2 = L_14;
		int32_t L_15 = __this->____index;
		int32_t L_16 = __this->____length;
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_17;
		L_17 = ReadOnlySpan_1_Slice_mDEEA4C304B13C8F7A63BC3D60B62FF17BBEE282B_inline((&V_2), L_15, L_16, NULL);
		return L_17;
	}

IL_008e:
	{
		RuntimeObject* L_18 = __this->____object;
		if (!L_18)
		{
			goto IL_00b9;
		}
	}
	{
		RuntimeObject* L_19 = __this->____object;
		int32_t L_20 = __this->____index;
		int32_t L_21 = __this->____length;
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_22;
		memset((&L_22), 0, sizeof(L_22));
		ReadOnlySpan_1__ctor_mDADE08E6B4594775C6675B389078E5FE98C745D5_inline((&L_22), ((CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)Castclass((RuntimeObject*)L_19, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 9))), L_20, ((int32_t)(L_21&((int32_t)2147483647LL))), NULL);
		return L_22;
	}

IL_00b9:
	{
		il2cpp_codegen_initobj((&V_2), sizeof(ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1));
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_23 = V_2;
		return L_23;
	}
}
// Method Definition Index: 2378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_mE4EA5DC4ACF51E07F85C4A55A3281E580903506F (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Memory_1_t48D1B1093D181D6F37155EB644659FD7D1DF616E V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		RuntimeObject* L_0 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 11))))
		{
			goto IL_0017;
		}
	}
	{
		RuntimeObject* L_1 = ___0_obj;
		V_0 = ((*(ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6*)UnBox(L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 11))));
		ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6 L_2 = V_0;
		bool L_3;
		L_3 = ReadOnlyMemory_1_Equals_mC4A7615E6A8C3467783EB6D9FBDA06A1B9745117(__this, L_2, NULL);
		return L_3;
	}

IL_0017:
	{
		RuntimeObject* L_4 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_4, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 13))))
		{
			goto IL_0033;
		}
	}
	{
		RuntimeObject* L_5 = ___0_obj;
		V_1 = ((*(Memory_1_t48D1B1093D181D6F37155EB644659FD7D1DF616E*)UnBox(L_5, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 13))));
		Memory_1_t48D1B1093D181D6F37155EB644659FD7D1DF616E L_6 = V_1;
		ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6 L_7;
		L_7 = Memory_1_op_Implicit_m2A354E866CDB4F77671ADADD234CFF9A31DAB8FB(L_6, NULL);
		bool L_8;
		L_8 = ReadOnlyMemory_1_Equals_mC4A7615E6A8C3467783EB6D9FBDA06A1B9745117(__this, L_7, NULL);
		return L_8;
	}

IL_0033:
	{
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool ReadOnlyMemory_1_Equals_mE4EA5DC4ACF51E07F85C4A55A3281E580903506F_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6>(__this);
	bool _returnValue;
	_returnValue = ReadOnlyMemory_1_Equals_mE4EA5DC4ACF51E07F85C4A55A3281E580903506F(_thisAdjusted, ___0_obj, method);
	return _returnValue;
}
// Method Definition Index: 2379
// Method Definition Index: 2380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_GetHashCode_m7D885665B2C6E9B5B157129A6CCAFC4938400D0D (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->____object;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		return 0;
	}

IL_000a:
	{
		RuntimeObject* L_1 = __this->____object;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = VirtualFuncInvoker0< int32_t >::Invoke(2, L_1);
		int32_t* L_3 = (int32_t*)(&__this->____index);
		int32_t L_4;
		L_4 = Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295(L_3, NULL);
		int32_t* L_5 = (int32_t*)(&__this->____length);
		int32_t L_6;
		L_6 = Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295(L_5, NULL);
		int32_t L_7;
		L_7 = ReadOnlyMemory_1_CombineHashCodes_m90089D22A29124B5DFB7253B5795FBBBA2E17BC1(L_2, L_4, L_6, NULL);
		return L_7;
	}
}
IL2CPP_EXTERN_C  int32_t ReadOnlyMemory_1_GetHashCode_m7D885665B2C6E9B5B157129A6CCAFC4938400D0D_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6>(__this);
	int32_t _returnValue;
	_returnValue = ReadOnlyMemory_1_GetHashCode_m7D885665B2C6E9B5B157129A6CCAFC4938400D0D(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 2381
// Method Definition Index: 2382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_CombineHashCodes_m90089D22A29124B5DFB7253B5795FBBBA2E17BC1 (int32_t ___0_h1, int32_t ___1_h2, int32_t ___2_h3, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_h1;
		int32_t L_1 = ___1_h2;
		int32_t L_2;
		L_2 = ReadOnlyMemory_1_CombineHashCodes_mE36B3B07DCBC22779CDDB94D35E0D00AD8568028(L_0, L_1, NULL);
		int32_t L_3 = ___2_h3;
		int32_t L_4;
		L_4 = ReadOnlyMemory_1_CombineHashCodes_mE36B3B07DCBC22779CDDB94D35E0D00AD8568028(L_2, L_3, NULL);
		return L_4;
	}
}
// Method Definition Index: 2383
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2373
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyMemory_1__ctor_m499964C79FA86289EF11F618EF98F63259D680F6_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___0_array, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_000b;
		}
	}
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4));
		return;
	}

IL_000b:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_1 = ___0_array;
		__this->____object = (RuntimeObject*)L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____object), (void*)(RuntimeObject*)L_1);
		__this->____index = 0;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_2 = ___0_array;
		NullCheck(L_2);
		int32_t L_3 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_2)->max_length),NULL));
		__this->____length = L_3;
		return;
	}
}
// Method Definition Index: 2374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyMemory_1__ctor_m1FD19CA8A2CB8A92DC095EE34D414ADE2FD39A12_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_10 = ___0_array;
		__this->____object = (RuntimeObject*)L_10;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____object), (void*)(RuntimeObject*)L_10);
		int32_t L_11 = ___1_start;
		__this->____index = L_11;
		int32_t L_12 = ___2_length;
		__this->____length = L_12;
		return;
	}
}
// Method Definition Index: 2375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_get_Length_m65A4E9056707DCDBB66443924288D7E0BA7D0074_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____length;
		return ((int32_t)(L_0&((int32_t)2147483647LL)));
	}
}
// Method Definition Index: 2376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlyMemory_1_ToString_m1AB8B8725FFC7144748E59C73875E2C6822E2321_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral958BF5561D89C9A90653416FCB65BBE72DADDE1E);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	String_t* V_0 = NULL;
	ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		bool L_0 = (Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		if (!L_0)
		{
			goto IL_0058;
		}
	}
	{
		RuntimeObject* L_1 = __this->____object;
		V_0 = ((String_t*)IsInstSealed((RuntimeObject*)L_1, String_t_il2cpp_TypeInfo_var));
		String_t* L_2 = V_0;
		if (L_2)
		{
			goto IL_003f;
		}
	}
	{
		ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC L_3;
		L_3 = ((  ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC (*) (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2)))(__this, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 2));
		V_1 = L_3;
		String_t* L_4;
		L_4 = ReadOnlySpan_1_ToString_m04AD05E39D10A41E67653C41A22108A810CA9857((&V_1), il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3));
		return L_4;
	}

IL_003f:
	{
		String_t* L_5 = V_0;
		int32_t L_6 = __this->____index;
		int32_t L_7 = __this->____length;
		NullCheck(L_5);
		String_t* L_8;
		L_8 = String_Substring_mB1D94F47935D22E130FF2C01DBB6A4135FBB76CE(L_5, L_6, ((int32_t)(L_7&((int32_t)2147483647LL))), NULL);
		return L_8;
	}

IL_0058:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_9 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 0)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_10;
		L_10 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_9, NULL);
		NullCheck(L_10);
		String_t* L_11;
		L_11 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_10);
		int32_t L_12 = __this->____length;
		int32_t L_13 = ((int32_t)(L_12&((int32_t)2147483647LL)));
		RuntimeObject* L_14 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_13);
		String_t* L_15;
		L_15 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral958BF5561D89C9A90653416FCB65BBE72DADDE1E, L_11, L_14, NULL);
		return L_15;
	}
}
IL2CPP_EXTERN_C  String_t* ReadOnlyMemory_1_ToString_m1AB8B8725FFC7144748E59C73875E2C6822E2321_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4>(__this);
	String_t* _returnValue;
	_returnValue = ReadOnlyMemory_1_ToString_m1AB8B8725FFC7144748E59C73875E2C6822E2321(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 2377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC ReadOnlyMemory_1_get_Span_m3BBCF2EFAFAB7DAA8882208AF319487E00DC3895_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54 V_0;
	memset((&V_0), 0, sizeof(V_0));
	String_t* V_1 = NULL;
	ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		int32_t L_0 = __this->____index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0039;
		}
	}
	{
		RuntimeObject* L_1 = __this->____object;
		NullCheck(((MemoryManager_1_tED55276A18D77758B15A7A09B9C0D4DB79480487*)CastclassClass((RuntimeObject*)L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))));
		Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54 L_2;
		L_2 = VirtualFuncInvoker0< Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54 >::Invoke(4, ((MemoryManager_1_tED55276A18D77758B15A7A09B9C0D4DB79480487*)CastclassClass((RuntimeObject*)L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))));
		V_0 = L_2;
		int32_t L_3 = __this->____index;
		int32_t L_4 = __this->____length;
		Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54 L_5;
		L_5 = ((  Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54 (*) (Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54*, int32_t, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5)))((&V_0), ((int32_t)(L_3&((int32_t)2147483647LL))), L_4, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 5));
		ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC L_6;
		L_6 = ((  ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC (*) (Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 6)))(L_5, NULL);
		return L_6;
	}

IL_0039:
	{
		bool L_7 = (Char_t521A6F19B456D956AF452D926C32709DC03D6B17_il2cpp_TypeInfo_var) == (il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
		if (!L_7)
		{
			goto IL_008e;
		}
	}
	{
		RuntimeObject* L_8 = __this->____object;
		V_1 = ((String_t*)IsInstSealed((RuntimeObject*)L_8, String_t_il2cpp_TypeInfo_var));
		String_t* L_9 = V_1;
		if (!L_9)
		{
			goto IL_008e;
		}
	}
	{
		String_t* L_10 = V_1;
		NullCheck(L_10);
		Il2CppChar* L_11;
		L_11 = String_GetRawStringData_m87BC50B7B314C055E27A28032D1003D42FDE411D(L_10, NULL);
		Il2CppFullySharedGenericAny* L_12;
		L_12 = il2cpp_unsafe_as_ref<Il2CppFullySharedGenericAny>(L_11);
		String_t* L_13 = V_1;
		NullCheck(L_13);
		int32_t L_14;
		L_14 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_13, NULL);
		ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC L_15;
		memset((&L_15), 0, sizeof(L_15));
		ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_inline((&L_15), L_12, L_14, NULL);
		V_2 = L_15;
		int32_t L_16 = __this->____index;
		int32_t L_17 = __this->____length;
		ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC L_18;
		L_18 = ((  ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC (*) (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC*, int32_t, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8)))((&V_2), L_16, L_17, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8));
		return L_18;
	}

IL_008e:
	{
		RuntimeObject* L_19 = __this->____object;
		if (!L_19)
		{
			goto IL_00b9;
		}
	}
	{
		RuntimeObject* L_20 = __this->____object;
		int32_t L_21 = __this->____index;
		int32_t L_22 = __this->____length;
		ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC L_23;
		memset((&L_23), 0, sizeof(L_23));
		ReadOnlySpan_1__ctor_mC9869776ABBFE9D2520512EEB39ABD1CFFE7F7B9_inline((&L_23), ((__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC*)Castclass((RuntimeObject*)L_20, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 9))), L_21, ((int32_t)(L_22&((int32_t)2147483647LL))), il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 10));
		return L_23;
	}

IL_00b9:
	{
		il2cpp_codegen_initobj((&V_2), sizeof(ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC));
		ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC L_24 = V_2;
		return L_24;
	}
}
// Method Definition Index: 2378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_m2C16DF2AE78D8E7182CFAE8E3E07C3B20A777888_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 V_0;
	memset((&V_0), 0, sizeof(V_0));
	Memory_1_t762D61B435521D1706EEDC25CA8C8EA1AE19AFEA V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		RuntimeObject* L_0 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_0, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 11))))
		{
			goto IL_0017;
		}
	}
	{
		RuntimeObject* L_1 = ___0_obj;
		V_0 = ((*(ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4*)UnBox(L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 11))));
		ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 L_2 = V_0;
		bool L_3;
		L_3 = ((  bool (*) (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4*, ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 12)))(__this, L_2, NULL);
		return L_3;
	}

IL_0017:
	{
		RuntimeObject* L_4 = ___0_obj;
		if (!((RuntimeObject*)IsInstSealed((RuntimeObject*)L_4, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 13))))
		{
			goto IL_0033;
		}
	}
	{
		RuntimeObject* L_5 = ___0_obj;
		V_1 = ((*(Memory_1_t762D61B435521D1706EEDC25CA8C8EA1AE19AFEA*)UnBox(L_5, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 13))));
		Memory_1_t762D61B435521D1706EEDC25CA8C8EA1AE19AFEA L_6 = V_1;
		ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 L_7;
		L_7 = ((  ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 (*) (Memory_1_t762D61B435521D1706EEDC25CA8C8EA1AE19AFEA, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 14)))(L_6, NULL);
		bool L_8;
		L_8 = ((  bool (*) (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4*, ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 12)))(__this, L_7, NULL);
		return L_8;
	}

IL_0033:
	{
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool ReadOnlyMemory_1_Equals_m2C16DF2AE78D8E7182CFAE8E3E07C3B20A777888_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4>(__this);
	bool _returnValue;
	_returnValue = ReadOnlyMemory_1_Equals_m2C16DF2AE78D8E7182CFAE8E3E07C3B20A777888(_thisAdjusted, ___0_obj, method);
	return _returnValue;
}
// Method Definition Index: 2379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlyMemory_1_Equals_mF7DA75997B4FFDFB2D6F953E8DC580EC3C941A61_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 ___0_other, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->____object;
		ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 L_1 = ___0_other;
		RuntimeObject* L_2 = L_1.____object;
		if ((!(((RuntimeObject*)(RuntimeObject*)L_0) == ((RuntimeObject*)(RuntimeObject*)L_2))))
		{
			goto IL_002b;
		}
	}
	{
		int32_t L_3 = __this->____index;
		ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 L_4 = ___0_other;
		int32_t L_5 = L_4.____index;
		if ((!(((uint32_t)L_3) == ((uint32_t)L_5))))
		{
			goto IL_002b;
		}
	}
	{
		int32_t L_6 = __this->____length;
		ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 L_7 = ___0_other;
		int32_t L_8 = L_7.____length;
		return (bool)((((int32_t)L_6) == ((int32_t)L_8))? 1 : 0);
	}

IL_002b:
	{
		return (bool)0;
	}
}
IL2CPP_EXTERN_C  bool ReadOnlyMemory_1_Equals_mF7DA75997B4FFDFB2D6F953E8DC580EC3C941A61_AdjustorThunk (RuntimeObject* __this, ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4 ___0_other, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4>(__this);
	bool _returnValue;
	_returnValue = ReadOnlyMemory_1_Equals_mF7DA75997B4FFDFB2D6F953E8DC580EC3C941A61(_thisAdjusted, ___0_other, method);
	return _returnValue;
}
// Method Definition Index: 2380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_GetHashCode_m96535F6C6512A16E6DBC2A8341F8FECDF2E47F54_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->____object;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		return 0;
	}

IL_000a:
	{
		RuntimeObject* L_1 = __this->____object;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = VirtualFuncInvoker0< int32_t >::Invoke(2, L_1);
		int32_t* L_3 = (int32_t*)(&__this->____index);
		int32_t L_4;
		L_4 = Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295(L_3, NULL);
		int32_t* L_5 = (int32_t*)(&__this->____length);
		int32_t L_6;
		L_6 = Int32_GetHashCode_m253D60FF7527A483E91004B7A2366F13E225E295(L_5, NULL);
		int32_t L_7;
		L_7 = ((  int32_t (*) (int32_t, int32_t, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 15)))(L_2, L_4, L_6, il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 15));
		return L_7;
	}
}
IL2CPP_EXTERN_C  int32_t ReadOnlyMemory_1_GetHashCode_m96535F6C6512A16E6DBC2A8341F8FECDF2E47F54_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* _thisAdjusted;
	_thisAdjusted = il2cpp_codegen_get_raw_data<ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4>(__this);
	int32_t _returnValue;
	_returnValue = ReadOnlyMemory_1_GetHashCode_m96535F6C6512A16E6DBC2A8341F8FECDF2E47F54(_thisAdjusted, method);
	return _returnValue;
}
// Method Definition Index: 2381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_CombineHashCodes_m74D5DE9AF0CCB1FF7B88058328C835DBD981C568_fshared (int32_t ___0_left, int32_t ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_left;
		int32_t L_1 = ___0_left;
		int32_t L_2 = ___1_right;
		return ((int32_t)(((int32_t)il2cpp_codegen_add(((int32_t)(L_0<<5)), L_1))^L_2));
	}
}
// Method Definition Index: 2382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlyMemory_1_CombineHashCodes_m1F4667745BEC8579517F436310474B5FEE8379E6_fshared (int32_t ___0_h1, int32_t ___1_h2, int32_t ___2_h3, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = ___0_h1;
		int32_t L_1 = ___1_h2;
		int32_t L_2;
		L_2 = ((  int32_t (*) (int32_t, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 16)))(L_0, L_1, NULL);
		int32_t L_3 = ___2_h3;
		int32_t L_4;
		L_4 = ((  int32_t (*) (int32_t, int32_t, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 16)))(L_2, L_3, NULL);
		return L_4;
	}
}
// Method Definition Index: 2383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyMemory_1_GetObjectStartLength_m76D2DB58E89EF8DAAB0BEEC5E80CC99E7C3CB752_fshared (ReadOnlyMemory_1_t192441E248CAB66088AAEDBD64DFEE110ADBEED4* __this, int32_t* ___0_start, int32_t* ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t* L_0 = ___0_start;
		int32_t L_1 = __this->____index;
		il2cpp_codegen_stind<int32_t>((int32_t*)L_0, (int32_t)L_1);
		int32_t* L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		il2cpp_codegen_stind<int32_t>((int32_t*)L_2, (int32_t)L_3);
		RuntimeObject* L_4 = __this->____object;
		return L_4;
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
// Method Definition Index: 86899
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 86899
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 86899
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyNativeVector_1__ctor_m64EB14620E394C425562A5F9AD826C959C29BD96_fshared (ReadOnlyNativeVector_1_tE45C179B8DFEAD70E1F59D744D5C6999873C0FFC* __this, intptr_t ___0_ptr, int32_t ___1_size, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		intptr_t L_0 = ___0_ptr;
		__this->___m_Ptr = L_0;
		int32_t L_1 = ___1_size;
		__this->___m_Count = L_1;
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
// Method Definition Index: 44301
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1__ctor_m4419FA66B84E4CCD02868DF8E11EEA8D94F61EAB_gshared (ReadOnlyObservableCollection_1_tDDB997B5588C61C7ACD439AF6BF94358E788B352* __this, ObservableCollection_1_tE47F2087FEB0CBCB2B73E995AF74710B883ABC62* ___0_list, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&INotifyCollectionChanged_tD2E4219A01AB2BD93859A30D924324BE793C2806_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&INotifyPropertyChanged_tF956261A15128123778A0D86196231401847CF8C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	{
		ObservableCollection_1_tE47F2087FEB0CBCB2B73E995AF74710B883ABC62* L_0 = ___0_list;
		ReadOnlyCollection_1__ctor_mC1890FAC00703F47A655C35CBCB613C74A811580((ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263*)__this, (RuntimeObject*)L_0, NULL);
		NullCheck((ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263*)__this);
		RuntimeObject* L_1;
		L_1 = ReadOnlyCollection_1_get_Items_m9959886AAEACF554130E90E83847131E07819CC0_inline((ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263*)__this, NULL);
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_2 = (NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF*)il2cpp_codegen_object_new(NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF_il2cpp_TypeInfo_var);
		NotifyCollectionChangedEventHandler__ctor_m4DAF76BAC5E44532E6E784CD15A0C4493156E05D(L_2, (RuntimeObject*)__this, (intptr_t)((void*)il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 2)), NULL);
		NullCheck(((RuntimeObject*)Castclass((RuntimeObject*)L_1, INotifyCollectionChanged_tD2E4219A01AB2BD93859A30D924324BE793C2806_il2cpp_TypeInfo_var)));
		InterfaceActionInvoker1< NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* >::Invoke(0, INotifyCollectionChanged_tD2E4219A01AB2BD93859A30D924324BE793C2806_il2cpp_TypeInfo_var, ((RuntimeObject*)Castclass((RuntimeObject*)L_1, INotifyCollectionChanged_tD2E4219A01AB2BD93859A30D924324BE793C2806_il2cpp_TypeInfo_var)), L_2);
		NullCheck((ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263*)__this);
		RuntimeObject* L_3;
		L_3 = ReadOnlyCollection_1_get_Items_m9959886AAEACF554130E90E83847131E07819CC0_inline((ReadOnlyCollection_1_t06F71F2F3EBC6E0A34714E0A7EB3367B6D248263*)__this, NULL);
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_4 = (PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA*)il2cpp_codegen_object_new(PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA_il2cpp_TypeInfo_var);
		PropertyChangedEventHandler__ctor_m4015A035EDCA8077D1EA9A8EAB5FE1CCD06A5B82(L_4, (RuntimeObject*)__this, (intptr_t)((void*)il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 3)), NULL);
		NullCheck(((RuntimeObject*)Castclass((RuntimeObject*)L_3, INotifyPropertyChanged_tF956261A15128123778A0D86196231401847CF8C_il2cpp_TypeInfo_var)));
		InterfaceActionInvoker1< PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* >::Invoke(0, INotifyPropertyChanged_tF956261A15128123778A0D86196231401847CF8C_il2cpp_TypeInfo_var, ((RuntimeObject*)Castclass((RuntimeObject*)L_3, INotifyPropertyChanged_tF956261A15128123778A0D86196231401847CF8C_il2cpp_TypeInfo_var)), L_4);
		return;
	}
}
// Method Definition Index: 44302
// Method Definition Index: 44303
// Method Definition Index: 44304
// Method Definition Index: 44305
// Method Definition Index: 44306
// Method Definition Index: 44307
// Method Definition Index: 44308
// Method Definition Index: 44309
// Method Definition Index: 44310
// Method Definition Index: 44311
// Method Definition Index: 44312
// Method Definition Index: 44313
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 44301
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1__ctor_mF80C6E8C5F97CA4D5D88B38D13476C1F1B01AA8F_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, ObservableCollection_1_t88F7DA7F7087023443CFAD5538FB4B4B561318A8* ___0_list, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&INotifyCollectionChanged_tD2E4219A01AB2BD93859A30D924324BE793C2806_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&INotifyPropertyChanged_tF956261A15128123778A0D86196231401847CF8C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	{
		ObservableCollection_1_t88F7DA7F7087023443CFAD5538FB4B4B561318A8* L_0 = ___0_list;
		((  void (*) (ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 0)))((ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*)__this, (RuntimeObject*)L_0, NULL);
		NullCheck((ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*)__this);
		RuntimeObject* L_1;
		L_1 = ((  RuntimeObject* (*) (ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 1)))((ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*)__this, NULL);
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_2 = (NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF*)il2cpp_codegen_object_new(NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF_il2cpp_TypeInfo_var);
		NotifyCollectionChangedEventHandler__ctor_m4DAF76BAC5E44532E6E784CD15A0C4493156E05D(L_2, (RuntimeObject*)__this, (intptr_t)((void*)il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 2)), NULL);
		NullCheck(((RuntimeObject*)Castclass((RuntimeObject*)L_1, INotifyCollectionChanged_tD2E4219A01AB2BD93859A30D924324BE793C2806_il2cpp_TypeInfo_var)));
		InterfaceActionInvoker1< NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* >::Invoke(0, INotifyCollectionChanged_tD2E4219A01AB2BD93859A30D924324BE793C2806_il2cpp_TypeInfo_var, ((RuntimeObject*)Castclass((RuntimeObject*)L_1, INotifyCollectionChanged_tD2E4219A01AB2BD93859A30D924324BE793C2806_il2cpp_TypeInfo_var)), L_2);
		NullCheck((ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*)__this);
		RuntimeObject* L_3;
		L_3 = ((  RuntimeObject* (*) (ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*, const RuntimeMethod*))il2cpp_codegen_get_direct_method_pointer(il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 1)))((ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8*)__this, NULL);
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_4 = (PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA*)il2cpp_codegen_object_new(PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA_il2cpp_TypeInfo_var);
		PropertyChangedEventHandler__ctor_m4015A035EDCA8077D1EA9A8EAB5FE1CCD06A5B82(L_4, (RuntimeObject*)__this, (intptr_t)((void*)il2cpp_rgctx_method(il2cpp_codegen_method_rgctx(method), 3)), NULL);
		NullCheck(((RuntimeObject*)Castclass((RuntimeObject*)L_3, INotifyPropertyChanged_tF956261A15128123778A0D86196231401847CF8C_il2cpp_TypeInfo_var)));
		InterfaceActionInvoker1< PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* >::Invoke(0, INotifyPropertyChanged_tF956261A15128123778A0D86196231401847CF8C_il2cpp_TypeInfo_var, ((RuntimeObject*)Castclass((RuntimeObject*)L_3, INotifyPropertyChanged_tF956261A15128123778A0D86196231401847CF8C_il2cpp_TypeInfo_var)), L_4);
		return;
	}
}
// Method Definition Index: 44302
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_System_Collections_Specialized_INotifyCollectionChanged_add_CollectionChanged_mF9AC017CF7E69D0A7F80CA4FA37C3A943A87098A_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_0 = ___0_value;
		VirtualActionInvoker1< NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* >::Invoke(39, __this, L_0);
		return;
	}
}
// Method Definition Index: 44303
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_System_Collections_Specialized_INotifyCollectionChanged_remove_CollectionChanged_mCB8721E5DAAD317154D8AE22A3DA07D09EE8903F_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_0 = ___0_value;
		VirtualActionInvoker1< NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* >::Invoke(40, __this, L_0);
		return;
	}
}
// Method Definition Index: 44304
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_add_CollectionChanged_mAE5825527A6D19E99895A8DAA509398246CB48CC_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* V_0 = NULL;
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* V_1 = NULL;
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* V_2 = NULL;
	{
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_0 = __this->___CollectionChanged;
		V_0 = L_0;
	}

IL_0007:
	{
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_1 = V_0;
		V_1 = L_1;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_2 = V_1;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_2, L_3, NULL);
		V_2 = ((NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF*)CastclassSealed((RuntimeObject*)L_4, NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF_il2cpp_TypeInfo_var));
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF** L_5 = (NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF**)(&__this->___CollectionChanged);
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_6 = V_2;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_7 = V_1;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_8;
		L_8 = il2cpp_intrinsic_interlocked_compare_exchange(L_5, L_6, L_7);
		V_0 = L_8;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_9 = V_0;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_10 = V_1;
		if ((!(((RuntimeObject*)(NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF*)L_9) == ((RuntimeObject*)(NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 44305
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_remove_CollectionChanged_m0053DE6CBB13E96949805B89FFC64CEA91103419_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* V_0 = NULL;
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* V_1 = NULL;
	NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* V_2 = NULL;
	{
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_0 = __this->___CollectionChanged;
		V_0 = L_0;
	}

IL_0007:
	{
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_1 = V_0;
		V_1 = L_1;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_2 = V_1;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_2, L_3, NULL);
		V_2 = ((NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF*)CastclassSealed((RuntimeObject*)L_4, NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF_il2cpp_TypeInfo_var));
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF** L_5 = (NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF**)(&__this->___CollectionChanged);
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_6 = V_2;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_7 = V_1;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_8;
		L_8 = il2cpp_intrinsic_interlocked_compare_exchange(L_5, L_6, L_7);
		V_0 = L_8;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_9 = V_0;
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_10 = V_1;
		if ((!(((RuntimeObject*)(NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF*)L_9) == ((RuntimeObject*)(NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 44306
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_OnCollectionChanged_mAE7DAA753B70EB3317D78FB5FA0257E729E1A94F_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA* ___0_args, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_0 = __this->___CollectionChanged;
		if (!L_0)
		{
			goto IL_0015;
		}
	}
	{
		NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* L_1 = __this->___CollectionChanged;
		NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA* L_2 = ___0_args;
		NullCheck(L_1);
		NotifyCollectionChangedEventHandler_Invoke_m8DED5705E365E59E2F6BDCBC6D6D762DA1ECA194_inline(L_1, (RuntimeObject*)__this, L_2, NULL);
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 44307
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_System_ComponentModel_INotifyPropertyChanged_add_PropertyChanged_m9A020E7658083E5A736BE0D497E950EC37969357_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_0 = ___0_value;
		VirtualActionInvoker1< PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* >::Invoke(42, __this, L_0);
		return;
	}
}
// Method Definition Index: 44308
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_System_ComponentModel_INotifyPropertyChanged_remove_PropertyChanged_m98914A0E669BC9B9CDE9E9309F27E52936EDE6B5_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* ___0_value, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_0 = ___0_value;
		VirtualActionInvoker1< PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* >::Invoke(43, __this, L_0);
		return;
	}
}
// Method Definition Index: 44309
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_add_PropertyChanged_m6CE7ED0E3F42D73E6C560FC7D77BD2A2C1FA2AC4_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* V_0 = NULL;
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* V_1 = NULL;
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* V_2 = NULL;
	{
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_0 = __this->___PropertyChanged;
		V_0 = L_0;
	}

IL_0007:
	{
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_1 = V_0;
		V_1 = L_1;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_2 = V_1;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_2, L_3, NULL);
		V_2 = ((PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA*)CastclassSealed((RuntimeObject*)L_4, PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA_il2cpp_TypeInfo_var));
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA** L_5 = (PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA**)(&__this->___PropertyChanged);
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_6 = V_2;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_7 = V_1;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_8;
		L_8 = il2cpp_intrinsic_interlocked_compare_exchange(L_5, L_6, L_7);
		V_0 = L_8;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_9 = V_0;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_10 = V_1;
		if ((!(((RuntimeObject*)(PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA*)L_9) == ((RuntimeObject*)(PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 44310
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_remove_PropertyChanged_m382159BA9BD50A2477160EBC20990968AB797CA0_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	//<source_info:<no-source>:1>
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* V_0 = NULL;
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* V_1 = NULL;
	PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* V_2 = NULL;
	{
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_0 = __this->___PropertyChanged;
		V_0 = L_0;
	}

IL_0007:
	{
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_1 = V_0;
		V_1 = L_1;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_2 = V_1;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_3 = ___0_value;
		Delegate_t* L_4;
		L_4 = Delegate_Remove_m8B7DD5661308FA972E23CA1CC3FC9CEB355504E3(L_2, L_3, NULL);
		V_2 = ((PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA*)CastclassSealed((RuntimeObject*)L_4, PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA_il2cpp_TypeInfo_var));
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA** L_5 = (PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA**)(&__this->___PropertyChanged);
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_6 = V_2;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_7 = V_1;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_8;
		L_8 = il2cpp_intrinsic_interlocked_compare_exchange(L_5, L_6, L_7);
		V_0 = L_8;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_9 = V_0;
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_10 = V_1;
		if ((!(((RuntimeObject*)(PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA*)L_9) == ((RuntimeObject*)(PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA*)L_10))))
		{
			goto IL_0007;
		}
	}
	{
		return;
	}
}
// Method Definition Index: 44311
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_OnPropertyChanged_m70EC4876E89241C63DE58EFB3DAB2CE4367A5391_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321* ___0_args, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_0 = __this->___PropertyChanged;
		if (!L_0)
		{
			goto IL_0015;
		}
	}
	{
		PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* L_1 = __this->___PropertyChanged;
		PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321* L_2 = ___0_args;
		NullCheck(L_1);
		PropertyChangedEventHandler_Invoke_m040B3056C279FDDC4EC678519D5D415771905298_inline(L_1, (RuntimeObject*)__this, L_2, NULL);
	}

IL_0015:
	{
		return;
	}
}
// Method Definition Index: 44312
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_HandleCollectionChanged_m99BF361C5E3CA28C8DEF290D528B75AF966D7C34_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, RuntimeObject* ___0_sender, NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA* ___1_e, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA* L_0 = ___1_e;
		VirtualActionInvoker1< NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA* >::Invoke(41, __this, L_0);
		return;
	}
}
// Method Definition Index: 44313
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlyObservableCollection_1_HandlePropertyChanged_m65504ADB9132538DE1DA8C5CEE7C8F781AC88972_fshared (ReadOnlyObservableCollection_1_tEE85386C201F8C8490489FDEC05654DD005248D6* __this, RuntimeObject* ___0_sender, PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321* ___1_e, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321* L_0 = ___1_e;
		VirtualActionInvoker1< PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321* >::Invoke(44, __this, L_0);
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
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m0C325927D022A4794D0573B41E1BAC18B83DB914 (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_12;
		L_12 = il2cpp_unsafe_as_ref<SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474>(L_11);
		int32_t L_13 = ___1_start;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_14;
		L_14 = il2cpp_unsafe_add<SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474,int32_t>(L_12, L_13, sizeof(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474));
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mAAA13269ECF8F777B3D66AA1381ABAF562ECAFAD (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_2;
		L_2 = il2cpp_unsafe_as_ref<SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474>((uint8_t*)L_1);
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* ReadOnlySpan_1_get_Item_m2D69BC4DF6C72A65AF6044FD5D9A1DA836E581CC (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_2 = __this->____pointer;
		V_0 = L_2;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_5;
		L_5 = il2cpp_unsafe_add<SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474,int32_t>(L_3, L_4, sizeof(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mC13BF984D3064FAB75E9F31240A0D461090D28B5 (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, Span_1_t291EAC5144D1F469ED3426F74052EFC6F1E9BCB8 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mBA6FA6140EADE76BD14920BDC6A3E87EDB72FB76_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t291EAC5144D1F469ED3426F74052EFC6F1E9BCB8 L_2 = ___0_destination;
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_3 = L_2.____pointer;
		V_0 = L_3;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474, (Il2CppByReference*)(&V_0));
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_5 = __this->____pointer;
		V_0 = L_5;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisSelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474_m8B225EBD79F98163072E7428252E4FDAFE5A3E1A(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m69C0DC7A9642D41BA974889DBF468384BE169D08 (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, Span_1_t291EAC5144D1F469ED3426F74052EFC6F1E9BCB8 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mBA6FA6140EADE76BD14920BDC6A3E87EDB72FB76_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t291EAC5144D1F469ED3426F74052EFC6F1E9BCB8 L_2 = ___0_destination;
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_3 = L_2.____pointer;
		V_1 = L_3;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474, (Il2CppByReference*)(&V_1));
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_5 = __this->____pointer;
		V_1 = L_5;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisSelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474_m8B225EBD79F98163072E7428252E4FDAFE5A3E1A(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mFD9D2B3920E4AFC4709C68A4404CD723A0ED8A19 (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 ReadOnlySpan_1_Slice_m76C587257127EDB2661E9B2E077F2D6C2904394C (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_2 = __this->____pointer;
		V_0 = L_2;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_5;
		L_5 = il2cpp_unsafe_add<SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474,int32_t>(L_3, L_4, sizeof(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m68B11A5CBD341B71C3B49A184D9A4A0F68B2BC1B_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 ReadOnlySpan_1_Slice_m31FCCE32E3D3F2C5C6059960D7F47D3C0D63FAB6 (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_5 = __this->____pointer;
		V_0 = L_5;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_8;
		L_8 = il2cpp_unsafe_add<SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474,int32_t>(L_6, L_7, sizeof(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m68B11A5CBD341B71C3B49A184D9A4A0F68B2BC1B_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* ReadOnlySpan_1_ToArray_m935765D78782E0DA24C534996DA3AF27448E26F6 (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* L_1;
		L_1 = Array_Empty_TisSelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474_m4433599FD227186796BAB47DC5BED2AF106ED67E_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* L_3 = (SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06*)(SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_6;
		L_6 = il2cpp_unsafe_as_ref<SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474>(L_5);
		ByReference_1_t30C53FE69558F45E4B0A33666EDEDF4CF1F8630F L_7 = __this->____pointer;
		V_0 = L_7;
		SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(SelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisSelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474_m8B225EBD79F98163072E7428252E4FDAFE5A3E1A(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_mD43349D875DC6D0FB0535BE3B3830C1450435420 (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 ___0_left, ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 L_0 = ___0_left;
		ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m273AE2A699A7739E89673154DA9E3BE6B08A180D(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m721EBEC6C55EAE29C1D18E357CBE1B795B121355 (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m5003E9917221426C5BF4F1DD6CCD9F9C627F8CB2 (ReadOnlySpan_1_t274763BD5FC8FCD51E9C71396D664D8858CF9E84* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m9B98D1EF2ED1923D9193FED9660EC7C6F5139AE9 (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_12;
		L_12 = il2cpp_unsafe_as_ref<ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E>(L_11);
		int32_t L_13 = ___1_start;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_14;
		L_14 = il2cpp_unsafe_add<ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E,int32_t>(L_12, L_13, sizeof(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E));
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m305845BDA2359516A6957E6737297A2A5F27AD55 (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_2;
		L_2 = il2cpp_unsafe_as_ref<ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E>((uint8_t*)L_1);
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* ReadOnlySpan_1_get_Item_m4FEF7ACAADCD53633743A309AF589DCE48F58BFC (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_2 = __this->____pointer;
		V_0 = L_2;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_5;
		L_5 = il2cpp_unsafe_add<ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E,int32_t>(L_3, L_4, sizeof(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mE406C4F286E720C0584E9688ABF2D74A7538A275 (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, Span_1_t14EC2E77DBAA48290810774B64FB790F124E6948 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mAB8B8F731C3753B681689724D8C10D0F6706324A_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t14EC2E77DBAA48290810774B64FB790F124E6948 L_2 = ___0_destination;
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_3 = L_2.____pointer;
		V_0 = L_3;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E, (Il2CppByReference*)(&V_0));
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_5 = __this->____pointer;
		V_0 = L_5;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E_mF7B64A7A3AED5F75779174F1164B6D60E51A6215(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mF65307BD5A58DED20FA10FBA697A4B1479A308C9 (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, Span_1_t14EC2E77DBAA48290810774B64FB790F124E6948 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mAB8B8F731C3753B681689724D8C10D0F6706324A_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t14EC2E77DBAA48290810774B64FB790F124E6948 L_2 = ___0_destination;
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_3 = L_2.____pointer;
		V_1 = L_3;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E, (Il2CppByReference*)(&V_1));
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_5 = __this->____pointer;
		V_1 = L_5;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E_mF7B64A7A3AED5F75779174F1164B6D60E51A6215(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mF128D42BDDC7EC3F96FE14AEAB23A36A4E823DA5 (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 ReadOnlySpan_1_Slice_m8959E5E6A0528D9001B8E7A6DB4E15D5549A7766 (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_2 = __this->____pointer;
		V_0 = L_2;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_5;
		L_5 = il2cpp_unsafe_add<ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E,int32_t>(L_3, L_4, sizeof(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m61974A48F0249F9BF95E2B30196D22127E7AD9CB_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 ReadOnlySpan_1_Slice_mAC81A34684A2A53A4B1421858AE1C17E55C7E314 (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_5 = __this->____pointer;
		V_0 = L_5;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_8;
		L_8 = il2cpp_unsafe_add<ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E,int32_t>(L_6, L_7, sizeof(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m61974A48F0249F9BF95E2B30196D22127E7AD9CB_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* ReadOnlySpan_1_ToArray_m1E4F61C18F32A1E062C45D8F74840BCA315F966E (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* L_1;
		L_1 = Array_Empty_TisATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E_m5293579F263F9A3750FDFA1AA45718B28873BFBB_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* L_3 = (ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F*)(ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_6;
		L_6 = il2cpp_unsafe_as_ref<ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E>(L_5);
		ByReference_1_tAAA6F8A1B2C11CBC8A0BD502702CEC3F265DC542 L_7 = __this->____pointer;
		V_0 = L_7;
		ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(ATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E_mF7B64A7A3AED5F75779174F1164B6D60E51A6215(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_mD128C1952337C40880D850BF3DDEF1E1BD97556B (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 ___0_left, ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 L_0 = ___0_left;
		ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mEDC9BE8DCA0B1A3E86A7A3BB531829C06CFA5034(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m88AB28B8ACE18061C33378B00F0BA95A8160E0C2 (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_mDAEFC113AB94712BFA3A94A4248AC661617B480D (ReadOnlySpan_1_tAE84767975672BD032424E74DD54F304DED0E2A7* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m7BB9FF6B73AD65BA3EA059FE070D35A5A05A2A2E (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_12;
		L_12 = il2cpp_unsafe_as_ref<AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03>(L_11);
		int32_t L_13 = ___1_start;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_14;
		L_14 = il2cpp_unsafe_add<AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03,int32_t>(L_12, L_13, sizeof(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03));
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m9FEAF5EB1E55A9031FA9585DEFFEE51EEC45BC6F (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_2;
		L_2 = il2cpp_unsafe_as_ref<AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03>((uint8_t*)L_1);
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* ReadOnlySpan_1_get_Item_m282D17A4B884549C2EB9B13456B88C31B99197E5 (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_2 = __this->____pointer;
		V_0 = L_2;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_5;
		L_5 = il2cpp_unsafe_add<AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03,int32_t>(L_3, L_4, sizeof(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m46DEC4F3A3D53153C9D0C402D4635342455C0336 (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, Span_1_t00FA2E5225B268183BB8AEC08EFAA8532A0B403D ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m1BA243F694B76E06057D36CAD8FE40AF6F9F10B0_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t00FA2E5225B268183BB8AEC08EFAA8532A0B403D L_2 = ___0_destination;
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_3 = L_2.____pointer;
		V_0 = L_3;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03, (Il2CppByReference*)(&V_0));
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_5 = __this->____pointer;
		V_0 = L_5;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisAnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03_mA05FD1200E24DDB998528FC769FF4219BC53EE43(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m5FFD4E944DB745A2F299E34DD01985A2962CE71E (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, Span_1_t00FA2E5225B268183BB8AEC08EFAA8532A0B403D ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m1BA243F694B76E06057D36CAD8FE40AF6F9F10B0_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t00FA2E5225B268183BB8AEC08EFAA8532A0B403D L_2 = ___0_destination;
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_3 = L_2.____pointer;
		V_1 = L_3;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03, (Il2CppByReference*)(&V_1));
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_5 = __this->____pointer;
		V_1 = L_5;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisAnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03_mA05FD1200E24DDB998528FC769FF4219BC53EE43(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mBD0A587250F8E6318164DA0BDF93C109ABD354CE (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 ReadOnlySpan_1_Slice_m0FF5D37B8FDF4C5B118F1221F68E0F6C965F8394 (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_2 = __this->____pointer;
		V_0 = L_2;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_5;
		L_5 = il2cpp_unsafe_add<AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03,int32_t>(L_3, L_4, sizeof(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_mDBF6374C7B6653E0696EFB86B12AB11D9D4EA4C5_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 ReadOnlySpan_1_Slice_m15F1EF949DE6BD5F565D3365FDB4255CC125DE14 (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_5 = __this->____pointer;
		V_0 = L_5;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_8;
		L_8 = il2cpp_unsafe_add<AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03,int32_t>(L_6, L_7, sizeof(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_mDBF6374C7B6653E0696EFB86B12AB11D9D4EA4C5_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* ReadOnlySpan_1_ToArray_m1EFD6FFB64F90F6DD29FE199808AA0EF7F376A80 (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* L_1;
		L_1 = Array_Empty_TisAnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03_mC9DEB54C94662C9FD5779A4BFFAB7AD0469A27C8_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* L_3 = (AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A*)(AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_6;
		L_6 = il2cpp_unsafe_as_ref<AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03>(L_5);
		ByReference_1_t90A708C4DD987EA8AA43AD48D777E5A1AAC12B5C L_7 = __this->____pointer;
		V_0 = L_7;
		AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(AnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisAnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03_mA05FD1200E24DDB998528FC769FF4219BC53EE43(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_mE892AA5F37E2BDA78DBB1E7DDA7FEE6E5496FEE8 (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 ___0_left, ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 L_0 = ___0_left;
		ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mA1563EEFB0CA3654CCED4F7716C0C745D5E04930(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mD40621B09E0649FC54CDC74AA9CB852EACAADA8B (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m644576233FAA28B5804B48CF6EDE9FDFEB2A2838 (ReadOnlySpan_1_tCEA230ECFDF9F457D5673993122C8B560D700029* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m4702ED3CB3E74C8B043D81F216ADF83AE0418021 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_12;
		L_12 = il2cpp_unsafe_as_ref<BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D>(L_11);
		int32_t L_13 = ___1_start;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_14;
		L_14 = il2cpp_unsafe_add<BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D,int32_t>(L_12, L_13, sizeof(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D));
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mF08CD0D591791D31E721C8F03DC3632EC611942E (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_2;
		L_2 = il2cpp_unsafe_as_ref<BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D>((uint8_t*)L_1);
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* ReadOnlySpan_1_get_Item_mB04EED1F39BF7ED9A120CBE4A518B201E47780D2 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_2 = __this->____pointer;
		V_0 = L_2;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_5;
		L_5 = il2cpp_unsafe_add<BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D,int32_t>(L_3, L_4, sizeof(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mA3BD60807A531BFB3AFB377B9B9F7CF0B1250B91 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, Span_1_t36B7E942A80FDA89A0043D5EDED0559212667428 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m739A39BB426B19B7583557F308B2BDA52E2C3DD9_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t36B7E942A80FDA89A0043D5EDED0559212667428 L_2 = ___0_destination;
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_3 = L_2.____pointer;
		V_0 = L_3;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D, (Il2CppByReference*)(&V_0));
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_5 = __this->____pointer;
		V_0 = L_5;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D_m9074142CF2F0828232AFEBB926F490D33E285B9B(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m202888740EDFA7E793DB939EF69BE64DA1BC6FD6 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, Span_1_t36B7E942A80FDA89A0043D5EDED0559212667428 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m739A39BB426B19B7583557F308B2BDA52E2C3DD9_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t36B7E942A80FDA89A0043D5EDED0559212667428 L_2 = ___0_destination;
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_3 = L_2.____pointer;
		V_1 = L_3;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D, (Il2CppByReference*)(&V_1));
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_5 = __this->____pointer;
		V_1 = L_5;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D_m9074142CF2F0828232AFEBB926F490D33E285B9B(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m407A2BF8F2614DCAD4AF6825F49D117C0B3C695E (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 ReadOnlySpan_1_Slice_m34FE3198E00F512E60480008BC8801F38D0BA881 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_2 = __this->____pointer;
		V_0 = L_2;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_5;
		L_5 = il2cpp_unsafe_add<BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D,int32_t>(L_3, L_4, sizeof(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m729E93C65964D6955F3A0794056B0A8FA3E1FB44_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 ReadOnlySpan_1_Slice_m2987082B3484085C44DF03114DBB885F2F148209 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_5 = __this->____pointer;
		V_0 = L_5;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_8;
		L_8 = il2cpp_unsafe_add<BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D,int32_t>(L_6, L_7, sizeof(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m729E93C65964D6955F3A0794056B0A8FA3E1FB44_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* ReadOnlySpan_1_ToArray_m0AE891B83FB21C2BA94D85716FC3F39D9CD54BEA (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* L_1;
		L_1 = Array_Empty_TisBatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D_m652D1F78F0DEF31A60A1D09589A89DE06F12C3E4_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* L_3 = (BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80*)(BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_6;
		L_6 = il2cpp_unsafe_as_ref<BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D>(L_5);
		ByReference_1_t6B219462DA620514530122E206E21B814BB5AE56 L_7 = __this->____pointer;
		V_0 = L_7;
		BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(BatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisBatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D_m9074142CF2F0828232AFEBB926F490D33E285B9B(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_mE2A7461F4801231FC4D69BB7DAA36D8F69F6A561 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 ___0_left, ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 L_0 = ___0_left;
		ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mF6DC606C39C6583C5F7B5BD9E2C131F7250B04E2(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m610EFAA346D93C8C29248F5F4AD9CB8D8F333739 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_mD33AF3D7885C2998A72682CC6B91F31482E10063 (ReadOnlySpan_1_tE73282FE39C46EDD2EE2F61A9E3E69BE943784B7* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mB347432307C32DE1B44BB1D83A146AD5AA63F64B (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_12;
		L_12 = il2cpp_unsafe_as_ref<BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988>(L_11);
		int32_t L_13 = ___1_start;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_14;
		L_14 = il2cpp_unsafe_add<BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988,int32_t>(L_12, L_13, sizeof(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988));
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mDF54D6EA94C50C1FE8B550ED4A7D53519A1FC7F2 (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_2;
		L_2 = il2cpp_unsafe_as_ref<BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988>((uint8_t*)L_1);
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* ReadOnlySpan_1_get_Item_mE8A2E0EAB97E156755E4CB0C5C978932AEC92C34 (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_2 = __this->____pointer;
		V_0 = L_2;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_5;
		L_5 = il2cpp_unsafe_add<BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988,int32_t>(L_3, L_4, sizeof(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m63CB56C481FA3DD4CAF49890F21DF6E4BDEB7024 (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, Span_1_t403EC894E0C29DB4AEFFFACE95188C989E44E524 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mD73C9E60D4C33EBDFB279AF8A4AB7B17EA1D4F3A_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t403EC894E0C29DB4AEFFFACE95188C989E44E524 L_2 = ___0_destination;
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_3 = L_2.____pointer;
		V_0 = L_3;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988, (Il2CppByReference*)(&V_0));
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_5 = __this->____pointer;
		V_0 = L_5;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988_m54E83FBDC4755CF4FADD61A5C99841D951AAEF78(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mA1638EFD65610920247974AE09120DA6C59DFA8A (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, Span_1_t403EC894E0C29DB4AEFFFACE95188C989E44E524 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mD73C9E60D4C33EBDFB279AF8A4AB7B17EA1D4F3A_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t403EC894E0C29DB4AEFFFACE95188C989E44E524 L_2 = ___0_destination;
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_3 = L_2.____pointer;
		V_1 = L_3;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988, (Il2CppByReference*)(&V_1));
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_5 = __this->____pointer;
		V_1 = L_5;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988_m54E83FBDC4755CF4FADD61A5C99841D951AAEF78(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m9214593F21890FDFEE9782AB3D26733CB18DC647 (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E ReadOnlySpan_1_Slice_m705DC35D57EC3EB3F8319D08A146456B3CAE09C7 (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_2 = __this->____pointer;
		V_0 = L_2;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_5;
		L_5 = il2cpp_unsafe_add<BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988,int32_t>(L_3, L_4, sizeof(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m872DD525AC862F1722D3C2CC755B72C6E857038D_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E ReadOnlySpan_1_Slice_mAEFE5FCD148C8314EA73B5FF13F3F47D2F91DDF6 (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_5 = __this->____pointer;
		V_0 = L_5;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_8;
		L_8 = il2cpp_unsafe_add<BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988,int32_t>(L_6, L_7, sizeof(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m872DD525AC862F1722D3C2CC755B72C6E857038D_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* ReadOnlySpan_1_ToArray_m7AC05734B66C9A9FFF8EC25F48AD4B4A7F162CC1 (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* L_1;
		L_1 = Array_Empty_TisBezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988_m89904E98E5508C2B001AC9AB5EE2E1CCA80CEEF3_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* L_3 = (BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB*)(BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_6;
		L_6 = il2cpp_unsafe_as_ref<BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988>(L_5);
		ByReference_1_tA8778AE57245FBD365809D1818CDCB24F6E69ACD L_7 = __this->____pointer;
		V_0 = L_7;
		BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisBezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988_m54E83FBDC4755CF4FADD61A5C99841D951AAEF78(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m15CA7407241EE51C3E109DD028D8F0D5A80BE8D3 (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E ___0_left, ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E L_0 = ___0_left;
		ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m06E929F8878539CC28D9D036C88463DB777E290A(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m5858B90FC80354E0B5856747097357F0CA4FB47E (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m93A02AAD7E7F1F30CBDD2D09D23FEFEDF5ED03EF (ReadOnlySpan_1_tDD110F0B54DB49DB5B5CBD48D21B14A0DDC4785E* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mDA4F4E24BF15E64CFA65836F14C3F0C288826416 (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_12;
		L_12 = il2cpp_unsafe_as_ref<BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3>(L_11);
		int32_t L_13 = ___1_start;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_14;
		L_14 = il2cpp_unsafe_add<BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3,int32_t>(L_12, L_13, sizeof(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3));
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m7B8FA2591526C1126629378DD2F14F260BDB35EB (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_2;
		L_2 = il2cpp_unsafe_as_ref<BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3>((uint8_t*)L_1);
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* ReadOnlySpan_1_get_Item_m2214D14DDBE6B10BD14503BD27D49E951008DA69 (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_2 = __this->____pointer;
		V_0 = L_2;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_5;
		L_5 = il2cpp_unsafe_add<BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3,int32_t>(L_3, L_4, sizeof(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m09DDCD487099B412DD46EBD13C1C828F2DBBB1A7 (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, Span_1_tEA5727F0F276960BF4E47192A9109344388B48E1 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m7A0384ECCF4D6B5955A86B1A3AAA14F426708FE9_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tEA5727F0F276960BF4E47192A9109344388B48E1 L_2 = ___0_destination;
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_3 = L_2.____pointer;
		V_0 = L_3;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3, (Il2CppByReference*)(&V_0));
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_5 = __this->____pointer;
		V_0 = L_5;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3_mD278D7E5E9C662C8F05E45162C4D4E540419CC20(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mF521C6CD28B3DEB8ADE854F383F4F68CEA2BADB3 (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, Span_1_tEA5727F0F276960BF4E47192A9109344388B48E1 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m7A0384ECCF4D6B5955A86B1A3AAA14F426708FE9_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tEA5727F0F276960BF4E47192A9109344388B48E1 L_2 = ___0_destination;
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_3 = L_2.____pointer;
		V_1 = L_3;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3, (Il2CppByReference*)(&V_1));
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_5 = __this->____pointer;
		V_1 = L_5;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3_mD278D7E5E9C662C8F05E45162C4D4E540419CC20(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m22451791C213BEEAFB678201D8C17A30F7FBD9BA (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F ReadOnlySpan_1_Slice_mE830CF0AB00F9F5D7C0D44E3A3E203DC26CDB4B9 (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_2 = __this->____pointer;
		V_0 = L_2;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_5;
		L_5 = il2cpp_unsafe_add<BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3,int32_t>(L_3, L_4, sizeof(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m5F9A39BA66796C6F498F90EC36259B20AAC259F8_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F ReadOnlySpan_1_Slice_mDBACF018F89495D190BA17268124A0EF9E8B15C5 (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_5 = __this->____pointer;
		V_0 = L_5;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_8;
		L_8 = il2cpp_unsafe_add<BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3,int32_t>(L_6, L_7, sizeof(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m5F9A39BA66796C6F498F90EC36259B20AAC259F8_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* ReadOnlySpan_1_ToArray_mE32E519A63F01CA06E51EB1BDF4E7A1D0FECC1B4 (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* L_1;
		L_1 = Array_Empty_TisBezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3_m77719C87E49330E8DA7EA4CD03C4AFDCE18C19C0_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* L_3 = (BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213*)(BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_6;
		L_6 = il2cpp_unsafe_as_ref<BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3>(L_5);
		ByReference_1_t5F44C7A0AF6683C99619D71B6BE935E82435F811 L_7 = __this->____pointer;
		V_0 = L_7;
		BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(BezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisBezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3_mD278D7E5E9C662C8F05E45162C4D4E540419CC20(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m5681B3EB9D6755B21A1933E1B0716AA7CE217C9C (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F ___0_left, ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F L_0 = ___0_left;
		ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mF59FE9D28EA17747D9710D454C4D0A9F68FFE7D4(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m7FDEAE23859A3046E9DEE18A6F4AFC838465141D (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m21A11864ED39F37E348614607BECDFD851328AF8 (ReadOnlySpan_1_t0FFFF083B6B5CDE72AF7004A768B049BB2D5C00F* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m34C5555BE5980FEDF468DC66E1DBE6FE2373FCAC (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		bool* L_12;
		L_12 = il2cpp_unsafe_as_ref<bool>(L_11);
		int32_t L_13 = ___1_start;
		bool* L_14;
		L_14 = il2cpp_unsafe_add<bool,int32_t>(L_12, L_13, sizeof(bool));
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mFFF826E0AE2F5714D74873D22EEEDEBD6B41DA93 (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		bool* L_2;
		L_2 = il2cpp_unsafe_as_ref<bool>((uint8_t*)L_1);
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool* ReadOnlySpan_1_get_Item_mFF62D7DF0E4B11ABB8F52E1DA4D674EE7C13F544 (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_2 = __this->____pointer;
		V_0 = L_2;
		bool* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(bool, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		bool* L_5;
		L_5 = il2cpp_unsafe_add<bool,int32_t>(L_3, L_4, sizeof(bool));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m68979CB9768517F7CB54FD06A135229BD67116EA (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, Span_1_t087F0E3724EBFD3A74A84E3F9E3F027249F37B51 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mED1253429B93CB6D2928015A22105A16FF64C86B_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t087F0E3724EBFD3A74A84E3F9E3F027249F37B51 L_2 = ___0_destination;
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_3 = L_2.____pointer;
		V_0 = L_3;
		bool* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(bool, (Il2CppByReference*)(&V_0));
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_5 = __this->____pointer;
		V_0 = L_5;
		bool* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(bool, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6C1A997DCFEFC3BA96EEB5E75FB8B54DF2D21198(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mE70871A3C5DE13F2BC43A9987E90B37A4DBF1F93 (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, Span_1_t087F0E3724EBFD3A74A84E3F9E3F027249F37B51 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mED1253429B93CB6D2928015A22105A16FF64C86B_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t087F0E3724EBFD3A74A84E3F9E3F027249F37B51 L_2 = ___0_destination;
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_3 = L_2.____pointer;
		V_1 = L_3;
		bool* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(bool, (Il2CppByReference*)(&V_1));
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_5 = __this->____pointer;
		V_1 = L_5;
		bool* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(bool, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6C1A997DCFEFC3BA96EEB5E75FB8B54DF2D21198(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mA76F634454FB10AB7CDBE939E630CA350DAE04F0 (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 ReadOnlySpan_1_Slice_m9DE3E1DDEEA271823A9F9654BC5BE8E82E823961 (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_2 = __this->____pointer;
		V_0 = L_2;
		bool* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(bool, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		bool* L_5;
		L_5 = il2cpp_unsafe_add<bool,int32_t>(L_3, L_4, sizeof(bool));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m5818A0EC9B5A3628E69F90A3521BDE96E1FDC74F_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 ReadOnlySpan_1_Slice_mF9D34C5700763E9CF78E2CF40CFC4E34CB78A3DF (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_5 = __this->____pointer;
		V_0 = L_5;
		bool* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(bool, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		bool* L_8;
		L_8 = il2cpp_unsafe_add<bool,int32_t>(L_6, L_7, sizeof(bool));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m5818A0EC9B5A3628E69F90A3521BDE96E1FDC74F_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* ReadOnlySpan_1_ToArray_m0403E6670DDBDDE0C996C3CAB0D597293FCB0A6D (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_1;
		L_1 = Array_Empty_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mAB215C445888719BD89809D99C3DBD3135C2B1E7_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_3 = (BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4*)(BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		bool* L_6;
		L_6 = il2cpp_unsafe_as_ref<bool>(L_5);
		ByReference_1_t98C4399D749F9F8F828547057023CF78951E6126 L_7 = __this->____pointer;
		V_0 = L_7;
		bool* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(bool, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_m6C1A997DCFEFC3BA96EEB5E75FB8B54DF2D21198(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m00EE5F5857AB90F97C163D601CCF105062DE9389 (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 ___0_left, ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 L_0 = ___0_left;
		ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m0D13DF1A2B584A204679D1E948943A8B7173B812(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mEEE2211222C6D7EE6566F457739199EB2AEFE0F0 (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m393DB2737EAAC6E768469A733343CABB2E207420 (ReadOnlySpan_1_t1CABD9AC48FDD39B6D2C26BCA3D87FCA5F94F1C2* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mFB384FD10F6D51D6A937EA9B1096145A2AEC76A6 (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_12;
		L_12 = il2cpp_unsafe_as_ref<Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3>(L_11);
		int32_t L_13 = ___1_start;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_14;
		L_14 = il2cpp_unsafe_add<Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3,int32_t>(L_12, L_13, sizeof(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3));
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mC05B1826DE4F2D24EA4ACBF887B46969A32E1864 (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_2;
		L_2 = il2cpp_unsafe_as_ref<Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3>((uint8_t*)L_1);
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* ReadOnlySpan_1_get_Item_m6E16724B56B2556345BB55AC0A7D8C91AA15B0C7 (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_2 = __this->____pointer;
		V_0 = L_2;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_5;
		L_5 = il2cpp_unsafe_add<Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3,int32_t>(L_3, L_4, sizeof(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m83454782190C82ABACA976D82FF6993162777E4E (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, Span_1_tF1D09AE3581D2E6304B9FC753705C3FC5DF90634 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mF487895F53310FF6F399B7BD07038C49D29A6EAB_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tF1D09AE3581D2E6304B9FC753705C3FC5DF90634 L_2 = ___0_destination;
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_3 = L_2.____pointer;
		V_0 = L_3;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3, (Il2CppByReference*)(&V_0));
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_5 = __this->____pointer;
		V_0 = L_5;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3_m4925E1D88272BE2E628F28AFFEC4FF09CC2EC647(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mB2196E028EF17F1F5A4C6C69A096BF0133FCC692 (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, Span_1_tF1D09AE3581D2E6304B9FC753705C3FC5DF90634 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mF487895F53310FF6F399B7BD07038C49D29A6EAB_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tF1D09AE3581D2E6304B9FC753705C3FC5DF90634 L_2 = ___0_destination;
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_3 = L_2.____pointer;
		V_1 = L_3;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3, (Il2CppByReference*)(&V_1));
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_5 = __this->____pointer;
		V_1 = L_5;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisBounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3_m4925E1D88272BE2E628F28AFFEC4FF09CC2EC647(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m3D8172EDC539D7ABA59D6A1845314901BA0B0537 (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 ReadOnlySpan_1_Slice_m70440B15F28E98927CBB808DA56044558856C55E (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_2 = __this->____pointer;
		V_0 = L_2;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_5;
		L_5 = il2cpp_unsafe_add<Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3,int32_t>(L_3, L_4, sizeof(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_mB60BC31800C0877424E21575E45AE6C8AA50ADA6_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 ReadOnlySpan_1_Slice_m0CA7AB9DED79E22C791839E29F63ABB68210603E (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_5 = __this->____pointer;
		V_0 = L_5;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_8;
		L_8 = il2cpp_unsafe_add<Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3,int32_t>(L_6, L_7, sizeof(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_mB60BC31800C0877424E21575E45AE6C8AA50ADA6_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* ReadOnlySpan_1_ToArray_m2B9E4011E3272DB371DBAE00E2C99D63C1096AF7 (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* L_1;
		L_1 = Array_Empty_TisBounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3_mB6CFBB5D8AF33F0BAE72154209AB29B8D52FBCDA_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* L_3 = (BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5*)(BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_6;
		L_6 = il2cpp_unsafe_as_ref<Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3>(L_5);
		ByReference_1_t9FE639FD03570F793D25C119C09D9C344FD5577B L_7 = __this->____pointer;
		V_0 = L_7;
		Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(Bounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisBounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3_m4925E1D88272BE2E628F28AFFEC4FF09CC2EC647(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m21E6C8D826AE3ECB1D7EEF0FCFA5CE159462FBF5 (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 ___0_left, ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 L_0 = ___0_left;
		ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m23D991AB6012F2C9896FC8EF47BC84CF63CF86CD(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mC41E4D57F9D0A34A5F0B4A1FA773F175A8C07F64 (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_mF294C697B0A025735FB030B09D96DE149981DB85 (ReadOnlySpan_1_tCA4391229D70967C5F52C769440BB78CBF80BE99* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m7B5C2765879EA5E8D1617D834CC465A39540A913 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		uint8_t* L_12;
		L_12 = il2cpp_unsafe_as_ref<uint8_t>(L_11);
		int32_t L_13 = ___1_start;
		uint8_t* L_14;
		L_14 = il2cpp_unsafe_add<uint8_t,int32_t>(L_12, L_13, sizeof(uint8_t));
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m470D1527EF015478E8677C7BCB52C8410A1DB604 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		uint8_t* L_2;
		L_2 = il2cpp_unsafe_as_ref<uint8_t>((uint8_t*)L_1);
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR uint8_t* ReadOnlySpan_1_get_Item_m878CB30FDAE11537A0DCBE9738B0104EF2C798CC (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_2 = __this->____pointer;
		V_0 = L_2;
		uint8_t* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(uint8_t, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		uint8_t* L_5;
		L_5 = il2cpp_unsafe_add<uint8_t,int32_t>(L_3, L_4, sizeof(uint8_t));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m8144AA3941DF7B7C2C665680221977E1D15D1462 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m8E944E4954E037877A25B9FF6B901F1F901D4769_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 L_2 = ___0_destination;
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_3 = L_2.____pointer;
		V_0 = L_3;
		uint8_t* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(uint8_t, (Il2CppByReference*)(&V_0));
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_5 = __this->____pointer;
		V_0 = L_5;
		uint8_t* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(uint8_t, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_mB1465EEEBE0A608FA51B29BC3F145F287AD04190(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m2C902AB57D2363B06C42BDDE761F3E0DFD5541B3 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m8E944E4954E037877A25B9FF6B901F1F901D4769_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 L_2 = ___0_destination;
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_3 = L_2.____pointer;
		V_1 = L_3;
		uint8_t* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(uint8_t, (Il2CppByReference*)(&V_1));
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_5 = __this->____pointer;
		V_1 = L_5;
		uint8_t* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(uint8_t, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_mB1465EEEBE0A608FA51B29BC3F145F287AD04190(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m72B233A5EEDFB1EC10568EBB03636205A2CD2BA6 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D ReadOnlySpan_1_Slice_mC8B7C665F49384744642F03EA355239F0E4AF966 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_2 = __this->____pointer;
		V_0 = L_2;
		uint8_t* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(uint8_t, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		uint8_t* L_5;
		L_5 = il2cpp_unsafe_add<uint8_t,int32_t>(L_3, L_4, sizeof(uint8_t));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m0FC0B92549C2968E80B5F75A85F28B96DBFCFD63_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D ReadOnlySpan_1_Slice_mEB3D3A427170FC5A0AB734619D4792C299697C89 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_5 = __this->____pointer;
		V_0 = L_5;
		uint8_t* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(uint8_t, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		uint8_t* L_8;
		L_8 = il2cpp_unsafe_add<uint8_t,int32_t>(L_6, L_7, sizeof(uint8_t));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m0FC0B92549C2968E80B5F75A85F28B96DBFCFD63_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ReadOnlySpan_1_ToArray_mF805FAE3816BCFA5C63592CA173237F9B1E4566E (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_1;
		L_1 = Array_Empty_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m6080CA526758F4FA182A066B2780D1761CD36ED5_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_3 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		uint8_t* L_6;
		L_6 = il2cpp_unsafe_as_ref<uint8_t>(L_5);
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_7 = __this->____pointer;
		V_0 = L_7;
		uint8_t* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(uint8_t, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_mB1465EEEBE0A608FA51B29BC3F145F287AD04190(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m05DA1EFA944F510C32BA01D5114BFC037E2226DA (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D ___0_left, ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D L_0 = ___0_left;
		ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mB932864AE19818E61EF5ADBBCB02A87BD771CDF7(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m8233C912934C08E206DCCC1E5D8A0F8DBCF01D46 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_mDC71816B8A88A167F396D550E922E9236155ECA8 (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mDADE08E6B4594775C6675B389078E5FE98C745D5 (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		Il2CppChar* L_12;
		L_12 = il2cpp_unsafe_as_ref<Il2CppChar>(L_11);
		int32_t L_13 = ___1_start;
		Il2CppChar* L_14;
		L_14 = il2cpp_unsafe_add<Il2CppChar,int32_t>(L_12, L_13, sizeof(Il2CppChar));
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mB16A8EC9CCDE68A166108BE32B6DDA7D7C88BC17 (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		Il2CppChar* L_2;
		L_2 = il2cpp_unsafe_as_ref<Il2CppChar>((uint8_t*)L_1);
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Il2CppChar* ReadOnlySpan_1_get_Item_mBDFB7BA59BFDB72611F8286970AFA25C5D9A2284 (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_2 = __this->____pointer;
		V_0 = L_2;
		Il2CppChar* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		Il2CppChar* L_5;
		L_5 = il2cpp_unsafe_add<Il2CppChar,int32_t>(L_3, L_4, sizeof(Il2CppChar));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m42F1668DECE40277D97A37705EA6BE27CF7D5030 (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mB79622153F80AD55A805C005842AF045F4FCF992_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D L_2 = ___0_destination;
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_3 = L_2.____pointer;
		V_0 = L_3;
		Il2CppChar* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_0));
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_5 = __this->____pointer;
		V_0 = L_5;
		Il2CppChar* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m62AF071D7F91DFC9A4D8B847D6A4472B820B5446(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m22E885FE905F1306DDE8000F155109F7F4987666 (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mB79622153F80AD55A805C005842AF045F4FCF992_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D L_2 = ___0_destination;
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_3 = L_2.____pointer;
		V_1 = L_3;
		Il2CppChar* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_1));
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_5 = __this->____pointer;
		V_1 = L_5;
		Il2CppChar* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m62AF071D7F91DFC9A4D8B847D6A4472B820B5446(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mB13A913EF211B14B719EE62133C15C298642E34D (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
	}
	{
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_0 = __this->____pointer;
		V_1 = L_0;
		Il2CppChar* L_1;
		L_1 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_1));
		Il2CppChar* L_2;
		L_2 = il2cpp_unsafe_as_ref<Il2CppChar>(L_1);
		V_0 = L_2;
		Il2CppChar* L_3 = V_0;
		uintptr_t L_4 = (il2cpp_codegen_conv<uintptr_t,Il2CppChar*,intptr_t,false,false>(L_3,NULL));
		int32_t L_5 = __this->____length;
		String_t* L_6;
		L_6 = String_CreateString_m3F8794FEB452558B8A68C65E1F0B603B3D94E0E2(NULL, (Il2CppChar*)L_4, 0, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ReadOnlySpan_1_Slice_mBF43FC5284A77519BB9C3BAB34F66A0A4B78CFE2 (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_2 = __this->____pointer;
		V_0 = L_2;
		Il2CppChar* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		Il2CppChar* L_5;
		L_5 = il2cpp_unsafe_add<Il2CppChar,int32_t>(L_3, L_4, sizeof(Il2CppChar));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ReadOnlySpan_1_Slice_mDEEA4C304B13C8F7A63BC3D60B62FF17BBEE282B (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_5 = __this->____pointer;
		V_0 = L_5;
		Il2CppChar* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		Il2CppChar* L_8;
		L_8 = il2cpp_unsafe_add<Il2CppChar,int32_t>(L_6, L_7, sizeof(Il2CppChar));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ReadOnlySpan_1_ToArray_m3CC38C41E9FB235D0C9D24033DE4D561B1017488 (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_1;
		L_1 = Array_Empty_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_mD1C1362CB74B91496D984B006ADC79B688D9B50D_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_3 = (CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)(CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		Il2CppChar* L_6;
		L_6 = il2cpp_unsafe_as_ref<Il2CppChar>(L_5);
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_7 = __this->____pointer;
		V_0 = L_7;
		Il2CppChar* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_m62AF071D7F91DFC9A4D8B847D6A4472B820B5446(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m35D4E25108DD27E665818B5772C1168732E2336A (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ___0_left, ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_0 = ___0_left;
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m3ADF20EB2963614D95DD3C4605667A643990414A(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mAFC53FF457D681D6B91A0C169D2EEEAB29625F7F (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m43FCB412B29F8C6C9215173C3D4A72DC006670DF (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m24EF794029094B57283ECDCD7740CC652BB2A6BD (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_12;
		L_12 = il2cpp_unsafe_as_ref<Color_tD001788D726C3A7F1379BEED0260B9591F440C1F>(L_11);
		int32_t L_13 = ___1_start;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_14;
		L_14 = il2cpp_unsafe_add<Color_tD001788D726C3A7F1379BEED0260B9591F440C1F,int32_t>(L_12, L_13, sizeof(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F));
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mBE7B7ABFDE7F2FEC27C3B667CCEB6429AF200EA3 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_2;
		L_2 = il2cpp_unsafe_as_ref<Color_tD001788D726C3A7F1379BEED0260B9591F440C1F>((uint8_t*)L_1);
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* ReadOnlySpan_1_get_Item_mF76CD5792F472262C704613F613D4CBE3BABF5E4 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_2 = __this->____pointer;
		V_0 = L_2;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_5;
		L_5 = il2cpp_unsafe_add<Color_tD001788D726C3A7F1379BEED0260B9591F440C1F,int32_t>(L_3, L_4, sizeof(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mE2EE6C72DB6F2B003ADEEA9E3B05F1A593A17268 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, Span_1_t162CEB1BEB175B131BEFB9D4647CEB2D48558018 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m83147EFB62B7FFBA49EEBEBE975F7348D665BAD0_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t162CEB1BEB175B131BEFB9D4647CEB2D48558018 L_2 = ___0_destination;
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_3 = L_2.____pointer;
		V_0 = L_3;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F, (Il2CppByReference*)(&V_0));
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_5 = __this->____pointer;
		V_0 = L_5;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisColor_tD001788D726C3A7F1379BEED0260B9591F440C1F_m3B15D4D61932A340BAAA5BE6E8D419605D3692E6(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m9018C365B112852E6A667852C5A759C2A04E94CF (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, Span_1_t162CEB1BEB175B131BEFB9D4647CEB2D48558018 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m83147EFB62B7FFBA49EEBEBE975F7348D665BAD0_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t162CEB1BEB175B131BEFB9D4647CEB2D48558018 L_2 = ___0_destination;
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_3 = L_2.____pointer;
		V_1 = L_3;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F, (Il2CppByReference*)(&V_1));
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_5 = __this->____pointer;
		V_1 = L_5;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisColor_tD001788D726C3A7F1379BEED0260B9591F440C1F_m3B15D4D61932A340BAAA5BE6E8D419605D3692E6(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m62ECA92EA53DA7C8231B7831217BCD47859F9AA6 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 ReadOnlySpan_1_Slice_m215C7FCD98D84544F3479D4C45E04E983D418901 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_2 = __this->____pointer;
		V_0 = L_2;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_5;
		L_5 = il2cpp_unsafe_add<Color_tD001788D726C3A7F1379BEED0260B9591F440C1F,int32_t>(L_3, L_4, sizeof(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_mD46BD07C5320D1B1A225E53FAF466E153FFC1679_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 ReadOnlySpan_1_Slice_m9584B58FA543FE0B53F8837A337FE79859D929B7 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_5 = __this->____pointer;
		V_0 = L_5;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_8;
		L_8 = il2cpp_unsafe_add<Color_tD001788D726C3A7F1379BEED0260B9591F440C1F,int32_t>(L_6, L_7, sizeof(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_mD46BD07C5320D1B1A225E53FAF466E153FFC1679_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* ReadOnlySpan_1_ToArray_mD4A471A0F445998FDD93C7C0BB5884E2CF199385 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* L_1;
		L_1 = Array_Empty_TisColor_tD001788D726C3A7F1379BEED0260B9591F440C1F_m7E922E24AAEBD664256383832D53DDF17E1F3052_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* L_3 = (ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389*)(ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_6;
		L_6 = il2cpp_unsafe_as_ref<Color_tD001788D726C3A7F1379BEED0260B9591F440C1F>(L_5);
		ByReference_1_t9B1B5AE3B920C99C8F3511B46F258F98CCFD0836 L_7 = __this->____pointer;
		V_0 = L_7;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(Color_tD001788D726C3A7F1379BEED0260B9591F440C1F, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisColor_tD001788D726C3A7F1379BEED0260B9591F440C1F_m3B15D4D61932A340BAAA5BE6E8D419605D3692E6(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m3E91CC83F896AF504BA48D2F8CF5F2BE0D185F1D (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 ___0_left, ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 L_0 = ___0_left;
		ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mB25172459C8906252EAB2A17B1BD28318B8A9AF2(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m67728F319A567D961B874F9B5EE71C6BC5432352 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m277AC104404EB5564C0F31960B2AA480BE297D45 (ReadOnlySpan_1_t7EE9E7096EE52CA433AA61A9353AD90B29B2C816* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m541FC0E035DCC15F5DF08D213BB9F453046A4333 (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_12;
		L_12 = il2cpp_unsafe_as_ref<Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B>(L_11);
		int32_t L_13 = ___1_start;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_14;
		L_14 = il2cpp_unsafe_add<Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B,int32_t>(L_12, L_13, sizeof(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B));
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m38E5369296C7EE2469A5481F2FA5FF6DBCAD417D (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_2;
		L_2 = il2cpp_unsafe_as_ref<Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B>((uint8_t*)L_1);
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* ReadOnlySpan_1_get_Item_mC80956E0E8614BADD2650A21F33984CBCF757260 (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_2 = __this->____pointer;
		V_0 = L_2;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_5;
		L_5 = il2cpp_unsafe_add<Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B,int32_t>(L_3, L_4, sizeof(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m698DDFC2F5E22F39C11D84BAAE310BA87EFDFCEA (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, Span_1_tCF27B3DC55C4AD13268A1F154352D6782B237F7D ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m413F20ECFE3F523163B62ED5BAB7DFA22F2FCCD4_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tCF27B3DC55C4AD13268A1F154352D6782B237F7D L_2 = ___0_destination;
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_3 = L_2.____pointer;
		V_0 = L_3;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B, (Il2CppByReference*)(&V_0));
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_5 = __this->____pointer;
		V_0 = L_5;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisColor32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B_mA65CAF282F03FD4DB46666E2C44C22E3D0035A68(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mB1560467E9707FB23EE15CAC3F2C4C93541D2153 (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, Span_1_tCF27B3DC55C4AD13268A1F154352D6782B237F7D ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m413F20ECFE3F523163B62ED5BAB7DFA22F2FCCD4_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tCF27B3DC55C4AD13268A1F154352D6782B237F7D L_2 = ___0_destination;
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_3 = L_2.____pointer;
		V_1 = L_3;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B, (Il2CppByReference*)(&V_1));
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_5 = __this->____pointer;
		V_1 = L_5;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisColor32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B_mA65CAF282F03FD4DB46666E2C44C22E3D0035A68(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mFECF2AEE52DEEB8AC80A73F4FAF03ACEE3DAC97B (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC ReadOnlySpan_1_Slice_m11D5B53A1442807A1A666F52BF070F057E0C5D54 (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_2 = __this->____pointer;
		V_0 = L_2;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_5;
		L_5 = il2cpp_unsafe_add<Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B,int32_t>(L_3, L_4, sizeof(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m841651F5E0DEC77984A109EF01090F3F776571FB_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC ReadOnlySpan_1_Slice_m7C374F833D54870C52C84B6EEDDAEB3DF98D067E (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_5 = __this->____pointer;
		V_0 = L_5;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_8;
		L_8 = il2cpp_unsafe_add<Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B,int32_t>(L_6, L_7, sizeof(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m841651F5E0DEC77984A109EF01090F3F776571FB_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* ReadOnlySpan_1_ToArray_m48EC24C05110C226EF52CD8157463D40CA67B796 (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* L_1;
		L_1 = Array_Empty_TisColor32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B_m5B415C4745E47108DD9258EBCCB422EFD6B8A0EB_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* L_3 = (Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259*)(Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_6;
		L_6 = il2cpp_unsafe_as_ref<Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B>(L_5);
		ByReference_1_t2F6DD447F09BA6643D703E03C0CB184F51C4BC97 L_7 = __this->____pointer;
		V_0 = L_7;
		Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(Color32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisColor32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B_mA65CAF282F03FD4DB46666E2C44C22E3D0035A68(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m16FD1C1E0B75454E96CFD82EC29CB75E4250237E (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC ___0_left, ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC L_0 = ___0_left;
		ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mB7E64928FFEDF69D6B4302B599C9461869E152C7(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m3E6B58352198D70006EC18B7534190957698402C (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_mD2CF79AC4B544BE1A8B0AE12D0B40A22A651D999 (ReadOnlySpan_1_t2E193A80641154947640E9A71614019D7A8F3DFC* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mAA7735D0514FBD6C8A8CE3541429D141DF69C5B9 (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_12;
		L_12 = il2cpp_unsafe_as_ref<CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE>(L_11);
		int32_t L_13 = ___1_start;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_14;
		L_14 = il2cpp_unsafe_add<CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE,int32_t>(L_12, L_13, sizeof(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE));
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m3CE893285B6A8F7F2A91E4683E82A02A8B926B3F (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_2;
		L_2 = il2cpp_unsafe_as_ref<CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE>((uint8_t*)L_1);
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* ReadOnlySpan_1_get_Item_m2EF43F1CE8F3BD9D2DC4B86C2AA0B81C13055792 (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_2 = __this->____pointer;
		V_0 = L_2;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_5;
		L_5 = il2cpp_unsafe_add<CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE,int32_t>(L_3, L_4, sizeof(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m36454CB3A8758048E693C69E5955D2E93F79F707 (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, Span_1_t417FB548242C07F6973D324BB1024F79C8D74EAD ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m0B73281BD3E653B71D0308FCD7C435EFD8D176E6_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t417FB548242C07F6973D324BB1024F79C8D74EAD L_2 = ___0_destination;
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_3 = L_2.____pointer;
		V_0 = L_3;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE, (Il2CppByReference*)(&V_0));
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_5 = __this->____pointer;
		V_0 = L_5;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisCombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE_m3C4417F1F221D78FCCCF0E2157B78FE672A7E86F(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m0C62BC5603E11DBDB5E149DB9ABE4A07177FC46B (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, Span_1_t417FB548242C07F6973D324BB1024F79C8D74EAD ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m0B73281BD3E653B71D0308FCD7C435EFD8D176E6_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t417FB548242C07F6973D324BB1024F79C8D74EAD L_2 = ___0_destination;
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_3 = L_2.____pointer;
		V_1 = L_3;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE, (Il2CppByReference*)(&V_1));
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_5 = __this->____pointer;
		V_1 = L_5;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisCombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE_m3C4417F1F221D78FCCCF0E2157B78FE672A7E86F(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mB66207CD651BDD85337C77CAE5CB4C179CAEDEDF (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A ReadOnlySpan_1_Slice_mB86E973ED5DA97C6062865BEB979887D19EDB838 (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_2 = __this->____pointer;
		V_0 = L_2;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_5;
		L_5 = il2cpp_unsafe_add<CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE,int32_t>(L_3, L_4, sizeof(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_mD840B6A68992D74BFDFF1A7A19DE2840B62C80CC_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A ReadOnlySpan_1_Slice_m7ACE5D49D2B9D8ADF87F7DF7F686FE6F4C8A1431 (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_5 = __this->____pointer;
		V_0 = L_5;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_8;
		L_8 = il2cpp_unsafe_add<CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE,int32_t>(L_6, L_7, sizeof(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_mD840B6A68992D74BFDFF1A7A19DE2840B62C80CC_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* ReadOnlySpan_1_ToArray_m1A08C59E2CB12A4E4706EECBBB4AF5F31AF64986 (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* L_1;
		L_1 = Array_Empty_TisCombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE_mC39B4BC80AD9C1F917509BB8BE77C4C73CB471F9_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* L_3 = (CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093*)(CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_6;
		L_6 = il2cpp_unsafe_as_ref<CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE>(L_5);
		ByReference_1_t5B8DF5CA7B07C33B7F1E66D9B30C82C77957E78B L_7 = __this->____pointer;
		V_0 = L_7;
		CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(CombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisCombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE_m3C4417F1F221D78FCCCF0E2157B78FE672A7E86F(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m2577602D0DD809DC8D70484B5F7EF5122427BF4A (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A ___0_left, ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A L_0 = ___0_left;
		ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m7666D2A6469776FB525177C22CFBEDDB5A0C3EEB(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mD5CBDDE7A389EC2D515E7DADB193BDCA6CF586CD (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m4861B1801881C1D20C1F4C5A58398D017B4311F2 (ReadOnlySpan_1_tF1B62A47054EA93184C489EEA227FFBD0348D74A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m032061AD3999853660D069A9EA55DB865F8A30A1 (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_12;
		L_12 = il2cpp_unsafe_as_ref<ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B>(L_11);
		int32_t L_13 = ___1_start;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_14;
		L_14 = il2cpp_unsafe_add<ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B,int32_t>(L_12, L_13, sizeof(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B));
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mF12C4BAD5EBEFD740D783A0199A885174F3B99A9 (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_2;
		L_2 = il2cpp_unsafe_as_ref<ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B>((uint8_t*)L_1);
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* ReadOnlySpan_1_get_Item_mD5E2C74DADFFF2EB2013C093403217F643E3FDEA (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_2 = __this->____pointer;
		V_0 = L_2;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_5;
		L_5 = il2cpp_unsafe_add<ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B,int32_t>(L_3, L_4, sizeof(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m68D6B223E56FE5CC9235A39AB8DE9FBC47859133 (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, Span_1_tBBAFA836A153BEF6FDEF8E7DE7F4E0F6DC6B0907 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mFC8114ED22DABAF53250748121F627D7199AFE7C_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tBBAFA836A153BEF6FDEF8E7DE7F4E0F6DC6B0907 L_2 = ___0_destination;
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_3 = L_2.____pointer;
		V_0 = L_3;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B, (Il2CppByReference*)(&V_0));
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_5 = __this->____pointer;
		V_0 = L_5;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B_m7B231E78B8FBBF3128AC7D5D9AA1EF250AB8BBB7(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mABACFEF143C915AAC252D2511FE39D713F095993 (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, Span_1_tBBAFA836A153BEF6FDEF8E7DE7F4E0F6DC6B0907 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mFC8114ED22DABAF53250748121F627D7199AFE7C_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tBBAFA836A153BEF6FDEF8E7DE7F4E0F6DC6B0907 L_2 = ___0_destination;
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_3 = L_2.____pointer;
		V_1 = L_3;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B, (Il2CppByReference*)(&V_1));
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_5 = __this->____pointer;
		V_1 = L_5;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B_m7B231E78B8FBBF3128AC7D5D9AA1EF250AB8BBB7(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mB9CD291D96454CA0A23BB1E9C39EE497AAFD7D89 (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B ReadOnlySpan_1_Slice_m837ADFC41A3283757D806263DCCF7F05FAAC286C (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_2 = __this->____pointer;
		V_0 = L_2;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_5;
		L_5 = il2cpp_unsafe_add<ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B,int32_t>(L_3, L_4, sizeof(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m3263AAE2466F33D0150BF7B8223568108EB82C0B_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B ReadOnlySpan_1_Slice_m43607F00E7BAB24EF955A2BC3D5A4F23EB19ECEE (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_5 = __this->____pointer;
		V_0 = L_5;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_8;
		L_8 = il2cpp_unsafe_add<ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B,int32_t>(L_6, L_7, sizeof(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m3263AAE2466F33D0150BF7B8223568108EB82C0B_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* ReadOnlySpan_1_ToArray_mECE9942E5C633C36CED10EE9B903F15E6B23305F (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* L_1;
		L_1 = Array_Empty_TisContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B_m2AE4E8C460D733DF5362B246ED865C5CE0C4AA60_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* L_3 = (ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A*)(ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_6;
		L_6 = il2cpp_unsafe_as_ref<ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B>(L_5);
		ByReference_1_t5201FA96B0CE6EF7DA17ED2350AB142709D673CE L_7 = __this->____pointer;
		V_0 = L_7;
		ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(ContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B_m7B231E78B8FBBF3128AC7D5D9AA1EF250AB8BBB7(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_mE925D73963DC373FE2AB0BFA0D832C03E5DDACD3 (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B ___0_left, ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B L_0 = ___0_left;
		ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mCFF3D8D583CDF24902773BD33205A6488B81C7B3(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mE7EF0D565949C53DA00C392FB82EBBF3F0C62204 (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m962F01CDE19CACEC7562C969E3FA432C9A506FBC (ReadOnlySpan_1_tE3532BC307A7083D956DB5168B470DDF370FB99B* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mE61DFBDCC9BBEB3D9E67475F3773405DF6FC847B (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_12;
		L_12 = il2cpp_unsafe_as_ref<ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C>(L_11);
		int32_t L_13 = ___1_start;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_14;
		L_14 = il2cpp_unsafe_add<ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C,int32_t>(L_12, L_13, sizeof(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C));
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mE6679731A19770BBE0DA855EA7D4EA93256CC87A (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_2;
		L_2 = il2cpp_unsafe_as_ref<ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C>((uint8_t*)L_1);
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* ReadOnlySpan_1_get_Item_m2D8780218AF6888A2D88FA1EFB26F8A0BBA85168 (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_2 = __this->____pointer;
		V_0 = L_2;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_5;
		L_5 = il2cpp_unsafe_add<ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C,int32_t>(L_3, L_4, sizeof(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m12C3FFBF34895F22A3355AD65545234FBB1DBC1E (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, Span_1_tFD51B8D0B2DA78C0F911056FC60AB6B732AD4F7C ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mBAB1E51297560C637528EE41B5EB5658D2EFE5D9_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tFD51B8D0B2DA78C0F911056FC60AB6B732AD4F7C L_2 = ___0_destination;
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_3 = L_2.____pointer;
		V_0 = L_3;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C, (Il2CppByReference*)(&V_0));
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_5 = __this->____pointer;
		V_0 = L_5;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C_m25EF20C7583AB5F948F033EAAAD3726E13608167(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m88D59F0F6F41F2D44F503746E6AE26BAB2A45005 (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, Span_1_tFD51B8D0B2DA78C0F911056FC60AB6B732AD4F7C ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mBAB1E51297560C637528EE41B5EB5658D2EFE5D9_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tFD51B8D0B2DA78C0F911056FC60AB6B732AD4F7C L_2 = ___0_destination;
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_3 = L_2.____pointer;
		V_1 = L_3;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C, (Il2CppByReference*)(&V_1));
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_5 = __this->____pointer;
		V_1 = L_5;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C_m25EF20C7583AB5F948F033EAAAD3726E13608167(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m03592E5474FB9DC85413161436F5AABBF47A89B8 (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 ReadOnlySpan_1_Slice_m7846DBD0464FDEB89067CC6610BAA49D2CE33CF3 (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_2 = __this->____pointer;
		V_0 = L_2;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_5;
		L_5 = il2cpp_unsafe_add<ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C,int32_t>(L_3, L_4, sizeof(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m137BDE40C94EA6785C970709ABF68088E145D866_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 ReadOnlySpan_1_Slice_m3DA9F65F222100E7D2AADE09DFE6D2FF5114504D (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_5 = __this->____pointer;
		V_0 = L_5;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_8;
		L_8 = il2cpp_unsafe_add<ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C,int32_t>(L_6, L_7, sizeof(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m137BDE40C94EA6785C970709ABF68088E145D866_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* ReadOnlySpan_1_ToArray_m006E0A1BC7969FD5892866486C384CDAA514C8AA (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* L_1;
		L_1 = Array_Empty_TisConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C_m4779104F7674EACD211A240162D4414087C431E8_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* L_3 = (ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38*)(ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_6;
		L_6 = il2cpp_unsafe_as_ref<ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C>(L_5);
		ByReference_1_t2C0A9D8C238DC2DEEFEA14FBC9E777B0AEFCD6F0 L_7 = __this->____pointer;
		V_0 = L_7;
		ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C_m25EF20C7583AB5F948F033EAAAD3726E13608167(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_mA5AF337E9D704B5834594ACFA9D981D58F9D60D9 (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 ___0_left, ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 L_0 = ___0_left;
		ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mE5D73B81C2027EABC9DF61DD832743B960E6C20B(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m804D1B7BFDC2D5BB9875789AD83D36D769B63E0E (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m7C071752001C4849D60892C3876A353248FEE04E (ReadOnlySpan_1_tB1D1F5FAB48CD1D2138B8A65584D64A35384B962* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mA69E37324A8CF5B873DD775675B156437C5DC12D (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_12;
		L_12 = il2cpp_unsafe_as_ref<ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1>(L_11);
		int32_t L_13 = ___1_start;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_14;
		L_14 = il2cpp_unsafe_add<ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1,int32_t>(L_12, L_13, sizeof(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1));
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m14A1B0C89315F17814260A9EC5B6BAEB97D133E5 (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_2;
		L_2 = il2cpp_unsafe_as_ref<ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1>((uint8_t*)L_1);
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* ReadOnlySpan_1_get_Item_mD9A78516014DFA302A15D239FA8202776A7E9539 (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_2 = __this->____pointer;
		V_0 = L_2;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_5;
		L_5 = il2cpp_unsafe_add<ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1,int32_t>(L_3, L_4, sizeof(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mA6C86547DDF9DE5A3EDB595EA6A0D54B795FB6CE (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, Span_1_tA69488C008961B8AE09141F37FDAE8A0F575CE48 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m3B50FB2CF29A0E7C0C8B69E4E5A72D75883EBC4B_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tA69488C008961B8AE09141F37FDAE8A0F575CE48 L_2 = ___0_destination;
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_3 = L_2.____pointer;
		V_0 = L_3;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1, (Il2CppByReference*)(&V_0));
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_5 = __this->____pointer;
		V_0 = L_5;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1_m747B7D7DC0AA466783F69D3676EC5EE854D08D52(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m3F9D681B9D277F095715FD341EE75C2AB52C3B5D (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, Span_1_tA69488C008961B8AE09141F37FDAE8A0F575CE48 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m3B50FB2CF29A0E7C0C8B69E4E5A72D75883EBC4B_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tA69488C008961B8AE09141F37FDAE8A0F575CE48 L_2 = ___0_destination;
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_3 = L_2.____pointer;
		V_1 = L_3;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1, (Il2CppByReference*)(&V_1));
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_5 = __this->____pointer;
		V_1 = L_5;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1_m747B7D7DC0AA466783F69D3676EC5EE854D08D52(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m578FEE09F52F46D6FFBAFC2FC35F5BBD77B4F60D (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A ReadOnlySpan_1_Slice_m0A4934BB064715E5225642C93C710CAAFED22776 (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_2 = __this->____pointer;
		V_0 = L_2;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_5;
		L_5 = il2cpp_unsafe_add<ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1,int32_t>(L_3, L_4, sizeof(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_mDB9E4DFF0687E0A5405FF4F6387379524B1B07AB_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A ReadOnlySpan_1_Slice_m67EFE7408463873AAFD43728C5FC0C2FE0599AF0 (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_5 = __this->____pointer;
		V_0 = L_5;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_8;
		L_8 = il2cpp_unsafe_add<ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1,int32_t>(L_6, L_7, sizeof(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_mDB9E4DFF0687E0A5405FF4F6387379524B1B07AB_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* ReadOnlySpan_1_ToArray_mA908B43038DF7C5AD269469D4E4B1D9862F4F2E1 (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* L_1;
		L_1 = Array_Empty_TisConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1_m164EDF2519D24430C10F8BB106ADB252BF225C26_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* L_3 = (ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2*)(ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_6;
		L_6 = il2cpp_unsafe_as_ref<ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1>(L_5);
		ByReference_1_tF22763B5EFC788349C264CA03884C061A3FD8051 L_7 = __this->____pointer;
		V_0 = L_7;
		ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(ConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1_m747B7D7DC0AA466783F69D3676EC5EE854D08D52(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m2A77135ABA53526AA5CF1F43C42ABD4F9B8A106F (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A ___0_left, ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A L_0 = ___0_left;
		ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m0615A4A62B9BB81921E873791EA6E0973505024A(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m9B4CC218C369D8AE805235830EECDF1E70D16E89 (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m2EEE014653545F86D63A44252AE86205115E84B0 (ReadOnlySpan_1_t1CFFA3B34C09F05622921AC4014011D43A25715A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mCBC2F8C3CEBB0E8F75933B7E87B5153421D89730 (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_12;
		L_12 = il2cpp_unsafe_as_ref<CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8>(L_11);
		int32_t L_13 = ___1_start;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_14;
		L_14 = il2cpp_unsafe_add<CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8,int32_t>(L_12, L_13, sizeof(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8));
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mD4CD6C8BBED5DFEF79F3824DC60BB75383680DE4 (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_2;
		L_2 = il2cpp_unsafe_as_ref<CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8>((uint8_t*)L_1);
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* ReadOnlySpan_1_get_Item_mAC6277F34695A2848F448A2A18244857563EA132 (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_2 = __this->____pointer;
		V_0 = L_2;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_5;
		L_5 = il2cpp_unsafe_add<CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8,int32_t>(L_3, L_4, sizeof(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m222C63C7A336C2501B38A7F611D522ECE358AAC9 (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, Span_1_t0F901C7F7248B37E2D8773703E8E8790204A0A30 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mD8B694DDD50485878B197BD97C4848FA0E770DCD_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t0F901C7F7248B37E2D8773703E8E8790204A0A30 L_2 = ___0_destination;
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_3 = L_2.____pointer;
		V_0 = L_3;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8, (Il2CppByReference*)(&V_0));
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_5 = __this->____pointer;
		V_0 = L_5;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisCopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8_m8A5B65BF262A2B1C988311D13733E08E197C8A03(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m2C323DCD5E97C48E69FE357A3F44708FF717A6EC (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, Span_1_t0F901C7F7248B37E2D8773703E8E8790204A0A30 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mD8B694DDD50485878B197BD97C4848FA0E770DCD_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t0F901C7F7248B37E2D8773703E8E8790204A0A30 L_2 = ___0_destination;
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_3 = L_2.____pointer;
		V_1 = L_3;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8, (Il2CppByReference*)(&V_1));
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_5 = __this->____pointer;
		V_1 = L_5;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisCopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8_m8A5B65BF262A2B1C988311D13733E08E197C8A03(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mB4866E67F294F9391FCC04089D39530ED81DF25A (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA ReadOnlySpan_1_Slice_m6DAB57C8973069754B0BCCBBC08567450597296D (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_2 = __this->____pointer;
		V_0 = L_2;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_5;
		L_5 = il2cpp_unsafe_add<CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8,int32_t>(L_3, L_4, sizeof(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m75A7463F5588DB7F45AC0CE204FFB188419EC066_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA ReadOnlySpan_1_Slice_m0521253D68232A086AA557E84BAB2AB444DB310D (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_5 = __this->____pointer;
		V_0 = L_5;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_8;
		L_8 = il2cpp_unsafe_add<CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8,int32_t>(L_6, L_7, sizeof(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m75A7463F5588DB7F45AC0CE204FFB188419EC066_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* ReadOnlySpan_1_ToArray_m96F8D688EFD1CF2C71600493BE7CC67A8A3018BE (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* L_1;
		L_1 = Array_Empty_TisCopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8_m636107AFEDC2888AAD9A190C5FCB115D4483F86F_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* L_3 = (CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5*)(CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_6;
		L_6 = il2cpp_unsafe_as_ref<CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8>(L_5);
		ByReference_1_tD6252C8872F720FDE0325E91E9F49CC9AA1059FB L_7 = __this->____pointer;
		V_0 = L_7;
		CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(CopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisCopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8_m8A5B65BF262A2B1C988311D13733E08E197C8A03(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_mCD19C469FAA1ECD1F15F1BD193AA554969E25A2E (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA ___0_left, ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA L_0 = ___0_left;
		ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m80E264A2D14E2F539A355539BA0FCA443D47DF41(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mDC00F904D02CA0104AE80CBDD9AB8AA0CFA850A5 (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_mFAF4CF4DA5577E6A03EC52FE1FFA6BE673216DEA (ReadOnlySpan_1_tD576C6727C2A9E78508B2FE71F72440F869DF5CA* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m633345025A44899B1D6B3B67DDC6E032D4E4B3B1 (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_12;
		L_12 = il2cpp_unsafe_as_ref<CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0>(L_11);
		int32_t L_13 = ___1_start;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_14;
		L_14 = il2cpp_unsafe_add<CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0,int32_t>(L_12, L_13, sizeof(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0));
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m5A5B3C790AD4E42DC6EDEA48B0D29E47501DC3C6 (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_2;
		L_2 = il2cpp_unsafe_as_ref<CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0>((uint8_t*)L_1);
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* ReadOnlySpan_1_get_Item_m6BDFC2A4D86CDB0F646B301A24BC419A669B9704 (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_2 = __this->____pointer;
		V_0 = L_2;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_5;
		L_5 = il2cpp_unsafe_add<CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0,int32_t>(L_3, L_4, sizeof(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mD48E9C4252FC88CDF405723646409C61631CFDCD (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, Span_1_tC85B5D30CDD48BE32D9DF528934ED5AEC5BFC116 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m385F87A001B432F97C5381EF1F01B485642ED27D_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tC85B5D30CDD48BE32D9DF528934ED5AEC5BFC116 L_2 = ___0_destination;
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_3 = L_2.____pointer;
		V_0 = L_3;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0, (Il2CppByReference*)(&V_0));
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_5 = __this->____pointer;
		V_0 = L_5;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisCullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0_m9A4B5780CB8BC1ACD63C193EC68D5E217CDBAC80(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mAF5D52A05AB954458A23F5ACD5E897042AFA2D3B (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, Span_1_tC85B5D30CDD48BE32D9DF528934ED5AEC5BFC116 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m385F87A001B432F97C5381EF1F01B485642ED27D_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tC85B5D30CDD48BE32D9DF528934ED5AEC5BFC116 L_2 = ___0_destination;
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_3 = L_2.____pointer;
		V_1 = L_3;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0, (Il2CppByReference*)(&V_1));
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_5 = __this->____pointer;
		V_1 = L_5;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisCullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0_m9A4B5780CB8BC1ACD63C193EC68D5E217CDBAC80(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mF9E6320B55655799993BE5B2BB07BADC41E61413 (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF ReadOnlySpan_1_Slice_m61BB46FA0EDADBD25CF22190712072857D29AC46 (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_2 = __this->____pointer;
		V_0 = L_2;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_5;
		L_5 = il2cpp_unsafe_add<CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0,int32_t>(L_3, L_4, sizeof(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m7515BCC9EAB305B335A968E684B597A1FC9A5F24_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF ReadOnlySpan_1_Slice_m2CEB62730124955C5F8EEFDF48A6E757EEB7BCA5 (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_5 = __this->____pointer;
		V_0 = L_5;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_8;
		L_8 = il2cpp_unsafe_add<CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0,int32_t>(L_6, L_7, sizeof(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m7515BCC9EAB305B335A968E684B597A1FC9A5F24_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* ReadOnlySpan_1_ToArray_m1BBC6D488C602F6741A91874B1FE889B2ADC0CAB (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* L_1;
		L_1 = Array_Empty_TisCullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0_m46787E1D6E481E4790F1F9E63000A067C9AF33CD_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* L_3 = (CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61*)(CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_6;
		L_6 = il2cpp_unsafe_as_ref<CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0>(L_5);
		ByReference_1_t81CF4D69552C11045F619B7C65392B2FB3646E25 L_7 = __this->____pointer;
		V_0 = L_7;
		CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(CullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisCullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0_m9A4B5780CB8BC1ACD63C193EC68D5E217CDBAC80(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m8F5E7BD359C4A2F109B65309B438C7234A617728 (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF ___0_left, ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF L_0 = ___0_left;
		ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m26E754ACC25871CADAADB0E10FEFBC9C47C4B94B(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m963C6BC056461AE1848F94FBAD5F8686C64C821D (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m2CB104DD7CC333E1E68A04D1DEC91802B8B7E257 (ReadOnlySpan_1_t7918A88B9E042EA0B873C90C85D2A8B97CB11BDF* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m5D86807E27A6B7E56D9C9AB27C7FD4AE01220E8A (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_12;
		L_12 = il2cpp_unsafe_as_ref<DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8>(L_11);
		int32_t L_13 = ___1_start;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_14;
		L_14 = il2cpp_unsafe_add<DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8,int32_t>(L_12, L_13, sizeof(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8));
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m543E9D2858975F4A41CEDFBE39BF05FFA2691782 (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_2;
		L_2 = il2cpp_unsafe_as_ref<DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8>((uint8_t*)L_1);
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* ReadOnlySpan_1_get_Item_mEF489930F18BF0BEDBC3A3172AC6736DCA8AE929 (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_2 = __this->____pointer;
		V_0 = L_2;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_5;
		L_5 = il2cpp_unsafe_add<DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8,int32_t>(L_3, L_4, sizeof(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mC8357ED7913713D78F0B6453E610B5F96D1D4B57 (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, Span_1_tAD7D80F571B6C63E604288FC6952D4E8B93EB381 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m9D56AB26DA029B854B802F9CC7B8E6E02EA21313_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tAD7D80F571B6C63E604288FC6952D4E8B93EB381 L_2 = ___0_destination;
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_3 = L_2.____pointer;
		V_0 = L_3;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8, (Il2CppByReference*)(&V_0));
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_5 = __this->____pointer;
		V_0 = L_5;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisDistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_mDC37E2EA7AF7BC4F29B1BBEC90A762EB26B760F5(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m8B1449BD7EE037FD5DEAE92055493EBED4D0D6A6 (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, Span_1_tAD7D80F571B6C63E604288FC6952D4E8B93EB381 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m9D56AB26DA029B854B802F9CC7B8E6E02EA21313_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tAD7D80F571B6C63E604288FC6952D4E8B93EB381 L_2 = ___0_destination;
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_3 = L_2.____pointer;
		V_1 = L_3;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8, (Il2CppByReference*)(&V_1));
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_5 = __this->____pointer;
		V_1 = L_5;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisDistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_mDC37E2EA7AF7BC4F29B1BBEC90A762EB26B760F5(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m5CFC5F6E65A0D06000DBA329F43D6EB3FCFD8827 (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A ReadOnlySpan_1_Slice_mB9A0B1AA407DBE43219A7F50EEFD2994B28D419C (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_2 = __this->____pointer;
		V_0 = L_2;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_5;
		L_5 = il2cpp_unsafe_add<DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8,int32_t>(L_3, L_4, sizeof(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m3171966ED88C89D4F7ED5107E056F3CBED6E8194_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A ReadOnlySpan_1_Slice_mB727F3BEA918BC5ED6C4B9E45198D9FCDAFD5A72 (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_5 = __this->____pointer;
		V_0 = L_5;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_8;
		L_8 = il2cpp_unsafe_add<DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8,int32_t>(L_6, L_7, sizeof(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m3171966ED88C89D4F7ED5107E056F3CBED6E8194_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* ReadOnlySpan_1_ToArray_m479BB637A1604B9906219306C2DB637141F54D75 (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* L_1;
		L_1 = Array_Empty_TisDistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_m7807FD2713C36285071ABDD0A617C0D77C6C1EB7_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* L_3 = (DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0*)(DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_6;
		L_6 = il2cpp_unsafe_as_ref<DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8>(L_5);
		ByReference_1_t78E10326049F2329477C3D516A55058379A36A6B L_7 = __this->____pointer;
		V_0 = L_7;
		DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(DistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisDistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_mDC37E2EA7AF7BC4F29B1BBEC90A762EB26B760F5(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m287435DE51819CBBCC9809BBE63D2528A1A7B533 (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A ___0_left, ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A L_0 = ___0_left;
		ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m11E5D65C3EBFEECBEED20DAF22DE8F003EEAF13B(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m2222DDC4B8FB7349CEEF3A41B3CD787AD109113E (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m0B72739918C1064B00D5050D5E54031169B5DB7F (ReadOnlySpan_1_tC12212EEF57DA164E6E8114CA45A68C63D16B72A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m209F73E03C4F82E43A9141254DE8DC27F13617CF (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_12;
		L_12 = il2cpp_unsafe_as_ref<DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5>(L_11);
		int32_t L_13 = ___1_start;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_14;
		L_14 = il2cpp_unsafe_add<DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5,int32_t>(L_12, L_13, sizeof(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5));
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mD98591E8F5A6E535D39AA58FCE41E4B27182E810 (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_2;
		L_2 = il2cpp_unsafe_as_ref<DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5>((uint8_t*)L_1);
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* ReadOnlySpan_1_get_Item_mE7EDF836F0E328D51C4004167476A50FFC2F9A22 (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_2 = __this->____pointer;
		V_0 = L_2;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_5;
		L_5 = il2cpp_unsafe_add<DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5,int32_t>(L_3, L_4, sizeof(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m5D653490DA3BDBB9D2DDE6AFF621140F80AAF0A0 (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, Span_1_tFCA7D5D0BF7401ABF1CDA871A9259927808E7F73 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mFF4419EF107B2F1CD3364357F9723EAC96433F03_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tFCA7D5D0BF7401ABF1CDA871A9259927808E7F73 L_2 = ___0_destination;
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_3 = L_2.____pointer;
		V_0 = L_3;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5, (Il2CppByReference*)(&V_0));
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_5 = __this->____pointer;
		V_0 = L_5;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisDrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5_mDF1CDB2973A50B932ED2E72CC7A57E75C7FCAB54(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m245CACE211DD4EE664BC4144ABCDC0F4BF1ADA9D (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, Span_1_tFCA7D5D0BF7401ABF1CDA871A9259927808E7F73 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mFF4419EF107B2F1CD3364357F9723EAC96433F03_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tFCA7D5D0BF7401ABF1CDA871A9259927808E7F73 L_2 = ___0_destination;
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_3 = L_2.____pointer;
		V_1 = L_3;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5, (Il2CppByReference*)(&V_1));
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_5 = __this->____pointer;
		V_1 = L_5;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisDrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5_mDF1CDB2973A50B932ED2E72CC7A57E75C7FCAB54(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m54BCA8931688B791BF303FC265317FFAB141290A (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B ReadOnlySpan_1_Slice_m8038527A50F3B7299802F8C1F4EF7F369E10BD4A (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_2 = __this->____pointer;
		V_0 = L_2;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_5;
		L_5 = il2cpp_unsafe_add<DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5,int32_t>(L_3, L_4, sizeof(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_mB6153B16E29314D6497048CA9F509CF21C277F71_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B ReadOnlySpan_1_Slice_mEF5B48D3930A1FEF921A8A8C0E032E95E65BE565 (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_5 = __this->____pointer;
		V_0 = L_5;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_8;
		L_8 = il2cpp_unsafe_add<DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5,int32_t>(L_6, L_7, sizeof(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_mB6153B16E29314D6497048CA9F509CF21C277F71_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* ReadOnlySpan_1_ToArray_m80BEE12A782B14B3A3E5ECF46321AD376CE13235 (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* L_1;
		L_1 = Array_Empty_TisDrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5_m05A4DA3A4ACD6D784909779870EE466DF7778EC7_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* L_3 = (DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB*)(DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_6;
		L_6 = il2cpp_unsafe_as_ref<DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5>(L_5);
		ByReference_1_t4E075C50F1605FECDAC4938F5DEBE21F2A040718 L_7 = __this->____pointer;
		V_0 = L_7;
		DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(DrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisDrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5_mDF1CDB2973A50B932ED2E72CC7A57E75C7FCAB54(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m5644C567B641FCB33941438C16910390FD8C3521 (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B ___0_left, ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B L_0 = ___0_left;
		ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mE3EC2F33B96AEACAFC2FAF883F633DF80BC23419(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m08F876928AAF55745A5F1EB5E1E1338B8531D456 (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_mEFDAA9C6A685091B275A8AE7D850F13B727034B7 (ReadOnlySpan_1_tB185515663CA8B149175531C2C9804EB48DDEC9B* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m2FF722F394612C0C9382C90474C88F2416F8AE2D (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_12;
		L_12 = il2cpp_unsafe_as_ref<EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4>(L_11);
		int32_t L_13 = ___1_start;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_14;
		L_14 = il2cpp_unsafe_add<EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4,int32_t>(L_12, L_13, sizeof(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4));
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mCF7E7ABBD5E04272C99D8741B8E86FC8AB8B529F (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_2;
		L_2 = il2cpp_unsafe_as_ref<EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4>((uint8_t*)L_1);
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* ReadOnlySpan_1_get_Item_m42DB9EE448D34AB4AE40E87D586F57A9132879E3 (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_2 = __this->____pointer;
		V_0 = L_2;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_5;
		L_5 = il2cpp_unsafe_add<EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4,int32_t>(L_3, L_4, sizeof(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mE6EC0D3CEBB9C6B47EEF8DF277CE1221324F9B40 (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, Span_1_t43234050271CE7DE614C13A0021A344861FB5593 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m43418E7D1C7802E4F31E08DAE3AD5C88BAACB75F_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t43234050271CE7DE614C13A0021A344861FB5593 L_2 = ___0_destination;
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_3 = L_2.____pointer;
		V_0 = L_3;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4, (Il2CppByReference*)(&V_0));
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_5 = __this->____pointer;
		V_0 = L_5;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisEasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4_m0343B8FCB3868B60552322D0BFD9C87585A924B1(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mCCA73F5E55936F36E10543E38C80859BA59B89CD (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, Span_1_t43234050271CE7DE614C13A0021A344861FB5593 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m43418E7D1C7802E4F31E08DAE3AD5C88BAACB75F_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t43234050271CE7DE614C13A0021A344861FB5593 L_2 = ___0_destination;
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_3 = L_2.____pointer;
		V_1 = L_3;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4, (Il2CppByReference*)(&V_1));
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_5 = __this->____pointer;
		V_1 = L_5;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisEasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4_m0343B8FCB3868B60552322D0BFD9C87585A924B1(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m412E02D15DE4E1BF997A59700BD5B46513E3E3A1 (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 ReadOnlySpan_1_Slice_mD199EE1CFA9B18A60C8655AF7AB2147D9637EF62 (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_2 = __this->____pointer;
		V_0 = L_2;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_5;
		L_5 = il2cpp_unsafe_add<EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4,int32_t>(L_3, L_4, sizeof(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_mF80BB7536BFA47CBB0B535E725155286821D1B34_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 ReadOnlySpan_1_Slice_m5989BEC5722C494B930E33DE8B0E6F33EE37F315 (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_5 = __this->____pointer;
		V_0 = L_5;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_8;
		L_8 = il2cpp_unsafe_add<EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4,int32_t>(L_6, L_7, sizeof(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_mF80BB7536BFA47CBB0B535E725155286821D1B34_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* ReadOnlySpan_1_ToArray_m485A8243005E55EC8B50969DED3BB39B7B0AAA58 (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* L_1;
		L_1 = Array_Empty_TisEasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4_m5BB1B3F942E717EB0EEA932DE146F79E347604BA_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* L_3 = (EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49*)(EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_6;
		L_6 = il2cpp_unsafe_as_ref<EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4>(L_5);
		ByReference_1_tA7F58D709342B3D9D7560427019EB2E4432E1EDA L_7 = __this->____pointer;
		V_0 = L_7;
		EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(EasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisEasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4_m0343B8FCB3868B60552322D0BFD9C87585A924B1(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m35EF104C7AC1D26A3119F3C09418E038C230FEED (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 ___0_left, ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 L_0 = ___0_left;
		ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m049B1C57C88411BB8B33BBA73450C898DD25013F(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m3A207D208958A65EFC450E4E2690105FAE6D7221 (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m02EA2775837B0A3916D64D65560FD2794901A63D (ReadOnlySpan_1_t56EB56BD24E849B1A28025A5D541442A2AB282E7* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mD4098D65E6BBC2BA630A548C16B84B48EDD8A3BF (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_12;
		L_12 = il2cpp_unsafe_as_ref<EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B>(L_11);
		int32_t L_13 = ___1_start;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_14;
		L_14 = il2cpp_unsafe_add<EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B,int32_t>(L_12, L_13, sizeof(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B));
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mCEE03EA1C21BB24DC73A46B465EE1D04E3B7D22B (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_2;
		L_2 = il2cpp_unsafe_as_ref<EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B>((uint8_t*)L_1);
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* ReadOnlySpan_1_get_Item_m7A125B067FB443CD8FDC8566371E0447F3C6138F (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_2 = __this->____pointer;
		V_0 = L_2;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_5;
		L_5 = il2cpp_unsafe_add<EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B,int32_t>(L_3, L_4, sizeof(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m7C4233C71F8E179C8722EE5CDD5454860E423291 (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, Span_1_t6834048668549DB2E233F0E7E8E71F1E3AFDE712 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m7DA42A78AA51399F6E2624EC951C4C5675A83DF4_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t6834048668549DB2E233F0E7E8E71F1E3AFDE712 L_2 = ___0_destination;
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_3 = L_2.____pointer;
		V_0 = L_3;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B, (Il2CppByReference*)(&V_0));
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_5 = __this->____pointer;
		V_0 = L_5;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisEmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B_mDFAF68DAF7C667016B41A5C085293991850D1D8C(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m8F8A70A092F5A159FB14261A13A7C5FBB5787F82 (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, Span_1_t6834048668549DB2E233F0E7E8E71F1E3AFDE712 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m7DA42A78AA51399F6E2624EC951C4C5675A83DF4_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t6834048668549DB2E233F0E7E8E71F1E3AFDE712 L_2 = ___0_destination;
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_3 = L_2.____pointer;
		V_1 = L_3;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B, (Il2CppByReference*)(&V_1));
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_5 = __this->____pointer;
		V_1 = L_5;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisEmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B_mDFAF68DAF7C667016B41A5C085293991850D1D8C(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m6A8FE05FEFB7E4D9F39AD377F7D38CE0683DE7B2 (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 ReadOnlySpan_1_Slice_mE6F7FEF5269F67616751BEBF8BAFF8C1F5857BDC (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_2 = __this->____pointer;
		V_0 = L_2;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_5;
		L_5 = il2cpp_unsafe_add<EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B,int32_t>(L_3, L_4, sizeof(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m47891B0EBECA0C6CBF5505295F2B0E603027A014_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 ReadOnlySpan_1_Slice_m3F56144D4C2C592B43D78C7B42F73A48A9E5177C (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_5 = __this->____pointer;
		V_0 = L_5;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_8;
		L_8 = il2cpp_unsafe_add<EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B,int32_t>(L_6, L_7, sizeof(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m47891B0EBECA0C6CBF5505295F2B0E603027A014_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* ReadOnlySpan_1_ToArray_mAEEC9EB01051A837B254B70729B5B1B1F336EDB8 (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* L_1;
		L_1 = Array_Empty_TisEmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B_m87CA399E64D82A02BCE81DF2F8F52AD65AC9BC25_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* L_3 = (EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1*)(EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_6;
		L_6 = il2cpp_unsafe_as_ref<EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B>(L_5);
		ByReference_1_tCB250A9F3C97CE952291FE2EB00C93049947EEF5 L_7 = __this->____pointer;
		V_0 = L_7;
		EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(EmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisEmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B_mDFAF68DAF7C667016B41A5C085293991850D1D8C(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m7CAE4183685BDA8C8842624970354F4D5BE1995F (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 ___0_left, ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 L_0 = ___0_left;
		ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m039886C6A427B52C591AA65AEA91EED22AB2F907(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mA28D1F03418262EBD1FB9594CA7FB7FD70610212 (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m1E0EF80A7EE77059FD9DAA68BD8D6790279AE80E (ReadOnlySpan_1_t0FC2D16AF841FEB9AE14EC7F2D86B77F0D003744* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mB0FC236D9E3748441AD25DDC3B692EA51BA6233A (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_12;
		L_12 = il2cpp_unsafe_as_ref<EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8>(L_11);
		int32_t L_13 = ___1_start;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_14;
		L_14 = il2cpp_unsafe_add<EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8,int32_t>(L_12, L_13, sizeof(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8));
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m0852AA20C81ECA081FB83A9EDDB3F1E66794F2BC (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_2;
		L_2 = il2cpp_unsafe_as_ref<EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8>((uint8_t*)L_1);
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* ReadOnlySpan_1_get_Item_m6FF93DA90032CD75D75A2B9413FC9C7C989EEC19 (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_2 = __this->____pointer;
		V_0 = L_2;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_5;
		L_5 = il2cpp_unsafe_add<EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8,int32_t>(L_3, L_4, sizeof(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m950DB44591C9EDF481214225C635C41C9D6C4822 (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, Span_1_t5D7E7A039DDB174888A2AC5552AF592E3B641F6A ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mF40B0389DD242EC35D44F25C7490012314A18568_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t5D7E7A039DDB174888A2AC5552AF592E3B641F6A L_2 = ___0_destination;
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_3 = L_2.____pointer;
		V_0 = L_3;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8, (Il2CppByReference*)(&V_0));
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_5 = __this->____pointer;
		V_0 = L_5;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisEntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8_m8D3BF3FF1317BAF44B55A14301469F3AB0E60900(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mE28997B1566C7D16DA39612A5B96C77D8729F19F (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, Span_1_t5D7E7A039DDB174888A2AC5552AF592E3B641F6A ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mF40B0389DD242EC35D44F25C7490012314A18568_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t5D7E7A039DDB174888A2AC5552AF592E3B641F6A L_2 = ___0_destination;
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_3 = L_2.____pointer;
		V_1 = L_3;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8, (Il2CppByReference*)(&V_1));
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_5 = __this->____pointer;
		V_1 = L_5;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisEntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8_m8D3BF3FF1317BAF44B55A14301469F3AB0E60900(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m39E431536CAFB0586A83817A0D8934F342A77F48 (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 ReadOnlySpan_1_Slice_mF70146AF37D44B84C4A0A11375621CC608C0B05E (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_2 = __this->____pointer;
		V_0 = L_2;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_5;
		L_5 = il2cpp_unsafe_add<EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8,int32_t>(L_3, L_4, sizeof(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m1F035A6038B22EE2B7A2E6000536EFB888AC4043_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 ReadOnlySpan_1_Slice_m99E30EBEBE3124057493D4338CB1EB24AE74C5C8 (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_5 = __this->____pointer;
		V_0 = L_5;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_8;
		L_8 = il2cpp_unsafe_add<EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8,int32_t>(L_6, L_7, sizeof(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m1F035A6038B22EE2B7A2E6000536EFB888AC4043_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* ReadOnlySpan_1_ToArray_mEEDDCFF19B7FF3D3231361BBE06198D8612EA390 (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* L_1;
		L_1 = Array_Empty_TisEntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8_m8EF4F258FBF5AEC804ED891772BFFC84A01DB5D9_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* L_3 = (EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9*)(EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_6;
		L_6 = il2cpp_unsafe_as_ref<EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8>(L_5);
		ByReference_1_tA1EDA2A801CDA440B5FACA1FE234FE951748D2FF L_7 = __this->____pointer;
		V_0 = L_7;
		EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(EntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisEntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8_m8D3BF3FF1317BAF44B55A14301469F3AB0E60900(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m6B0288B6C51C7CE7E8D21200E8A4365DA7B3A5BE (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 ___0_left, ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 L_0 = ___0_left;
		ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m0064C72F0B13F04420687FB7276738A0B0DFFB3A(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m2773D8208EA7C89CB595446E96CE633046F24AEB (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m302AC96825FFAAED523A298F89F52C7973BF4303 (ReadOnlySpan_1_t48362DFB63F63089AE9628BE8FD9AAEE1B48D686* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m861A004ADC4E206F950124A879DB0F0551831A07 (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_12;
		L_12 = il2cpp_unsafe_as_ref<FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060>(L_11);
		int32_t L_13 = ___1_start;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_14;
		L_14 = il2cpp_unsafe_add<FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060,int32_t>(L_12, L_13, sizeof(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060));
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mF496B198D389067B7F239EF7716257B3228E4467 (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_2;
		L_2 = il2cpp_unsafe_as_ref<FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060>((uint8_t*)L_1);
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* ReadOnlySpan_1_get_Item_mBA9C1F7B7B9AB4539E9CC87CF4AD1711E5F8A98A (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_2 = __this->____pointer;
		V_0 = L_2;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_5;
		L_5 = il2cpp_unsafe_add<FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060,int32_t>(L_3, L_4, sizeof(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mD75A6DE532250846E959AB86A0CCFD13DB49EB5F (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, Span_1_t420A830887305E0E02A744003AACBACED16172EB ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m89645090890B982B6625634865741039212A2D7D_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_t420A830887305E0E02A744003AACBACED16172EB L_2 = ___0_destination;
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_3 = L_2.____pointer;
		V_0 = L_3;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060, (Il2CppByReference*)(&V_0));
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_5 = __this->____pointer;
		V_0 = L_5;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisFilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060_mDD7300E8E4A6CFCCD3B437359FAAA469263822D0(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_mC0FCD6A49D97B1CA7977E753C61E4C42E25533CA (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, Span_1_t420A830887305E0E02A744003AACBACED16172EB ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m89645090890B982B6625634865741039212A2D7D_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_t420A830887305E0E02A744003AACBACED16172EB L_2 = ___0_destination;
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_3 = L_2.____pointer;
		V_1 = L_3;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060, (Il2CppByReference*)(&V_1));
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_5 = __this->____pointer;
		V_1 = L_5;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisFilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060_mDD7300E8E4A6CFCCD3B437359FAAA469263822D0(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m7E3FCD13544821C35B8A0AFAE0A27D70F69AE5F5 (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 ReadOnlySpan_1_Slice_m12C38A9EDC24DFEEDF4489D7381EB0C2EE759B3A (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_2 = __this->____pointer;
		V_0 = L_2;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_5;
		L_5 = il2cpp_unsafe_add<FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060,int32_t>(L_3, L_4, sizeof(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m25FED3B4C89777BD3B6B73F981D9CF573F5C3435_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 ReadOnlySpan_1_Slice_m1288D0DB0AFF5016B60F7D6BDB99E633185007D4 (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_5 = __this->____pointer;
		V_0 = L_5;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_8;
		L_8 = il2cpp_unsafe_add<FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060,int32_t>(L_6, L_7, sizeof(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m25FED3B4C89777BD3B6B73F981D9CF573F5C3435_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* ReadOnlySpan_1_ToArray_mEB0D2B4FAFDD5692939C9E1F8CD7A0BDE224A6B2 (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* L_1;
		L_1 = Array_Empty_TisFilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060_mA4E39A2097C6E2511B93A1BF0C9221165B65938C_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* L_3 = (FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58*)(FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_6;
		L_6 = il2cpp_unsafe_as_ref<FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060>(L_5);
		ByReference_1_tCBFED671C53B2B6095EBEF9264D493D000CFD8BB L_7 = __this->____pointer;
		V_0 = L_7;
		FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(FilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisFilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060_mDD7300E8E4A6CFCCD3B437359FAAA469263822D0(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_mB1CC0C17E261B1D43AAB86A96023F2A92ABCDADB (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 ___0_left, ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 L_0 = ___0_left;
		ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mFCC221F4B2884F75CC518EB825129A4DEE06CE76(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mD948D784B1BDA890E13B4FF06594CFB443347BA2 (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m5834E9AA2796A5F41B37C8984A34510F97CE8EA0 (ReadOnlySpan_1_tDEA15065D44CF10909D8352C30DA4AEDE24E6A27* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m4C511E2F136A548ECFBCA294BA8FD85BD355D99D (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_12;
		L_12 = il2cpp_unsafe_as_ref<FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9>(L_11);
		int32_t L_13 = ___1_start;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_14;
		L_14 = il2cpp_unsafe_add<FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9,int32_t>(L_12, L_13, sizeof(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9));
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m1C8AC4EE46F78E5DF857EB1D8AF48D8C0631230A (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_2;
		L_2 = il2cpp_unsafe_as_ref<FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9>((uint8_t*)L_1);
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* ReadOnlySpan_1_get_Item_m1962D06EEBD19BCF0642E8BEE47A9FFEA7396891 (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_2 = __this->____pointer;
		V_0 = L_2;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_5;
		L_5 = il2cpp_unsafe_add<FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9,int32_t>(L_3, L_4, sizeof(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_m46C011590C615EE1995C1FD8BCFA812A9EC74747 (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, Span_1_tD4A141E2CAE7DFB331921ECC10AB8884452E2784 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m3A8F09C0012040ACB55950D35820310E9F94B6E6_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tD4A141E2CAE7DFB331921ECC10AB8884452E2784 L_2 = ___0_destination;
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_3 = L_2.____pointer;
		V_0 = L_3;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9, (Il2CppByReference*)(&V_0));
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_5 = __this->____pointer;
		V_0 = L_5;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisFlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9_m6C3DAA8091937F2A2CBA773FD3BAA3DCD1C9B2EF(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m8234EBF1F8DF0994005B3DBFB32B055AE2E248FC (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, Span_1_tD4A141E2CAE7DFB331921ECC10AB8884452E2784 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m3A8F09C0012040ACB55950D35820310E9F94B6E6_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tD4A141E2CAE7DFB331921ECC10AB8884452E2784 L_2 = ___0_destination;
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_3 = L_2.____pointer;
		V_1 = L_3;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9, (Il2CppByReference*)(&V_1));
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_5 = __this->____pointer;
		V_1 = L_5;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisFlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9_m6C3DAA8091937F2A2CBA773FD3BAA3DCD1C9B2EF(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m4AFB35884B095AA96A1C155C6502E62B59655B41 (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A ReadOnlySpan_1_Slice_mCFBC3E42C951155CD5E45BA0B7541E0D2E9D83A7 (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_2 = __this->____pointer;
		V_0 = L_2;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_5;
		L_5 = il2cpp_unsafe_add<FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9,int32_t>(L_3, L_4, sizeof(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_mBBF4ED998EFF74C1D72698CA0007CD8300A88835_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A ReadOnlySpan_1_Slice_m5D2590C10646897DD26604BAF3D589DBEC85B33C (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_5 = __this->____pointer;
		V_0 = L_5;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_8;
		L_8 = il2cpp_unsafe_add<FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9,int32_t>(L_6, L_7, sizeof(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_mBBF4ED998EFF74C1D72698CA0007CD8300A88835_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* ReadOnlySpan_1_ToArray_mDFDBB2EF9FEBD670B86E618BEC9537E74E8A76A4 (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* L_1;
		L_1 = Array_Empty_TisFlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9_m7E0303803800EFD09E46C64192C01A6D011D4260_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* L_3 = (FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27*)(FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_6;
		L_6 = il2cpp_unsafe_as_ref<FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9>(L_5);
		ByReference_1_tA39086CECC388213B09BE44EA41243514E812960 L_7 = __this->____pointer;
		V_0 = L_7;
		FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisFlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9_m6C3DAA8091937F2A2CBA773FD3BAA3DCD1C9B2EF(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m3A775C9A3EF150BED6A98801F48B8814D26FDEB3 (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A ___0_left, ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A L_0 = ___0_left;
		ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m377EF66B9E4E4BD87483BC46AF730DD83D4917D1(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_m64CD0FBD582742DCAA58498AF9AEE78A8A4EB1E3 (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m76CBA0D4B9697DA47003F7E57EA5DB0FC3E5DA76 (ReadOnlySpan_1_tFAD7BABA71EFEFA7202D4FBD768E4D891E34851A* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mE9C7349911188719FE72173855A83927B6668ECF (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_12;
		L_12 = il2cpp_unsafe_as_ref<FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986>(L_11);
		int32_t L_13 = ___1_start;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_14;
		L_14 = il2cpp_unsafe_add<FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986,int32_t>(L_12, L_13, sizeof(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986));
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mEA2F30DD68B20840FE78AA2B0AEA1063123B2DCD (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_2;
		L_2 = il2cpp_unsafe_as_ref<FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986>((uint8_t*)L_1);
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* ReadOnlySpan_1_get_Item_m028394A5CD70B65EC6880CBB955D60389C8359E1 (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_2 = __this->____pointer;
		V_0 = L_2;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_5;
		L_5 = il2cpp_unsafe_add<FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986,int32_t>(L_3, L_4, sizeof(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mAB6909969542CD8A7AB76A2CF065ADE75E46BCE6 (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, Span_1_tF25812E0445523D140232909284012C24E110428 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m8CC502CE0D66420E8B9B92D8F233B020CF67E932_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tF25812E0445523D140232909284012C24E110428 L_2 = ___0_destination;
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_3 = L_2.____pointer;
		V_0 = L_3;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986, (Il2CppByReference*)(&V_0));
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_5 = __this->____pointer;
		V_0 = L_5;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisFlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986_m19C1CA978BDC4B53F03AF962E7E6296C9E5A5A33(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m794B8EAE8C0059EE70B7F7F7BEBD91EE20430743 (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, Span_1_tF25812E0445523D140232909284012C24E110428 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_m8CC502CE0D66420E8B9B92D8F233B020CF67E932_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tF25812E0445523D140232909284012C24E110428 L_2 = ___0_destination;
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_3 = L_2.____pointer;
		V_1 = L_3;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986, (Il2CppByReference*)(&V_1));
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_5 = __this->____pointer;
		V_1 = L_5;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisFlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986_m19C1CA978BDC4B53F03AF962E7E6296C9E5A5A33(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_m48FA3677332A10CF3FECEBA72A0FAA2D391CB2FB (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D ReadOnlySpan_1_Slice_m28858EF9216FB83BE8EE6797C9E30D55B9B8E2F4 (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_2 = __this->____pointer;
		V_0 = L_2;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_5;
		L_5 = il2cpp_unsafe_add<FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986,int32_t>(L_3, L_4, sizeof(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_mAD01EEA7AC8462087E2A66DC8852371D56D2B4DD_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D ReadOnlySpan_1_Slice_mFD956FF8C6FEF4BB32473AD0FF26A421DAAA38AF (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_5 = __this->____pointer;
		V_0 = L_5;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_8;
		L_8 = il2cpp_unsafe_add<FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986,int32_t>(L_6, L_7, sizeof(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_mAD01EEA7AC8462087E2A66DC8852371D56D2B4DD_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* ReadOnlySpan_1_ToArray_mF76C8CB545A98027197C664BAF9006B109BEEC06 (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* L_1;
		L_1 = Array_Empty_TisFlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986_m907AAB5CF7AEF4C510C711BBF98ABA5EA468D2B1_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* L_3 = (FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643*)(FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_6;
		L_6 = il2cpp_unsafe_as_ref<FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986>(L_5);
		ByReference_1_t0CF44D19E5248AED5E1176DD36BA980A15ED1036 L_7 = __this->____pointer;
		V_0 = L_7;
		FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(FlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisFlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986_m19C1CA978BDC4B53F03AF962E7E6296C9E5A5A33(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_m0710F52B9C3451D9E9F6F0357F2008D64AC23669 (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D ___0_left, ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D L_0 = ___0_left;
		ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_mEC6D213E2DE3F5E82725CD7FFB7EE813023463C0(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mCFAC5E57DBB983007E678029E278CDFBAFCF4E7F (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_m019C909AF701476FC6A263A2C7B7F9D2AA2F8466 (ReadOnlySpan_1_t07EF4373EB65B21BC4119917FB0DC0A16BBD338D* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 2384
// Method Definition Index: 2385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m1CC8B26430416B686554A6482D492EAEFEEF51F3 (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_12;
		L_12 = il2cpp_unsafe_as_ref<GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32>(L_11);
		int32_t L_13 = ___1_start;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_14;
		L_14 = il2cpp_unsafe_add<GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32,int32_t>(L_12, L_13, sizeof(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32));
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mBBD9B663DF23946ABDB0362F55E5460AF62636EE (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, void* ___0_pointer, int32_t ___1_length, const RuntimeMethod* method) 
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
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_2;
		L_2 = il2cpp_unsafe_as_ref<GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32>((uint8_t*)L_1);
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_3;
		memset((&L_3), 0, sizeof(L_3));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_3), L_2);
		__this->____pointer = L_3;
		int32_t L_4 = ___1_length;
		__this->____length = L_4;
		return;
	}
}
// Method Definition Index: 2387
// Method Definition Index: 2388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* ReadOnlySpan_1_get_Item_mE2698C4D6F000B32A207D2CF0944F5F3CF0AD921 (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, int32_t ___0_index, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_index;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) >= ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowIndexOutOfRangeException_m86F753A24E2765A35546BA6352A7E4F0BB8A66B5(NULL);
	}

IL_000e:
	{
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_2 = __this->____pointer;
		V_0 = L_2;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_index;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_5;
		L_5 = il2cpp_unsafe_add<GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32,int32_t>(L_3, L_4, sizeof(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32));
		return L_5;
	}
}
// Method Definition Index: 2389
// Method Definition Index: 2390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReadOnlySpan_1_CopyTo_mF0492FF80613669BE399A99FD0FB7C03C0B76E8C (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, Span_1_tBFB0B8ED6E219789174070F50BFF3D1D12DDADA4 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mE30066A0C4D7876DED68538384FA5CC160C69D64_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0038;
		}
	}
	{
		Span_1_tBFB0B8ED6E219789174070F50BFF3D1D12DDADA4 L_2 = ___0_destination;
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_3 = L_2.____pointer;
		V_0 = L_3;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32, (Il2CppByReference*)(&V_0));
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_5 = __this->____pointer;
		V_0 = L_5;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32, (Il2CppByReference*)(&V_0));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisGPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32_m83F98C10F75BBF5CBB456D86857C192E9B2D06B6(L_4, L_6, L_8, NULL);
		return;
	}

IL_0038:
	{
		ThrowHelper_ThrowArgumentException_DestinationTooShort_m6468934A3BBB67DBC5BAEF7A64D91BD5BBBB3D4D(NULL);
		return;
	}
}
// Method Definition Index: 2391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_TryCopyTo_m92A744C49631D9647E75ABABD41FD59EB3C1E621 (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, Span_1_tBFB0B8ED6E219789174070F50BFF3D1D12DDADA4 ___0_destination, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	bool V_0 = false;
	ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		V_0 = (bool)0;
		int32_t L_0 = __this->____length;
		int32_t L_1;
		L_1 = Span_1_get_Length_mE30066A0C4D7876DED68538384FA5CC160C69D64_inline((&___0_destination), NULL);
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_003b;
		}
	}
	{
		Span_1_tBFB0B8ED6E219789174070F50BFF3D1D12DDADA4 L_2 = ___0_destination;
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_3 = L_2.____pointer;
		V_1 = L_3;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_4;
		L_4 = IL2CPP_BY_REFERENCE_GET_VALUE(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32, (Il2CppByReference*)(&V_1));
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_5 = __this->____pointer;
		V_1 = L_5;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32, (Il2CppByReference*)(&V_1));
		int32_t L_7 = __this->____length;
		int64_t L_8 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_7,NULL));
		Buffer_Memmove_TisGPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32_m83F98C10F75BBF5CBB456D86857C192E9B2D06B6(L_4, L_6, L_8, NULL);
		V_0 = (bool)1;
	}

IL_003b:
	{
		bool L_9 = V_0;
		return L_9;
	}
}
// Method Definition Index: 2392
// Method Definition Index: 2393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* ReadOnlySpan_1_ToString_mBE9318B2249AD978B9F06C7107CB99FBB27F16FF (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D);
		s_Il2CppMethodInitialized = true;
	}
	CHECKED_LOCAL(classRgctxInit);
	CHECKED_LOCAL(Type_t_StaticInit);
	//<source_info:<no-source>:1>
	Il2CppChar* V_0 = NULL;
	ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 V_1;
	memset((&V_1), 0, sizeof(V_1));
	{
		goto IL_003e;
	}

IL_003e:
	{
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (il2cpp_rgctx_type(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 3)) };
		CHECKED_LOCAL_INIT(Type_t_StaticInit,(Type_t_il2cpp_TypeInfo_var),il2cpp_codegen_runtime_class_init_inline);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		String_t* L_2;
		L_2 = VirtualFuncInvoker0< String_t* >::Invoke(8, L_1);
		int32_t L_3 = __this->____length;
		int32_t L_4 = L_3;
		RuntimeObject* L_5 = Box(Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var, &L_4);
		String_t* L_6;
		L_6 = String_Format_mFB7DA489BD99F4670881FF50EC017BFB0A5C0987(_stringLiteral2DCA6040AD5A31C81EBE664FAFD10DAFB1B1CD5D, L_2, L_5, NULL);
		return L_6;
	}
}
// Method Definition Index: 2394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 ReadOnlySpan_1_Slice_m19B781349C743374B253C9C6D6B9CF0729157619 (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, int32_t ___0_start, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) > ((uint32_t)L_1))))
		{
			goto IL_000e;
		}
	}
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_2 = __this->____pointer;
		V_0 = L_2;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_3;
		L_3 = IL2CPP_BY_REFERENCE_GET_VALUE(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32, (Il2CppByReference*)(&V_0));
		int32_t L_4 = ___0_start;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_5;
		L_5 = il2cpp_unsafe_add<GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32,int32_t>(L_3, L_4, sizeof(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32));
		int32_t L_6 = __this->____length;
		int32_t L_7 = ___0_start;
		ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 L_8;
		memset((&L_8), 0, sizeof(L_8));
		ReadOnlySpan_1__ctor_m10A374622B0CBE7DF98D6204B69DB5AB8EBEDC48_inline((&L_8), L_5, ((int32_t)il2cpp_codegen_subtract(L_6, L_7)), NULL);
		return L_8;
	}
}
// Method Definition Index: 2395
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 ReadOnlySpan_1_Slice_m765EAD28F9EA0424C91BE5AFF53D864B23DEF250 (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_5 = __this->____pointer;
		V_0 = L_5;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_8;
		L_8 = il2cpp_unsafe_add<GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32,int32_t>(L_6, L_7, sizeof(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m10A374622B0CBE7DF98D6204B69DB5AB8EBEDC48_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2396
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* ReadOnlySpan_1_ToArray_mCA51B6748B301CAF804FAE141AB0EB24AD7301B9 (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = __this->____length;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* L_1;
		L_1 = Array_Empty_TisGPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32_m0C324743704C230D82B1D3F4A8A72DAECCD01F62_inline(il2cpp_rgctx_method(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 7));
		return L_1;
	}

IL_000e:
	{
		int32_t L_2 = __this->____length;
		GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* L_3 = (GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE*)(GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE*)SZArrayNew(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 8), (uint32_t)L_2);
		GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* L_4 = L_3;
		NullCheck((RuntimeArray*)L_4);
		uint8_t* L_5;
		L_5 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_4, NULL);
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_6;
		L_6 = il2cpp_unsafe_as_ref<GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32>(L_5);
		ByReference_1_tDCD87F57A922EB5D677F348193F5D9E910764BA6 L_7 = __this->____pointer;
		V_0 = L_7;
		GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32* L_8;
		L_8 = IL2CPP_BY_REFERENCE_GET_VALUE(GPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32, (Il2CppByReference*)(&V_0));
		int32_t L_9 = __this->____length;
		int64_t L_10 = (il2cpp_codegen_conv<int64_t,int32_t,int32_t,false,false>(L_9,NULL));
		Buffer_Memmove_TisGPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32_m83F98C10F75BBF5CBB456D86857C192E9B2D06B6(L_6, L_8, L_10, NULL);
		return L_4;
	}
}
// Method Definition Index: 2397
// Method Definition Index: 2398
// Method Definition Index: 2399
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_op_Inequality_mAF5514CA4488B4EC7414F3278D56A9FCA234FE2C (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 ___0_left, ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 ___1_right, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 L_0 = ___0_left;
		ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60 L_1 = ___1_right;
		bool L_2;
		L_2 = ReadOnlySpan_1_op_Equality_m1D1955D11B345171E9FF68FAD8B91958FB6F0653(L_0, L_1, NULL);
		return (bool)((((int32_t)L_2) == ((int32_t)0))? 1 : 0);
	}
}
// Method Definition Index: 2400
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ReadOnlySpan_1_Equals_mF1CCAC7D6B3F02FED4609FDF35F79281F912CEED (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral69508A540AFD085A745316DD7D6345B1C8CC662D)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2401
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ReadOnlySpan_1_GetHashCode_mEE92EAC535359994053B53C5675D8C885B32FD3D (ReadOnlySpan_1_t34988ADF8A190D8E783D3F6CEBB997ECB1625B60* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_0 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_0, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralECE618215BAC99C6FD12D8A273CC2118945EDCC8)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_0, method);
	}
}
// Method Definition Index: 2402
// Method Definition Index: 2403
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Method Definition Index: 709
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline (String_t* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		int32_t L_0 = __this->____stringLength;
		return L_0;
	}
}
// Method Definition Index: 44416
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void NotifyCollectionChangedEventHandler_Invoke_m8DED5705E365E59E2F6BDCBC6D6D762DA1ECA194_inline (NotifyCollectionChangedEventHandler_t521FADBD1190F88022BC2160A5F82FDFC08364AF* __this, RuntimeObject* ___0_sender, NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA* ___1_e, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, NotifyCollectionChangedEventArgs_tFF32515F3E2B116CAB376B5B57C6A8CB617351FA*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_sender, ___1_e, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 43902
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void PropertyChangedEventHandler_Invoke_m040B3056C279FDDC4EC678519D5D415771905298_inline (PropertyChangedEventHandler_tF1250C146BE6FB43477D29C5D18A10B6329419DA* __this, RuntimeObject* ___0_sender, PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321* ___1_e, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, PropertyChangedEventArgs_tFA01343D8F2BD799951B40A3EBF4A6B38EE63321*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_sender, ___1_e, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 3237
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR uint8_t* Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline (RuntimeArray* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RawData_t37CAF2D3F74B7723974ED7CEEE9B297D8FA64ED0* L_0;
		L_0 = il2cpp_unsafe_as<RawData_t37CAF2D3F74B7723974ED7CEEE9B297D8FA64ED0*>(__this);
		NullCheck(L_0);
		uint8_t* L_1 = (uint8_t*)(&L_0->___Data);
		return L_1;
	}
}
// Method Definition Index: 2491
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 Span_1_Slice_m9D8BA8245B8DC9BFB4A4164759CBAAEAD1318CD6_inline (Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_5 = __this->____pointer;
		V_0 = L_5;
		uint8_t* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(uint8_t, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		uint8_t* L_8;
		L_8 = il2cpp_unsafe_add<uint8_t,int32_t>(L_6, L_7, sizeof(uint8_t));
		int32_t L_9 = ___1_length;
		Span_1_tDADAC65069DFE6B57C458109115ECD795ED39305 L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_m947BF95D54571BF3897F96822B7A8FDA5853497B_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2385
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_m7B5C2765879EA5E8D1617D834CC465A39540A913_inline (ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tA850A6C0E88ABBA37646A078ACBC24D6D5FD9B4D));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		uint8_t* L_12;
		L_12 = il2cpp_unsafe_as_ref<uint8_t>(L_11);
		int32_t L_13 = ___1_start;
		uint8_t* L_14;
		L_14 = il2cpp_unsafe_add<uint8_t,int32_t>(L_12, L_13, sizeof(uint8_t));
		ByReference_1_t9C85BCCAAF8C525B6C06B07E922D8D217BE8D6FC L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2377
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ReadOnlyMemory_1_get_Span_m21E4F67DBB43CC122008ADE132AB9213723588EA_inline (ReadOnlyMemory_1_t9495785584BC99CFCF956B34621F038E83C257D6* __this, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	//<source_info:<no-source>:1>
	Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D V_0;
	memset((&V_0), 0, sizeof(V_0));
	String_t* V_1 = NULL;
	ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 V_2;
	memset((&V_2), 0, sizeof(V_2));
	{
		int32_t L_0 = __this->____index;
		if ((((int32_t)L_0) >= ((int32_t)0)))
		{
			goto IL_0039;
		}
	}
	{
		RuntimeObject* L_1 = __this->____object;
		NullCheck(((MemoryManager_1_t9E713A8E4F8473E685FE4A73C98D29C9DB1EE583*)CastclassClass((RuntimeObject*)L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))));
		Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D L_2;
		L_2 = VirtualFuncInvoker0< Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D >::Invoke(4, ((MemoryManager_1_t9E713A8E4F8473E685FE4A73C98D29C9DB1EE583*)CastclassClass((RuntimeObject*)L_1, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 4))));
		V_0 = L_2;
		int32_t L_3 = __this->____index;
		int32_t L_4 = __this->____length;
		Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D L_5;
		L_5 = Span_1_Slice_mEFBC3C78FD443FFE23F9E841D43B7B0271622843_inline((&V_0), ((int32_t)(L_3&((int32_t)2147483647LL))), L_4, NULL);
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_6;
		L_6 = Span_1_op_Implicit_m03D0CEDC1BC95844236105D1DE24A702B956BFE4(L_5, NULL);
		return L_6;
	}

IL_0039:
	{
	}
	{
		RuntimeObject* L_7 = __this->____object;
		V_1 = ((String_t*)IsInstSealed((RuntimeObject*)L_7, String_t_il2cpp_TypeInfo_var));
		String_t* L_8 = V_1;
		if (!L_8)
		{
			goto IL_008e;
		}
	}
	{
		String_t* L_9 = V_1;
		NullCheck(L_9);
		Il2CppChar* L_10;
		L_10 = String_GetRawStringData_m87BC50B7B314C055E27A28032D1003D42FDE411D(L_9, NULL);
		Il2CppChar* L_11;
		L_11 = il2cpp_unsafe_as_ref<Il2CppChar>(L_10);
		String_t* L_12 = V_1;
		NullCheck(L_12);
		int32_t L_13;
		L_13 = String_get_Length_m42625D67623FA5CC7A44D47425CE86FB946542D2_inline(L_12, NULL);
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_14;
		memset((&L_14), 0, sizeof(L_14));
		ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_inline((&L_14), L_11, L_13, NULL);
		V_2 = L_14;
		int32_t L_15 = __this->____index;
		int32_t L_16 = __this->____length;
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_17;
		L_17 = ReadOnlySpan_1_Slice_mDEEA4C304B13C8F7A63BC3D60B62FF17BBEE282B_inline((&V_2), L_15, L_16, NULL);
		return L_17;
	}

IL_008e:
	{
		RuntimeObject* L_18 = __this->____object;
		if (!L_18)
		{
			goto IL_00b9;
		}
	}
	{
		RuntimeObject* L_19 = __this->____object;
		int32_t L_20 = __this->____index;
		int32_t L_21 = __this->____length;
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_22;
		memset((&L_22), 0, sizeof(L_22));
		ReadOnlySpan_1__ctor_mDADE08E6B4594775C6675B389078E5FE98C745D5_inline((&L_22), ((CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB*)Castclass((RuntimeObject*)L_19, il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 9))), L_20, ((int32_t)(L_21&((int32_t)2147483647LL))), NULL);
		return L_22;
	}

IL_00b9:
	{
		il2cpp_codegen_initobj((&V_2), sizeof(ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1));
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_23 = V_2;
		return L_23;
	}
}
// Method Definition Index: 2491
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D Span_1_Slice_mEFBC3C78FD443FFE23F9E841D43B7B0271622843_inline (Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_5 = __this->____pointer;
		V_0 = L_5;
		Il2CppChar* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		Il2CppChar* L_8;
		L_8 = il2cpp_unsafe_add<Il2CppChar,int32_t>(L_6, L_7, sizeof(Il2CppChar));
		int32_t L_9 = ___1_length;
		Span_1_tEDDF15FCF9EC6DEBA0F696BAACDDBAB9D92C252D L_10;
		memset((&L_10), 0, sizeof(L_10));
		Span_1__ctor_mC9BE2938B716B46BB6B9070B94DBE5CE814BC0E2_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2387
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mFA6EE52BCF39100AE30C79E73F0F972182D0CA2A_fshared_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, Il2CppFullySharedGenericAny* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Il2CppFullySharedGenericAny* L_0 = ___0_ptr;
		ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 L_1;
		memset((&L_1), 0, sizeof(L_1));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_1), L_0);
		__this->____pointer = L_1;
		int32_t L_2 = ___1_length;
		__this->____length = L_2;
		return;
	}
}
// Method Definition Index: 2395
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 ReadOnlySpan_1_Slice_mDEEA4C304B13C8F7A63BC3D60B62FF17BBEE282B_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, int32_t ___0_start, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		int32_t L_0 = ___0_start;
		int32_t L_1 = __this->____length;
		if ((!(((uint32_t)L_0) <= ((uint32_t)L_1))))
		{
			goto IL_0014;
		}
	}
	{
		int32_t L_2 = ___1_length;
		int32_t L_3 = __this->____length;
		int32_t L_4 = ___0_start;
		if ((!(((uint32_t)L_2) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_3, L_4))))))
		{
			goto IL_0019;
		}
	}

IL_0014:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0019:
	{
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_5 = __this->____pointer;
		V_0 = L_5;
		Il2CppChar* L_6;
		L_6 = IL2CPP_BY_REFERENCE_GET_VALUE(Il2CppChar, (Il2CppByReference*)(&V_0));
		int32_t L_7 = ___0_start;
		Il2CppChar* L_8;
		L_8 = il2cpp_unsafe_add<Il2CppChar,int32_t>(L_6, L_7, sizeof(Il2CppChar));
		int32_t L_9 = ___1_length;
		ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1 L_10;
		memset((&L_10), 0, sizeof(L_10));
		ReadOnlySpan_1__ctor_m0152E50B40750679B83FF9F30CA539FFBB98EEE8_inline((&L_10), L_8, L_9, NULL);
		return L_10;
	}
}
// Method Definition Index: 2385
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mDADE08E6B4594775C6675B389078E5FE98C745D5_inline (ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1* __this, CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_t59614EA6E51A945A32B02AB17FBCBDF9A5C419C1));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		Il2CppChar* L_12;
		L_12 = il2cpp_unsafe_as_ref<Il2CppChar>(L_11);
		int32_t L_13 = ___1_start;
		Il2CppChar* L_14;
		L_14 = il2cpp_unsafe_add<Il2CppChar,int32_t>(L_12, L_13, sizeof(Il2CppChar));
		ByReference_1_t7BA5A6CA164F770BC688F21C5978D368716465F5 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 2385
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void ReadOnlySpan_1__ctor_mC9869776ABBFE9D2520512EEB39ABD1CFFE7F7B9_fshared_inline (ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC* __this, __Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* ___0_array, int32_t ___1_start, int32_t ___2_length, const RuntimeMethod* method) 
{
	CHECKED_LOCAL(classRgctxInit);
	const uint32_t SizeOf_T_t120950580BDFD368223E024446019DB239080837 = il2cpp_codegen_sizeof(il2cpp_rgctx_data(CHECKED_LOCAL_INIT_PARAM(classRgctxInit,(il2cpp_codegen_method_rgctx(method)),il2cpp_codegen_initialized_method_rgctx,(method)), 1));
	//<source_info:<no-source>:1>
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_0 = ___0_array;
		if (L_0)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_1 = ___1_start;
		if (L_1)
		{
			goto IL_0009;
		}
	}
	{
		int32_t L_2 = ___2_length;
		if (!L_2)
		{
			goto IL_000e;
		}
	}

IL_0009:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_000e:
	{
		il2cpp_codegen_initobj(__this, sizeof(ReadOnlySpan_1_tC416A5627E04F69CA2947A2A13F0A1DF096CABAC));
		return;
	}

IL_0016:
	{
		int32_t L_3 = ___1_start;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_4 = ___0_array;
		NullCheck(L_4);
		int32_t L_5 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_4)->max_length),NULL));
		if ((!(((uint32_t)L_3) <= ((uint32_t)L_5))))
		{
			goto IL_0024;
		}
	}
	{
		int32_t L_6 = ___2_length;
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_7 = ___0_array;
		NullCheck(L_7);
		int32_t L_8 = (il2cpp_codegen_conv<int32_t,int64_t,int64_t,false,false>((((RuntimeArray*)L_7)->max_length),NULL));
		int32_t L_9 = ___1_start;
		if ((!(((uint32_t)L_6) > ((uint32_t)((int32_t)il2cpp_codegen_subtract(L_8, L_9))))))
		{
			goto IL_0029;
		}
	}

IL_0024:
	{
		ThrowHelper_ThrowArgumentOutOfRangeException_mD7D90276EDCDF9394A8EA635923E3B48BB71BD56(NULL);
	}

IL_0029:
	{
		__Il2CppFullySharedGenericTypeU5BU5D_tCAB6D060972DD49223A834B7EEFEB9FE2D003BEC* L_10 = ___0_array;
		NullCheck((RuntimeArray*)L_10);
		uint8_t* L_11;
		L_11 = Array_GetRawSzArrayData_m2F8F5B2A381AEF971F12866D9C0A6C4FBA59F6BB_inline((RuntimeArray*)L_10, NULL);
		Il2CppFullySharedGenericAny* L_12;
		L_12 = il2cpp_unsafe_as_ref<Il2CppFullySharedGenericAny>(L_11);
		int32_t L_13 = ___1_start;
		Il2CppFullySharedGenericAny* L_14;
		L_14 = il2cpp_unsafe_add<Il2CppFullySharedGenericAny,int32_t>(L_12, L_13, SizeOf_T_t120950580BDFD368223E024446019DB239080837);
		ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 L_15;
		memset((&L_15), 0, sizeof(L_15));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_15), L_14);
		__this->____pointer = L_15;
		int32_t L_16 = ___2_length;
		__this->____length = L_16;
		return;
	}
}
// Method Definition Index: 11085
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* ReadOnlyCollection_1_get_Items_m0C20F5911B7A7A3EBAE35C0C5DC6F8E796FAD090_fshared_inline (ReadOnlyCollection_1_t5B7AA4E006906DE6818A44873F2D5987EFBF3AB8* __this, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		RuntimeObject* L_0 = __this->___list;
		return L_0;
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
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* Array_Empty_TisSelectorWorkItem_tBF6E4B85CACDC4734FBEAF1530157885BA83A474_m4433599FD227186796BAB47DC5BED2AF106ED67E_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		SelectorWorkItemU5BU5D_tD4000858C14565D18EFA0D397ECB4A37841FDE06* L_0 = ((EmptyArray_1_t0542F0EB5C43D54C8F4ADCAB1E94F18AEA151867_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* Array_Empty_TisATGMeshInfo_t92E7F6713ADB43FC6EB49CEEC1DF4FAFC5C78A9E_m5293579F263F9A3750FDFA1AA45718B28873BFBB_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		ATGMeshInfoU5BU5D_tEA25ABAF8CAF15CC88901DD4F41D1CE7FF0BE05F* L_0 = ((EmptyArray_1_t910BE047C1A6E9E334831BFBF9773D3C3852ED95_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* Array_Empty_TisAnimatorClipInfo_t0C913173594C893E36282602F54ABD06AC1CFA03_mC9DEB54C94662C9FD5779A4BFFAB7AD0469A27C8_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		AnimatorClipInfoU5BU5D_t202D955679134FCCD698779FBABD2530E58CD60A* L_0 = ((EmptyArray_1_t5B5242ED623249C0903BF0800195DB10397B3BCD_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* Array_Empty_TisBatchCullingOutputDrawCommands_t61DD68A8D702A9468AF6D5DDF4529CE7BD5A636D_m652D1F78F0DEF31A60A1D09589A89DE06F12C3E4_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		BatchCullingOutputDrawCommandsU5BU5D_t74BE7663EB19A8F858498AE303D905FBCCF54F80* L_0 = ((EmptyArray_1_tA6E339CF983886C190886835190061C2E3B679F2_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* Array_Empty_TisBezierCurve_t9D2CC33508284289953A8A3A881F91ED39502988_m89904E98E5508C2B001AC9AB5EE2E1CCA80CEEF3_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		BezierCurveU5BU5D_tA657AED609D3AC0702376CEBA8B8BC2BEE35EBDB* L_0 = ((EmptyArray_1_tC8FD7AE710007FB0A9059B5125949067B761A8E5_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* Array_Empty_TisBezierKnot_t528BE6DD984DC35E0953B14AF9DEAF7EEB692CD3_m77719C87E49330E8DA7EA4CD03C4AFDCE18C19C0_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		BezierKnotU5BU5D_tD73EE2AF8C0739CBCB3B8DBC36A179CBE7844213* L_0 = ((EmptyArray_1_t234239C1AFE29F273AE44FBF753D3916E498AC5C_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* Array_Empty_TisBoolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_mAB215C445888719BD89809D99C3DBD3135C2B1E7_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		BooleanU5BU5D_tD317D27C31DB892BE79FAE3AEBC0B3FFB73DE9B4* L_0 = ((EmptyArray_1_t7902087DD0C0221C9C7362ABAB7CB57D6D519A99_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* Array_Empty_TisBounds_t367E830C64BBF235ED8C3B2F8CF6254FDCAD39C3_mB6CFBB5D8AF33F0BAE72154209AB29B8D52FBCDA_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		BoundsU5BU5D_t45563ED55B62FA0536E8117454C56C0CFA5B25F5* L_0 = ((EmptyArray_1_t42D5099F7FCE2834141926DB372392B8C75ADBEC_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* Array_Empty_TisByte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_m6080CA526758F4FA182A066B2780D1761CD36ED5_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ((EmptyArray_1_t7187E746F328254739F076CFBCAABB28D4B4554C_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* Array_Empty_TisChar_t521A6F19B456D956AF452D926C32709DC03D6B17_mD1C1362CB74B91496D984B006ADC79B688D9B50D_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB* L_0 = ((EmptyArray_1_t7BBC8CED754F364A777871A238BBBE3F94FFDDE1_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* Array_Empty_TisColor_tD001788D726C3A7F1379BEED0260B9591F440C1F_m7E922E24AAEBD664256383832D53DDF17E1F3052_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		ColorU5BU5D_t612261CF293F6FFC3D80AB52259FF0DC2B2CC389* L_0 = ((EmptyArray_1_t9FC4EAC7DDBFA164B697CFEBFF39A52B321CC8C9_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* Array_Empty_TisColor32_t73C5004937BF5BB8AD55323D51AAA40A898EF48B_m5B415C4745E47108DD9258EBCCB422EFD6B8A0EB_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		Color32U5BU5D_t38116C3E91765C4C5726CE12C77FAD7F9F737259* L_0 = ((EmptyArray_1_tC72AE83DCB4E240E4B2AC2595448148BB03F9498_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* Array_Empty_TisCombineInstance_t77DDB096E69D0AD2690C57046C5ACD1239451BBE_mC39B4BC80AD9C1F917509BB8BE77C4C73CB471F9_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		CombineInstanceU5BU5D_tF7855763C164A0775855FF142E891E7BE0ABE093* L_0 = ((EmptyArray_1_tF7ADA3AE49000F6D4933A7D7B32DDF6230630DC0_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* Array_Empty_TisContactPairHeader_tA8C64AAEEF017149E77A6600B10A10F1A1C5648B_m2AE4E8C460D733DF5362B246ED865C5CE0C4AA60_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		ContactPairHeaderU5BU5D_tB754539003A0C6D67CC58AF905E0ADC94468872A* L_0 = ((EmptyArray_1_tF01C5B6CDD56CDA122E6B85402777F31EE92A685_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* Array_Empty_TisConvertMeshExtrasData_t1693BAC0EEE403DD216948512B0DAA8C88A05D6C_m4779104F7674EACD211A240162D4414087C431E8_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		ConvertMeshExtrasDataU5BU5D_t1420B76450ADCE1EE0AD98D00A25825A1DA9EE38* L_0 = ((EmptyArray_1_tCE52674ADF71EC22E199D7DF299141790948751A_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* Array_Empty_TisConvertMeshJobData_t35F3B601671C4880929BC30A858410B14459E3B1_m164EDF2519D24430C10F8BB106ADB252BF225C26_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		ConvertMeshJobDataU5BU5D_tB2771774E6E94A75BA5D65C7D84F0B7F3AD6D9E2* L_0 = ((EmptyArray_1_tB029523A05EA2FFE1EB90490D66A384F7A0D4574_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* Array_Empty_TisCopyMeshJobData_t8A9A7785E58E23442500E07C72C0C00E91B2BAF8_m636107AFEDC2888AAD9A190C5FCB115D4483F86F_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		CopyMeshJobDataU5BU5D_tE59B1B0057B43C36FA221B4FF0B973D16E978ED5* L_0 = ((EmptyArray_1_t0605DD263F295CE065C4B24BC0263FB5067617EC_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* Array_Empty_TisCullingSplit_tA645AE6542408220387BA7188513D7FEDBC1CAC0_m46787E1D6E481E4790F1F9E63000A067C9AF33CD_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		CullingSplitU5BU5D_tE7E4F4841F80A466E7A6149F61460774D8813F61* L_0 = ((EmptyArray_1_t27401338DAB1F4C8F9B8E695AB18ABBBF6573E17_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* Array_Empty_TisDistanceToInterpolation_t88DD77AC188D5F202C38FE6B29DD721AA84DC6B8_m7807FD2713C36285071ABDD0A617C0D77C6C1EB7_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		DistanceToInterpolationU5BU5D_tC429AD4AFACBC1980B8EE06AAF3C1ED4F179E2B0* L_0 = ((EmptyArray_1_tD918BE070C4FAED797D16A5616D8A88DC319BF09_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* Array_Empty_TisDrawBufferRange_t289089F19A7539FDA77D3E63416BAFBA0955E2D5_m05A4DA3A4ACD6D784909779870EE466DF7778EC7_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		DrawBufferRangeU5BU5D_t6B42ADE630C5C9FFF52E20462A038BB9696764DB* L_0 = ((EmptyArray_1_t640F34C5C1351444FCE089711FFEF74161372D64_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* Array_Empty_TisEasingFunction_t5197D3B06056326A8B5C96032CDEBD5D3BDCA7A4_m5BB1B3F942E717EB0EEA932DE146F79E347604BA_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		EasingFunctionU5BU5D_t3EEBBFFAD92EA74C3960D5F78D2A98BCEEA62E49* L_0 = ((EmptyArray_1_t7B0255D5BEE03D8E592C7F4DB26CEE268619FD74_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* Array_Empty_TisEmbeddedLODBuffer_t6D75CECAC228D0710132158E77ACB46D0C43027B_m87CA399E64D82A02BCE81DF2F8F52AD65AC9BC25_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		EmbeddedLODBufferU5BU5D_tE9306A2B9F9117018BC148A2D7AC217806AAF0F1* L_0 = ((EmptyArray_1_t662659478531CA2736931D12958F36C14566158B_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* Array_Empty_TisEntityId_t982FBD037EAC5CA077B1602A7EA40E3523AA0FC8_m8EF4F258FBF5AEC804ED891772BFFC84A01DB5D9_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		EntityIdU5BU5D_t8A607D0FD754ABFC6D83D6B358F75FEE0F9B6FB9* L_0 = ((EmptyArray_1_t9D02D6D1FF8E81441083A08AA0F8970E511F0900_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* Array_Empty_TisFilterParameter_tC6BAE67835BA35634A769B28BF35314AF42BE060_mA4E39A2097C6E2511B93A1BF0C9221165B65938C_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		FilterParameterU5BU5D_t0EF6EE29F03D6988D16E72B9209B753EE379FF58* L_0 = ((EmptyArray_1_t2AA231E0C904F8398D206EDF41018BEF22BD2325_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* Array_Empty_TisFlattenedSelector_tB2AFFD9D91833D68734821D583367B18D35A0AC9_m7E0303803800EFD09E46C64192C01A6D011D4260_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		FlattenedSelectorU5BU5D_tCFD484131041FE6AEF088704B99CA1C6DC732B27* L_0 = ((EmptyArray_1_t2D4836FD2685619474AF41CBDA7A6813DB13F5CA_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* Array_Empty_TisFlattenedSelectorPart_tD9EBC73D6704A9B622C3FF7F0DEB81EF0E115986_m907AAB5CF7AEF4C510C711BBF98ABA5EA468D2B1_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		FlattenedSelectorPartU5BU5D_t6A3A49EF6948716EE80F0909EB9C7D5526F19643* L_0 = ((EmptyArray_1_t00CB7A66F677EBAF03269ABB2C35F4597F8AFA77_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 3286
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* Array_Empty_TisGPUDrivenMeshData_t0D53E18AECCB1ED48149AFD77C12CF1624340C32_m0C324743704C230D82B1D3F4A8A72DAECCD01F62_inline (const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	CHECKED_LOCAL(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit);
	//<source_info:<no-source>:1>
	{
		CHECKED_LOCAL_INIT(EmptyArray_1_t6C301AAECD8237FCD6328D15A5BC0071D53D11A1_StaticInit,(il2cpp_rgctx_data(method->rgctx_data, 0)),il2cpp_codegen_runtime_class_init_inline);
		GPUDrivenMeshDataU5BU5D_tADCD87573A22AEB8F7978D2BC358C75F8B976BCE* L_0 = ((EmptyArray_1_t3264D01575877F752602C31CBF848A455D557C15_StaticFields*)il2cpp_codegen_static_fields_for(il2cpp_rgctx_data(method->rgctx_data, 0)))->___Value;
		return L_0;
	}
}
// Method Definition Index: 2480
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Span_1__ctor_mBA868F06359701D9950DEB1B10F52F848E9FF6DA_fshared_inline (Span_1_tDEB40BEFA77B5E4BB49B058CD3050EEA4DD36C54* __this, Il2CppFullySharedGenericAny* ___0_ptr, int32_t ___1_length, const RuntimeMethod* method) 
{
	//<source_info:<no-source>:1>
	{
		Il2CppFullySharedGenericAny* L_0 = ___0_ptr;
		ByReference_1_t607C1F3BC28B0E21B969461CDB0720FB01A82141 L_1;
		memset((&L_1), 0, sizeof(L_1));
		il2cpp_codegen_by_reference_constructor((Il2CppByReference*)(&L_1), L_0);
		__this->____pointer = L_1;
		int32_t L_2 = ___1_length;
		__this->____length = L_2;
		return;
	}
}
