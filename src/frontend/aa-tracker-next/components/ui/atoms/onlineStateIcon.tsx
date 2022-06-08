import * as React from 'react';
import { StatusOfflineIcon, StatusOnlineIcon } from '@heroicons/react/solid';

interface OnlineStateIconProps {
  isOnline?: boolean;
  className?: string;
}

const OnlineStateIcon: React.FC<OnlineStateIconProps> = ({ isOnline, className }) => {
  return isOnline ? <StatusOnlineIcon className={`text-green-800 ${className}`} /> : <StatusOfflineIcon className={`text-red-800 ${className}`} />;
};
export default OnlineStateIcon;
