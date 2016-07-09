
namespace GracenoteSDK {

using System;
using System.Runtime.InteropServices;

/**
* Provides services for local lookup of various identification queries such as
* text and CD TOC search; plus provides services for local retreival of content
* such as cover art.
*
* When enabled other GNSDK query objects are able to perform local lookups for
* various types of Gracenote searches. For example your application can perform
* a local text search via GnMusicId by enabling GnLookupLocal and setting the
* GnMusicId lookup mode such that local lookups are allowed.
*
* GnLookupLocal uses Gracenote databases that are pre-generated by Gracenote
* based on region. You application must make these databases available to GNSDK
* by placing them in read/write portion of the filesystem accessible by the
* application and then set the storage location accordingly.
*
* The Gracenote databases are regularly updated. When new versions of the
* databases are made available you application is responsible for replacing
* the old databases with the new version. This is typically accomplished by
* shutting down GNSDK and replacing the database files.
*/
public class GnLookupLocal : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GnLookupLocal(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(GnLookupLocal obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~GnLookupLocal() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          gnsdk_csharp_marshalPINVOKE.delete_GnLookupLocal(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

/**
* Enable local lookup for various identification queries such as
* text and CD TOC search; plus enable services for local retreival of
* content such as cover art.
* @ingroup Music_LookupLocal_InitializationFunctions
*/
  public static GnLookupLocal Enable() {
    GnLookupLocal ret = new GnLookupLocal(gnsdk_csharp_marshalPINVOKE.GnLookupLocal_Enable(), false);
    if (gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Pending) throw gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void StorageCompact(GnLocalStorageName storageName) {
    gnsdk_csharp_marshalPINVOKE.GnLookupLocal_StorageCompact(swigCPtr, (int)storageName);
    if (gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Pending) throw gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Retrieve();
  }

/**
*  Sets location where GNSDK can find a specific local lookup database
*	@param storageName 		[in] local storage name
*	@param storageLocation 	[in] local storage location
*/
  public void StorageLocation(GnLocalStorageName storageName, string storageLocation) {
    gnsdk_csharp_marshalPINVOKE.GnLookupLocal_StorageLocation(swigCPtr, (int)storageName, storageLocation);
    if (gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Pending) throw gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Retrieve();
  }

/**
*  Performs validation on named local storage.
*	@param storageName [in] local storage name
*/
  public void StorageValidate(GnLocalStorageName storageName) {
    gnsdk_csharp_marshalPINVOKE.GnLookupLocal_StorageValidate(swigCPtr, (int)storageName);
    if (gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Pending) throw gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Retrieve();
  }

/**
*  Retrieves the Local Storage information for specific storage
*  @param storageName 		[in] local storage name
*  @param storageInfo 		[in] local storage info type
*  @param ordinal 			[in] ordinal
*  @return info string if successful, GNSDK_NULL if not successful
*/
  public string StorageInfo(GnLocalStorageName storageName, GnLocalStorageInfo storageInfo, uint ordinal) {
    string ret = gnsdk_csharp_marshalPINVOKE.GnLookupLocal_StorageInfo(swigCPtr, (int)storageName, (int)storageInfo, ordinal);
    if (gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Pending) throw gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

/**
*  Retrieves the Local Storage information count for specific storage
*  @param storageName 		[in] local storage name
*  @param storageInfo 		[in] local storage info type
*  @return count if successful, 0 if not successful
*/
  public uint StorageInfoCount(GnLocalStorageName storageName, GnLocalStorageInfo storageInfo) {
    uint ret = gnsdk_csharp_marshalPINVOKE.GnLookupLocal_StorageInfoCount(swigCPtr, (int)storageName, (int)storageInfo);
    if (gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Pending) throw gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

/**
*  Version
*  Retrieves the Lookup Local SDK version string.
*  @return version string if successful
*  @return GNSDK_NULL if not successful
*  <p><b>Remarks:</b></p>
*  This API can be called at any time, after getting instance of GnManager successfully. The returned
*  string is a constant. Do not attempt to modify or delete.
*  Example version string: 1.2.3.123 (Major.Minor.Improvement.Build)
*  Major: New functionality
*  Minor: New or changed features
*  Improvement: Improvements and fixes
*  Build: Internal build number
*/
  public string Version {
	get
	{   
		/* csvarout typemap code */
		return GnMarshalUTF8.StringFromNativeUtf8(gnsdk_csharp_marshalPINVOKE.GnLookupLocal_Version_get(swigCPtr) );
	} 

  }

/**
*  BuildDate
*  Retrieves the Lookup Local SDK's build date string.
*  @return Note Build date string of the format: YYYY-MM-DD hh:mm UTC
*  <p><b>Remarks:</b></p>
*  This API can be called at any time, after getting instance of GnManager successfully. The returned
*   string is a constant. Do not attempt to modify or delete.
*  Example build date string: 2008-02-12 00:41 UTC
*/
  public string BuildDate {
	get
	{   
		/* csvarout typemap code */
		return GnMarshalUTF8.StringFromNativeUtf8(gnsdk_csharp_marshalPINVOKE.GnLookupLocal_BuildDate_get(swigCPtr) );
	} 

  }

}

}