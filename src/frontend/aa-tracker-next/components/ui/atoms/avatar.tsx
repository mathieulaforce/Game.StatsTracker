import React from 'react';
import Image from 'next/image';

type Variant = 'square' | 'circle' | 'rounded-square';
type Size = 'small' | 'medium' | 'large';

interface AvatarProps {
  imageSrc: string;
  alt?: string;
  variant?: Variant;
  size?: Size;
  className?: string;
}

const getClassName = (variant?: Variant): string | undefined => {
  if (variant === 'circle') {
    return 'rounded-full';
  }
  if (variant === 'rounded-square') {
    return 'rounded-md';
  }
};

const getPxSize = (size?: Size): string => {
  if (size === 'small') {
    return '30px';
  }
  if (size === 'medium') {
    return '50px';
  }
  return '80px';
};

const Avatar: React.FC<AvatarProps> = ({ imageSrc, alt, variant = 'square', size = 'medium', className }) => {
  const sizeInPx = getPxSize(size);
  const imageClassName = `${getClassName(variant)} ${className}`;
  return <Image src={imageSrc} alt={alt} width={sizeInPx} height={sizeInPx} layout="fixed" className={imageClassName} />;
};
export default Avatar;
