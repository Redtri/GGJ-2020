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
        static const AkUniqueID PLAY_BATTLEFIELDAMBIANCE = 3316639316U;
        static const AkUniqueID PLAY_TEST = 3187507146U;
        static const AkUniqueID SET_DOORCLOSE = 238286794U;
        static const AkUniqueID SET_DOOROPEN = 2718873070U;
        static const AkUniqueID SET_INTENSITYCALM = 4236667796U;
        static const AkUniqueID SET_INTENSITYEXTREME = 1912983523U;
        static const AkUniqueID SET_INTENSITYHIGH = 4034190369U;
        static const AkUniqueID SET_INTENSITYLOW = 3635806469U;
        static const AkUniqueID SET_INTENSITYMEDIUM = 716215376U;
    } // namespace EVENTS

    namespace STATES
    {
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
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN_SNB = 763229880U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIANCES = 1404066300U;
        static const AkUniqueID BATTLEFIELD_DRY = 1210761507U;
        static const AkUniqueID FOLEYS = 4035004657U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID UI = 1551306167U;
        static const AkUniqueID VO = 1534528548U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID BATTLEFIELD_FILTERED = 1550058001U;
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
