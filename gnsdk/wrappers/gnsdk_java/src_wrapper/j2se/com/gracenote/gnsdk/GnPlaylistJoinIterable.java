
package com.gracenote.gnsdk;

/** 
* <b>Experimental</b> 
*/ 
 
public class GnPlaylistJoinIterable {
  private transient long swigCPtr;
  protected transient boolean swigCMemOwn;

  protected GnPlaylistJoinIterable(long cPtr, boolean cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  protected static long getCPtr(GnPlaylistJoinIterable obj) {
    return (obj == null) ? 0 : obj.swigCPtr;
  }

  protected void finalize() {
    delete();
  }

  public synchronized void delete() {
    if (swigCPtr != 0) {
      if (swigCMemOwn) {
        swigCMemOwn = false;
        gnsdk_javaJNI.delete_GnPlaylistJoinIterable(swigCPtr);
      }
      swigCPtr = 0;
    }
  }

  public GnPlaylistJoinIterable(collection_join_provider provider, long start) {
    this(gnsdk_javaJNI.new_GnPlaylistJoinIterable(collection_join_provider.getCPtr(provider), provider, start), true);
  }

  public GnPlaylistJoinIterator getIterator() {
    return new GnPlaylistJoinIterator(gnsdk_javaJNI.GnPlaylistJoinIterable_getIterator(swigCPtr, this), true);
  }

  public GnPlaylistJoinIterator end() {
    return new GnPlaylistJoinIterator(gnsdk_javaJNI.GnPlaylistJoinIterable_end(swigCPtr, this), true);
  }

  public long count() {
    return gnsdk_javaJNI.GnPlaylistJoinIterable_count(swigCPtr, this);
  }

  public GnPlaylistJoinIterator at(long index) {
    return new GnPlaylistJoinIterator(gnsdk_javaJNI.GnPlaylistJoinIterable_at(swigCPtr, this, index), true);
  }

  public GnPlaylistJoinIterator getByIndex(long index) {
    return new GnPlaylistJoinIterator(gnsdk_javaJNI.GnPlaylistJoinIterable_getByIndex(swigCPtr, this, index), true);
  }

}