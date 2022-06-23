import React from 'react';
import Button from './button';
interface IconButtonProps {
  onClick?: React.MouseEventHandler<HTMLButtonElement> | null;
  variant?: 'primary' | 'light' | 'dark';
  className?: string;
  icon: React.ReactNode;
  text?: string;
  disabled?: boolean;
  iconPlacement?: 'before' | 'after';
}

export const IconButton: React.FC<IconButtonProps> = ({ icon, onClick, variant = 'light', className, disabled, iconPlacement = 'before', text }) => {
  return (
    <Button
      variant={variant}
      className={`inline-flex items-center mr-2 ${className}`}
      disabled={disabled}
      onClick={(e) => {
        onClick?.(e);
      }}
    >
      {iconPlacement === 'before' && <div>{text}</div>}
      {icon}
      {iconPlacement === 'after' && <div>{text}</div>}
    </Button>
  );
};

export default Button;
