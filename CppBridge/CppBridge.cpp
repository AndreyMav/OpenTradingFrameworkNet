// This is the main DLL file.

#include "stdafx.h"
#include <Windows.h>

#include "CppBridge.h"

using namespace System::IO;
using namespace System::Reflection;
using namespace MetatraderLibDll;

Assembly ^LoadFromSameFolder(Object ^sender, ResolveEventArgs ^args)
{
	String ^folderPath = Path::GetDirectoryName(Assembly::GetExecutingAssembly()->Location);
	String ^assemblyPath = Path::Combine(folderPath, (gcnew AssemblyName(args->Name))->Name + ".dll");
	if (File::Exists(assemblyPath) == false) return nullptr;
	Assembly ^assembly = Assembly::LoadFrom(assemblyPath);
	return assembly;
}


void InitializeAssemblyResolver()
{
	static bool alreadyDone = false;
	if (alreadyDone) return;
	alreadyDone = true;
	AppDomain ^currentDomain = AppDomain::CurrentDomain;
	currentDomain->AssemblyResolve += gcnew ResolveEventHandler(LoadFromSameFolder);
}


//extern "C" __declspec(dllexport) int OTFN_Test(wchar_t *test)
//{
////	InitializeAssemblyResolver();
//
//	return 1;
//}

int DoInit(String ^brokerName, String ^accountId, String ^strategyName, int magic, String ^symbol, int timeframe)
{
	return OpenTradingFrameworkMTLib::Init(gcnew System::String(brokerName), gcnew System::String(accountId), gcnew System::String(strategyName), magic, gcnew System::String(symbol), timeframe);
}

extern "C" __declspec(dllexport) int OTFN_Init(wchar_t *brokerName, wchar_t *accountId, wchar_t *strategyName, int magic, wchar_t *symbol, int timeframe)
{
	InitializeAssemblyResolver();
	return DoInit(gcnew String(brokerName), gcnew String(accountId), gcnew String(strategyName), magic, gcnew String(symbol), timeframe);
}

