
package com.gracenote.gnsdk;

public enum GnLocalStorageInfo {
  kLocalStorageInfoInvalid(0),
  kLocalStorageInfoVersion,
  kLocalStorageInfoImageSize;

  protected final int swigValue() {
    return swigValue;
  }

  protected static GnLocalStorageInfo swigToEnum(int swigValue) {
    GnLocalStorageInfo[] swigValues = GnLocalStorageInfo.class.getEnumConstants();
    if (swigValue < swigValues.length && swigValue >= 0 && swigValues[swigValue].swigValue == swigValue)
      return swigValues[swigValue];
    for (GnLocalStorageInfo swigEnum : swigValues)
      if (swigEnum.swigValue == swigValue)
        return swigEnum;
    throw new IllegalArgumentException("No enum " + GnLocalStorageInfo.class + " with value " + swigValue);
  }

  @SuppressWarnings("unused")
  private GnLocalStorageInfo() {
    this.swigValue = SwigNext.next++;
  }

  @SuppressWarnings("unused")
  private GnLocalStorageInfo(int swigValue) {
    this.swigValue = swigValue;
    SwigNext.next = swigValue+1;
  }

  @SuppressWarnings("unused")
  private GnLocalStorageInfo(GnLocalStorageInfo swigEnum) {
    this.swigValue = swigEnum.swigValue;
    SwigNext.next = this.swigValue+1;
  }

  private final int swigValue;

  private static class SwigNext {
    private static int next = 0;
  }
}
