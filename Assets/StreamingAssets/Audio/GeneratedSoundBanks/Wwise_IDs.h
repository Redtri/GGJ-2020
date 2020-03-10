/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID AMB_FORGE = 3017358643U;
        static const AkUniqueID CATAPULT_HIT = 1548123135U;
        static const AkUniqueID FOLEY_DOOR = 3332999173U;
        static const AkUniqueID PLAY_BATTLEFIELDAMBIANCE = 3316639316U;
        static const AkUniqueID PLAY_MUSIC_LOOSE = 3640900608U;
        static const AkUniqueID PLAY_MUSIC_SPLASHSCREEN = 2524099115U;
        static const AkUniqueID PLAY_MUSIC_WIN = 3368677440U;
        static const AkUniqueID SET_INTENSITYCALM = 4236667796U;
        static const AkUniqueID SET_INTENSITYEXTREME = 1912983523U;
        static const AkUniqueID SET_INTENSITYHIGH = 4034190369U;
        static const AkUniqueID SET_INTENSITYLOW = 3635806469U;
        static const AkUniqueID SET_INTENSITYMEDIUM = 716215376U;
        static const AkUniqueID SET_LOOSING = 1592413729U;
        static const AkUniqueID SET_WINNING = 1205857978U;
        static const AkUniqueID STOP_AMBIANCE = 2839405212U;
        static const AkUniqueID UI_ADDITEM = 2921734634U;
        static const AkUniqueID UI_DEATHEVENT = 802821452U;
        static const AkUniqueID UI_DIALEVENT = 3688695384U;
        static const AkUniqueID UI_HAMMERCLICK = 2751239240U;
        static const AkUniqueID UI_LIVINGEVENT = 4012865327U;
        static const AkUniqueID UI_REMOVEITEM = 2628486379U;
        static const AkUniqueID UI_VALIDATE = 873092944U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace BATTLEINTENSITY
        {
            static const AkUniqueID GROUP = 2514675686U;

            namespace STATE
            {
                static const AkUniqueID CALM = 3753286132U;
                static const AkUniqueID EXTREME = 2188986819U;
                static const AkUniqueID HIGH = 3550808449U;
                static const AkUniqueID LOW = 545371365U;
                static const AkUniqueID MEDIUM = 2849147824U;
            } // namespace STATE
        } // namespace BATTLEINTENSITY

        namespace DOORSTATES
        {
            static const AkUniqueID GROUP = 33159115U;

            namespace STATE
            {
                static const AkUniqueID CLOSED = 3012222945U;
                static const AkUniqueID OPEN = 3072142513U;
            } // namespace STATE
        } // namespace DOORSTATES

        namespace WINNINGSTATE
        {
            static const AkUniqueID GROUP = 125032842U;

            namespace STATE
            {
                static const AkUniqueID LOOSING = 1545774708U;
                static const AkUniqueID WINNING = 2996998095U;
            } // namespace STATE
        } // namespace WINNINGSTATE

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID INTENSITY = 2470328564U;
        static const AkUniqueID MASTERVOLUME = 2918011349U;
        static const AkUniqueID MUSICVOLUME = 2346531308U;
        static const AkUniqueID SFXVOLUME = 988953028U;
        static const AkUniqueID SIDECHAIN = 1883033791U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID BATTLEFIELD_SNB = 776956199U;
        static const AkUniqueID MUSIC_SNB = 2767260180U;
        static const AkUniqueID SFX_SNB = 26215100U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID ADDITEM = 2127285419U;
        static const AkUniqueID AMB_BATTLEFIELD = 3665832216U;
        static const AkUniqueID AMB_CATAPULTHIT = 3902753649U;
        static const AkUniqueID AMB_FORGE = 3017358643U;
        static const AkUniqueID AMBIANCE = 2981377429U;
        static const AkUniqueID AUX = 983310469U;
        static const AkUniqueID FOLEY_FORGE = 2919630506U;
        static const AkUniqueID FOLEYS = 4035004657U;
        static const AkUniqueID FORGE = 12377074U;
        static const AkUniqueID HAMMER = 703486095U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID SOUND_DESIGN = 4134381996U;
        static const AkUniqueID UI = 1551306167U;
        static const AkUniqueID UI_FORGE = 2605541363U;
        static const AkUniqueID VO = 1534528548U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID RVB_BATTLEFIELD = 1775981102U;
        static const AkUniqueID RVB_FORGE = 3907231393U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
