/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.12
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace GracenoteSDK {

using System;
using System.Runtime.InteropServices;

public class GnStringValueEnumerable : System.Collections.Generic.IEnumerable<System.String>, IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GnStringValueEnumerable(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(GnStringValueEnumerable obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~GnStringValueEnumerable() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          gnsdk_csharp_marshalPINVOKE.delete_GnStringValueEnumerable(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

			System.Collections.Generic.IEnumerator<System.String> System.Collections.Generic.IEnumerable<System.String> .GetEnumerator( )
			{
				return GetEnumerator( );
			}
			System.Collections.IEnumerator System.Collections.IEnumerable.
			    GetEnumerator( )
			{
				return GetEnumerator( );
			}
		
  public GnStringValueEnumerable(gn_gdo_string_provider provider, uint start) : this(gnsdk_csharp_marshalPINVOKE.new_GnStringValueEnumerable(gn_gdo_string_provider.getCPtr(provider), start), true) {
    if (gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Pending) throw gnsdk_csharp_marshalPINVOKE.SWIGPendingException.Retrieve();
  }

  public GnStringValueEnumerator GetEnumerator() {
    GnStringValueEnumerator ret = new GnStringValueEnumerator(gnsdk_csharp_marshalPINVOKE.GnStringValueEnumerable_GetEnumerator(swigCPtr), true);
    return ret;
  }

  public GnStringValueEnumerator end() {
    GnStringValueEnumerator ret = new GnStringValueEnumerator(gnsdk_csharp_marshalPINVOKE.GnStringValueEnumerable_end(swigCPtr), true);
    return ret;
  }

  public uint count() {
    uint ret = gnsdk_csharp_marshalPINVOKE.GnStringValueEnumerable_count(swigCPtr);
    return ret;
  }

  public GnStringValueEnumerator at(uint index) {
    GnStringValueEnumerator ret = new GnStringValueEnumerator(gnsdk_csharp_marshalPINVOKE.GnStringValueEnumerable_at(swigCPtr, index), true);
    return ret;
  }

  public GnStringValueEnumerator getByIndex(uint index) {
    GnStringValueEnumerator ret = new GnStringValueEnumerator(gnsdk_csharp_marshalPINVOKE.GnStringValueEnumerable_getByIndex(swigCPtr, index), true);
    return ret;
  }

}

}