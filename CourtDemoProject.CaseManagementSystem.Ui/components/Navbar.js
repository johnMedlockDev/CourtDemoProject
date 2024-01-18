import Link from 'next/link'
import styles from '../styles/Navbar.module.scss'

const Navbar = () => {
	return (
		<nav className={styles.navbar}>
			<ul className={styles.navList}>
				<li className={styles.navItem}>
					<Link href="/case-details">
						<a className={styles.navLink}>Case Details</a>
					</Link>
				</li>
				<li className={styles.navItem}>
					<Link href="/charges">
						<a className={styles.navLink}>Charges</a>
					</Link>
				</li>
				<li className={styles.navItem}>
					<Link href="/case-participants">
						<a className={styles.navLink}>Case Participants</a>
					</Link>
				</li>
				<li className={styles.navItem}>
					<Link href="/cases">
						<a className={styles.navLink}>Cases</a>
					</Link>
				</li>
			</ul>
		</nav>
	)
}

export default Navbar
