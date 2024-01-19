import NextLink from 'next/link'
import { AppBar, Toolbar, Button, Typography, Box } from '@mui/material'

const Navbar = () => {
	return (
		<AppBar position="static" sx={{ marginBottom: 3, padding: 0 }}>
			<Toolbar>
				<Box sx={{ flexGrow: 1 }} />
				<NextLink href="/case-details" passHref>
					<Button color="inherit">
						<Typography variant="button" sx={{ color: 'white', textDecoration: 'none' }}>Case Details</Typography>
					</Button>
				</NextLink>
				<NextLink href="/charges" passHref>
					<Button color="inherit">
						<Typography variant="button" sx={{ color: 'white', textDecoration: 'none' }}>Charges</Typography>
					</Button>
				</NextLink>
				<NextLink href="/case-participants" passHref>
					<Button color="inherit">
						<Typography variant="button" sx={{ color: 'white', textDecoration: 'none' }}>Case Participants</Typography>
					</Button>
				</NextLink>
				<NextLink href="/cases" passHref>
					<Button color="inherit">
						<Typography variant="button" sx={{ color: 'white', textDecoration: 'none' }}>Cases</Typography>
					</Button>
				</NextLink>
			</Toolbar>
		</AppBar>
	)
}

export default Navbar
