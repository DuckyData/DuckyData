/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.12
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

package com.gracenote.gnsdk;

public class GnVideoCreditIterable {
  private transient long swigCPtr;
  protected transient boolean swigCMemOwn;

  protected GnVideoCreditIterable(long cPtr, boolean cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  protected static long getCPtr(GnVideoCreditIterable obj) {
    return (obj == null) ? 0 : obj.swigCPtr;
  }

  protected void finalize() {
    delete();
  }

  public synchronized void delete() {
    if (swigCPtr != 0) {
      if (swigCMemOwn) {
        swigCMemOwn = false;
        gnsdk_javaJNI.delete_GnVideoCreditIterable(swigCPtr);
      }
      swigCPtr = 0;
    }
  }

  public GnVideoCreditIterable(GnVideoCreditProvider provider, long start) {
    this(gnsdk_javaJNI.new_GnVideoCreditIterable(GnVideoCreditProvider.getCPtr(provider), provider, start), true);
  }

  public GnVideoCreditIterator getIterator() {
    return new GnVideoCreditIterator(gnsdk_javaJNI.GnVideoCreditIterable_getIterator(swigCPtr, this), true);
  }

  public GnVideoCreditIterator end() {
    return new GnVideoCreditIterator(gnsdk_javaJNI.GnVideoCreditIterable_end(swigCPtr, this), true);
  }

  public long count() {
    return gnsdk_javaJNI.GnVideoCreditIterable_count(swigCPtr, this);
  }

  public GnVideoCreditIterator at(long index) {
    return new GnVideoCreditIterator(gnsdk_javaJNI.GnVideoCreditIterable_at(swigCPtr, this, index), true);
  }

  public GnVideoCreditIterator getByIndex(long index) {
    return new GnVideoCreditIterator(gnsdk_javaJNI.GnVideoCreditIterable_getByIndex(swigCPtr, this, index), true);
  }

}