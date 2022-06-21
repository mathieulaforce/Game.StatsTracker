import React from 'react';
import { useMemo, useState } from 'react';
import Link from 'next/link';
import { getNavbarItems } from './navbarItems';
import NavLink from '../../atoms/navigationLink';

const StickyNavbar: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const navigationItems = useMemo(() => getNavbarItems(), []);
  return (
    <div
      className={`sticky top-0 z-40 w-full backdrop-blur flex-none transition-colors duration-500 lg:z-50
        bg-transparent lg:border-b  border-slate-50/[0.06] `}
    >
      <div className="max-w-8xl mx-auto">
        <div className="py-4 border-b lg:px-8 lg:border-0 border-slate-300/10 mx-4 lg:mx-0">
          <div className="relative flex items-center">
            <a className="mr-3 flex-none w-[2.0625rem] overflow-hidden md:w-auto" href="#">
              <Link href="/">
                <h1 className=" cursor-pointer">Tracker</h1>
              </Link>
            </a>

            <div className="relative hidden lg:flex items-center ml-auto">
              <nav className="font-semibold capitalize text-slate-200">
                <ul className="flex space-x-8">
                  {navigationItems.map((navItem) => {
                    return (
                      <li key={navItem.id} className="hover:text-sky-500 cursor-pointer">
                        <NavLink onClick={() => setIsOpen(false)} key={navItem.id} href={navItem.url}>
                          <div>{navItem.displayName}</div>
                        </NavLink>
                      </li>
                    );
                  })}
                </ul>
              </nav>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default StickyNavbar;
