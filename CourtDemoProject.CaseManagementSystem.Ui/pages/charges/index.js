import PropTypes from 'prop-types'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, Typography, List, ListItem, ListItemText, Button, Link as MuiLink } from '@mui/material'
import NextLink from 'next/link'

const ChargesPage = ({ charges }) => {
	const router = useRouter()

	const handleDelete = async (chargeId) => {
		try {
			await axios.delete(`http://api:8080/v1/Charges/${chargeId}`)
			router.replace(router.asPath) // Refresh the page to update the list
		} catch (error) {
			console.error('Error deleting charge:', error)
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 2 }}>Charges</Typography>
			<NextLink href="/charges/create" passHref>
				<Button variant="contained" color="primary" sx={{ mb: 2 }}>
                    Create New Charge
				</Button>
			</NextLink>
			<List>
				{charges.map((charge) => (
					<ListItem key={charge.chargeId} divider>
						<ListItemText
							primary={`Charge Name: ${charge.chargeName}`}
							secondary={`Charge Code: ${charge.chargeCode} | Type: ${charge.chargeType} | Judgement: ${charge.judgementType} | Fine: $${charge.fineAmount} | Sentence (days): ${charge.sentenceLengthIndays}`}
						/>
						<NextLink href={`/charges/${charge.chargeId}`} passHref>
							<MuiLink>View</MuiLink>
						</NextLink>
						<Button
							variant="outlined"
							color="secondary"
							onClick={() => handleDelete(charge.chargeId)}
							sx={{ ml: 2 }}
						>
                            Delete
						</Button>
					</ListItem>
				))}
			</List>
		</Container>
	)
}

export const getServerSideProps = async () => {
	const res = await axios.get('http://api:8080/v1/Charges')
	const charges = res.data // Adjust this according to the API response

	return {
		props: { charges }
	}
}

ChargesPage.propTypes = {
	charges: PropTypes.arrayOf(
		PropTypes.shape({
			chargeId: PropTypes.string.isRequired,
			chargeName: PropTypes.string.isRequired,
			chargeCode: PropTypes.string.isRequired,
			chargeType: PropTypes.string, // Assuming conversion of enum to string
			judgementType: PropTypes.string, // Assuming conversion of enum to string
			fineAmount: PropTypes.number,
			sentenceLengthIndays: PropTypes.number
		})
	).isRequired
}

export default ChargesPage
