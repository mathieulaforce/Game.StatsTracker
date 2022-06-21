import * as React from 'react';
import { StatusOfflineIcon, StatusOnlineIcon, WifiIcon } from '@heroicons/react/solid';

interface OnlineStateIconProps {
  isOnline?: boolean;
  className?: string;
  allowTriState?: boolean;
}

const OnlineStateIcon: React.FC<OnlineStateIconProps> = ({ isOnline, className, allowTriState }) => {
  if (isOnline) {
    return <StatusOnlineIcon className={`text-green-500 ${className}`} />;
  }
  if (isOnline === false || (isOnline === undefined && !allowTriState)) {
    return <StatusOfflineIcon className={`text-red-500 ${className}`} />;
  }
  return <WifiIcon className={`text-yellow-500 ${className}`} />;
};
export default OnlineStateIcon;
