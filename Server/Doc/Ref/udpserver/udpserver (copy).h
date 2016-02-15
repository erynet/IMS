// main module declaration file


#define MAX_MESSAGE_LEN		200

// server wait at this port (UDP)
#define LOCAL_SERVER_PORT	1500


// ------------------ "messaging" ---------
#define Debug
//#define Debug_sim

#ifdef Debug
#define MSG(string, args...)  printf(string,##args)
#else
#define MSG(string, args...)
#endif
 
