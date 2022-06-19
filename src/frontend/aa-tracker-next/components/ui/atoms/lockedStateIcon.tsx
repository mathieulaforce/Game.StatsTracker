import * as React from 'react';
import { LockOpenIcon, LockClosedIcon } from '@heroicons/react/solid';

interface LockedStateIconProps {
  isLocked?: boolean;
  className?: string;
}

const LockedStateIcon: React.FC<LockedStateIconProps> = ({ isLocked, className }) => {
  return isLocked ? <LockClosedIcon className={`${className}`} /> : <LockOpenIcon className={`${className}`} />;
};
export default LockedStateIcon;
