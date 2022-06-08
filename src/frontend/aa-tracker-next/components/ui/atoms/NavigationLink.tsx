import * as React from 'react';
import Link from 'next/link';
import { UrlObject } from 'url';
import { useRouter } from 'next/router';

interface NavLinkProps {
  className?: string;
  activeClassName?: string;
  children: React.ReactElement;
  href: string;
  onClick?: () => void;
}

const NavLink: React.FC<NavLinkProps> = (props) => {
  const router = useRouter();

  const classNames = router.pathname === props.href ? ` ${props.activeClassName}` : props.className;

  const clone = React.cloneElement(props.children, {
    className: classNames,
    onClick: props.onClick,
  });
  return <Link href={props.href}>{clone}</Link>;
};
export default NavLink;
