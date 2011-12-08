/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.4
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class lgLcdDeviceDesc : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal lgLcdDeviceDesc(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(lgLcdDeviceDesc obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~lgLcdDeviceDesc() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          LogitechLCDPINVOKE.delete_lgLcdDeviceDesc(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public uint Width {
    set {
      LogitechLCDPINVOKE.lgLcdDeviceDesc_Width_set(swigCPtr, value);
    } 
    get {
      uint ret = LogitechLCDPINVOKE.lgLcdDeviceDesc_Width_get(swigCPtr);
      return ret;
    } 
  }

  public uint Height {
    set {
      LogitechLCDPINVOKE.lgLcdDeviceDesc_Height_set(swigCPtr, value);
    } 
    get {
      uint ret = LogitechLCDPINVOKE.lgLcdDeviceDesc_Height_get(swigCPtr);
      return ret;
    } 
  }

  public uint Bpp {
    set {
      LogitechLCDPINVOKE.lgLcdDeviceDesc_Bpp_set(swigCPtr, value);
    } 
    get {
      uint ret = LogitechLCDPINVOKE.lgLcdDeviceDesc_Bpp_get(swigCPtr);
      return ret;
    } 
  }

  public uint NumSoftButtons {
    set {
      LogitechLCDPINVOKE.lgLcdDeviceDesc_NumSoftButtons_set(swigCPtr, value);
    } 
    get {
      uint ret = LogitechLCDPINVOKE.lgLcdDeviceDesc_NumSoftButtons_get(swigCPtr);
      return ret;
    } 
  }

  public lgLcdDeviceDesc() : this(LogitechLCDPINVOKE.new_lgLcdDeviceDesc(), true) {
  }

}