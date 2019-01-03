/**
 * @(#)JacspcscLoader.java
 *
 *
 * @author 
 * Cenon G. Torres
 *
 * @version 1.00 2008/3/24
 */


public class JacspcscLoader {
	
	private static int hContext, cchReaders, hCard, dwActiveProtocols, cbRecvLength; 
	private static int cbAtrLen, dwState, dwProtocol;
	private static int cchReaderLen, lpBytesReturned;
	private static int rsUserData, rsRdrEventState, rsATRLength, rsRdrCurrState;	
	private static int ret = 0;
	
	
    public JacspcscLoader() {
    }
    
    // PCSC WRAPPER FUNCTIONS **********************************************************************
    
    
    public int jSCardEstablishContext(int dwscope, int pvReserved1, int pvReserved2, int [] phContext)						 						
	{				 								
		ret = SCardEstablishContext(dwscope, pvReserved1, pvReserved2, hContext);			
		phContext[0] = hContext; //return value of hContext;	 			
		return ret;						 						
	};
	public int jSCardReleaseContext(int [] phContext)
	{
		ret = SCardReleaseContext(phContext[0]);
		return ret;
	};
	
	public int jSCardListReaders(int [] phContext, int mszGroups, byte[] szReaders, int [] pcchReaders)
	{
		ret = SCardListReaders(phContext[0], mszGroups, szReaders, cchReaders);
		pcchReaders[0] = cchReaders; //return value of cchReaders
		return ret;
	};
	
	public int jSCardConnect(int [] phContext, String myReader, int dwShareMode, 
									int dwPreferredProtocols, int [] phCard, int [] pdwActiveProtocols)
	{
		byte [] tmpReader	= myReader.getBytes();
	    byte [] szReader	= new byte[myReader.length()+1];
	      
	      for (int i=0; i<myReader.length(); i++)
	      	szReader[i] = tmpReader[i];
	      szReader[myReader.length()] = 0; //set null terminator
		
		ret = SCardConnect(phContext[0], szReader, dwShareMode, dwPreferredProtocols, hCard, dwActiveProtocols);
		phCard[0] = hCard;
		pdwActiveProtocols[0] =  dwActiveProtocols;
		return ret;							 		
	};
		
	public int jSCardReconnect(int [] phCard, int dwShareMode, int dwPreferredProtocols, 
								int dwInitialization, int [] pdwActiveProtocols)
	{		
		ret = SCardReconnect(phCard[0], dwShareMode, dwPreferredProtocols, dwInitialization, dwActiveProtocols);
		pdwActiveProtocols[0] =  dwActiveProtocols;
		return ret;							 		
	};				
	
	public int jSCardDisconnect(int [] phCard, int dwDisposition)			   				 	
	{
		ret = SCardDisconnect(phCard[0], dwDisposition);
		return ret;	
	};				 
	
	public int jSCardTransmit(int [] phCard, ACSModule.SCARD_IO_REQUEST pioSendPci,
							byte[] pbSendBuffer, int cbSendLength, ACSModule.SCARD_IO_REQUEST pioRecvPci, 
							byte[] pbRecvBuffer, int [] pcbRecvLength)
								
	{
		try 
		{
				cbRecvLength = pcbRecvLength[0];		
				ret = SCardTransmit(phCard[0], pioSendPci, pbSendBuffer, cbSendLength, pioRecvPci, 
							pbRecvBuffer, cbRecvLength);
				pcbRecvLength[0] = cbRecvLength;						
				
		} catch (Exception e)
		{
			e.printStackTrace();
		}
		return ret;		
	};
	
	public int jSCardBeginTransaction(int [] phCard)
	{
		ret = SCardBeginTransaction(phCard[0]);
		return ret;
	};

	public int jSCardEndTransaction(int [] phCard, int dwDisposition)	
	{
		ret = SCardEndTransaction(phCard[0], dwDisposition);
		return ret;
	};

	public int jSCardState(int [] phCard, int [] pdwState, int [] pdwProtocol, byte[] pbATR, int [] pcbAtrLen)		
	{
		cbAtrLen = pcbAtrLen[0];
		dwState = pdwState[0];
		dwProtocol = pdwProtocol[0];
		ret = SCardState(phCard[0], dwState, dwProtocol, pbATR, cbAtrLen); 
		pcbAtrLen[0] = cbAtrLen;	
		pdwState[0] = dwState;
		pdwProtocol[0] = dwProtocol;
		return ret;
	};

	public int jSCardGetStatusChange(int [] phContext, int dwTimeout, ACSModule.SCARD_READERSTATE rgReaderStates, int cReaders)
    {
		String strRdr = rgReaderStates.RdrName;
		
		byte [] RdrName;	
		if (strRdr != null)
		{			
			byte [] tmpReader = strRdr.getBytes();
	        RdrName = new byte[strRdr.length()+1];	      
		    for (int i=0; i<strRdr.length(); i++)
		      RdrName[i] = tmpReader[i];
		    RdrName[strRdr.length()] = 0; //set null terminator
		} else {
			RdrName  = new byte[256];
			RdrName[0] = 0;	     
		}
		if (rgReaderStates.ATRValue == null)
		{
			rgReaderStates.ATRValue = new byte[36];
		}	
		
		//private static int rsUserData, rsRdrCurrState, rsRdrEventState, rsATRLength;
			
		ret = SCardGetStatusChange(phContext[0], dwTimeout, cReaders, 			                       			                       
			                       RdrName, 				//----------
			                       rsUserData,              //
			                       rsRdrCurrState,			//  SCARD_READERSTATE structure variables		
			                       rsRdrEventState,			//
			                       rsATRLength,				//
			                       rgReaderStates.ATRValue);//----------		
			                       	
		//rgReaderStates.RdrName = new String(RdrName);
		rgReaderStates.UserData = rsUserData;
		rgReaderStates.RdrCurrState = rsRdrCurrState;
		rgReaderStates.RdrEventState = rsRdrEventState;
		rgReaderStates.ATRLength = rsATRLength;				                       	
				
		return ret;
	};
                    						
	public int jSCardStatus(int [] phCard, byte[] mszReaderNames, int [] pcchReaderLen, int [] pdwState,
            int [] pdwProtocol, byte[] pbATR, int [] pcbAtrLen)
	{        
		cchReaderLen = pcchReaderLen[0];
		dwState = pdwState[0];
		dwProtocol = pdwProtocol[0];
		cbAtrLen = pcbAtrLen[0];  
	
		try 
		{
			ret = SCardStatus(phCard[0], mszReaderNames, cchReaderLen, dwState, dwProtocol, pbATR, cbAtrLen);
			
			if (ret == ACSModule.SCARD_S_SUCCESS)   
			{   
				pcchReaderLen[0] = cchReaderLen;
				pdwState[0] = dwState;
				pdwProtocol[0] = dwProtocol;
				pcbAtrLen[0] = cbAtrLen;          
			}
		} 
		catch (Exception e)
		{
			e.printStackTrace();
		} 
	return ret;
	};
	
	public int jSCardControl(int [] phCard, int dwControlCode, byte[] lpInBuffer, int nInBufferSize, 
		                     byte[] lpOutBuffer, int [] nOutBufferSize, int [] plpBytesReturned) 
	{
	
		cbRecvLength = nOutBufferSize[0];
		lpBytesReturned = plpBytesReturned[0];
		ret = SCardControl(phCard[0], dwControlCode, lpInBuffer, nInBufferSize, lpOutBuffer, cbRecvLength, lpBytesReturned );
		plpBytesReturned[0] = lpBytesReturned;
		nOutBufferSize[0] = cbRecvLength;
		return ret;
	};
	
	
	
	//PCSC JNI FUNCTIONS *************************************************************************
	
	private native int SCardEstablishContext(int dwscope,
						 					int pvReserved1,
						 					int pvReserved2,
						 					int phContext);
	private native int SCardReleaseContext(int hContext);
	private native int SCardListReaders(int phContext,
										int mszGroups,
										byte[] szReaders,
										int pcchReaders);

	private native int SCardConnect(int hContext,
					   				byte[] szReaders,
					   				int dwShareMode,
					   				int dwPreferredProtocols,
					   				int phCard,
					   				int pdwActiveProtocols);

	private native int SCardReconnect(int hContext,
					   				 int dwShareMode,
					   				 int dwPreferredProtocols,
					   				 int dwInitialization,
					   				 int pdwActiveProtocols);

	private native int SCardDisconnect(int hCard,
					   				 int dwDisposition);

	private native int SCardBeginTransaction(int hCard);

	private native int SCardEndTransaction(int hCard,
										  int dwDisposition);

	private native int SCardState(int hCard,
                    			 int pdwState,
                    			 int pdwProtocol,
                    			 byte[] pbATR,
                    			 int pcbAtrLen);

	private native int SCardGetStatusChange(int hContext,
                    						int dwTimeout,                    						
                    						int cReaders,
                    						
                    						//SCARD_READERSTATE structure variables
                    						byte [] RdrName,
                    						int UserData,
                    						int RdrCurrState,
                    						int RdrEventState,
                    						int ATRLength,
                    						byte [] pbATR); 
                    						
	private native int SCardStatus(int hCard,
                      			   byte [] mszReaderNames,
                      			   int pcchReaderLen, 
                      			   int pdwState,
                      			   int pdwProtocol,
                      			   byte[] pbATR,
                      			   int pcbAtrLen);


	private native int SCardTransmit(int hCard,
									ACSModule.SCARD_IO_REQUEST pioSendPci,
									byte[] pbSendBuffer,
									int cbSendLength,
									ACSModule.SCARD_IO_REQUEST pioRecvPci,
									byte[] pbRecvBuffer,
									int pcbRecvLength);


	private native int SCardControl(int hCard,
								   int dwControlCode,
								   byte[] lpInBuffer,
								   int nInBufferSize,
								   byte[] lpOutBuffer,
								   int nOutBufferSize,
								   int lpBytesReturned); 

    static {
       System.loadLibrary("Jacspcsc");
    }
	
}