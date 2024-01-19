import { Container, Typography, Paper } from '@mui/material'

function HomePage () {
	return (
		<Container maxWidth="md">
			<Paper style={{ padding: '20px', marginTop: '30px' }}>
				<Typography variant="h4" component="h1" gutterBottom>
                    Case Management System
				</Typography>
				<Typography variant="body1">
                    Welcome to the Case Management System. This application provides a comprehensive platform for managing various aspects of legal cases. Users can navigate through different sections such as Case Details, Charges, Case Participants, and Cases. Each section allows for viewing, creating, editing, and deleting specific case-related information. Our aim is to streamline case management processes, making them more efficient and user-friendly.
				</Typography>
			</Paper>
		</Container>
	)
}

export default HomePage
